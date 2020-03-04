import { Component, OnInit } from '@angular/core';
import { TableService } from 'app/services/table.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, RouterEvent, NavigationEnd, ParamMap, Router } from '@angular/router';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';
import { Subject } from 'rxjs';
import { SignalRService } from 'app/services/signal-r.service';

@Component({
  selector: 'app-tables-manager',
  templateUrl: './tables-manager.component.html',
  styleUrls: ['./tables-manager.component.scss']
})
export class TablesManagerComponent implements OnInit {

  constructor(
    public signalRService: SignalRService,
    private tableService: TableService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public datepipe: DatePipe,
    private activateRoute: ActivatedRoute,
    private modalService: NgbModal) { }

  //logic vars
  isNotFound = false;
  closeResult: string;
  now = new Date();
  selectedStartDate;
  selectedEndDate;
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
    StartTime: [this.selectedStartDate, [Validators.required]],
    EndTime: [this.selectedEndDate, [Validators.required]],
    TableId: ['', [Validators.required]],
    Comment: [''],
    ClientId: ['', [Validators.required]]
  });
  submittedBook = false;
  //forms

  //params
  locationId = localStorage.getItem("MyLocationId");
  clientId = localStorage.getItem("ClientId");
  welcomeLocationId;
  isLocationExist = (this.locationId != null && this.locationId != '0');
  //params

  //bool vars for role access
  isLogin = (localStorage.getItem('token') != null);
  isManager = ((this.isLogin) && (localStorage.getItem('IsManager').toLocaleLowerCase() == 'true'));
  isCurrentUser = ((this.isLogin) && (localStorage.getItem('CurrentRole').toLocaleLowerCase() == 'false'));
  isEdit = (this.isLocationExist && this.isManager && !this.isCurrentUser);
  isMy = false;
  //bool vars for role access

  //server list
  tables = new Array();
  tableBookingHistory = new Array();
  tableBookingNow = new Array();
  freeTables = new Array();
  freeTablesNow = new Array();
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
      if (this.welcomeLocationId == 'my') {
        this.isMy = true;
        this.welcomeLocationId = this.locationId;
      }
      else {
        this.isEdit = false;
      }
    }

    if (!this.isLocationExist && this.isEdit) {
      return;
    }
    this.loadLocationTables();
  }

  loadLocationTables() {
    if (this.isMy) {
      this.tableService.getMyTableBooks().subscribe(
        res => {
          this.tableBookingNow = res as [];
          console.log("my-total-books", this.tableBookingNow);
        },
        err => {
          this.isNotFound = true;
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
    else {
      this.tableService.getAllTablesByLocation(this.welcomeLocationId).subscribe(
        res => {
          this.tables = res as [];
          this.isNotFound = (this.tables.length == 0 && !this.isEdit);
          this.tableBookingNow = new Array();
          this.tables.forEach(e => {
            console.log("books", e.TableBooks.length);
            if (e.TableBooks != null && e.TableBooks.length > 0) {
              this.tableBookingNow = this.tableBookingNow.concat(e.TableBooks);
              console.log("concat");
            }
          })
          console.log("total books", this.tableBookingNow);
          this.countFreeTablesNow();
        },
        err => {
          this.isNotFound = true;
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
  }

  startOrder(tableId) {
    const body = {
      IsActive: true,
      StartDate: new Date(),
      TableId: tableId,
    }
    this.signalRService.hubConnection.invoke('StartSession', body, null)
      .catch(err => console.error(err));
  }

  editTable(id: number) {
    this.isEditTable = true;
    const table = this.tables.find(m => m.Id == id);
    this.tableForm.patchValue({ Id: id, ...table });
  }

  compareDate(date1: Date, date2: Date): number {
    let d1 = new Date(date1); let d2 = new Date(date2);
    // Check if the dates are equal
    let same = d1.getTime() === d2.getTime();
    if (same) return 0;
    // Check if the first is greater than second
    if (d1 > d2) return 1;
    // Check if the first is less than second
    if (d1 < d2) return -1;
  }

  pickFreeTables() {
    this.freeTables = new Array();
    const start = moment(new Date(this.selectedStartDate)).format("YYYY-MM-DD HH:mm:ss");
    const end = moment(new Date(this.selectedEndDate)).format("YYYY-MM-DD HH:mm:ss");
    console.log("start", start);
    console.log("end", end);

    this.tables.forEach(t => {
      let isFree = true;
      // debugger;
      t.TableBooks.forEach(b => {
        let res1 = (this.compareDate(new Date(start), b.StartTime) == 0 || this.compareDate(new Date(start), b.StartTime) == 1); //0.1
        let res2 = (this.compareDate(new Date(start), b.EndTime) == -1); //-1
        let res3 = (this.compareDate(new Date(end), b.StartTime) == 1); //1
        let res4 = (this.compareDate(new Date(end), b.EndTime) == 0 || this.compareDate(new Date(end), b.EndTime) == -1); //-1.0

        if ((res1 && res2) || (res3 && res4)) {
          isFree = false;
        }
      });
      if (isFree || t.TableBooks.length == 0) {
        this.freeTables.push(t);
      }
    })
  }

  countFreeTablesNow() {
    this.freeTablesNow = new Array();
    const start = new Date();
    const end = new Date();
    this.tables.forEach(t => {
      // debugger;
      let isFree = true;
      t.TableBooks.forEach(b => {
        let res1 = (this.compareDate(new Date(start), b.StartTime) == 0 || this.compareDate(new Date(start), b.StartTime) == 1); //0.1
        let res2 = (this.compareDate(new Date(start), b.EndTime) == -1); //-1
        let res3 = (this.compareDate(new Date(end), b.StartTime) == 1); //1
        let res4 = (this.compareDate(new Date(end), b.EndTime) == 0 || this.compareDate(new Date(end), b.EndTime) == -1); //-1.0

        if ((res1 && res2) || (res3 && res4)) {
          isFree = false;
          t.IsFree = false;
        }
      });
      if ((isFree || t.TableBooks.length == 0) && !t.IsHaveOrderNow) {
        this.freeTablesNow.push(t);
        t.IsFree = true;
      }
    })
    return this.freeTablesNow.length;
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
    let startTime = moment(new Date(this.selectedStartDate)).format("YYYY-MM-DD HH:mm:ss");
    let endTime = moment(new Date(this.selectedEndDate)).format("YYYY-MM-DD HH:mm:ss");
    this.tableService.createTableBook(this.bookForm, startTime, endTime).subscribe(
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

  cancelBook(id) {
    this.tableService.cancelBook(id).subscribe(
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

  acceptBook(id) {
    this.tableService.acceptBook(id).subscribe(
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

  declineBook(id) {
    this.tableService.declineBook(id).subscribe(
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
      let now = new Date();
      this.bookForm.patchValue({ StartTime: now, EndTime: now });
    }
    if (goal == "newBookUser") {
      this.bookForm.reset();
      let now = new Date();
      this.bookForm.patchValue({ StartTime: now, EndTime: now, ClientId: this.clientId });
    }
    this.modalService.open(content, { size: 'xl', ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {

    this.tableForm.reset();
    this.bookForm.reset();

    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
