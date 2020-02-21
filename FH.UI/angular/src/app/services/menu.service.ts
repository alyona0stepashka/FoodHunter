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
  updateMenu(registerFormData: FormGroup, locationId: string) {
    const body: FormData = new FormData();
    body.append("Id", registerFormData.value.Id);
    body.append("LocationId", locationId);
    body.append("Title", registerFormData.value.Title);
    body.append("Info", registerFormData.value.Info);
    body.append("IconId", registerFormData.value.IconId);
    return this.http.put(environment.serverURL + '/api/menu', body);
  }

  createMenuItem(registerFormData: FormGroup, fileToUpload: File) {
    const body: FormData = new FormData();
    body.append("Title", registerFormData.value.Title);
    body.append("Info", registerFormData.value.Info);
    body.append("Price", registerFormData.value.Price);
    body.append("PriceWithSales", registerFormData.value.PriceWithSales);
    body.append("IsActive", registerFormData.value.IsActive);
    body.append("MenuId", registerFormData.value.MenuId);
    body.append("Note", registerFormData.value.Note);
    body.append("Photo", fileToUpload);
    return this.http.post(environment.serverURL + '/api/menu/item', body);
  }

  deleteMenuItem(id: string) {
    return this.http.delete(environment.serverURL + '/api/menu/item/' + id);
  }

  // Id: id,
  // Title: this.editMenuItem.Title,
  // Info: this.editMenuItem.Info,
  // Price: this.editMenuItem.Price,
  // PriceWithSales: this.editMenuItem.PriceWithSales,
  // IsActive: this.editMenuItem.IsActive,
  // MenuId: this.editMenuItem.MenuId,
  // Note: this.editMenuItem.Note,
  // Photo: this.editMenuItem.Photo,
  // IconId: this.editMenuItem.Icon.Id

  updateMenuItem(registerFormData: FormGroup, fileToUpload?: File) {
    const body: FormData = new FormData();
    body.append("Id", registerFormData.value.Id);
    body.append("Title", registerFormData.value.Title);
    body.append("Info", registerFormData.value.Info);
    body.append("Price", registerFormData.value.Price);
    if (registerFormData.value.PriceWithSales != null) {
      body.append("PriceWithSales", registerFormData.value.PriceWithSales);
    }
    body.append("IsActive", registerFormData.value.IsActive);
    body.append("MenuId", registerFormData.value.MenuId);
    body.append("Note", registerFormData.value.Note);
    // body.append("IconId", registerFormData.value.IconId);
    if (fileToUpload != null) {
      body.append("Photo", fileToUpload);
    }
    return this.http.put(environment.serverURL + '/api/menu/item', body);
  }
}
