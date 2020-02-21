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
  menuForm: FormGroup;
  menuItemForm: FormGroup;
  public isLogin = (localStorage.getItem('token') != null);
  public locationId = localStorage.getItem("MyLocationId");
  public welcomeLocationId;
  public isLocationExist = (this.locationId != null && this.locationId != '0');
  public isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  public isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  public isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  public menus = new Array();
  public icons = new Array();

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

}