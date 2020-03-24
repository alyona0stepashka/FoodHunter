import { Component, OnInit, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FeedbackService } from 'app/services/feedback.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Lightbox } from 'ngx-lightbox';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.scss']
})
export class FeedbackListComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    private feedbackService: FeedbackService,
    private activateRoute: ActivatedRoute,
    private lbLightbox: Lightbox,
    private router: Router) { }

  // server list
  @Input() feedbacks;
  // server list

  // logic vars
  serverUrl = environment.serverURL;
  isEmpty = false;
  // logic vars

  // images
  private lbAlbum: any[] = new Array();
  // images

  ngOnInit() {
    this.feedbacks.forEach(element => {
      let index = 0;
      this.lbAlbum = new Array();
      if (element.Photos[0] != null) {
        element.Photos[0].Number = index;
        index++;
        this.lbAlbum.push({ src: environment.serverURL + element.Photos[0].Value });
      }
      if (element.User.Photo != null) {
        element.User.Photo.Number = index;
        index++;
        this.lbAlbum.push({ src: environment.serverURL + element.User.Photo.Value, caption: element.User.FullName + ', ' + element.User.Age });
      }
    });
  }

  open(index: number): void {
    console.log("click-img", this.lbAlbum[index]);

    this.lbLightbox.open(this.lbAlbum, index);
  }

  close(): void {
    this.lbLightbox.close();
  }

}
