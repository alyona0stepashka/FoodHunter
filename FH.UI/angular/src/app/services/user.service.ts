import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  register(registerFormData: FormGroup, fileToUpload: File) {
    const body: FormData = new FormData();
    body.append("Email", registerFormData.value.Email);
    body.append("FirstName", registerFormData.value.FirstName);
    body.append("LastName", registerFormData.value.LastName);
    body.append("Password", registerFormData.value.Password);
    body.append("Phone", registerFormData.value.Phone);
    body.append("DateBirth", registerFormData.value.DateBirth);
    if (registerFormData.value.LocationId != null && registerFormData.value.LocationId != undefined && registerFormData.value.LocationId != '0') {

      body.append("LocationId", registerFormData.value.LocationId);
    }
    body.append("SexId", registerFormData.value.Sex);
    body.append("Photo", fileToUpload);
    body.append("Role", registerFormData.value.Role);
    return this.http.post(environment.serverURL + '/api/account/register', body);
  }

  login(loginFormData: FormGroup) {
    const body = {
      Email: loginFormData.value.Email,
      Password: loginFormData.value.Password
    };
    return this.http.post(environment.serverURL + '/api/account/login', body);
  }

  getMyProfile() {
    return this.http.get(environment.serverURL + '/api/acoount/current');
  }

  getStaffByLocation() {
    return this.http.get(environment.serverURL + '/api/account/staff');
  }

  deleteUser(id) {
    return this.http.delete(environment.serverURL + '/api/account/delete');
  }
}
