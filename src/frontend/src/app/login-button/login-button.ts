import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login-button',
  imports: [MatButtonModule, MatIconModule],
  templateUrl: './login-button.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginButton {
  protected onLoginClick() {
    console.log('Login button clicked');
    // Login logic will be implemented later
  }
}
