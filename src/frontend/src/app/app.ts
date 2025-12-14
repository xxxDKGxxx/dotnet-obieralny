import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderBar } from './header-bar/header-bar';
import { Hero } from './hero/hero';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderBar, Hero],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
