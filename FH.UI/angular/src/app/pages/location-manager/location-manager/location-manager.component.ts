import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'app/services/location.service';
import { CompanyService } from 'app/services/company.service';
import { StaticService } from 'app/services/static.service';
import { MouseEvent } from '@agm/core';


import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'environments/environment';

declare var google: any;

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
  public myLatlng: any;
  public lat = 0;
  public lng = 0;
  public zoom = 1;
  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';
  public serverUrl = environment.serverURL;

  ngOnInit() {
    this.locationForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      CompanyId: ['', [Validators.required]],
      Longitude: ['', [Validators.required]],
      Latitude: ['', [Validators.required]],
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
    this.loadStatic();
    this.loadMap();
  }

  loadStatic() {
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

  loadMap() {
    this.getPosition().then(pos => {
      this.lng = pos.lng;
      this.lat = pos.lat;
      this.myLatlng = new google.maps.LatLng(pos.lat, pos.lng);
      console.log(`Positon: ${pos.lng} ${pos.lat}`);
    });
    // var mapOptions = {
    //   zoom: 13,
    //   center: this.myLatlng,
    //   scrollwheel: false, //we disable de scroll over the map, it is a really annoing when you scroll through page
    //   styles: [{ "featureType": "water", "stylers": [{ "saturation": 43 }, { "lightness": -11 }, { "hue": "#0088ff" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "hue": "#ff0000" }, { "saturation": -100 }, { "lightness": 99 }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "color": "#808080" }, { "lightness": 54 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "color": "#ece2d9" }] }, { "featureType": "poi.park", "elementType": "geometry.fill", "stylers": [{ "color": "#ccdca1" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#767676" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b8cb93" }] }, { "featureType": "poi.park", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.medical", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "stylers": [{ "visibility": "simplified" }] }]

    // }
    // var mapEl = document.getElementById("mapGoogle");
    // console.log("map", mapEl);

    // var map = new google.maps.Map(document.getElementById("mapGoogle"), mapOptions);

    // var marker = new google.maps.Marker({
    //   position: this.myLatlng,
    //   title: "Your location"
    // });

    // // To add the marker to the map, call setMap();
    // marker.setMap(map);
  }

  setGeolocationForm() {
    this.locationForm.patchValue({ Latitude: this.myLatlng.lat(), Longitude: this.myLatlng.lng() });
    this.modalService.dismissAll();
  }

  getPosition(): Promise<any> {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition(resp => {

        resolve({ lng: resp.coords.longitude, lat: resp.coords.latitude });
      },
        err => {
          reject(err);
        });
    });

  }
  // mapClicked($event: MouseEvent) {
  //   this.markers.push({
  //     lat: $event.coords.lat,
  //     lng: $event.coords.lng,
  //     draggable: true
  //   });
  // }

  markerDragEnd(m: marker, $event: MouseEvent) {
    console.log('dragEnd', m, $event);
    this.markers = new Array();
    this.markers.push(m);
  }

  markers: marker[] = [
    {
      lat: this.lat,
      lng: this.lng,
      label: 'Your location',
      draggable: true
    }
  ];

}
interface marker {
  lat: number;
  lng: number;
  label?: string;
  draggable: boolean;
}
