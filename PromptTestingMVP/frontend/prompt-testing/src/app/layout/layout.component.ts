import { Component } from '@angular/core';
import { ContextEditorComponent } from '../context/context-editor.component';
import { PromptListComponent } from '../prompts/prompt-list.component';
import { ScopeSelectorComponent } from '../scope/scope-selector.component';
import { ResultsCardComponent } from '../testing/results-card.component';
import { TestExecutionComponent } from '../testing/test-execution.component';
@Component({
  standalone: true,
  selector: 'app-layout',
  imports:[ScopeSelectorComponent, PromptListComponent, ContextEditorComponent, TestExecutionComponent, ResultsCardComponent],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {}