import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {


  constructor(private http: HttpClient) { }

  getAllCompanies() {
    return this.http.get(environment.serverURL + '/api/company');
  }

  createCompany(registerFormData: FormGroup, fileToUpload: File) {
    const body: FormData = new FormData();
    body.append("ContactInfo", registerFormData.value.ContactInfo);
    body.append("Vk", registerFormData.value.Vk);
    body.append("Facebook", registerFormData.value.Facebook);
    body.append("Instagram", registerFormData.value.Instagram);
    body.append("Site", registerFormData.value.Site);
    body.append("Name", registerFormData.value.Name);
    body.append("Describe", registerFormData.value.Describe);
    body.append("Photo", fileToUpload);
    body.append("SpecificationId", registerFormData.value.SpecificationId);
    return this.http.post(environment.serverURL + '/api/company', body);
  }
}
