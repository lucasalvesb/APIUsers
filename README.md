# APIUsers

* Esse projeto é uma API de Usuários que tem uma lista de jogos associados a eles.

## Sumário

* [Tecnologias utilizadas](https://github.com/lucasalvesb/APIUsers/#tecnologias-utilizadas)
* [Instruções para executar o projeto](https://github.com/lucasalvesb/APIUsers#instru%C3%A7%C3%B5es-para-executar-o-projeto)
* [Desenvolvimento](https://github.com/lucasalvesb/APIUsers/#desenvolvimento)

## Tecnologias utilizadas

* [ASP.NET](https://dotnet.microsoft.com/pt-br/apps/aspnet)
* [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
* [Entity Framework Core](https://learn.microsoft.com/en-us/aspnet/entity-framework)
* [Swagger](https://swagger.io/)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Instruções para executar o projeto

Caso deseje executar localmente, segue instruções:

### Tenha instalado na sua máquina:
```
Git
Node V16.15.1
Visual Studio
SQL Server 2019
.NET 6.0
```

### Clone o repositório com o comando git clone:

```
git clone https://github.com/lucasalvesb/APIUsers.git
```

### Tenha certeza de que está no diretório correto, chamado APIUsers. Caso não, dê um:

```
cd APIUsers
```

### Instale as dependências:

```
dotnet restore
```
Elas incluem:
```
Microsoft.AspNetCore.Cors 2.2.0
Microsoft.EntityFrameworkCore 7.0.8
Microsoft.EntityFrameworkCore.Design 7.0.8
Microsoft.EntityFrameworkCore.SqlServer 7.0.8
Microsoft.EntityFrameworkCore.Tools 7.0.8
Swashbuckle.AspNetCore 6.2.3
```

### Abra o appsettings.json e edite as informações do banco de dados para os do seu SQL Server. 
```
Edite a linha "DefaultConnection". Você vai precisar inserir o nome do seu servidor, o nome da database(aconselho 'APIUsers'), o logon e sua senha.
```
![image](https://github.com/lucasalvesb/APIUsers/assets/71532408/ca0985f5-cbb2-4adb-9790-303cdd43d532)


### Crie uma nova migração:

```
dotnet ef migrations add InitialCreate
```

### Aplique as migrações ao banco de dados:

```
dotnet ef database update
```

### Rode o projeto:

```
dotnet run (ou clique em APIUsers no topo do Visual Studio)
```

### Faça os testes utilizando a API na tela html do Swagger que abriu automaticamente. 

## Desenvolvimento

### Microsoft.AspNetCore.Cors:
Habilita o suporte a Compartilhamento de Recursos de Origem Cruzada (CORS) para a aplicação ASP.NET Core.

### Microsoft.EntityFrameworkCore:
Fornece funcionalidades do Entity Framework Core para acesso a dados e ORM (Object-Relational Mapping), ou seja, é usado para realizar as operações CRUD e definir o modelo de dados.

### Microsoft.EntityFrameworkCore.Design:
Oferece suporte de design para comandos do Entity Framework Core, como as migrações.

### Microsoft.EntityFrameworkCore.SqlServer:
Habilita o suporte ao banco de dados SQL Server para o Entity Framework Core. Ou seja, que se conecte e se comunique (faça operações) com o banco de dados SQL Server.

### Microsoft.EntityFrameworkCore.Tools:
Fornece ferramentas adicionais para o Entity Framework Core, também utilizado nas migrações. 

### Swashbuckle.AspNetCore:
Gera a documentação da API usando o Swagger para a aplicação ASP.NET Core e também serve para testar a aplicação.


