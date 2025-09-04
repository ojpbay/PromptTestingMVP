import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
@Component({
  standalone: true,
  selector: 'app-context-editor',
  imports:[ReactiveFormsModule],
  templateUrl: './context-editor.component.html',
  styleUrls: ['./context-editor.component.scss']
})
export class ContextEditorComponent implements OnChanges {
  /** Current context text passed from parent */
  @Input() value = '';
  /** Emits whenever the user edits the context */
  @Output() valueChange = new EventEmitter<string>();

  text = new FormControl<string>('', { nonNullable: true });

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['value'] && this.value !== this.text.value) {
      this.text.setValue(this.value, { emitEvent: false });
    }
  }

  constructor(){
  this.text.valueChanges.subscribe((v: string) => this.valueChange.emit(v));
  }
}