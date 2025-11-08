import { ChangeDetectionStrategy, Component } from '@angular/core';
@Component({
  selector: 'app-test',
  imports: [],
  template: `<p>Test</p>`,
  styleUrl: './test.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TestComponent {}
