import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderBar } from './header-bar/header-bar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderBar],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
