import { Component, EventEmitter, output } from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-new-menu-item',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './new-menu-item.component.html',
  styleUrl: './new-menu-item.component.css',
})
export class NewMenuItemComponent {
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

  onSubmit() {
    console.log('works!');
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
