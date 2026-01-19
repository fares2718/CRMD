import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { MenuItem } from '../models/menu-item.model';
import { MenuService } from './menu.service';
import { CurrencyPipe, KeyValuePipe } from '@angular/common';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CurrencyPipe, KeyValuePipe],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent implements OnInit {
  menuItems = signal<MenuItem[] | undefined>(undefined);
  isFetching = signal(false);
  error = signal('');
  private destroyRef = inject(DestroyRef);
  private menuService = inject(MenuService);
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
  ngOnInit(): void {
    const subsciption = this.menuService.fetchMenuItems().subscribe({
      next: (menuData) => {
        this.menuItems.set(menuData.menuItems);
      },
      error: (err: Error) => {
        this.error.set(err.message);
      },
      complete: () => {
        this.isFetching.set(false);
      },
    });

    this.destroyRef.onDestroy(() => {
      subsciption.unsubscribe();
    });
  }
}
