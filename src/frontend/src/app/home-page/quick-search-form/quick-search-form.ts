import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-quick-search-form',
  imports: [MatCardModule, MatFormFieldModule, MatInputModule],
  templateUrl: './quick-search-form.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class QuickSearchForm {}
