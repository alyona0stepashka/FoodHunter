import { Routes } from '@angular/router';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthGuard } from './guards/auth.guard';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';

import {LayoutComponent} from './components/layout/layout/layout.component';
import {LoginComponent} from './pages/login/login/login.component';
import {RegisterComponent} from './pages/register/register/register.component';
import {AboutUsComponent} from './pages/about-us/about-us.component/../about-us/about-us.component';
import {ContactUsComponent} from './pages/contact-us/contact-us.component/../contact-us/contact-us.component';

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
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'about-us', component: AboutUsComponent},
      {path: 'contact-us', component: ContactUsComponent},
    ]
  }, {
    path: 'dashboard-user', component: LayoutComponent,
    children: [
      // {path: 'login', component: LoginComponent},
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
