import { Injectable } from "@angular/core";
import { Prompt, ScopeSelection, TestRunResult } from "../models/prompt.models";

const BASE = "/"; // adjust if API served under different base path

@Injectable({ providedIn: "root" })
export class PromptTestingApiService {
  async getPrompts(scope: ScopeSelection): Promise<Prompt[]> {
    const params = new URLSearchParams({
      team: scope.team,
      brokingSegment: scope.brokingSegment,
      globalLineOfBusiness: scope.globalLineOfBusiness,
      product: scope.product,
    });
    const r = await fetch(`${BASE}prompts?${params.toString()}`);
    if (!r.ok) throw new Error("Failed to load prompts");
    return await r.json();
  }

  async getPromptContext(
    id: string
  ): Promise<{ promptId: string; content: string }> {
    const r = await fetch(`${BASE}prompts/${id}/context`);
    if (r.status === 404) throw new Error("Prompt not found");
    if (!r.ok) throw new Error("Failed to load prompt context");
    return await r.json();
  }

  async executeTest(
    id: string,
    context: string,
    scope: ScopeSelection
  ): Promise<{ testId: string; status: string }> {
    const r = await fetch(`${BASE}prompts/${id}/test`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ context, scope }),
    });
    if (r.status === 202) return await r.json();
    if (r.status === 409) throw new Error("Test already running");
    throw new Error("Failed to start test");
  }

  async getTestResult(id: string): Promise<TestRunResult> {
    const r = await fetch(`${BASE}tests/${id}/results`);
    if (r.status === 404) throw new Error("Test not found");
    if (!r.ok) throw new Error("Failed to get test results");
    return await r.json();
  }
}
