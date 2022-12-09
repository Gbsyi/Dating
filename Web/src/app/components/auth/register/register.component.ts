import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { BehaviorSubject, finalize, map } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent implements OnInit {
  form = this.fb.nonNullable.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  formValid$ = this.form.statusChanges.pipe(
    map((status) => status === 'VALID')
  );
  registerPending$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly fb: FormBuilder
  ) {}

  ngOnInit(): void {}

  goToLogin() {
    void this.router.navigate(['/login']);
  }

  register() {
    this.registerPending$.next(true);
    if (this.form.valid) {
      const value = this.form.getRawValue();
      this.authService
        .register(value.username, value.password)
        .pipe(finalize(() => this.registerPending$.next(false)))
        .subscribe(() => {
          void this.router.navigateByUrl('/profile-create');
        });
    }
  }

  hasError(control: FormControl, error: string): boolean {
    return control.hasError(error);
  }
}
