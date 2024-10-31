import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from "../environments/environment"
import { LocalStorage } from '../utils/LocalStorage';

export abstract class BaseService {

    protected UrlApi: string = environment.apiUrl;
    protected UrlConta: string = environment.contaUrl;

    public LocalStorage = new LocalStorage();

    protected ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.LocalStorage.obterTokenUsuario()}`
            })
        };
    }

    protected extractData(response: any) {
        if (response == null) {
            return
        }
        const { token } = response;
        if (token) {
            return token
        }
        return response || {}
    }

    protected serviceError(response: Response | any) {
      let customError: string[] = [];
      let customResponse = { error: { errors: [] } };
  
      if (response instanceof HttpErrorResponse) {
          if (response.statusText === "Unknown Error") {
              customError.push("Ocorreu um erro desconhecido");
          }
  
          if (response.status === 500) {
              customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");
          }
          if (response.status === 401) {
            customError.push("Para realizar está ação você precisa estar logado.");
          }

          if (response.error?.errors) {
            customError = response.error.errors
          }
          // Adiciona os erros ao customResponse
          customResponse.error.errors = customError;
  
          // Retorna o erro customizado
          return throwError(customResponse);
      }
  
      // Em caso de outros erros, apenas loga e retorna o erro original
      console.error(response);
      return throwError(response);
  }
  
}