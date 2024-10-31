export class LocalStorage {
    
  public limparDadosTokenUsuario() {
    localStorage.removeItem('token');
  }

  public obterTokenUsuario(): string | null {
      return localStorage.getItem('token');
  }

  public salvarTokenUsuario(token: string) {
      localStorage.setItem('token', token);
  }
}