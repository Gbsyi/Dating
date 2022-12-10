import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatchingComponent } from './components/home/matching/matching.component';
import { HomeTemplateComponent } from './components/home/home-template/home-template.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ProfileCreateComponent } from './components/auth/profile-create/profile-create.component';
import { AuthGuard } from './utils/auth.guard';
import { ProfileGuard } from './utils/profile.guard';
import { PairsComponent } from './components/home/pairs/pairs.component';
import { PairComponent } from './components/home/pair/pair.component';
import { ChatComponent } from './components/home/chat/chat.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    component: HomeTemplateComponent,
    canActivate: [AuthGuard, ProfileGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'matches',
      },
      {
        path: 'matches',
        pathMatch: 'full',
        component: MatchingComponent,
        canActivate: [AuthGuard, ProfileGuard],
      },
      {
        path: 'pairs',
        pathMatch: 'full',
        component: PairsComponent,
        canActivate: [AuthGuard, ProfileGuard],
      },
      {
        path: 'pairs/:chatId',
        pathMatch: 'full',
        component: ChatComponent,
        canActivate: [AuthGuard, ProfileGuard],
      },
    ],
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'register',
    pathMatch: 'full',
    component: RegisterComponent,
  },
  {
    path: 'profile-create',
    pathMatch: 'full',
    component: ProfileCreateComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
