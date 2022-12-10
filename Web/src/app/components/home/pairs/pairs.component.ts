import { Component, OnInit } from '@angular/core';
import { PairApiService } from '../../../services/api/pair-api.service';
import { BehaviorSubject, finalize } from 'rxjs';
import { PairVm } from '../../../services/models/pair-vm';
import { environment } from '../../../../environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pairs',
  templateUrl: './pairs.component.html',
  styleUrls: ['./pairs.component.less'],
})
export class PairsComponent implements OnInit {
  pairs$ = new BehaviorSubject<PairVm[]>([]);

  pairsLoading$ = new BehaviorSubject<boolean>(true);

  constructor(
    private readonly pairApiService: PairApiService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.pairApiService
      .getUserPairs()
      .pipe(finalize(() => this.pairsLoading$.next(false)))
      .subscribe({
        next: (pairs) => {
          this.pairs$.next(pairs);
        },
      });
  }

  getPicUrl(pictureId: string) {
    return `${environment.baseUrl}/picture/${pictureId}`;
  }

  goToChat(chatId: string) {
    this.router.navigate(['/pairs', chatId]);
  }
}
