import { Component, OnInit } from '@angular/core';
import { MenuService } from 'app/services/menu.service';
import { StaticService } from 'app/services/static.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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
    private modalService: NgbModal) { }

  menuForm: FormGroup;
  public locationId = localStorage.getItem("MyLocationId");
  public isLocationExist = (this.locationId != null && this.locationId != '0');
  public menus = new Array();
  public icons = new Array();

  submittedMenu = false;


  ngOnInit() {
    if (!this.isLocationExist) {
      return;
    }
    this.loadLocationMenu();
    this.menuForm = this.formBuilder.group({
      Title: ['', [Validators.required]],
      Info: ['', [Validators.required]],
      IconId: ['', [Validators.required]]
    });
  }

  loadLocationMenu() {
    this.menuService.getLocationMenus(this.locationId).subscribe(
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

  onSubmitMenu() {
    this.submittedMenu = true;
    if (this.menuForm.invalid) {
      return null;
    }
    this.menuService.createMenu(this.menuForm, this.locationId).subscribe(
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

}
