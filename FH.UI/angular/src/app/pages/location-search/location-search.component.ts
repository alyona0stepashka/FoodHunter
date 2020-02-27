import { Component, OnInit } from '@angular/core';
import { StaticService } from 'app/services/static.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'environments/environment';
import { Lightbox } from 'ngx-lightbox';
import { Router } from '@angular/router';

@Component({
  selector: 'app-location-search',
  templateUrl: './location-search.component.html',
  styleUrls: ['./location-search.component.scss']
})
export class LocationSearchComponent implements OnInit {

  constructor(private router: Router,
    private toastr: ToastrService,
    private staticService: StaticService,
    private lbLightbox: Lightbox
  ) { }

  //search result
  searchResult = new Array();
  isCompany = false;
  isLocation = false;
  isSearched = false;
  key = '';
  searchObjs = new Array();
  //search result

  //img vars
  serverUrl = environment.serverURL;
  lbAlbum = new Array();
  //img vars

  ngOnInit() {
  }

  getSearchResult() {
    this.searchObjs = new Array();
    if (this.isLocation) {
      this.searchObjs.push("location");
    }
    if (this.isCompany) {
      this.searchObjs.push("company");
    }

    this.staticService.getSearchResult(this.key, this.searchObjs).subscribe(
      res => {
        this.searchResult = res as [];
        this.isSearched = true;
        this.lbAlbum = new Array();
        let i = 0;
        this.searchResult.forEach(e => {
          e.Number = i++;
          this.lbAlbum.push({ src: environment.serverURL + e.PhotoPath, caption: e.Title });
        })
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  redirect(ObjId, ObjType) {
    if (ObjType == 'location') {
      this.router.navigateByUrl('/dashboard-user/location/' + ObjId);
    }
  }

  open(index: number): void {
    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }

}
