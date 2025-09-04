import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
@Component({
  standalone: true,
  selector: 'app-context-editor',
  imports:[ReactiveFormsModule],
  templateUrl: './context-editor.component.html',
  styleUrls: ['./context-editor.component.scss']
})
export class ContextEditorComponent {
  text = new FormControl<string>('', { nonNullable: true });
}