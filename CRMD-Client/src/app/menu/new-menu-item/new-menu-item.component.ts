import {
  Component,
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
} from '@angular/forms';
import { ItemService } from '../../Services/item.service';
import { Item } from '../../models/item.model';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-new-menu-item',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './new-menu-item.component.html',
  styleUrl: './new-menu-item.component.css',
})
export class NewMenuItemComponent implements OnInit {
  categories=input.required<Category[]|undefined>();
  items = signal<Item[] | undefined>(undefined);
  private itemService = inject(ItemService);
  private destroyRef = inject(DestroyRef);
  close = output<void>();
  form = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(),
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

  onSubmit() {}

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
