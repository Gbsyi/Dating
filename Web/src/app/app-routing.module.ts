import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MatchingComponent} from "./components/home/matching/matching.component";
import {HomeTemplateComponent} from "./components/home/home-template/home-template.component";
import {LoginComponent} from "./components/auth/login/login.component";
import {RegisterComponent} from "./components/auth/register/register.component";
import { ProfileCreateComponent } from './components/auth/profile-create/profile-create.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    component: HomeTemplateComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: MatchingComponent,
      }
    ]
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent
  },
  {
    path: 'register',
    pathMatch: 'full',
    component: RegisterComponent
  },
  {
    path: 'profile-create',
    pathMatch: 'full',
    component: ProfileCreateComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
