import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class StaticService {

  constructor(private http: HttpClient) { }

  getSearchResult(key: string, objs: any) {
    const body = {
      Key: key,
      SearchObjs: objs
    }
    return this.http.post(environment.serverURL + '/api/static/search', body);
  }

  getSexes() {
    return this.http.get(environment.serverURL + '/api/static/sex');
  }

  getSpecifications() {
    return this.http.get(environment.serverURL + '/api/static/specification');
  }

  getIcons() {
    return this.http.get(environment.serverURL + '/api/static/icon');
  }
}
