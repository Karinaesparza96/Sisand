import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { AlertErrorsComponent } from '../alert-errors/alert-errors.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, AlertErrorsComponent],
  templateUrl: './login.component.html',
})
export class LoginComponent  {
  loginForm: FormGroup;
  errors: [] = []

  constructor(private fb: FormBuilder, private router: Router, private userService: UserService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      const usuario = {...this.loginForm.value} ;

      this.userService.login(usuario)
      .subscribe(
        sucesso => { this.processarSucesso(sucesso) },
        falha => { this.processarFalha(falha) }
      );
    }
  }

  processarSucesso(sucesso: string) {
    this.loginForm.reset();
    this.errors = [];
    this.userService.LocalStorage.salvarTokenUsuario(sucesso)
    this.router.navigate(['/usuarios']);
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
  }

}

