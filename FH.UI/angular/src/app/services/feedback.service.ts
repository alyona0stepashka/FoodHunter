import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  getLocationFeedbacks(locationId) {
    return this.http.get(environment.serverURL + '/api/feedback/location/' + locationId);
  }

  getMenuItemFeedbacks(menuItemId) {
    return this.http.get(environment.serverURL + '/api/feedback/item/' + menuItemId);
  }

  createFeedback(registerFormData: FormGroup, fileToUpload: File) {
    const body: FormData = new FormData();
    body.append("Stars", registerFormData.value.Stars);
    body.append("Comment", registerFormData.value.Comment);
    if (registerFormData.value.LocationId != null && registerFormData.value.LocationId != undefined) {
      body.append("LocationId", registerFormData.value.LocationId);
    }
    if (registerFormData.value.MenuItemId != null && registerFormData.value.MenuItemId != undefined) {
      body.append("MenuItemId", registerFormData.value.MenuItemId);
    }
    body.append("UserProfileId", registerFormData.value.UserProfileId);
    body.append("UploadPhoto", fileToUpload);
    return this.http.post(environment.serverURL + '/api/feedback', body);
  }
}
