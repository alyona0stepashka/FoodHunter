import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { Lightbox } from 'ngx-lightbox';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.scss']
})
export class StaffComponent implements OnInit {

  constructor(private userService: UserService,
    private lbLightbox: Lightbox, ) { }

  //bool vars
  isOpenRegister = false;
  //bool vars

  //sever data
  staff = new Array();
  locationId = localStorage.getItem('MyLocationId');
  //sever data

  //images
  private lbAlbum: any[] = new Array();
  index = 0;
  serverUrl = environment.serverURL;
  //images

  ngOnInit() {
    this.loadStaff();
  }

  loadStaff() {
    this.userService.getStaffByLocation().subscribe(
      res => {
        this.staff = res as [];
        this.staff.forEach(user => {
          if (user != null && user.Photo != null) {
            user.Photo.Number = this.index;
            this.index++;
            this.lbAlbum.push({ src: environment.serverURL + user.Photo.Value, caption: user.FullName });
          }
        });
      },
      err => {
        console.log(err);
        // this.toastr.error(err.error, 'Error');
      }
    );
  }

  deleteUser(id) {
    this.userService.deleteUser(id).subscribe(
      res => {
        this.loadStaff();
      },
      err => {
        console.log(err);
        // this.toastr.error(err.error, 'Error');
      }
    );
  }
  toggleRegister() {
    this.isOpenRegister = !this.isOpenRegister;
  }

  open(index: number): void {
    console.log("click-img", this.lbAlbum[index]);

    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }
}
