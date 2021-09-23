using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExtractCore.Entity
{
    public partial class New
    {
        public int ImageId { get; set; }
        public int UploadObuaId { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? LastDownloadTimestamp { get; set; }
        public int? LastDownloadObuaId { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
    }

    public partial class Old
    {
        [Key]
        public int ImageId { get; set; }
        public int UploadObuaId { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime RecordCreateDate { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? LastDownloadTimestamp { get; set; }
        public int? LastDownloadObuaId { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public byte[] Image { get; set; }
    }
}
