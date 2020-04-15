import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'environments/environment';
import { LocationService } from 'app/services/location.service';
import { ToastrService } from 'ngx-toastr';
import { Lightbox } from 'ngx-lightbox';

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
    private lbLightbox: Lightbox,
    private router: Router) { }

  //logic vars
  serverUrl = environment.serverURL;
  isNotFound = false;
  //logic vars

  //location tab
  locationId;
  locationInfo;
  //location tab

  //photos tab
  companyPhoto = '';
  topPhoto = './assets/img/header.jpg';
  private lbAlbum: any[] = new Array();
  //photos tab

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.locationId = params.id);
    this.locationService.getLocation(this.locationId).subscribe(
      res => {
        this.locationInfo = res;
        this.isNotFound = this.locationInfo == null;
        this.companyPhoto = this.serverUrl + this.locationInfo.CompanyPhoto;
        let index = 0;
        this.lbAlbum = new Array();
        this.locationInfo.PhotoAlbum.forEach(element => {
          element.Number = index;
          index++;
          this.lbAlbum.push({ src: environment.serverURL + element.Value });
        });
      },
      err => {
        console.log(err);
        this.isNotFound = true;
        // this.toastr.error(err.error, 'Error');
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
}
