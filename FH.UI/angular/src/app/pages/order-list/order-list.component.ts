import { Component, OnInit } from '@angular/core';
import { SignalRService } from 'app/services/signal-r.service';
import { Router, ActivatedRoute } from '@angular/router';
import { OrderService } from 'app/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { environment } from 'environments/environment';
import { FeedbackService } from 'app/services/feedback.service';

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
    private activateRoute: ActivatedRoute,
    private feedbackService: FeedbackService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal) { }

  //server data
  orders = new Array();
  filteredOrders = new Array();
  calls = new Array();
  //server data

  //logic vars
  selectedOrderId = 0;
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isHaveOrders = true;
  userProfileId = localStorage.getItem('ClientId');
  //logic vars

  //images
  closeResult: string;
  serverUrl = environment.serverURL;
  imageUrl = './assets/img/upload-photo.jpg';
  //images

  ngOnInit() {
    this.isLogin = (localStorage.getItem('token') != null);
    this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    if (this.isManager) {
      this.loadAllOrders();
    }
    else {
      this.loadOrderHistory();
    }
    this.disableCallListener();
    this.createOrderListener();
  }

  disableCallListener() {
    this.signalRService.hubConnection.on('AcceptCallManager', (data) => {
      // this.order.ManagerCalls = this.order.ManagerCalls.filter(function (m) {
      //   return m.Id != data.Id;
      // });
      if (this.isManager) {
        this.loadAllOrders();
      }
      else {
        this.loadOrderHistory();
      }
      console.log("disable-call-listener", data);
    });
  }

  createOrderListener() {
    this.signalRService.hubConnection.on('StartSession', (data) => {
      // this.order.ManagerCalls = this.order.ManagerCalls.filter(function (m) {
      //   return m.Id != data.Id;
      // });
      if (this.isManager) {
        this.loadAllOrders();
      }
      else {
        this.loadOrderHistory();
      }
      console.log("new-order-listener", data);
    });
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
        this.orders.forEach(o => {
          this.calls = this.calls.concat(o.Calls);
        })
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
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

  assignOrderAsManager(orderId) {
    this.signalRService.hubConnection.invoke('AssignManagerToSession', orderId, null)
      .then(res => {
        new Promise(resolve => setTimeout(resolve, 1000)).then(res => { this.redirectToOrder(orderId); });

      })
      .catch(err => console.error(err));
  }

  disableManagerCall(callId) {
    this.signalRService.hubConnection.invoke('AcceptCallManager', callId)
      .then(res => {
      })
      .catch(err => console.error(err));
    console.log("click - disable - call");

  }

}
