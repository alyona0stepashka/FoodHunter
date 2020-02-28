import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'environments/environment';
import { ActivatedRoute } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Lightbox } from 'ngx-lightbox';

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
    private lbLightbox: Lightbox,
    private modalService: NgbModal) { }

  //logic vars
  currentMenuId: number;
  closeResult: string;
  IsActive = true;
  isNotFound = false;
  IsChecked = true;
  //logic vars

  //firat tab form
  menuForm: FormGroup = this.formBuilder.group({
    Id: [''],
    Title: ['', [Validators.required]],
    Info: ['', [Validators.required]],
    IconId: ['', [Validators.required]]
  });
  menuItemForm: FormGroup = this.formBuilder.group({
    Id: [''],
    Title: ['', [Validators.required]],
    Info: ['', [Validators.required]],
    Price: ['', [Validators.required]],
    PriceWithSales: [''],
    IsActive: [this.IsChecked, [Validators.required]],
    MenuId: ['', [Validators.required]],
    Note: ['', [Validators.required]],
    Photo: [''/*, [Validators.required]*/]
  });
  submittedMenu = false;
  submittedEditMenu = false;
  submittedMenuItem = false;
  submittedEditMenuItem = false;
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

  //crud menu items
  editMenuItem: any;
  editItemPhotoPath;
  openMenuItem = {
    Id: 1,
    Title: "Veritatis vel culpa",
    Info: "Veritatis ipsa at v",
    Note: "Id nihil accusamus a",
    Price: 7,
    Rate: 0,
    PriceWithSales: 6,
    IsActive: false,
    MenuId: 1,
    MenuTitle: "Ramen",
    Photo: {
      Value: "/Images/a5b3f3fb-a925-4982-a787-65c546bf0477.jpg",
      Description: null,
      Id: 1,
      Number: 0
    },
    Feedbacks: null
  };
  openItemPhotoPath;
  //crud menu items

  //work with images
  // TODO 21.02.2020 need to reset image after submit
  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';
  serverUrl = environment.serverURL;
  private lbAlbum: any[] = new Array();
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
    this.loadLocationMenu();
  }

  open(index: number): void {
    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }

  toggleIsActive() {
    this.IsChecked = !this.IsChecked;
    //this.IsActive = !this.IsActive;
    //this.menuItemForm.patchValue({ IsActive: this.IsActive });
  }

  loadLocationMenu() {
    this.menuService.getLocationMenus(this.welcomeLocationId).subscribe(
      res => {
        this.menus = res as [];
        this.isNotFound = (this.menus.length == 0 && !this.isEdit);
        let index = 0;
        this.menus.forEach(menu => {
          if (menu != null && menu.MenuItems != null) {
            menu.MenuItems.forEach(item => {
              this.openMenuItem = item;
              item.Photo.Number = index;
              index++;
              this.lbAlbum.push({ src: environment.serverURL + item.Photo.Value, caption: item.Title });
            })
          }
        });
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
        this.isNotFound = true;
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
    this.menuItemForm.patchValue({ IsActive: this.IsChecked });
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

  onSubmitEditMenuItem() {
    this.submittedEditMenu = true;
    this.menuItemForm.patchValue({ IsActive: this.IsChecked });
    if (this.menuItemForm.invalid) {
      return null;
    }
    this.menuService.updateMenuItem(this.menuItemForm).subscribe(
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
      this.menuItemForm.patchValue({ MenuId: id, IsActive: true });
      this.IsActive = true;
      this.IsChecked = true;
    }
    if (goal == "editMenu") {
      this.editMenu = this.menus.find(m => m.Id == id);
      this.menuForm.patchValue({ Id: id, Title: this.editMenu.Title, Info: this.editMenu.Info, IconId: this.editMenu.Icon.Id });
    }
    if (goal == "editMenuItem") {
      const editMenu = this.menus.find(m => m.Id == this.currentMenuId);
      const editMenuItem = editMenu.MenuItems.find(m => m.Id == id);
      this.menuItemForm.patchValue(
        {
          ...editMenuItem,
          Photo: null
        });
      this.IsChecked = editMenuItem.IsActive;
      this.editItemPhotoPath = environment.serverURL + editMenuItem.Photo.Value;
    }
    if (goal == "openMenuItem") {
      console.log("id", id);
      var menu = this.menus.find(m => m.Id == this.currentMenuId);
      console.log("openMenu", menu);
      this.openMenuItem = menu.MenuItems.find(m => m.Id == id);
      console.log("openMenuItem", this.openMenuItem);
      this.openItemPhotoPath = environment.serverURL + this.openMenuItem.Photo.Value;
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