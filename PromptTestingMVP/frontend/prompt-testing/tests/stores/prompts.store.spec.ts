import { PromptsStore } from '../../src/app/prompts/prompts.store';
import { ScopeStore } from '../../src/app/scope/scope.store';
import { PromptTestingApiService } from '../../src/app/shared/services/prompt-testing-api.service';

class ApiMock extends PromptTestingApiService {
  constructor(private data: any){ super(); }
  override async getPrompts(): Promise<any[]> { return this.data; }
}

describe('PromptsStore', () => {
  it('does not load if scope incomplete', async () => {
    const scope = new ScopeStore();
    const api = new ApiMock([{ id:'1', name:'P', version:'1', dataPoint:'dp', status:'Active'}]);
    const store = new PromptsStore(api as any, scope);
    await store.load();
    expect(store.prompts().length).toBe(0);
  });

  it('loads when scope complete', async () => {
    const scope = new ScopeStore();
    scope.set({ team:'T', brokingSegment:'B', globalLineOfBusiness:'G', product:'P'});
    const api = new ApiMock([{ id:'1', name:'P', version:'1', dataPoint:'dp', status:'Active'}]);
    const store = new PromptsStore(api as any, scope);
    await store.load();
    expect(store.prompts().length).toBe(1);
    expect(store.prompts()[0].name).toBe('P');
  });
});