import { Component, OnInit } from '@angular/core';
import { TableService } from 'app/services/table.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tables-manager',
  templateUrl: './tables-manager.component.html',
  styleUrls: ['./tables-manager.component.scss']
})
export class TablesManagerComponent implements OnInit {

  constructor(
    private tableService: TableService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private activateRoute: ActivatedRoute,
    private modalService: NgbModal) { }

  //logic vars
  isNotFound = false;
  closeResult: string;
  //logic vars

  //forms
  tableForm: FormGroup = this.formBuilder.group({
    Id: [''],
    Number: ['', [Validators.required]],
    Info: ['', [Validators.required]]
  });
  submittedTable = false;
  //forms

  //params
  locationId = localStorage.getItem("MyLocationId");
  welcomeLocationId;
  isLocationExist = (this.locationId != null && this.locationId != '0');
  //params

  //bool vars for role access
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  //bool vars for role access

  //server list
  tables = new Array();
  //server list

  //crud tables
  isEditTable = false;
  //crud tables

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
    this.tableForm = this.formBuilder.group({
      Id: [''],
      Number: ['', [Validators.required]],
      Info: ['', [Validators.required]]
    });
    this.loadLocationTables();
  }

  loadLocationTables() {
    this.tableService.getAllTablesByLocation(this.welcomeLocationId).subscribe(
      res => {
        this.tables = res as [];
        this.isNotFound = (this.tables.length == 0 && !this.isEdit);
      },
      err => {
        this.isNotFound = true;
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }
  editTable(id) {
    this.isEditTable = true;
    this.tableForm.patchValue({ Id: id });
  }

  deleteTable(id) {
    this.tableService.deleteTable(id).subscribe(
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
        this.loadLocationTables();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  onSubmitTable() {
    this.submittedTable = true;
    if (this.tableForm.invalid) {
      return null;
    }
    if (this.isEditTable) {
      this.tableService.updateTable(this.tableForm, this.locationId).subscribe(
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
          this.loadLocationTables();
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
    else {
      this.tableService.createTable(this.tableForm, this.locationId).subscribe(
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
          this.loadLocationTables();
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
  }

  get f() { return this.tableForm.controls; }
}
