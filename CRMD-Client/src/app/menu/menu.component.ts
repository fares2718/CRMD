import { Component, signal } from '@angular/core';
import { MenuItemsComponent } from './menu-items/menu-items.component';
import { NewMenuItemComponent } from './new-menu-item/new-menu-item.component';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MenuItemsComponent, NewMenuItemComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent {
  isAddingItem = signal(false);

  onStartAddingItem() {
    this.isAddingItem.set(true);
    console.log('adding');
  }

  onCloseAddItem() {
    this.isAddingItem.set(false);
  }
}
