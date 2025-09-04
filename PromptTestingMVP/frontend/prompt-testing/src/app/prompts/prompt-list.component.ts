import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
// ag-grid Angular module placeholder (user must install package):
// import { AgGridAngular } from 'ag-grid-angular';
import { PromptsStore } from './prompts.store';
import { Prompt } from '../shared/models/prompt.models';
@Component({
  standalone: true,
  selector: 'app-prompt-list',
  imports:[CommonModule],
  templateUrl: './prompt-list.component.html',
  styleUrls: ['./prompt-list.component.scss']
})
export class PromptListComponent {
  columnDefs: { field:keyof Prompt; headerName:string; valueFormatter?: (p:{ value:unknown })=>string }[] = [
    { field: 'name', headerName: 'Name' },
    { field: 'status', headerName: 'Status' },
    { field: 'accuracy', headerName: 'Accuracy', valueFormatter:(p:{value:unknown})=> p.value == null ? '-' : String(p.value)+'%' },
    { field: 'lastRun', headerName: 'Last Run' }
  ];
  defaultColDef = { resizable:true, sortable:true, filter:true };
  rowData = (): Prompt[] => this.store.prompts();
  constructor(public store: PromptsStore){}
}