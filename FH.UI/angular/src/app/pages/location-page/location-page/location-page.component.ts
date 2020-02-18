import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'environments/environment';
import { LocationService } from 'app/services/location.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-location-page',
  templateUrl: './location-page.component.html',
  styleUrls: ['./location-page.component.scss']
})
export class LocationPageComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    private locationService: LocationService,
    private activateRoute: ActivatedRoute,
    private router: Router) { }

  locationId;
  myLocationId = localStorage.getItem('MyLocationId');
  public isEdit;
  public location: any;

  UploadFile: File = null;
  imageTopUrl = './assets/img/upload-photo.jpg';
  public serverUrl = environment.serverURL;

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.locationId = params.id);
    this.isEdit = (this.locationId == this.myLocationId);
    this.locationService.getLocation(this.locationId).subscribe(
      res => {
        this.location = res;
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  // uploadTopPhoto(file: FileList) {
  //   this.UploadFile = file.item(0);
  //   const reader = new FileReader();
  //   reader.onload = (event: any) => {
  //     this.imageTopUrl = event.target.result;
  //   };
  //   reader.readAsDataURL(this.UploadFile);
  // }

}
