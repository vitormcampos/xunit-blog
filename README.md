# XUnitBlog - Um simples blog

XUnitBlog é um projeto de blog desenvolvido com ASP.NET Core (Razor Pages), aplicando princípios de Domain-Driven Design (DDD), separação de camadas, e com forte foco em testes unitários utilizando xUnit.  
O projeto foi criado com o objetivo de servir como base de estudos e referência para aplicações web organizadas, testáveis e escaláveis.

## Funcionalidades Atuais
* Home pública com listagem de posts
* Área Administrativa
  * Posts
  * Usuários
* Sistema de Autenticação
  * Login
  * Cadastro de usuário pelo Admin
* Gerenciamento de arquivos
* RBAC (perfis de usuários)

## Arquitetura do projeto

```
XUnitBlog.App               // Camada Web (Razor Pages)
  - Pages
  - Extensions

XUnitBlog.Domain            // Núcleo da aplicação
  /Dtos
  /Entities
  /Exceptions
  /Repositories             // Interfaces de repositórios
  /Services                 // Interfaces + Serviços concretos

XUnitBlog.Data              // Persistência
  /Repositories             // Implementações concretas
  /DbContext
  /EntityConfigurations

XUnitBlog.Tests             // Testes unitários (xUnit)
  /Auth
  /Files
  /Users
  /Posts
  /Hash
```

## Tecnologias utilizadas
* ASP.NET Core 8 – Razor Pages
* Entity Framework Core
* PostgreSQL
* xUnit
* JWT customizado
* DDD + TDD + SOLID
* AlpineJS (JS minimalista e reativo)
* Uikit CSS (alternativa ao Bootstrap)

## Como executar o projeto
### 1. Clone o projeto
```bash
git clone https://github.com/vitormcampos/xunit-blog.git
cd xunit-blog
```

### 2. Configure o banco de dados
Edite a connection string no arquivo
```
XUnitBlog.App/appsettings.json
```

### 3.Execute as migrações 
```
dotnet ef database update --project XUnitBlog.Data --startup-project XUnitBlog.App
```

### 4. Rode a aplicação
```
dotnet run --project XUnitBlog.App
```
