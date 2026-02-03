import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { MenuItem } from '../../models/menu-item.model';
import { MenuService } from '../../services/menu.service';
import { CurrencyPipe, KeyValuePipe } from '@angular/common';

@Component({
  selector: 'app-menu-items',
  standalone: true,
  imports: [CurrencyPipe, KeyValuePipe],
  templateUrl: './menu-items.component.html',
  styleUrl: './menu-items.component.css',
})
export class MenuItemsComponent implements OnInit {
  isFetching = signal(false);
  error = signal('');
  private destroyRef = inject(DestroyRef);
  private menuService = inject(MenuService);
  menuItems = signal<MenuItem[] | undefined>(this.menuService.allMenuItems());
  groupedMenuItems = computed(() => {
    const items = this.menuItems();
    if (!items) return {};
    return items.reduce(
      (acc, item) => {
        if (!acc[item.category]) {
          acc[item.category] = [];
        }
        acc[item.category].push(item);
        return acc;
      },
      {} as Record<string, MenuItem[]>,
    );
  });
  ngOnInit() {
    this.isFetching.set(true);
    const subscription = this.menuService.fetchMenuItems().subscribe({
      next: (menuData) => {
        this.menuItems.set(menuData.response.value);
      },
      error: (err: Error) => {
        this.error.set(err.message);
      },
      complete: () => {
        this.isFetching.set(false);
      },
    });

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });
  }

  onDeleteItem(id: number) {
    this.menuService.removeMenuItem(id).subscribe({
      next: (res) => console.log(res),
    });
  }

  onEditItem() {
    console.log('edit');
  }
}
