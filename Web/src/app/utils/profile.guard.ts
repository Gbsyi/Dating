import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { Profiler } from 'inspector';
import { ProfileService } from '../services/profile.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileGuard implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly profileService: ProfileService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this.profileService.isLoaded) {
      if (this.profileService.isCreated) {
        return true;
      }

      this.router.navigateByUrl('/profile-create');

      return false;
    }

    return this.profileService.loadProfile();
  }
}
