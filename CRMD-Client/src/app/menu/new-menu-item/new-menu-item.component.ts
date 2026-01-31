import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-menu-item',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './new-menu-item.component.html',
  styleUrl: './new-menu-item.component.css',
})
export class NewMenuItemComponent {
  form = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(),
    price: new FormControl(),
  });

  onSubmit() {
    console.log('works!');
  }
}
