import { Routes } from '@angular/router';
import { TestComponent } from './test/test.component';
import { HomePage } from './home-page/home-page';

export const routes: Routes = [
  { path: 'test', component: TestComponent },
  { path: '', component: HomePage, pathMatch: 'full' },
];
