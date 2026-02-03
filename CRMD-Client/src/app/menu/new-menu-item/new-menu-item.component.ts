import {
  ChangeDetectionStrategy,
  Component,
  computed,
  DestroyRef,
  EventEmitter,
  inject,
  input,
  OnInit,
  output,
  signal,
} from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ItemService } from '../../services/item.service';
import { Item } from '../../models/item.model';
import { Category } from '../../models/category.model';
import { MenuService } from '../../services/menu.service';

@Component({
  selector: 'app-new-menu-item',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './new-menu-item.component.html',
  styleUrl: './new-menu-item.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewMenuItemComponent implements OnInit {
  categories = input.required<Category[] | undefined>();
  items = signal<Item[] | undefined>(undefined);
  private menuService = inject(MenuService);
  private itemService = inject(ItemService);
  private destroyRef = inject(DestroyRef);
  close = output<void>();
  form = new FormGroup({
    name: new FormControl<string>('', {
      validators: [Validators.required],
    }),
    categoryId: new FormControl<number>(0, {
      validators: [Validators.required],
    }),
    recipe: new FormArray([
      new FormGroup({
        ingredient: new FormControl(),
        quantity: new FormControl(),
      }),
    ]),
    price: new FormControl(),
  });

  ngOnInit() {
    const subscription = this.itemService.fetchItems().subscribe({
      next: (itemsData) => this.items.set(itemsData.items),
    });
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });
  }

  onSubmit() {
    const name = this.form.controls.name.value;
    const price = Number(this.form.controls.price.value);
    const recipeItems = this.form.controls.recipe.controls.map((control) => {
      const ingredientId = Number(control.controls.ingredient.value);
      const quantity = Number(control.controls.quantity.value);
      const item = this.items()?.find((i) => i.itemId === ingredientId);
      return {
        ingredientId: ingredientId,
        quantity: quantity,
      };
    });
    const categoryId = this.form.controls.categoryId.value;
    const payload = {
      name: name,
      recipe: {
        items: recipeItems,
      },
      price: price,
      categoryId: categoryId,
    };

    const subscription = this.menuService.addMenuItem(payload).subscribe({
      next: (response) => console.log(response),
    });
    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });
    this.close.emit();
  }

  onAddItem() {
    this.form.controls.recipe.push(
      new FormGroup({
        ingredient: new FormControl(),
        quantity: new FormControl(),
      }),
    );
  }

  onClose() {
    this.close.emit();
  }
}
