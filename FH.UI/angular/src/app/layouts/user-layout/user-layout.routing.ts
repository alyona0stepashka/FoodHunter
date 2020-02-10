import { Routes } from '@angular/router';

import {LoginComponent} from '../../pages/login/login/login.component';
import {RegisterComponent} from '../../pages/register/register.component/../register/register.component';
import {AboutUsComponent} from '../../pages/about-us/about-us.component/../about-us/about-us.component';
import {ContactUsComponent} from '../../pages/contact-us/contact-us.component/../contact-us/contact-us.component';

export const UserLayoutRoutes: Routes = [
    { path: 'login',      component: LoginComponent },
    { path: 'register',           component: RegisterComponent },
    { path: 'about-us',          component: AboutUsComponent },
    { path: 'contact-us',     component: ContactUsComponent }
];
