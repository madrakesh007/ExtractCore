import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class ApiServiceService {

  constructor(private _http: HttpClient) { }

  extractRecord(url: string) {
    return this._http.get<Observable<any>>(url);
  }

  deleteRecord(url: string) {
    return this._http.get<Observable<any>>(url);
  }

}
