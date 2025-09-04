import { signal } from '@angular/core';
import { ScopeStore } from '../scope/scope.store';
import { Prompt } from '../shared/models/prompt.models';
import { PromptTestingApiService } from '../shared/services/prompt-testing-api.service';

export class PromptsStore {
  prompts = signal<Prompt[]>([]);
  loading = signal(false);
  constructor(private api: PromptTestingApiService, private scopeStore: ScopeStore) {}
  async load() {
    if(!this.scopeStore.isComplete()) return;
    this.loading.set(true);
    try { this.prompts.set(await this.api.getPrompts(this.scopeStore.selection())); }
    finally { this.loading.set(false); }
  }
}