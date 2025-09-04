import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ResultsCardComponent } from '../../src/app/testing/results-card.component';

@Component({
  standalone: true,
  imports:[ResultsCardComponent],
  template:`<app-results-card />`
})
class HostComponent {}

describe('ResultsCardComponent', () => {
  let fixture: ComponentFixture<HostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[HostComponent]
    }).compileComponents();
    fixture = TestBed.createComponent(HostComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    const compEl = fixture.nativeElement.querySelector('app-results-card');
    expect(compEl).toBeTruthy();
  });
});