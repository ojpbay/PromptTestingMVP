import { Component, inject } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ScopeStore } from './scope.store';
@Component({
  standalone: true,
  selector: 'app-scope-selector',
  imports:[ReactiveFormsModule],
  templateUrl: './scope-selector.component.html',
  styleUrls: ['./scope-selector.component.scss']
})
export class ScopeSelectorComponent {
  private fb = inject(NonNullableFormBuilder);
  form: FormGroup = this.fb.group({
    team: [''],
    brokingSegment: [''],
    globalLineOfBusiness: [''],
    product: ['']
  });
  constructor(public store: ScopeStore) {
  this.form.valueChanges.subscribe(v => this.store.set(v as Partial<{team:string;brokingSegment:string;globalLineOfBusiness:string;product:string}>));
    // initialize form with existing selection
    const sel = this.store.selection();
    this.form.patchValue(sel, { emitEvent:false });
  }
  apply(){ /* trigger prompt reload; consumer listens to store */ }
}