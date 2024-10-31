import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../../models/User';
import { UserService } from '../../services/user.service';
import { ModalComponent } from '../modal/modal.component';
import { AlertErrorsComponent } from "../alert-errors/alert-errors.component";
import { NgxMaskPipe } from 'ngx-mask';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  standalone: true,
  imports: [RouterModule, CommonModule, ModalComponent, AlertErrorsComponent, NgxMaskPipe]
})
export class UserListComponent implements OnInit {
  usuarios$!: Observable<User[]>
  isModalVisible: boolean
  usuario: User
  errors: [] = []

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.usuarios$ = this.obterTodosUsuarios()
  }

  obterTodosUsuarios() {
    return this.userService.obterTodos()
  }

  excluirUsuario(user: User) {
    this.isModalVisible = true
    this.usuario = user
  }

  onConfirmacaoExcluir(confirmado: boolean) {
    if (confirmado) {
      this.userService.excluirUsuario(this.usuario.id).subscribe(
        sucesso => { this.processarSucesso() },
        falha => { this.processarFalha(falha) })
    }
    this.isModalVisible = false;
  }

  processarSucesso() {
   this.usuarios$ = this.obterTodosUsuarios()
  }
  processarFalha(fail: any) {
    this.errors = fail.error.errors;
  }
}
