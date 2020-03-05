import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Lightbox } from 'ngx-lightbox';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'environments/environment';
import { OrderService } from 'app/services/order.service';
import { DatePipe } from '@angular/common';
import { SignalRService } from 'app/services/signal-r.service';

@Component({
  selector: 'app-order-manager',
  templateUrl: './order-manager.component.html',
  styleUrls: ['./order-manager.component.scss']
})
export class OrderManagerComponent implements OnInit {

  constructor(
    public signalRService: SignalRService,
    private router: Router,
    private orderService: OrderService,
    private menuService: MenuService,
    private staticService: StaticService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public datepipe: DatePipe,
    private activateRoute: ActivatedRoute,
    private lbLightbox: Lightbox,
    private modalService: NgbModal) {
  }


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
  managerCallMessage = '';
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
    this.callManagerListener();
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
        this.isOrderExist = (res != null);
      },
      err => {
        this.isNotFound = true;
        this.isOrderExist = true;
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  callManagerListener() {
    this.signalRService.hubConnection.on('CallManager', (data) => {
      // this.order.ManagerCalls.push(data);
      this.loadOrder();
      console.log("call-manager-listener", data);
    });
  }

  redirectLocation(locId) {
    this.router.navigateByUrl('/dashboard-user/location/' + locId);
  }

  deleteOrderItem(id) {
    this.signalRService.hubConnection.invoke('RemoveOrderItem', id)
      .catch(err => console.error(err));
  }

  cancelSession() {
    this.signalRService.hubConnection.invoke('CancelSession', this.order.Id, null)
      .then(res => { location.reload(); })
      .catch(err => console.error(err));
  }

  // leaveSession() {
  //   this.signalRService.hubConnection.invoke('ExitClientFromSession', this.order.Id, null)
  //     .then(res => { location.reload(); })
  //     .catch(err => console.error(err));
  // }

  callManager() {
    this.signalRService.hubConnection.invoke('CallManager', this.order.Id, this.managerCallMessage, null)
      .catch(err => console.error(err));
    this.managerCallMessage = '';
    this.modalService.dismissAll();
  }

  setCurrentClientId(id) {
    this.currentClientId = id;
    console.log(id);
  }
  openModal(content, goal?: string, id?: any) {
    // if (goal == "callManager") {
    //   this.callManager();
    // }
    this.modalService.open(content, { size: 'xl', ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
