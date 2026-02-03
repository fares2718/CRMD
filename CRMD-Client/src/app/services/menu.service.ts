import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { MenuItem } from '../models/menu-item.model';
import { catchError, map, tap, throwError } from 'rxjs';
import { Category } from '../models/category.model';
@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private httpClient = inject(HttpClient);
  private menuItems = signal<MenuItem[] | undefined>(undefined);
  private menuCategories = signal<Category[] | undefined>(undefined);

  allMenuItems = this.menuItems.asReadonly();
  allMenuCategories = this.menuCategories.asReadonly();

  fetchMenuItems() {
    return this.httpClient
      .get<any>('http://localhost:5145/api/Menu/get-menu-items')
      .pipe(
        map((res) => {
          // Prefer explicit server shape: { response: { isError, value: MenuItem[] } }
          const resp = res?.response ?? res?.Response;
          if (resp) {
            if (resp.isError) {
              const first = resp.firstError ?? resp.FirstError;
              const msg = first?.description ?? 'API returned an error';
              throw new Error(msg);
            }
            const arr = resp.value ?? resp.Value ?? [];
            return { menuItems: Array.isArray(arr) ? arr : [] };
          }

          // Fallback shapes
          if (Array.isArray(res)) return { menuItems: res };
          if (res?.menuItems ?? res?.MenuItems)
            return { menuItems: res.menuItems ?? res.MenuItems };

          return { menuItems: [] };
        }),
        catchError((err) =>
          throwError(
            () =>
              new Error(
                err?.message ?? 'Something went wrong while fetching Menu',
              ),
          ),
        ),
        tap({ next: (items) => this.menuItems.set(items.menuItems) }),
      );
  }

  fetchMenuCategories() {
    return this.httpClient
      .get<any>('http://localhost:5145/api/Menu/get-menu-categories')
      .pipe(
        map((res) => {
          // Prefer explicit server shape: { response: { isError, value: MenuItem[] } }
          const resp = res?.response ?? res?.Response;
          if (resp) {
            if (resp.isError) {
              const first = resp.firstError ?? resp.FirstError;
              const msg = first?.description ?? 'API returned an error';
              throw new Error(msg);
            }
            const arr = resp.value ?? resp.Value ?? [];
            return { menuCategories: Array.isArray(arr) ? arr : [] };
          }

          // Fallback shapes
          if (Array.isArray(res)) return { menuCategories: res };
          if (res?.menuCategories ?? res?.MenuCategories)
            return { menuCategories: res.menuCategories ?? res.MenuCategories };

          return { menuCategories: [] };
        }),
        catchError((err) =>
          throwError(
            () =>
              new Error(
                err?.message ??
                  'Something went wrong while fetching Menu categories',
              ),
          ),
        ),
        tap({
          next: (categories) =>
            this.menuCategories.set(categories.menuCategories),
        }),
      );
  }

  addMenuItem(newMenuItem: any) {
    return this.httpClient.post<any>(
      'http://localhost:5145/api/Menu/add-menu-item',
      newMenuItem,
    );
  }
}
