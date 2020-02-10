import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserLayoutRoutes } from './user-layout.routing';

import {LoginComponent} from '../../pages/login/login/login.component';
import {RegisterComponent} from '../../pages/register/register.component/../register/register.component';
import {AboutUsComponent} from '../../pages/about-us/about-us.component/../about-us/about-us.component';
import {ContactUsComponent} from '../../pages/contact-us/contact-us.component/../contact-us/contact-us.component';

// import { DashboardComponent }       from '../../pages/dashboard/dashboard.component';
// import { UserComponent }            from '../../pages/user/user.component';
// import { TableComponent }           from '../../pages/table/table.component';
// import { TypographyComponent }      from '../../pages/typography/typography.component';
// import { IconsComponent }           from '../../pages/icons/icons.component';
// import { MapsComponent }            from '../../pages/maps/maps.component';
// import { NotificationsComponent }   from '../../pages/notifications/notifications.component';
// import { UpgradeComponent }         from '../../pages/upgrade/upgrade.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(UserLayoutRoutes),
    FormsModule,
    NgbModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    AboutUsComponent,
    ContactUsComponent
  ]
})

export class UserLayoutModule {}
