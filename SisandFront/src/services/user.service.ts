import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { User } from '../models/User';
import { LoginUser } from '../models/LoginUser';
import { RegisterUser } from '../models/RegisterUser';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(private http: HttpClient) { super() }

  obterTodos(): Observable<User[]> {
    return this.http.get<User[]>(`${this.UrlApi}/usuarios`).pipe(
      map(super.extractData),
      catchError(super.serviceError));
  }

  obterPorId(id: number): Observable<User> {
    return this.http.get<User>(`${this.UrlApi}/usuarios/${id}`)
    .pipe(
      map(super.extractData),
      catchError(super.serviceError));
  }

  criarUsuario(user: User) {
    return this.http.post(`${this.UrlApi}/usuarios`, user,
      this.ObterAuthHeaderJson()
    ).pipe(
      map(super.extractData),
      catchError(super.serviceError));
  }

  atualizarUsuario(id: number, user:User) {
    return this.http.put(`${this.UrlApi}/usuarios/${id}`, user, 
      this.ObterAuthHeaderJson())
      .pipe(
        map(super.extractData), 
        catchError(super.serviceError))
  }

  excluirUsuario(id: number) {
    return this.http.delete(`${this.UrlApi}/usuarios/${id}`, 
      this.ObterAuthHeaderJson())
      .pipe(
        map(super.extractData), 
        catchError(super.serviceError))
  }

  login(user: LoginUser) {
    return this.http
            .post(`${this.UrlConta}/login`, user, this.ObterHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));
  }

  registrar(user: RegisterUser) {
    return this.http
            .post(`${this.UrlConta}/registrar`, user, this.ObterHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));
  }
}
