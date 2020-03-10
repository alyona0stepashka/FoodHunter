import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Lightbox } from 'ngx-lightbox';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'environments/environment';
import { OrderService } from 'app/services/order.service';
import { DatePipe } from '@angular/common';
import { SignalRService } from 'app/services/signal-r.service';
import { FeedbackService } from 'app/services/feedback.service';

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
    private feedbackService: FeedbackService,
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

  //crud items
  order: any;
  openedItem: any;
  openItemPhotoPath;
  managerCallMessage = '';
  check = new Array();
  clientTotal: any;
  statuses = ["In start progress", "Cooked in the kitchen", "Ready to serve", "Served on client table", "Paid"];
  //crud items

  //forms
  feedbackForm: FormGroup = this.formBuilder.group({
    Stars: [5, [Validators.required]],
    Comment: ['', [Validators.required]],
    MenuItemId: [0, [Validators.required]],
    UserProfileId: [0, [Validators.required]],
    Photo: [null]
  });
  submittedFeedback = false;
  UploadFile: File = null;
  //forms

  //work with images
  serverUrl = environment.serverURL;
  private lbAlbum: any[] = new Array();
  imageUrl = './assets/img/upload-photo.jpg';
  i = 0;
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
        this.order.Clients.forEach(client => {
          client.OrderItems.forEach(item => {
            item.Photo.Number = this.i;
            this.i++;
            this.lbAlbum.push({ src: environment.serverURL + item.Photo.Value, caption: item.Title });
          });
        });
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

  onChangeStatus(event: any, itemId: number) {
    console.log("value-status", event.target.value);

    this.signalRService.hubConnection.invoke('ChangeOrderItemStatus', itemId, event.target.value)
      .then(res => { location.reload(); })
      .catch(err => console.error(err));
  }

  setCurrentClientId(id) {
    this.currentClientId = id;
    console.log(id);
  }

  getClientTotal(clientId: number) {
    if (clientId == 0) {
      clientId = parseInt(localStorage.getItem("ClientId"), 10);
    }
    let items = this.order.Clients.find(function (m) {
      return m.User.UserProfileId == clientId;
    }).OrderItems;
    let sum = 0;
    items.forEach(e => {
      sum += e.PricePerItem * e.Count;
    });
    this.clientTotal = {};
    this.clientTotal.Items = items;
    this.clientTotal.Sum = sum;
  }

  onSubmitFeedback() {
    this.submittedFeedback = true;
    if (this.feedbackForm.invalid) {
      return null;
    }
    this.feedbackService.createFeedback(this.feedbackForm, this.UploadFile).subscribe(
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

  get f() { return this.feedbackForm.controls; }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.UploadFile);
  }

  openModal(content, goal?: string, id?: any) {
    if (goal == "showCheck") {
      this.getClientTotal(0);
    }
    if (goal == "addFeedback") {
      this.feedbackForm.patchValue({ UserProfileId: parseInt(localStorage.getItem("ClientId"), 10), MenuItemId: id });
    }
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
