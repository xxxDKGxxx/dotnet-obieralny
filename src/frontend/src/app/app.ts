import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

// intentionally unused variable to verify eslint detects errors
const unusedVariableForLintTest = 42;

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
