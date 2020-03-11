import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
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
import { TablesManagerComponent } from './pages/tables-manager/tables-manager.component';
import { LocationSearchComponent } from './pages/location-search/location-search.component';
import { OrderManagerComponent } from './pages/order-manager/order-manager.component';
import { OrderListComponent } from './pages/order-list/order-list.component';
import { FeedbackListComponent } from './pages/feedback-list/feedback-list.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

export const AppRoutes: Routes = [
  //   {
  //     path: '',
  //     redirectTo: 'dashboard',
  //     pathMatch: 'full',
  //     //canActivate: [AuthGuard]
  //   }, {
  //     path: '',
  //     component: AdminLayoutComponent,
  //     //canActivate: [AuthGuard],
  //     children: [
  //         {
  //       path: '',
  //       loadChildren: './layouts/admin-layout/admin-layout.module#AdminLayoutModule'
  // }]},{
  //     path: 'welcome',
  //     component: UserLayoutComponent,
  //     children: [
  //         {
  //       path: '',
  //       loadChildren: './layouts/user-layout/user-layout.module#UserLayoutModule'
  //   }]},{
  {
    path: 'welcome', component: LayoutComponent,
    children: [
      { path: 'forbidden', component: ForbiddenComponent },
      { path: 'oops', component: OopsComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'about-us', component: AboutUsComponent },
      { path: 'contact-us', component: ContactUsComponent },
    ]
  }, {
    path: 'dashboard-manager', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'location', component: LocationManagerComponent, canActivate: [AuthGuard] },
      { path: 'location/:id', component: LocationPageComponent, canActivate: [AuthGuard] },
      { path: 'menu/:id', component: MenuManagerComponent, canActivate: [AuthGuard] },
      { path: 'table/:id', component: TablesManagerComponent, canActivate: [AuthGuard] },
      { path: 'search', component: LocationSearchComponent, canActivate: [AuthGuard] },
      { path: 'orders', component: OrderListComponent, canActivate: [AuthGuard] },
      { path: 'order/:id', component: OrderManagerComponent, canActivate: [AuthGuard] },
      { path: 'feedbacks', component: FeedbackListComponent, canActivate: [AuthGuard] },
      // {path: 'register', component: RegisterComponent},
      // {path: 'about-us', component: AboutUsComponent},
      // {path: 'contact-us', component: ContactUsComponent},
    ]
  }, {
    path: 'dashboard-user', component: LayoutComponent, canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'location/:id', component: LocationPageComponent, canActivate: [AuthGuard] },
      { path: 'menu/:id', component: MenuManagerComponent, canActivate: [AuthGuard] },
      { path: 'table/:id', component: TablesManagerComponent, canActivate: [AuthGuard] },
      { path: 'orders', component: OrderListComponent, canActivate: [AuthGuard] },
      { path: 'order/:id', component: OrderManagerComponent, canActivate: [AuthGuard] },
      { path: 'feedbacks', component: FeedbackListComponent, canActivate: [AuthGuard] },
      // {path: 'register', component: RegisterComponent},
      // {path: 'about-us', component: AboutUsComponent},
      // {path: 'contact-us', component: ContactUsComponent},
    ]
  },
  {
    path: '**',
    redirectTo: 'welcome/login'
  }
]
