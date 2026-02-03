import { Component, DestroyRef, inject, signal } from '@angular/core';
import { MenuItemsComponent } from './menu-items/menu-items.component';
import { NewMenuItemComponent } from './new-menu-item/new-menu-item.component';
import { MenuService } from '../services/menu.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MenuItemsComponent, NewMenuItemComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent {
  isAddingItem = signal(false);
  private menuService = inject(MenuService);
  private destroyRef = inject(DestroyRef);
  menuCategories = signal<Category[] | undefined>(undefined);

  onStartAddingItem() {
    this.isAddingItem.set(true);
    const subscription = this.menuService.fetchMenuCategories().subscribe({
      next: (categories) => this.menuCategories.set(categories.menuCategories),
    });
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });
  }

  onCloseAddItem() {
    this.isAddingItem.set(false);
  }
}
