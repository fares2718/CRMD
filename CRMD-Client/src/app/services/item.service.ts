import { inject, Injectable, signal } from '@angular/core';
import { Item } from '../models/item.model';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  private httpClient = inject(HttpClient);
  private items = signal<Item[] | undefined>(undefined);

  allItems = this.items.asReadonly();

  fetchItems() {
    return this.httpClient
      .get<any>('http://localhost:5145/api/Item/get-items')
      .pipe(
        map((res) => {
          const resp = res?.response ?? res?.Response;
          if (resp) {
            if (resp.isError) {
              const first = resp.firstError ?? resp.FirstError;
              const msg = first?.description ?? 'API returned an error';
              throw new Error(msg);
            }
            const arr = resp.value ?? resp.Value ?? [];
            return { items: Array.isArray(arr) ? arr : [] };
          }

          if (Array.isArray(res)) return { items: res };
          if (res?.items ?? res?.Items)
            return { items: res.items ?? res.Items };

          return { items: [] };
        }),
        catchError((err) =>
          throwError(
            () =>
              new Error(
                err?.message ?? 'Something went wrong while fetching Menu',
              ),
          ),
        ),
        tap({ next: (items) => this.items.set(items.items) }),
      );
  }
}
