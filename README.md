# Order Management API

API de gerenciamento de pedidos desenvolvida em **.NET 10** utilizando **Clean Architecture**, **CQRS com MediatR**, **Entity Framework Core**, **SQLite**, **FluentValidation**, **JWT Authentication** e **Serilog**.

---

## Tecnologias

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core 10
* SQLite
* CQRS + MediatR
* FluentValidation
* AutoMapper
* JWT
* Serilog
* Swagger
* Docker
* xUnit + Moq

---

## Arquitetura

O projeto segue Clean Architecture com separação de responsabilidades:

```
OrderManagement

├── OrderManagement.Api
├── OrderManagement.Application
├── OrderManagement.Domain
├── OrderManagement.Infrastructure
└── OrderManagement.Tests
```

Principais padrões utilizados:

* CQRS com Commands e Queries separados
* MediatR para comunicação entre camadas
* Repository Pattern
* Unit of Work
* Pipeline Behaviors para validação e logging
* Middleware global de exceções

---

# Executando localmente

## Pré-requisitos

Necessário ter instalado:

* .NET SDK 10
* Visual Studio 2026 18.8.0
* Git

---

## Restaurar dependências

```bash
dotnet restore
```

---

## Banco de dados

A aplicação utiliza SQLite.

As migrations são aplicadas automaticamente durante a inicialização da API através do Entity Framework Core.

---

## Executar a API

Acesse o projeto:

```bash
cd OrderManagement.Api
```

Execute:

```bash
dotnet run
```

A API ficará disponível em:

```
http://localhost:8080
```

Swagger:

```
http://localhost:8080/swagger
```

---

# Executando via Docker

## Pré-requisitos

* Docker Desktop instalado

---

## Build da imagem

Na raiz do projeto:

```bash
docker build -t order-management-api .
```

---

## Executar o container

```bash
docker run -p 8080:8080 order-management-api
```

A API ficará disponível em:

```
http://localhost:8080
```

Swagger:

```
http://localhost:8080/swagger
```

---

# Executando com Docker Compose

Para subir a aplicação:

```bash
docker compose up --build
```

Para executar em background:

```bash
docker compose up -d
```

Parar os containers:

```bash
docker compose down
```

---

# Autenticação

A API utiliza JWT.

Endpoint de login:

```
POST /api/auth/login
```

Usuário de teste:

```json
{
  "username": "dev@martech.com",
  "password": "Senha@123"
}
```

Após receber o token, utilize no Swagger:

```
Authorize → token
```

---

# Banco de dados

O projeto utiliza SQLite.

A aplicação executa as migrations automaticamente na inicialização.

Arquivo gerado:

```
orders.db
```

---

# Testes

Executar testes:

```bash
dotnet test
```

Testes implementados:

* CreateOrderCommandHandler
* CancelOrderCommandHandler
* LoginCommandHandler

---

# Endpoints principais

```
POST   /api/auth/login
POST   /api/orders
GET    /api/orders
GET    /api/orders/{id}
PATCH  /api/orders/{id}/cancel
```
