import { Component } from "@angular/core";
import { firstValueFrom } from 'rxjs';
import { ContextEditorComponent } from "../context/context-editor.component";
import { PromptListComponent } from "../prompts/prompt-list.component";
import { PromptsStore } from "../prompts/prompts.store";
import { ScopeSelectorComponent } from "../scope/scope-selector.component";
import { Prompt, ScopeSelection } from "../shared/models/prompt.models";
import { PromptTestingApiService } from "../shared/services/prompt-testing-api.service";
import { ResultsCardComponent } from "../testing/results-card.component";
import { TestExecutionComponent } from "../testing/test-execution.component";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatChipsModule } from "@angular/material/chips";
import { MatButtonModule } from "@angular/material/button";
@Component({
  standalone: true,
  selector: "app-layout",
  imports: [
    ScopeSelectorComponent,
    PromptListComponent,
    ContextEditorComponent,
    TestExecutionComponent,
    ResultsCardComponent,
    MatToolbarModule,
    MatChipsModule,
    MatButtonModule,
  ],
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.scss"],
})
export class LayoutComponent {
  selectedPrompt?: Prompt;
  contextText = "";
  loadingContext = false;
  scope?: ScopeSelection;
  lastAccuracy?: number;
  lastCompletedAt?: Date;

  constructor(
    private readonly api: PromptTestingApiService,
    public readonly promptsStore: PromptsStore
  ) {}

  async onPromptSelected(p: Prompt) {
    this.selectedPrompt = p;
    this.loadingContext = true;
    try {
      const ctx = await firstValueFrom(this.api.getPromptContext(p.id));
      this.contextText = ctx.content;      
    } finally {
      this.loadingContext = false;
    }
  }

  onContextChanged(v: string) {
    this.contextText = v;
  }

  onTestCompleted(e: { accuracy?: number; finishedAt: Date }) {
    this.lastAccuracy = e.accuracy;
    this.lastCompletedAt = e.finishedAt;
  }

  async onScopeApplied(scope: ScopeSelection) {
    this.scope = scope;
    // Reset any currently selected prompt when scope changes
    this.selectedPrompt = undefined;
    this.contextText = "";
    this.lastAccuracy = undefined;
    this.lastCompletedAt = undefined;
    await this.promptsStore.load();
  }

  backToPrompts() {
    this.selectedPrompt = undefined;
    // keep context/results? Requirement implies returning to only prompt list view;
    // we'll retain last results internally but they are hidden without a selected prompt.
  }
}
