import { Component, OnInit } from '@angular/core';
import { PairApiService } from '../../../services/api/pair-api.service';
import { BehaviorSubject, finalize } from 'rxjs';
import { NextPairVm } from '../../../services/models/next-pair-vm';
import { filterNil } from '@ngneat/elf';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-matching',
  templateUrl: './matching.component.html',
  styleUrls: ['./matching.component.less'],
})
export class MatchingComponent implements OnInit {
  private pairInternal = new BehaviorSubject<NextPairVm | null>(null);
  pair$ = this.pairInternal.pipe(filterNil());
  loading$ = new BehaviorSubject<boolean>(true);

  constructor(
    private readonly pairApiService: PairApiService,
    private readonly notificationService: NzNotificationService
  ) {}

  ngOnInit(): void {
    this.pairApiService
      .getNextPair()
      .pipe(finalize(() => this.loading$.next(false)))
      .subscribe({
        next: (pair) => this.pairInternal.next(pair),
        error: (err) => this.notificationService.error('Ошибка', err.message),
      });
  }

  getUrl(pictureId: string) {
    return `${environment.baseUrl}/picture/${pictureId}`;
  }
}
