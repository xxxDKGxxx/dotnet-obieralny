import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { FormsModule } from '@angular/forms';
import { ApplicationRoutes } from '../../app.routes';
import { Router } from '@angular/router';

interface QuickSearchFormData {
  amount: number;
  duration: number;
}

@Component({
  selector: 'app-quick-search-form',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    FormsModule,
  ],
  templateUrl: './quick-search-form.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class QuickSearchForm {
  protected amount!: number;
  protected duration!: Date;

  private readonly router = inject(Router);

  protected redirectToFullSearch(data: QuickSearchFormData) {
    if (!data.amount || !data.duration) {
      return;
    }

    this.router.navigate(['/', ApplicationRoutes.search], {
      queryParams: data,
    });
  }
}
