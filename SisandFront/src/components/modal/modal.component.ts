import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  standalone: true,
  imports: [CommonModule]
})
export class ModalComponent  {
  @Input() titulo: string
  @Input() conteudo: string
  @Output() confirmaAcao = new EventEmitter<boolean>();
  @Input() isVisible: boolean;

  fecharModal() {
    this.confirmaAcao.emit(false)
  }

  confirmar() {
    this.confirmaAcao.emit(true)
  }

}
