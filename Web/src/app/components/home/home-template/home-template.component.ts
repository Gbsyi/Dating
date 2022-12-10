import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-template',
  templateUrl: './home-template.component.html',
  styleUrls: ['./home-template.component.less'],
})
export class HomeTemplateComponent implements OnInit {
  constructor(private readonly router: Router) {}

  ngOnInit(): void {}

  logout() {
    localStorage.removeItem('auth');
    this.router.navigate(['/login']);
  }
}
