import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Lightbox } from 'ngx-lightbox';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'environments/environment';
import { OrderService } from 'app/services/order.service';

@Component({
  selector: 'app-order-manager',
  templateUrl: './order-manager.component.html',
  styleUrls: ['./order-manager.component.scss']
})
export class OrderManagerComponent implements OnInit {

  constructor(
    private orderService: OrderService,
    private menuService: MenuService,
    private staticService: StaticService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private activateRoute: ActivatedRoute,
    private lbLightbox: Lightbox,
    private modalService: NgbModal) { }


  //logic vars
  currentClientId: number;
  closeResult: string;
  isNotFound = false;
  //logic vars

  //my locationId & locationId from url
  orderId = localStorage.getItem("CurrentOrderId");
  welcomeOrderId;
  isOrderExist = (this.orderId != null && this.orderId != '0');
  //myLocationId & locationId from url

  //bool vars for role access
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  isEdit = (this.isManager && !this.isCurrentUser);
  //bool vars for role access

  //server lists
  order: any;
  //server lists
  openedItem: any;
  openItemPhotoPath;
  //crud menu items

  //work with images
  serverUrl = environment.serverURL;
  private lbAlbum: any[] = new Array();
  //work with images


  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.welcomeOrderId = params.id);

    this.isLogin = (localStorage.getItem('token') != null);
    this.orderId = localStorage.getItem("CurrentOrderId");
    this.isOrderExist = (this.orderId != null && this.orderId != '0');
    this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    this.isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
    this.isEdit = (this.isManager && !this.isCurrentUser);

    if (this.welcomeOrderId == '0') {
      //this.welcomeOrderId = this.orderId;
      this.isEdit = false;
    }
    this.loadOrder();
  }

  open(index: number): void {
    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }

  loadOrder() {
    this.orderService.getOrderById(this.welcomeOrderId).subscribe(
      res => {
        this.order = res;
        this.isNotFound = (res == null);
      },
      err => {
        this.isNotFound = true;
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  deleteOrderItem(id) {
    // TODO: SignalR Remove order item
    this.orderService.deleteOrderItem(id).subscribe(
      (res: any) => {
        this.toastr.success(
          '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Your business location is registered</span>',
          "",
          {
            timeOut: 4000,
            closeButton: true,
            enableHtml: true,
            toastClass: "alert alert-success alert-with-icon"
          }
        );
        this.loadOrder();
        this.modalService.dismissAll();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  setCurrentClientId(id) {
    this.currentClientId = id;
    console.log(id);
  }
}
