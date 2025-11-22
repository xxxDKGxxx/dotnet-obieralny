import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-login-button',
  standalone: true,
  imports: [],
  templateUrl: './login-button.html',
  styleUrl: './login-button.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginButton {}
