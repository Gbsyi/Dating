import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient } from '@angular/common/http';
import { CreateProfileVm } from '../models/create-profile-vm';
import { Observable } from 'rxjs';
import { CreateProfileResultVm } from '../models/create-profile-result-vm';
import { GetProfileResult } from '../models/get-profile-result';

@Injectable({
  providedIn: 'root',
})
export class ProfileApiService {
  constructor(
    @Inject(BASE_API_URL) private readonly baseUrl: string,
    private readonly http: HttpClient
  ) {}

  getUserProfile(): Observable<GetProfileResult> {
    return this.http.get<GetProfileResult>(`${this.baseUrl}/profile`);
  }

  createProfile(vm: CreateProfileVm): Observable<CreateProfileResultVm> {
    return this.http.post<CreateProfileResultVm>(
      `${this.baseUrl}/profile/create`,
      vm
    );
  }
}
