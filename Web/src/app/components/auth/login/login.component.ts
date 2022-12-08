import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {

  form = this.fb.group({
    username: '',
    password: ''
  })

  constructor(private readonly router: Router,
              private readonly fb: FormBuilder) { }

  ngOnInit(): void {
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}
