import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'environments/environment';
import { ActivatedRoute } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-menu-manager',
  templateUrl: './menu-manager.component.html',
  styleUrls: ['./menu-manager.component.scss']
})
export class MenuManagerComponent implements OnInit {

  constructor(
    private menuService: MenuService,
    private staticService: StaticService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private activateRoute: ActivatedRoute,
    private modalService: NgbModal) { }

  //logic vars
  currentMenuId: number;
  closeResult: string;
  IsActive = true;
  //logic vars

  //firat tab form
  menuForm: FormGroup;
  menuItemForm: FormGroup;
  submittedMenu = false;
  submittedEditMenu = false;
  submittedMenuItem = false;
  //firat tab form

  //my locationId & locationId from url
  locationId = localStorage.getItem("MyLocationId");
  welcomeLocationId;
  isLocationExist = (this.locationId != null && this.locationId != '0');
  //myLocationId & locationId from url

  //bool vars for role access
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  //bool vars for role access

  //server lists
  menus = new Array();
  icons = new Array();
  //server lists

  //crud menu
  editMenu: any;
  //crud menu

  //work with images
  // TODO 21.02.2020 need to reset image after submit
  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';
  serverUrl = environment.serverURL;
  //work with images

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.welcomeLocationId = params.id);

    this.isLogin = (localStorage.getItem('token') != null);
    this.locationId = localStorage.getItem("MyLocationId");
    this.isLocationExist = (this.locationId != null && this.locationId != '0');
    this.isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
    this.isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
    this.isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);

    if (this.welcomeLocationId == '0') {
      this.welcomeLocationId = this.locationId;
      this.isEdit = true;
    }
    else {
      this.isEdit = false;
    }
    if (!this.isLocationExist && this.isEdit) {
      return;
    }
    this.menuForm = this.formBuilder.group({
      Id: [''],
      Title: ['', [Validators.required]],
      Info: ['', [Validators.required]],
      IconId: ['', [Validators.required]]
    });
    this.menuItemForm = this.formBuilder.group({
      Id: [''],
      Title: ['', [Validators.required]],
      Info: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      PriceWithSales: [''],
      IsActive: [this.IsActive, [Validators.required]],
      MenuId: ['', [Validators.required]],
      Note: ['', [Validators.required]],
      Photo: ['', [Validators.required]]
    });
    this.loadLocationMenu();
  }

  toggleIsActive() {
    this.IsActive = !this.IsActive;
    this.menuItemForm.patchValue({ IsActive: this.IsActive });
  }

  loadLocationMenu() {
    this.menuService.getLocationMenus(this.welcomeLocationId).subscribe(
      res => {
        this.menus = res as [];
        this.staticService.getIcons().subscribe(
          res => {
            this.icons = res as [];
          },
          err => {
            console.log(err);
            this.toastr.error(err.error, 'Error');
          }
        );
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  get f() { return this.menuForm.controls; }

  get f2() { return this.menuItemForm.controls; }

  onSubmitMenu() {
    this.submittedMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.createMenu(this.menuForm, this.locationId).subscribe(
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
        this.loadLocationMenu();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  onSubmitMenuItem() {
    this.submittedMenuItem = true;
    if (this.menuItemForm.invalid) {
      return null;
    }
    this.menuService.createMenuItem(this.menuItemForm, this.UploadFile).subscribe(
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
        this.loadLocationMenu();
        this.modalService.dismissAll();
        this.menuItemForm.reset();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  onSubmitEditMenu() {
    this.submittedEditMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.updateMenu(this.menuForm, this.locationId).subscribe(
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
        this.loadLocationMenu();
        this.modalService.dismissAll();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  deleteMenu(id) {
    this.menuService.deleteMenu(id).subscribe(
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
        this.loadLocationMenu();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  deleteMenuItem(id) {
    this.menuService.deleteMenuItem(id).subscribe(
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
        this.loadLocationMenu();
        this.modalService.dismissAll();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  setCurrentMenuId(id) {
    this.currentMenuId = id;
    console.log(id);
  }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.UploadFile);
  }

  openModal(content, goal?: string, id?: any) {
    if (goal == "newMenuItem") {
      this.menuItemForm.patchValue({ MenuId: id });
      this.IsActive = true;
    }
    if (goal == "editMenu") {
      this.editMenu = this.menus.find(m => m.Id == id);
      this.menuForm.patchValue({ Id: id, Title: this.editMenu.Title, Info: this.editMenu.Info, IconId: this.editMenu.Icon.Id });
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