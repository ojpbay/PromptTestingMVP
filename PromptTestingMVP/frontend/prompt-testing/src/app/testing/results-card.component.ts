import { DatePipe } from "@angular/common";
import { Component, Input } from "@angular/core";
@Component({
  standalone: true,
  selector: "app-results-card",
  templateUrl: "./results-card.component.html",
  styleUrls: ["./results-card.component.scss"],
  imports: [DatePipe],
})
export class ResultsCardComponent {
  @Input() accuracy?: number;
  @Input() completedAt?: Date;
}
