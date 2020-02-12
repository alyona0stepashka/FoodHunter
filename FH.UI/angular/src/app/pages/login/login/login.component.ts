import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'app/services/user.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router,
    private service: UserService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService) { }

  loginForm: FormGroup;
  submitted = false;

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      const token = jwt_decode(localStorage.getItem('token'));
      (token.IsManager) ? this.router.navigateByUrl('/control-manager/dashboard') : this.router.navigateByUrl('/control-user/dashboard');
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
        localStorage.setItem('IsManager', token.IsManager);
        localStorage.setItem('FullName', token.FullName);
        localStorage.setItem('Icon', token.Icon);
        let IsManager = (token.IsManager.toLowerCase() === 'true');
        (IsManager) ? localStorage.setItem('CurrentRole', "true") : localStorage.setItem('CurrentRole', "false");
        window.location.reload(true);
        (IsManager) ? this.router.navigateByUrl('/control-manager/dashboard') : this.router.navigateByUrl('/control-user/dashboard');
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
