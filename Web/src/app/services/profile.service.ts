import { Injectable } from '@angular/core';
import { ProfileVm } from './models/profile-vm';
import { ProfileApiService } from './api/profile-api.service';
import { map, Observable } from 'rxjs';
import { CreateProfileVm } from './models/create-profile-vm';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  profile!: ProfileVm;

  constructor(private readonly profileApiService: ProfileApiService) {}

  isLoaded = false;
  isCreated = false;

  loadProfile(): Observable<boolean> {
    return this.profileApiService.getUserProfile().pipe(
      map((res) => {
        if (res.isCreated) {
          this.profile = res.profile!;
          this.isCreated = true;
        }

        this.isLoaded = true;
        return res.isCreated;
      })
    );
  }

  createProfile(vm: CreateProfileVm): Observable<string> {
    return this.profileApiService.createProfile(vm).pipe(
      map((res) => {
        return res.profileId;
      })
    );
  }
}
