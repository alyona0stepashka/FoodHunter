import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'app/services/user.service';
import * as jwt_decode from "jwt-decode";
import * as colors from '../../../shared/fixedplugin/fixedplugin.component';
import { OrderService } from 'app/services/order.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router,
    private orderService: OrderService,
    private service: UserService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService) { }

  loginForm: FormGroup;
  submitted = false;
  isManager;
  isCurrentManager;


  public isLogin = (localStorage.getItem('token') != null);

  ngOnInit() {
    if (this.isLogin) {
      this.isManager = localStorage.getItem('IsManager').toLowerCase();
      this.isCurrentManager = localStorage.getItem('CurrentRole').toLowerCase();
      //const token = jwt_decode(localStorage.getItem('token'));
      ((this.isManager === 'true') && (this.isCurrentManager === 'true')) ? this.router.navigateByUrl('/dashboard-manager/dashboard') : this.router.navigateByUrl('/dashboard-user/dashboard');
    }
    this.loginForm = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }


  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return null;
    }

    this.service.login(this.loginForm).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        const token = jwt_decode(res.token);
        localStorage.setItem('ClientId', token.ProfileId);
        localStorage.setItem('IsManager', token.IsManager);
        localStorage.setItem('MyLocationId', token.MyLocationId);
        localStorage.setItem('FullName', token.FullName);
        localStorage.setItem('Icon', token.Icon);
        let IsManager = (token.IsManager.toLowerCase() === 'true');
        (IsManager) ? localStorage.setItem('CurrentRole', "true") : localStorage.setItem('CurrentRole', "false");

        this.orderService.getOrderById(0).subscribe(
          (res: any) => {
            localStorage.setItem('CurrentOrderId', (res.Id == null) ? '0' : res.Id);
          },
          err => {
            console.log(err);
            // this.toastr.error(err.error, 'Error');
          }
        );
        //window.location.reload();
        (IsManager) ? this.router.navigateByUrl('/dashboard-manager/dashboard') : this.router.navigateByUrl('/dashboard-user/dashboard');
        // true = manager, false = user
      },
      err => {
        this.toastr.error(
          '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">' + err.error + '</span>',
          "",
          {
            timeOut: 4000,
            enableHtml: true,
            closeButton: true,
            toastClass: "alert alert-danger alert-with-icon",
            positionClass: "toast-top-right"
          }
        );
      }
    );

  }
}
