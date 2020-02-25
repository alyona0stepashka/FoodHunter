import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  getAllTablesByLocation(locationId) {
    return this.http.get(environment.serverURL + '/api/table/' + locationId);
  }

  createTable(registerFormData: FormGroup, locationId) {
    const body: FormData = new FormData();
    body.append("Number", registerFormData.value.Number);
    body.append("Info", registerFormData.value.Info);
    body.append("LocationId", locationId);
    return this.http.post(environment.serverURL + '/api/table', body);
  }

  createTableBook(registerFormData: FormGroup) {
    const body: FormData = new FormData();
    body.append("StartTime", registerFormData.value.StartTime);
    body.append("EndTime", registerFormData.value.EndTime);
    body.append("TableId", registerFormData.value.TableId);
    body.append("ClientId", registerFormData.value.ClientId);
    return this.http.post(environment.serverURL + '/api/table/booking', body);
  }

  acceptBook(bookId) {
    return this.http.get(environment.serverURL + '/api/table/' + bookId + '/booking/accept');
  }

  declineBook(bookId) {
    return this.http.get(environment.serverURL + '/api/table/' + bookId + '/booking/decline');
  }

  cancelBook(bookId) {
    return this.http.get(environment.serverURL + '/api/table/' + bookId + '/booking/cancel');
  }

  deleteTable(tableId) {
    return this.http.delete(environment.serverURL + '/api/table/' + tableId);
  }

  updateTable(registerFormData: FormGroup, locationId) {
    const body: FormData = new FormData();
    body.append("Id", registerFormData.value.Id);
    body.append("Number", registerFormData.value.Number);
    body.append("Info", registerFormData.value.Info);
    body.append("LocationId", locationId);
    return this.http.put(environment.serverURL + '/api/table', body);
  }
}