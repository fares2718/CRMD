import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { MenuItem } from '../models/menu-item.model';
import { catchError, map, tap, throwError } from 'rxjs';
import { Category } from '../models/category.model';
@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private httpClient = inject(HttpClient);
  private menuItems = signal<MenuItem[]>([]);
  private menuCategories = signal<Category[]>([]);

  allMenuItems = this.menuItems.asReadonly();
  allMenuCategories = this.menuCategories.asReadonly();

  fetchMenuItems() {
    return this.httpClient
      .get<{
        response: {
          isError: boolean;
          value: MenuItem[];
        };
      }>('http://localhost:5145/api/Menu/get-menu-items')
      .pipe(
        map((data) => {
          console.log(data.response.value);
          return data.response;
        }),
        catchError((err) =>
          throwError(
            () => new Error('Something went wrong while fetching Menu'),
          ),
        ),
        tap({
          next: (items) => {
            this.menuItems.set(items.value);
          },
        }),
      );
  }

  fetchMenuCategories() {
    return this.httpClient
      .get<{
        response: {
          isError: boolean;
          value: Category[];
        };
      }>('http://localhost:5145/api/Menu/get-menu-categories')
      .pipe(
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
            this.menuCategories.set(categories.response.value),
        }),
      );
  }

  addMenuItem(newMenuItem: any) {
    const prevMenu = this.menuItems();
    this.menuItems.set([...prevMenu, newMenuItem]);
    return this.httpClient
      .post('http://localhost:5145/api/Menu/add-menu-item', newMenuItem, {
        headers: new HttpHeaders('application/json'),
      })
      .pipe(
        catchError((err) => {
          this.menuItems.set(prevMenu);
          return throwError(() => {
            throw new Error(err);
          });
        }),
      );
  }

  removeMenuItem(id: number) {
    const prevMenu = this.menuItems();
    this.menuItems.update((Menu) => {
      return Menu.filter((i) => i.menuItemId !== id);
    });
    return this.httpClient
      .delete(`http://localhost:5145/api/Menu/delete-menu-item/${id}`)
      .pipe(
        catchError((err) => {
          this.menuItems.set(prevMenu);
          return throwError(() => {
            throw new Error(err);
          });
        }),
      );
  }

  updateMenuItemRecipe() {}
}
