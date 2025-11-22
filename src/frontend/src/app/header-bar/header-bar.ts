import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LoginButton } from '../login-button/login-button';

@Component({
  selector: 'app-header-bar',
  standalone: true,
  imports: [LoginButton],
  templateUrl: './header-bar.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderBar {}
