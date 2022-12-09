import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { BehaviorSubject, finalize, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit {
  form = this.fb.nonNullable.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
  });

  formValid$ = this.form.statusChanges.pipe(
    map((status) => status === 'VALID')
  );

  loginPending$ = new BehaviorSubject<boolean>(false);

  constructor(
    private readonly router: Router,
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {}

  goToRegister() {
    this.router.navigate(['/register']);
  }

  login() {
    if (this.form.valid) {
      this.loginPending$.next(true);
      const value = this.form.getRawValue();
      this.authService
        .login(value.username, value.password)
        .pipe(finalize(() => this.loginPending$.next(false)))
        .subscribe(() => {
          this.router.navigateByUrl('/');
        });
    }
  }

  hasError(control: FormControl, error: string) {
    return control.hasError(error);
  }
}
