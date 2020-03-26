import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SignalRService } from 'app/services/signal-r.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  public isLogin = (localStorage.getItem('token') != null);

  constructor(
    public toastrService: ToastrService,
    public signalRService: SignalRService) { }

  ngOnInit() {
    if (this.isLogin) {
      this.signalRService.startConnection();

    }
    //this.addSendListener();
    //this.addSendFriendRequestListener();
  }

  // public

  // public addSendListener() {
  //   this.signalRService.hubConnection.on('Send', (data) => {
  //     const mess = data;
  //     if (mess.text.length > 15) {
  //       mess.text = mess.text.substring(0, 14) + '...';
  //     }
  //     this.toastrService.info(mess.senderName + ': ' + mess.text, 'New Message');
  //     this.signalRService.soundNotify.load();
  //     this.signalRService.soundNotify.play();
  //     console.log(data);
  //   });
  // }

  // public addSendFriendRequestListener() {
  //   this.signalRService.hubConnection.on('SendFriendRequest', (data) => {
  //     this.toastrService.info(data, 'New Friend Request');
  //     this.signalRService.soundNotify.load();
  //     this.signalRService.soundNotify.play();
  //     console.log(data);
  //   });
  // }

}
