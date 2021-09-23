using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExtractCore.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExtractCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [Route("Extract")]
        public async Task<ActionResult> Extract()
        {
            var res = await ExtractData();
            return Ok(res);
        }

        [Route("Delete")]
        public async Task<ActionResult> Delete()
        {
            var res = await DeleteData();
            return Ok(res);
        }

        private async Task<bool> ExtractData()
        {
            try
            {
                var foldername = @"C:\ExtractedImages";
                if (!Directory.Exists(foldername))
                {
                    Directory.CreateDirectory(foldername);
                }

                var context = new Entity.devcanContext();

                var oldList = context.Old.AsQueryable();

                foreach (var old in oldList)
                {
                    foldername = @"C:\ExtractedImages\" + old.ApplicationId;
                    if (!Directory.Exists(foldername))
                    {
                        Directory.CreateDirectory(foldername);
                    }

                    var filepath = $"C:/ExtractedImages/{old.ApplicationId}/{old.Filename}";
                    filepath = NextAvailableFilename(filepath);
                    System.IO.File.WriteAllBytes(filepath, old.Image);
                    var newData = new New()
                    {
                        UploadObuaId = old.UploadObuaId,
                        ApplicationId = old.ApplicationId,
                        Description = old.Description,
                        DocumentTypeId = old.DocumentTypeId,
                        Filename = old.Filename,
                        LastDownloadObuaId = old.LastDownloadObuaId,
                        LastDownloadTimestamp = old.LastDownloadTimestamp,
                        RecordCreateDate = old.RecordCreateDate,
                        Path = filepath,
                        UploadDate = old.UploadDate
                    };
                    await context.New.AddAsync(newData);
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string NextAvailableFilename(string path)
        {
            string numberPattern = " ({0})";
            if (!System.IO.File.Exists(path))
                return path;

            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            return GetNextFilename(path + numberPattern);
        }
        private string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!System.IO.File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (System.IO.File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (System.IO.File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        private async Task<bool> DeleteData()
        {
            try
            {
                var context = new Entity.devcanContext();

                var newList = context.New.AsQueryable();

                foreach (var newData in newList)
                {
                    string _imageToBeDeleted = Path.Combine(newData.Path);
                    if ((System.IO.File.Exists(_imageToBeDeleted)))
                    {
                        System.IO.File.Delete(_imageToBeDeleted);
                    }
                    context.New.Remove(newData);
                }
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}