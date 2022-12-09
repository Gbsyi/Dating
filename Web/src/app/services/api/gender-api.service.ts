import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenderVm } from '../models/gender-vm';

@Injectable({
  providedIn: 'root',
})
export class GenderApiService {
  constructor(
    @Inject(BASE_API_URL) private readonly baseUrl: string,
    private readonly http: HttpClient
  ) {}

  /**
   * Get all genders
   */
  getGenders(): Observable<Array<GenderVm>> {
    return this.http.get<Array<GenderVm>>(`${this.baseUrl}/gender/list`);
  }
}
