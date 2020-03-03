import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) { }

  getCurrentOrder() {
    return this.http.get(environment.serverURL + '/api/order/current');
  }

  getOrderById(id) {
    return this.http.get(environment.serverURL + '/api/order/' + id);
  }

  getOrderHistory() {
    return this.http.get(environment.serverURL + '/api/order/history');
  }

  deleteOrderItem(id: string) {
    return this.http.delete(environment.serverURL + '/api/order/' + id);
  }

  // createMenu(registerFormData: FormGroup, locationId: string) {
  //   const body: FormData = new FormData();
  //   body.append("LocationId", locationId);
  //   body.append("Title", registerFormData.value.Title);
  //   body.append("Info", registerFormData.value.Info);
  //   body.append("IconId", registerFormData.value.IconId);
  //   return this.http.post(environment.serverURL + '/api/menu', body);
  // }
  // updateMenu(registerFormData: FormGroup, locationId: string) {
  //   const body: FormData = new FormData();
  //   body.append("Id", registerFormData.value.Id);
  //   body.append("LocationId", locationId);
  //   body.append("Title", registerFormData.value.Title);
  //   body.append("Info", registerFormData.value.Info);
  //   body.append("IconId", registerFormData.value.IconId);
  //   return this.http.put(environment.serverURL + '/api/menu', body);
  // }
}
