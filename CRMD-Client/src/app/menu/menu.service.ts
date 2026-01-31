import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { MenuItem } from '../models/menu-item.model';
import { catchError, map, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private httpClient = inject(HttpClient);
  private menuItems = signal<MenuItem[] | undefined>(undefined);

  allMenuItems = this.menuItems.asReadonly();

  fetchMenuItems() {
    return this.httpClient
      .get<any>('http://localhost:5145/api/Menu/get-menu-items')
      .pipe(
        map((res) => {
          console.log('fetchMenuItems raw response:', res);
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

  addMenuItem() {}
}
