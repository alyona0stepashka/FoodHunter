import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'environments/environment';
import { ActivatedRoute } from '@angular/router';
import { Lightbox } from 'ngx-lightbox';

@Component({
  selector: 'app-menu-manager',
  templateUrl: './menu-manager.component.html',
  styleUrls: ['./menu-manager.component.scss']
})
export class MenuManagerComponent implements OnInit {

  public isNotFound = false;

  constructor(
    private menuService: MenuService,
    private staticService: StaticService,
    private lbLightbox: Lightbox,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private activateRoute: ActivatedRoute,
    private modalService: NgbModal) { }

  closeResult: string;
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
    IsActive: [this.IsActive, [Validators.required]],
    MenuId: ['', [Validators.required]],
    Note: ['', [Validators.required]],
    Photo: ['', [Validators.required]],
    // IconId: ['']
  });
  public isLogin = (localStorage.getItem('token') != null);
  public locationId = localStorage.getItem("MyLocationId");
  public welcomeLocationId;
  public isLocationExist = (this.locationId != null && this.locationId != '0');
  public isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  public isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  public isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  public menus = new Array();
  public icons = new Array();
  IsActive = true;
  editItemPhotoPath;
  openItemPhotoPath;

  currentMenuId: number;

  private lbAlbum: any[] = new Array();

  editMenu: any;
  editMenuItem: any;
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

  submittedMenu = false;
  submittedEditMenu = false;
  submittedMenuItem = false;
  submittedEditMenuItem = false;

  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';
  public serverUrl = environment.serverURL;


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
      Photo: ['', [Validators.required]],
      // IconId: ['']
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
        this.isNotFound = (this.menus.length == 0 && !this.isEdit);
        this.menus.forEach(menu => {
          if (menu != null && menu.MenuItems != null) {
            let index = 0;
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
  open(index: number): void {
    console.log("click-img", this.lbAlbum[index]);

    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }

  get f() { return this.menuForm.controls; }

  get f2() { return this.menuItemForm.controls; }

  onSubmitMenu() {
    this.submittedMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.createMenu(this.menuForm, this.welcomeLocationId).subscribe(
      (res: any) => {
        // this.userId = res as string;
        // this.resetForm();
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

  onSubmitEditMenu() {
    this.submittedEditMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.updateMenu(this.menuForm, this.welcomeLocationId).subscribe(
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
        this.modalService.dismissAll();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  onSubmitEditMenuItem() {
    this.submittedEditMenu = true;
    if (this.menuForm.invalid) {
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
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.UploadFile);
  }

  setCurrentMenuId(id) {
    this.currentMenuId = id;
    console.log(id);

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
    if (goal == "editMenuItem") {
      console.log("id", id);
      var editMenu = this.menus.find(m => m.Id == this.currentMenuId);
      console.log("editMenu", editMenu);
      this.editMenuItem = editMenu.MenuItems.find(m => m.Id == id);
      console.log("editMenuItem", this.editMenuItem);
      this.menuItemForm.patchValue(
        {
          Id: id,
          Title: this.editMenuItem.Title,
          Info: this.editMenuItem.Info,
          Price: this.editMenuItem.Price,
          PriceWithSales: this.editMenuItem.PriceWithSales,
          IsActive: this.editMenuItem.IsActive,
          MenuId: this.editMenuItem.MenuId,
          Note: this.editMenuItem.Note,
          Photo: "null",
          // IconId: this.editMenuItem.Icon.Id
        });
      this.editItemPhotoPath = environment.serverURL + this.editMenuItem.Photo.Value;
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
