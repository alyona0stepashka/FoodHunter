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
    console.log("form1", registerFormData);
    const body: FormData = new FormData();
    body.append("Email", registerFormData.value.Email);
    body.append("FirstName", registerFormData.value.FirstName);
    body.append("LastName", registerFormData.value.LastName);
    body.append("Password", registerFormData.value.Password);
    body.append("Phone", registerFormData.value.Phone);
    body.append("DateBirth", registerFormData.value.DateBirth);
    body.append("SexId", registerFormData.value.Sex);
    body.append("Photo", fileToUpload);
    body.append("Role", registerFormData.value.Role);
    console.log("form2", body);

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
    return this.http.get(environment.serverURL + '/api/users/current');
  }
}
