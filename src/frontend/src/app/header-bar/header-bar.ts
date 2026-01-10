import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NavigationMenu } from './navigation-menu/navigation-menu';
import { RouterLink } from '@angular/router';
import { LoginButton } from '../login-button/login-button';

@Component({
  selector: 'app-header-bar',
  imports: [NavigationMenu, RouterLink, LoginButton],
  templateUrl: './header-bar.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderBar {}
