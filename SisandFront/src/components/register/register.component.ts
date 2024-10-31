import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { AlertErrorsComponent } from '../alert-errors/alert-errors.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, AlertErrorsComponent],
})
export class RegisterComponent  {
  registerForm: FormGroup;
  errors: [] = []

  constructor(private fb: FormBuilder, private router: Router, private userService: UserService) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.dirty && this.registerForm.valid) {
      const usuario = {...this.registerForm.value} ;

      this.userService.registrar(usuario)
      .subscribe(
        sucesso => { this.processarSucesso(sucesso) },
        falha => { this.processarFalha(falha) }
      );
    }
  }

  processarSucesso(response) {
    this.registerForm.reset();
    this.errors = [];
    this.userService.LocalStorage.salvarTokenUsuario(response)
    this.router.navigate(['/usuarios']);
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
  }

}
