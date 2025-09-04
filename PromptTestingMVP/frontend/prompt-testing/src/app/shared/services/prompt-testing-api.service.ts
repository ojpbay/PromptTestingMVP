export class PromptTestingApiService {
  async getPrompts(scope: any): Promise<any[]> { return []; }
  async getPromptContext(id: string): Promise<{promptId: string, content: string}> { throw new Error('Not implemented'); }
  async executeTest(id: string, context: string, scope: any): Promise<{testId: string, status: string}> { throw new Error('Not implemented'); }
  async getTestResult(id: string): Promise<any> { throw new Error('Not implemented'); }
}