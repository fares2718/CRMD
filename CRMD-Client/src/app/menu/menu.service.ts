import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { MenuItem } from '../models/menu-item.model';
import { catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private httpClient = inject(HttpClient);
  private menuItems = signal<MenuItem[] | undefined>(undefined);

  allMenuItems = this.menuItems.asReadonly();

  fetchMenuItems() {
    return this.httpClient
      .get<{
        menuItems: MenuItem[];
      }>('http://localhost:5145/api/Menu/get-menu-items')
      .pipe(
        catchError((err) =>
          throwError(
            () => new Error('Something went wrong while fetching Menu'),
          ),
        ),
        tap({
          next: (items) => this.menuItems.set(items.menuItems),
        }),
      );
  }
}
