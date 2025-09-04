import { TestRunResultDto, TestRunStore } from '../../src/app/testing/test-run.store';

describe('TestRunStore', () => {
  it('transitions to completed', async () => {
    const store = new TestRunStore();
    let calls = 0;
    const getResult = async (): Promise<TestRunResultDto> => {
      calls++;
      if (calls < 3) return { status:'Running' } as any;
      return { status:'Completed', accuracy: 87 };
    };
    await store.startPolling(getResult, 'id1');
    expect(store.status()).toBe('completed');
    expect(store.accuracy()).toBe(87);
  });

  it('transitions to failed', async () => {
    const store = new TestRunStore();
    const getResult = async (): Promise<TestRunResultDto> => ({ status:'Failed' });
    await store.startPolling(getResult, 'id1');
    expect(store.status()).toBe('failed');
  });
});