import { Component, OnInit } from '@angular/core';
import { SignalRService } from 'app/services/signal-r.service';
import { Router, ActivatedRoute } from '@angular/router';
import { OrderService } from 'app/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {

  constructor(
    public signalRService: SignalRService,
    private router: Router,
    private orderService: OrderService,
    private toastr: ToastrService,
    public datepipe: DatePipe,
    private activateRoute: ActivatedRoute, ) { }

  //server data
  orders = new Array();
  filteredOrders = new Array();
  //server data

  //logic vars
  selectedOrderId = 0;
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isHaveOrders = true;
  //logic vars

  ngOnInit() {
    this.isLogin = (localStorage.getItem('token') != null);
    this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    if (this.isManager) {
      this.loadAllOrders();
    }
    else {
      this.loadOrderHistory();
    }
  }



  redirectToOrder(ordId) {
    this.router.navigateByUrl('/dashboard-manager/order/' + ordId);
  }
  redirectToLocation(locId) {
    this.router.navigateByUrl('/dashboard-user/location/' + locId);
  }

  loadAllOrders() {
    this.orderService.getAllOrdersByLocation().subscribe(
      res => {
        this.orders = res as [];
        this.filteredOrders = this.orders;
        this.isHaveOrders = (this.orders != null && this.orders.length > 0);
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  assignOrderAsManager(orderId) {
    this.signalRService.hubConnection.invoke('AssignManagerToSession', orderId, null)
      .then(res => { this.redirectToOrder(orderId); })
      .catch(err => console.error(err));
  }

  loadOrderHistory() {
    this.orderService.getOrderHistory().subscribe(
      res => {
        this.orders = res as [];
        this.filteredOrders = this.orders;
        this.isHaveOrders = (this.orders != null && this.orders.length > 0);
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

}
