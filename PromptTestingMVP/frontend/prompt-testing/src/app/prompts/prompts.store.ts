import { Injectable, signal } from "@angular/core";
import { ScopeStore } from "../scope/scope.store";
import { Prompt } from "../shared/models/prompt.models";
import { PromptTestingApiService } from "../shared/services/prompt-testing-api.service";

@Injectable({ providedIn: "root" })
export class PromptsStore {
  prompts = signal<Prompt[]>([]);
  loading = signal(false);
  error = signal<string | undefined>(undefined);
  constructor(
    private readonly api: PromptTestingApiService,
    private readonly scopeStore: ScopeStore
  ) {}
  async load() {
    if (!this.scopeStore.isComplete()) return;
    this.loading.set(true);
    this.error.set(undefined);
    try {
      const data = await this.api.getPrompts(this.scopeStore.selection());
      this.prompts.set(data);
    } catch (e: any) {
      this.prompts.set([]);
      this.error.set(e?.message || "Failed to load prompts");
    } finally {
      this.loading.set(false);
    }
  }
}
