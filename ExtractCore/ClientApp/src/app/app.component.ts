import { Component } from '@angular/core';
import { ApiServiceService } from './api-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private _as: ApiServiceService) { }
  isdeleteDisabled = true;
  isextractDisabled = false;
  loader = false;
  extractRecord() {
    this.loader = true;
    const url = "http://localhost:54426/api/Data/Extract";
    this._as.extractRecord(url).subscribe(
      data => {
        console.log(data);
        if (data) {
          alert("data inserted to new table!");
          this.isdeleteDisabled = false;
          this.isextractDisabled = true;
        }
        else {
          alert("Some error occured!");
        }
        this.loader = false;
      },
      error => { console.log(error) }
    );
  }

  deleteRecord() {
    this.loader = true;
    const url = "http://localhost:54426/api/Data/Delete";
    this._as.deleteRecord(url).subscribe(
      data => {
        console.log(data)
        if (data) {
          alert("data deleted from new table!");
          this.isdeleteDisabled = true;
          this.isextractDisabled = false;
          this.loader = false;
        }
        else {
          alert("Some error occured!");
        }
      },
      error => { console.log(error) }
    );
  }
}
