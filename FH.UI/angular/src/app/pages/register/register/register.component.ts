import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'app/services/user.service';
import { DatePipe } from '@angular/common';
import { StaticService } from 'app/services/static.service';
import { StaticBase } from 'app/models/static-base.model';

@Component({
  selector: 'app-register',
  moduleId: module.id,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private router: Router,
    private service: UserService,
    public datepipe: DatePipe,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private staticService: StaticService) { }

  registerForm: FormGroup;
  submitted = false;
  UploadFile: File = null;
  imageUrl = './assets/img/upload-photo.jpg';
  public sexes: StaticBase[] = new Array();

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      FirstName: ['', [Validators.required]],
      LastName: ['', [Validators.required]],
      Phone: ['', [Validators.required/*, Validators.pattern("^(375-)[0-9]{2}(-)[0-9]{3}(-)[0-9]{2}(-)[0-9]{2}$")*/]],
      Sex: ['', [Validators.required]],
      DateBirth: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]],
      Photo: [null, [Validators.required]],
      Role: ['', [Validators.required]]
    });
    this.staticService.getSexes().subscribe(
      res => {
        this.sexes = res as StaticBase[];
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return null;
    }

    this.service.register(this.registerForm, this.UploadFile).subscribe(
      (res: any) => {
        // this.userId = res as string;
        // this.resetForm();
        this.toastr.success(
          '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Welcome to <b>Food Hunter!</b></span>',
          "",
          {
            timeOut: 4000,
            closeButton: true,
            enableHtml: true,
            toastClass: "alert alert-success alert-with-icon"
          }
        );
        this.router.navigate(['/welcome/login']);
        // this.router.navigate(['/auth/first/' + this.userId]); 
        // this.router.navigate(['/auth/login/' + this.userId]);
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


}
