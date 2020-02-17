import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpClient) { }

  getAllLocations() {
    return this.http.get(environment.serverURL + '/api/location');
  }

  getLocation(id: number) {
    return this.http.get(environment.serverURL + '/api/location/' + id);
  }

  createLocation(registerFormData: FormGroup/*, fileToUpload: File*/) {
    const body: FormData = new FormData();
    body.append("CompanyId", registerFormData.value.CompanyId);
    body.append("Longitude", registerFormData.value.Longitude);
    body.append("Latitude", registerFormData.value.Latitude);
    body.append("Name", registerFormData.value.Name);
    body.append("Address", registerFormData.value.Address);
    //body.append("Photo", fileToUpload);
    return this.http.post(environment.serverURL + '/api/location', body);
  }
}
