import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BehaviorSubject, map, switchMap } from 'rxjs';
import { ProfileService } from '../../../services/profile.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Router } from '@angular/router';
import { GenderApiService } from '../../../services/api/gender-api.service';
import { GenderVm } from '../../../services/models/gender-vm';
import { NzSelectOptionInterface } from 'ng-zorro-antd/select';

@Component({
  selector: 'app-profile-create',
  templateUrl: './profile-create.component.html',
  styleUrls: ['./profile-create.component.less'],
})
export class ProfileCreateComponent implements OnInit {
  form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    age: [0, Validators.required],
    description: ['', Validators.required],
    genderId: ['', Validators.required],
    preferredGenders: [[] as string[], Validators.required],
  });

  formValid$ = this.form.statusChanges.pipe(
    map((status) => status === 'VALID')
  );

  profileCreatePending$ = new BehaviorSubject<boolean>(false);

  private genders = new BehaviorSubject<GenderVm[]>([]);

  gendersSelectOptions$ = this.genders.pipe(
    map((genders): NzSelectOptionInterface[] => {
      return genders.map((gender) => {
        return {
          value: gender.id,
          label: gender.name,
        };
      });
    })
  );

  constructor(
    private readonly fb: FormBuilder,
    private readonly profileService: ProfileService,
    private readonly notificationService: NzNotificationService,
    private readonly router: Router,
    private readonly genderApiService: GenderApiService
  ) {}

  ngOnInit(): void {
    this.genderApiService.getGenders().subscribe((genders) => {
      this.genders.next(genders);
    });
  }

  hasError(control: FormControl, error: string): boolean {
    return control.hasError(error);
  }

  submit() {
    if (this.form.valid) {
      const value = this.form.getRawValue();
      this.profileService
        .createProfile(value)
        .pipe(switchMap(() => this.profileService.loadProfile()))
        .subscribe((isLoaded) => {
          if (isLoaded) {
            this.router.navigateByUrl('/');
          } else {
            this.notificationService.error(
              'Ошибка',
              'Не получилось создать профиль'
            );
          }
        });
    }
  }
}
