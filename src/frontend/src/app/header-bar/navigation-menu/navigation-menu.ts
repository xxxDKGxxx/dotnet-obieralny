import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApplicationRoutes } from '../../app.routes';

@Component({
  selector: 'app-navigation-menu',
  imports: [RouterLink],
  templateUrl: './navigation-menu.html',
  styleUrl: './navigation-menu.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavigationMenu {
  protected readonly searchPath = ApplicationRoutes.search;
}
