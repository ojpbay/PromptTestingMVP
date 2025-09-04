import { signal } from '@angular/core';
import { ScopeSelection } from '../shared/models/prompt.models';

export class ScopeStore {
  selection = signal<ScopeSelection>({ team:'', brokingSegment:'', globalLineOfBusiness:'', product:''});
  set(partial: Partial<ScopeSelection>) { this.selection.update(s => ({...s, ...partial})); }
  isComplete() { const v = this.selection(); return !!(v.team && v.brokingSegment && v.globalLineOfBusiness && v.product); }
}