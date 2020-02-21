import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'environments/environment';
import { ActivatedRoute } from '@angular/router';

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
    private activateRoute: ActivatedRoute) { }

  //logic vars
  currentMenuId: number;
  serverUrl = environment.serverURL;
  //logic vars

  //firat tab form
  menuForm: FormGroup;
  submittedMenu = false;
  //firat tab form

  //my locationId & locationId from url
  locationId = localStorage.getItem("MyLocationId");
  welcomeLocationId;
  isLocationExist = (this.locationId != null && this.locationId != '0');
  //myLocationId & locationId from url

  //bool vars for role access
  public isLogin = (localStorage.getItem('token') != null);
  public isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  public isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  public isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  //bool vars for role access

  //server lists
  public menus = new Array();
  public icons = new Array();
  //server lists

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
    this.loadLocationMenu();
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

  onSubmitMenu() {
    this.submittedMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.createMenu(this.menuForm, this.welcomeLocationId).subscribe(
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

  setCurrentMenuId(id) {
    this.currentMenuId = id;
    console.log(id);

  }

}