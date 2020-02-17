import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'app/services/location.service';
import { CompanyService } from 'app/services/company.service';
import { StaticService } from 'app/services/static.service';

import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-location-manager',
  templateUrl: './location-manager.component.html',
  styleUrls: ['./location-manager.component.scss']
})
export class LocationManagerComponent implements OnInit {

  constructor(private locationService: LocationService,
    private staticService: StaticService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private companyService: CompanyService,
    private modalService: NgbModal) { }

  closeResult: string;
  locationForm: FormGroup;
  companyForm: FormGroup;
  submittedCompany = false;
  submitted = false;
  public companies = new Array();
  public specifications = new Array();

  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';

  ngOnInit() {
    this.locationForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      CompanyId: ['', [Validators.required]],
      Longitude: ['', [Validators.required]],
      Latitude: ['', [Validators.required]],
      DateBirth: ['', [Validators.required]],
      Address: ['', [Validators.required]]
    });
    this.companyForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      ContactInfo: ['', [Validators.required]],
      Vk: [''],
      Facebook: [''],
      Instagram: [''],
      Site: [''],
      Describe: ['', [Validators.required]],
      Photo: [null, [Validators.required]],
      SpecificationId: ['']
    });
    this.companyService.getAllCompanies().subscribe(
      res => {
        this.companies = res as [];
        this.staticService.getSpecifications().subscribe(
          res => {
            this.specifications = res as [];
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

  get f() { return this.locationForm.controls; }

  get f2() { return this.companyForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.locationForm.invalid) {
      return null;
    }

    this.locationService.createLocation(this.locationForm).subscribe(
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
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  onSubmitCompany() {
    this.submittedCompany = true;

    if (this.companyForm.invalid) {
      return null;
    }

    this.companyService.createCompany(this.companyForm, this.UploadFile).subscribe(
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

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
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
