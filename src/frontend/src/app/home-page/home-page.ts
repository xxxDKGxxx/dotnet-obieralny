import { Component } from '@angular/core';
import { QuickSearchForm } from './quick-search-form/quick-search-form';
import { Counter } from '../counter-section/counter';

@Component({
  selector: 'app-home-page',
  imports: [QuickSearchForm, Counter],
  templateUrl: './home-page.html',
})
export class HomePage {}
