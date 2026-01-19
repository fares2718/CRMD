import { Component } from '@angular/core';
import { MenuItemsComponent } from './menu-items/menu-items.component';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MenuItemsComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent {}
