import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Output } from "@angular/core";
// ag-grid Angular module placeholder (user must install package):
// import { AgGridAngular } from 'ag-grid-angular';
import { Prompt } from "../shared/models/prompt.models";
import { PromptsStore } from "./prompts.store";
import { AgGridModule } from "ag-grid-angular";
@Component({
  standalone: true,
  selector: "app-prompt-list",
  imports: [CommonModule, AgGridModule],
  templateUrl: "./prompt-list.component.html",
  styleUrls: ["./prompt-list.component.scss"],
})
export class PromptListComponent {
  @Output() selectPrompt = new EventEmitter<Prompt>();
  public columnDefs: {
    field: keyof Prompt;
    headerName: string;
    valueFormatter?: (p: { value: unknown }) => string;
  }[] = [
    { field: "name", headerName: "Name" },
    { field: "status", headerName: "Status" },
    {
      field: "accuracy",
      headerName: "Accuracy",
      valueFormatter: (p: { value: unknown }) =>
        p.value == null ? "-" : String(p.value) + "%",
    },
    { field: "lastRun", headerName: "Last Run" },
  ];
  public defaultColDef = { resizable: true, sortable: true, filter: true };
  public rowData = (): Prompt[] => this.store.prompts();
  constructor(public store: PromptsStore) {}

  onSelect(p?: Prompt) {
    if (p) {
      this.selectPrompt.emit(p);
    }
  }
}
