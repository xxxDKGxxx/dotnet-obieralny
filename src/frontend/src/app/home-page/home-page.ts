import { ChangeDetectionStrategy, Component } from '@angular/core';
import { QuickSearchForm } from './quick-search-form/quick-search-form';

@Component({
  selector: 'app-home-page',
  imports: [QuickSearchForm],
  templateUrl: './home-page.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomePage {}
