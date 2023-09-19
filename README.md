# API Financeira - Desafio Técnico

Este repositório contém uma API para uma aplicação financeira que oferece uma série de funcionalidades relacionadas a pessoas, contas, cartões e transações. A API foi desenvolvida como parte de um desafio técnico e oferece recursos essenciais para operações financeiras.

## Funcionalidades Principais

A API Financeira oferece as seguintes funcionalidades principais:

1. **Cadastro de Pessoas**
   - As pessoas podem se cadastrar na aplicação fornecendo nome, documento (CPF ou CNPJ) e senha.

2. **Gerenciamento de Contas**
   - Uma pessoa pode ter várias contas.
   - É possível adicionar e listar contas para uma pessoa.

3. **Gerenciamento de Cartões**
   - Uma conta pode ter vários cartões.
   - Os cartões podem ser físicos ou virtuais.
   - Listagem de cartões com exibição dos últimos 4 dígitos do número do cartão e CVV com 3 dígitos.

4. **Transações em Contas**
   - Realização de transações em uma conta, com validação de saldo para evitar saldo negativo.
   - Listagem de transações em uma conta com paginação e filtro por data.

5. **Consulta de Saldo**
   - Consulta do saldo de uma conta.

6. **Reversão de Transações**
   - Reversão de uma transação, realizando a ação inversa (crédito para débito e vice-versa).

## Endpoints Principais

A API oferece os seguintes endpoints principais:

- `POST /people`: Criação de uma pessoa.
- `POST /people/:peopleId/accounts`: Criação de uma conta para uma pessoa.
- `GET /people/:peopleId/accounts`: Listagem de todas as contas de uma pessoa.
- `POST /accounts/:accountId/cards`: Criação de um cartão em uma conta.
- `GET /accounts/:accountId/cards`: Listagem de todos os cartões de uma conta.
- `GET /people/:peopleId/cards`: Listagem de todos os cartões de uma pessoa.
- `POST /accounts/:accountId/transactions`: Criação de uma transação em uma conta.
- `GET /accounts/:accountId/transactions`: Listagem de todas as transações de uma conta.
- `GET /accounts/:accountId/balance`: Consulta do saldo de uma conta.
- `POST /accounts/:accountId/transactions/:transactionId/revert`: Reversão de uma transação.

## Pré-requisitos

Certifique-se de que você tenha os seguintes pré-requisitos instalados em seu ambiente:

- .NET Core SDK 6.0
- Microsoft.AspNetCore.App 6.0.0 ou superior
- Microsoft.EntityFrameworkCore.SqlServer 6.0.0 ou superior
- Microsoft.EntityFrameworkCore.Tools 6.0.0 ou superior
- Microsoft.VisualStudio.Web.CodeGeneration.Design 6.0.0 ou superior

## Tecnologias Utilizadas

A API Financeira foi desenvolvida com base nas seguintes tecnologias:

- ASP.NET Core
- Entity Framework Core
- PostgreSQL (Banco de Dados)
- Swagger para documentação da API

## Configuração do Banco de Dados

A configuração de acesso ao banco de dados PostgreSQL é definida no arquivo `appsettings.json`. Certifique-se de fornecer as informações corretas de conexão antes de executar a aplicação.

```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=cubos;Username=postgres;Password=123456"
}
```

## Execução da Aplicação

Para executar a aplicação, siga os passos abaixo:

1. Clone este repositório em seu ambiente local.

2. Certifique-se de ter os pré-requisitos instalados.

3. Atualize a configuração do banco de dados no arquivo `appsettings.json`.

4. Abra um terminal na pasta do projeto e execute o seguinte comando para aplicar as migrações e criar o banco de dados:

   ```
   dotnet ef database update
   ```

5. Execute o seguinte comando para iniciar a aplicação:

   ```
   dotnet run
   ```

A API estará disponível em `https://localhost:7111;http://localhost:5007`.

## Documentação da API

A API inclui documentação Swagger que pode ser acessada em `https://localhost:7111/swagger`. A documentação detalha todos os endpoints disponíveis, seus parâmetros e exemplos de uso.
