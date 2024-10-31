import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alert-errors',
  templateUrl: './alert-errors.component.html',
  standalone: true,
  imports: [CommonModule]
})
export class AlertErrorsComponent  {
  @Input() errors: string[] = [];
  ativo: boolean = true;

  close() {
    this.ativo = false;
  }
}
