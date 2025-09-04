import { Component } from '@angular/core';
@Component({
  standalone: true,
  selector: 'app-results-card',
  templateUrl: './results-card.component.html',
  styleUrls: ['./results-card.component.scss']
})
export class ResultsCardComponent { accuracy?: number; completedAt?: Date; }