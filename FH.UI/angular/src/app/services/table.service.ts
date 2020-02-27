import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  getMyTableBooks() {
    return this.http.get(environment.serverURL + '/api/table/my');
  }

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

  createTableBook(registerFormData: FormGroup, startDate, endDate) {
    const body: FormData = new FormData();
    console.log("in_service-start-date", startDate);
    console.log("in_service-end-date", endDate);

    body.append("StartTime", startDate);
    body.append("EndTime", endDate);
    body.append("TableId", registerFormData.value.TableId);
    if (registerFormData.value.Comment != '' && registerFormData.value.Comment != null) {
      body.append("Comment", registerFormData.value.Comment);
    }
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