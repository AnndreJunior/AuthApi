# AuthApi

<p align="center">
    <img src="https://img.shields.io/static/v1?label=csharp&message=language&color=512BD4&style=for-the-badge&logo=CSHARP"/>
    <img src="https://img.shields.io/static/v1?label=dotnet&message=framework&color=512BD4&style=for-the-badge&logo=dotnet"/>
    <img src="https://img.shields.io/static/v1?label=postgresql&message=database&color=4169E1&style=for-the-badge&logo=postgresql"/>
    <img src="https://img.shields.io/static/v1?label=status&message=em desenvolvimento&color=green&style=for-the-badge"/>
</p>

> Status do Projeto: :warning: Em desenvolvimento

### Tópicos

- [Descrição do projeto](#descrição-do-projeto)
- [Funcionalidades](#funcionalidades)
- [Pré-requisitos](#pré-requisitos)
- [Como executar a aplicação](#como-executar-a-aplicação)
- [Como consumir a API](#como-consumir-a-api)

## Descrição do projeto

Este projeto consiste em uma API de autenticação feita em C# e .Net que permite, de forma simples e direta, a criação e gerenciamento de contas de usuários, permitindo upload de fotos de perfil que serão salvas em um sistema de storage, alteração de senha, nome e etc.

## Funcionalidades

- :heavy_check_mark: Login e autenticação
- :heavy_check_mark: Upload de fotos de perfil
- :warning: Mudar nome de usuário
- :warning: Mudar senha
- :warning: Mudar nome
- :warning: Deletar perfil

## Pré-requisitos

Para executar esse projeto localmente é necessário ter o [.Net](https://dotnet.microsoft.com/pt-br/download) instalado e um banco PostgreSql.

### Banco de dados

Para usar o PostgreSql você pode usar o [Docker](https://www.docker.com/get-started/)

```bash
# baixe a imagem do PostgreSql
docker pull postgres
```

```bash
# execute a imagem
docker run --name <nome da imagem> -e POSTGRES_PASSWORD=admin -e POSTGRES_DB=users_db -p 5432:5432 -d postgres
```

Também é possível instalar o PostgreSql sem uma imagem docker pelo próprio site, basta [entrar no site](https://www.postgresql.org/download/), selecionar seu sistema e seguir os passos.

Banco instalado e configurado execute o seguinte comando:

```bash
psql -U postgres -c "CREATE DATABASE users_db;"
```

## Como executar a aplicação

Antes de executar a aplicação, clone o repositório:

```bash
git clone https://github.com/AnndreJunior/AuthApi.git
```

Acesse a pasta do projeto e execute o seguinte comando:

```bash
dotnet restore
```

Após isso será necessário setar o secret jwt e a string de conexão do banco de dados. Para isso será usado o [user-secrets](https://learn.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows) do .Net para maior segurança.

```bash
# para isso acesse o projeto de api
cd AuthApi.Api
```

```bash
# crie o secret jwt
dotnet user-secrets set "jwt:secret" "aBcDeFgH1234567890IjKlMnOpQrStUvWxYz"
```

```bash
# crie a string de conexão do banco
dotnet user-secrets set "database:connection" "Host=localhost;Database=users_db;Username=postgres;Password=admin"
```

```bash
# após isso retorne ao root da solução
cd ..
```

Com tudo isso feito basta garantir que a ferramenta [dotnet-ef](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet) está instalado. Para isso execute o seguinte comando:

```bash
dotnet ef
```

A saída deve ser isso:

```
                    _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 2.1.3-rtm-32065

<Usage documentation follows, not shown.>
```

Caso seja algo diferente disso instale a ferramenta:

```bash
dotnet tool install --global dotnet-ef
```

```bash
# caso já esteja instalada garante que ela está atualizada
dotnet tool update --global dotnet-ef
```

Então basta executar a migration e rodar a aplicação:

```bash
# migration
dotnet ef database update --project AuthApi.Infra -s AuthApi.Api

# executar a aplicação
dotnet run --project AuthApi.Api
```

## Como consumir a API

Para acessar os endpoints disponíveis e testar a api acesse a [página do swagger da aplicação](http://localhost:5231/swagger) e teste o projeto.

## Linguagens, dependências e libs utilizadas :books:

- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/tour-of-csharp/)
- [.Net](https://dotnet.microsoft.com/pt-br/)
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
- [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next)
- [Entity Framework Core Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/9.0.0-preview.3.24172.4)
- [Npgsql](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/9.0.0-preview.3)
