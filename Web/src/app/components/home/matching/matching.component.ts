import { Component, OnInit } from '@angular/core';
import { PairApiService } from '../../../services/api/pair-api.service';
import { BehaviorSubject, finalize, switchMap } from 'rxjs';
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
  pair$ = this.pairInternal.asObservable();
  loading$ = new BehaviorSubject<boolean>(true);

  constructor(
    private readonly pairApiService: PairApiService,
    private readonly notificationService: NzNotificationService
  ) {}

  ngOnInit(): void {
    this.loadNextPair();
  }

  loadNextPair() {
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

  like() {
    const pair = this.pairInternal.value;
    if (pair) {
      this.pairApiService.likeProfile(pair.userId).subscribe((result) => {
        if (result.isMutual) {
          this.notificationService.success('Пара', 'Это взаимный лайк');
          this.loadNextPair();
          // update pairs list
        } else {
          this.loadNextPair();
        }
      });
    }
  }

  dislike() {
    const pair = this.pairInternal.value;
    if (pair) {
      this.pairApiService.dislikeProfile(pair.userId).subscribe(() => {
        this.loadNextPair();
      });
    }
  }
}
