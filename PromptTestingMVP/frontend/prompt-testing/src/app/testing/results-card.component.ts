import { DatePipe } from "@angular/common";
import { Component, Input } from "@angular/core";
import { MatCardModule } from "@angular/material/card";
import { MatChipsModule } from "@angular/material/chips";
@Component({
  standalone: true,
  selector: "app-results-card",
  templateUrl: "./results-card.component.html",
  styleUrls: ["./results-card.component.scss"],
  imports: [DatePipe, MatCardModule, MatChipsModule],
})
export class ResultsCardComponent {
  @Input() accuracy?: number;
  @Input() completedAt?: Date;
}
