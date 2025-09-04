// T054 Polling logic store
import { signal } from '@angular/core';

export interface TestRunResultDto { status: 'Completed'|'Failed'|'Running'|'Pending'; accuracy?: number; }

export class TestRunStore {
  status = signal<'idle'|'running'|'completed'|'failed'>('idle');
  accuracy = signal<number|undefined>(undefined);
  private polling = false;

  async startPolling(getResult: (id:string)=>Promise<TestRunResultDto>, id: string) {
    if (this.polling) return; this.polling = true; this.status.set('running');
    let delay = 500;
    while (this.polling) {
      const res = await getResult(id);
      if (res.status === 'Completed') { this.status.set('completed'); this.accuracy.set(res.accuracy); this.polling = false; break; }
      if (res.status === 'Failed') { this.status.set('failed'); this.polling = false; break; }
      await new Promise(r=>setTimeout(r, delay));
      delay = Math.min(delay * 2, 4000);
    }
  }
}