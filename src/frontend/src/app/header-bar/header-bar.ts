import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NavigationMenu } from './navigation-menu/navigation-menu';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header-bar',
  imports: [NavigationMenu, RouterLink],
  templateUrl: './header-bar.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderBar {}
