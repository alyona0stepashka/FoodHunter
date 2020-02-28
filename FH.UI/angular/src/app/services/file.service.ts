import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  // getAllCompanies() {
  //   return this.http.get(environment.serverURL + '/api/file');
  // }

  uploadLocationAlbumPhoto(fileToUpload: File, locationId: string) {
    const formData: FormData = new FormData();
    formData.append('Photo', fileToUpload);
    return this.http.post(environment.serverURL + '/api/file/location/album/' + locationId, formData);
  }

  // uploadLocationTopPhoto(fileToUpload: File, locationId: string) {
  //   const formData: FormData = new FormData();
  //   formData.append('Photo', fileToUpload);
  //   return this.http.post(environment.serverURL + '/api/file/location/top/' + locationId, formData);
  // }

  deleteLocationAlbumPhoto(id: number) {
    return this.http.delete(environment.serverURL + '/api/file/location/album/' + id);
  }

}
