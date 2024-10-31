import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AlertErrorsComponent } from '../alert-errors/alert-errors.component';
import { User } from '../../models/User';
import { UserService } from '../../services/user.service';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, AlertErrorsComponent, NgxMaskDirective]
})
export class UserFormComponent implements OnInit {
  user!: User;
  usuarioForm!: FormGroup;
  errors: [] = []

  constructor(private fb: FormBuilder, 
              private userService: UserService, 
              private router: Router, 
              private route: ActivatedRoute ) {}

  ngOnInit(): void {
    this.carregarUsuario();
  }

  carregarUsuario(): void {
    const id = +this.route.snapshot.params['id']
    if (id) {
      this.userService.obterPorId(id).subscribe(usr => {
        this.user = usr; 
        this.initializeForm();
      },err => this.processarFalha(err))
    } else {
      this.initializeForm();
    }
  }

  initializeForm() {
    this.usuarioForm = this.fb.group({
      id: [this.user?.id],
      nomeCompleto: [this.user?.nomeCompleto || '', [Validators.required]],
      dataNascimento: [this.formataData() || '', [Validators.required]],
      cpf: [this.user?.cpf || '', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      dataCadastro: [this.user?.dataCadastro],
      dataAtualizacao: [this.user?.dataAtualizacao],
    });
  }

  onSubmit() {
    if (this.usuarioForm.valid) {
      const usuario = {...this.usuarioForm.value};
      if (usuario.id == null) {
        this.userService.criarUsuario(usuario).subscribe(
          _ => { this.processarSucesso() },
          falha => { this.processarFalha(falha) }
        );
      } else {
        this.userService.atualizarUsuario(usuario.id, usuario).subscribe(
          _ => this.processarSucesso(),
          falha => this.processarFalha(falha)
        );
      }
    } else {
      this.markAllAsTouched()
    }
  }

  processarSucesso() {
    this.usuarioForm.reset();
    this.errors = [];
    this.router.navigate(['/usuarios']);
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
  }

  private formataData(): string {
    const dataNascimento = this.user?.dataNascimento ? new Date(this.user.dataNascimento) : '';
    const formattedDate = dataNascimento ? new Intl.DateTimeFormat('en-CA').format(dataNascimento) : '';
    return formattedDate
  }

  private markAllAsTouched() {
    Object.keys(this.usuarioForm.controls).forEach(controlName => {
      this.usuarioForm.get(controlName)?.markAsTouched();
    });
  }

}
