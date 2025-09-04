import { Component } from '@angular/core';
@Component({
  standalone: true,
  selector: 'app-test-execution',
  templateUrl: './test-execution.component.html',
  styleUrls: ['./test-execution.component.scss']
})
export class TestExecutionComponent {
  running=false; status='Idle';
  run(){ this.running=true; this.status='Running'; setTimeout(()=>{ this.status='Completed'; this.running=false;}, 500); }
}