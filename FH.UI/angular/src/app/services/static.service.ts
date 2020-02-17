import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaticService {

  constructor(private http: HttpClient) { }

  getSexes() {
    return this.http.get(environment.serverURL + '/api/static/sex');
  }

  getSpecifications() {
    return this.http.get(environment.serverURL + '/api/static/specification');
  }
}
