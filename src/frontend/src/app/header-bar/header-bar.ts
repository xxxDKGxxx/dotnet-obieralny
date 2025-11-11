import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-header-bar',
  imports: [],
  templateUrl: './header-bar.html',
  styleUrl: './header-bar.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderBar {

}
