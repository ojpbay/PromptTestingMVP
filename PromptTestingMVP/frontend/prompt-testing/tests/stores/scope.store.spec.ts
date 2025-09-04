import { ScopeStore } from '../../src/app/scope/scope.store';

describe('ScopeStore', () => {
  it('isComplete false when any field missing', () => {
    const store = new ScopeStore();
    expect(store.isComplete()).toBe(false);
    store.set({ team:'A', brokingSegment:'', globalLineOfBusiness:'', product:'' });
    expect(store.isComplete()).toBe(false);
  });

  it('isComplete true when all fields set', () => {
    const store = new ScopeStore();
    store.set({ team:'T', brokingSegment:'B', globalLineOfBusiness:'G', product:'P'});
    expect(store.isComplete()).toBe(true);
  });
});