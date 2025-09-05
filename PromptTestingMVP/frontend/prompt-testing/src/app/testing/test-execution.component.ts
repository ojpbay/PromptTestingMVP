import { Component, EventEmitter, Input, Output } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ScopeSelection } from '../shared/models/prompt.models';
import { PromptTestingApiService } from '../shared/services/prompt-testing-api.service';
import { TestRunStore } from './test-run.store';
import { MatButtonModule } from "@angular/material/button";
import { MatChipsModule } from "@angular/material/chips";
@Component({
  standalone: true,
  selector: "app-test-execution",
  templateUrl: "./test-execution.component.html",
  styleUrls: ["./test-execution.component.scss"],
  imports: [MatButtonModule, MatChipsModule],
})
export class TestExecutionComponent {
  @Input() promptId?: string;
  @Input() context = "";
  @Input() scope?: ScopeSelection;
  @Output() completed = new EventEmitter<{
    accuracy?: number;
    finishedAt: Date;
  }>();
  store = new TestRunStore();
  currentTestId?: string;

  constructor(private readonly api: PromptTestingApiService) {}

  async run() {
    if (!this.promptId || !this.scope) return;
    if (!this.scope.team) return; // minimal guard
    this.store.status.set('running');
    const res = await firstValueFrom(this.api.executeTest(this.promptId, this.context, this.scope));
    this.currentTestId = res.testId;
    await this.store.startPolling(async (id) => {
      const r = await firstValueFrom(this.api.getTestResult(id));
      return { status: r.status as any, accuracy: r.accuracy };
    }, res.testId);
    if (this.store.status() === 'completed') {
      this.completed.emit({ accuracy: this.store.accuracy(), finishedAt: new Date() });
    }
  }

  get canRun(): boolean {
    if (this.store.status() === "running") return false;
    if (!this.promptId) return false;
    if (!this.context || this.context.trim().length === 0) return false;
    return true;
  }
}