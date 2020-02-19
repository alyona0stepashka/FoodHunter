import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient) { }

  getLocationMenus(id: string) {
    return this.http.get(environment.serverURL + '/api/menu/location/' + id);
  }

  deleteMenu(id: string) {
    return this.http.delete(environment.serverURL + '/api/menu/' + id);
  }

  createMenu(registerFormData: FormGroup, locationId: string) {
    const body: FormData = new FormData();
    body.append("LocationId", locationId);
    body.append("Title", registerFormData.value.Title);
    body.append("Info", registerFormData.value.Info);
    body.append("IconId", registerFormData.value.IconId);
    return this.http.post(environment.serverURL + '/api/menu', body);
  }
}
