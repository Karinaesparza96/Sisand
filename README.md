# README - Projeto Sisand

## Passos para Execução do Projeto

### Pré-requisitos

- .NET SDK 8.0 
- Node v20.9.0
- npm 10.8.2
- Visual Studio 2022 (ou qualquer IDE de sua preferência)
- Git

### 1. Clone o Repositório

Abra seu terminal e execute o seguinte comando para clonar o repositório:

```bash
git clone https://github.com/Karinaesparza96/Sisand.git
cd Sisand
   ```

2. **Executar a Api:**
   
   ```bash
   cd SisandApi/src/Api
   dotnet run
   ```
- Após isso, a aplicação estará rodando em http://localhost:5048.
- Você pode testar as funcionalidades da API utilizando o Swagger: http://localhost:5048/swagger/index.html
- Observação: As funcionalidades do CRUD de novos usuários requerem um token no Bearer.
  Para obter o token, você pode:

  - Registrar um novo usuário.
  - Fazer login se já tiver um cadastro.

  Depois, insira o token no botão "Authorize" como Bearer {seutoken}.

3. **Executar a Front Angular:**
   
   ```bash
   cd SisandFront/src
   npm install
   npm start
   ```
   
Após isso, a aplicação estará rodando em http://localhost:4200/

**Instruções**
- No primeiro acesso, você deve realizar o registro. Após isso, estará logado por uma hora, que é a duração do JWT.

