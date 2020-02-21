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

  //logic vars
  serverUrl = environment.serverURL;
  //logic vars

  //location tab
  locationId;
  locationInfo;
  //location tab

  //photos tab
  companyPhoto = '';
  topPhoto = './assets/img/header.jpg';
  //photos tab

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.locationId = params.id);
    this.locationService.getLocation(this.locationId).subscribe(
      res => {
        this.locationInfo = res;
        this.companyPhoto = this.serverUrl + this.locationInfo.CompanyPhoto;
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }
}
