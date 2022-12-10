import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/auth/login/login.component';
import { MatchingComponent } from './components/home/matching/matching.component';
import { HomeTemplateComponent } from './components/home/home-template/home-template.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { ru_RU } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import ru from '@angular/common/locales/ru';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { ProfileCreateComponent } from './components/auth/profile-create/profile-create.component';
import { AuthInterceptor } from './utils/auth.interceptor';
import { BASE_API_URL } from './services/api/api-config';
import { environment } from '../environments/environment';
import {
  NzNotificationModule,
  NzNotificationService,
} from 'ng-zorro-antd/notification';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { PairsComponent } from './components/home/pairs/pairs.component';
import { PairComponent } from './components/home/pair/pair.component';
import { ChatComponent } from './components/home/chat/chat.component';

registerLocaleData(ru);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MatchingComponent,
    HomeTemplateComponent,
    RegisterComponent,
    ProfileCreateComponent,
    PairsComponent,
    PairComponent,
    ChatComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NzFormModule,
    NzInputModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzNotificationModule,
    NzSelectModule,
    NzMenuModule,
    NzSpinModule,
    NzIconModule,
  ],
  providers: [
    {
      provide: NZ_I18N,
      useValue: ru_RU,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: BASE_API_URL,
      useValue: environment.baseUrl,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
