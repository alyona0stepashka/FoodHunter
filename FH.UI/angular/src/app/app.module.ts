import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToastrModule } from "ngx-toastr";
import { CommonModule } from '@angular/common';
import { SidebarModule } from './sidebar/sidebar.module';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { FixedPluginModule } from './shared/fixedplugin/fixedplugin.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';

import { DlDateTimeDateModule, DlDateTimePickerModule } from 'angular-bootstrap-datetimepicker';

import { NgxDropzoneModule } from 'ngx-dropzone';

import { GalleryModule } from '@ngx-gallery/core';
import { LightboxModule } from 'ngx-lightbox';

import { MatTabsModule } from '@angular/material/tabs';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';

import { AuthInterceptor } from './guards/auth.interceptor';
import { LayoutComponent } from './components/layout/layout/layout.component';
import { LoginComponent } from './pages/login/login/login.component';
import { RegisterComponent } from './pages/register/register/register.component';
import { AboutUsComponent } from './pages/about-us/about-us.component/../about-us/about-us.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component/../contact-us/contact-us.component';
import { ForbiddenComponent } from './pages/forbidden/403/forbidden.component';
import { LocationManagerComponent } from './pages/location-manager/location-manager/location-manager.component';
import { OopsComponent } from './pages/500/oops.component';
import { LocationPageComponent } from './pages/location-page/location-page/location-page.component';
import { MenuManagerComponent } from './pages/menu-manager/menu-manager/menu-manager.component';
import { BrowserModule } from '@angular/platform-browser';
import { NotFoundComponent } from './pages/404/not-found/not-found.component';
import { TablesManagerComponent } from './pages/tables-manager/tables-manager.component';
import { LocationSearchComponent } from './pages/location-search/location-search.component';
import { OrderManagerComponent } from './pages/order-manager/order-manager.component';
import { OrderListComponent } from './pages/order-list/order-list.component';
import { FeedbackListComponent } from './pages/feedback-list/feedback-list.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

//import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';


@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    LoginComponent,
    RegisterComponent,
    AboutUsComponent,
    ContactUsComponent,
    ForbiddenComponent,
    LocationManagerComponent,
    OopsComponent,
    NotFoundComponent,
    LocationPageComponent,
    MenuManagerComponent,
    TablesManagerComponent,
    LocationSearchComponent,
    OrderManagerComponent,
    OrderListComponent,
    FeedbackListComponent,
    DashboardComponent,
    //AdminLayoutComponent
  ],
  imports: [
    DlDateTimeDateModule,  // <--- Determines the data type of the model
    DlDateTimePickerModule,
    MatTabsModule,
    MatSlideToggleModule,
    BrowserModule,
    GalleryModule,
    NgxDropzoneModule,
    LightboxModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes, {
      useHash: true,
      onSameUrlNavigation: 'reload'
    }),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCpVhQiwAllg1RAFaxMWSpQruuGARy0Y1k'
    }),
    SidebarModule,
    NavbarModule,
    ToastrModule.forRoot(),
    FooterModule,
    FixedPluginModule,
    CommonModule,
    FormsModule,
    NgbModule
  ],
  providers: [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
