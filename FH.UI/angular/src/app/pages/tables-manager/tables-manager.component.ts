import { Component, OnInit } from '@angular/core';
import { TableService } from 'app/services/table.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

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
  bookForm: FormGroup = this.formBuilder.group({
    Id: [''],
    StartTime: ['', [Validators.required]],
    EndTime: ['', [Validators.required]],
    TableId: ['', [Validators.required]],
    ClientId: ['', [Validators.required]]
  });
  submittedBook = false;
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
  tableBookingHistory = new Array();
  tableBookingNow = new Array();
  freeTables = new Array();
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
    this.loadLocationTables();
  }

  loadLocationTables() {
    this.tableService.getAllTablesByLocation(this.welcomeLocationId).subscribe(
      res => {
        this.tables = res as [];
        this.isNotFound = (this.tables.length == 0 && !this.isEdit);
        this.tables.forEach(e => {
          if (e.TableBooks != null && e.TableBooks.length > 0) {
            this.tableBookingNow.concat(e.TableBooks);
          }
        })
      },
      err => {
        this.isNotFound = true;
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  editTable(id: number) {
    this.isEditTable = true;
    const table = this.tables.find(m => m.Id == id);
    this.tableForm.patchValue({ Id: id, Number: table.Number, Info: table.Info });
  }

  pickFreeTables() {
    this.freeTables = new Array();
    const start = this.bookForm.value.StarTime;
    const end = this.bookForm.value.EndTime;
    this.tables.forEach(t => {
      let isFree = false;
      t.TableBooks.forEach(b => {
        if ((start < b.StartTime && end < b.StartTime) || (start > b.EndTime && end > b.EndTime)) {
          isFree = true;
        }
      });
      if (isFree || t.TableBooks.length == 0) {
        this.freeTables.push(t);
      }
    })
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
        this.modalService.dismissAll();
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
          this.modalService.dismissAll();
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
          this.modalService.dismissAll();
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
  }

  onSubmitBook() {
    this.submittedBook = true;
    if (this.bookForm.invalid) {
      return null;
    }
    this.tableService.createTableBook(this.bookForm).subscribe(
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
        this.modalService.dismissAll();
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  setHistoryColor(book) {
    const now = new Date();
    if (book.StartTime < now && book.EndTime > now) {
      return '#cbdaf9';
    }
    if (book.StartTime > now && book.EndTime > now) {
      return 'antiquewhite';
    }
    if (book.StartTime < now && book.EndTime < now) {
      return 'rgb(206, 206, 206)';
    }
  }

  get f() { return this.tableForm.controls; }

  openModal(content, goal?: string, id?: any) {
    if (goal == "newTable") {
      this.tableForm.reset();
      this.isEditTable = false;
    }
    if (goal == "editTable") {
      this.tableForm.reset();
      this.editTable(id);
    }
    if (goal == "openTableBooks") {
      const table = this.tables.find(m => m.Id == id);
      this.tableBookingHistory = table.TableBooks;
      console.log("history", table.TableBooks);
    }
    if (goal == "newBook") {
      this.bookForm.reset();
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
