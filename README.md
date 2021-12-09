# Introdução
Aplicação desenvolvida em cima da ASP.NET Core 6 utilizando seu tema padrão do bootstrap5.

### Build and run com Docker

Essa forma requer o [Client do Docker](https://store.docker.com/editions/community/docker-ce-desktop-windows).

Você pode construir e executar-lo no Docker usando os comandos a seguir. As instruções presumem que você está na raiz do repositório.

```console
docker build -t teste_comeia_todo .
docker run -dp 5034:5034 teste_comeia_todo
```

A aplicação sera executada e visivel pelo endereço local: <br>
http://localhost:5034

### Build and run com Dotnet Local
Essa forma requer o [.NET e ASP.NET Core](https://dotnet.microsoft.com/en-us/download)

Você pode executar diretamente na maquina local usando os comandos a seguir. As instruções presumem que você está na raiz do repositório.
```console
dotnet run
```
A aplicação sera executada e visivel pelos endereços locais:<br>
http://localhost:5034 <br>
https://localhost:7067

### Requisitos do projeto pela Comeia
```
Projeto de Avaliação para vaga C#.

Desenvolva uma aplicação Web de “Lista de Tarefas” com a linguagem C#.

A aplicação deve permitir ao usuário cadastrar, editar, excluir e listar tarefas.
Os atributos de uma tarefa devem ser os seguintes: código, data, descrição e
uma flag que identifica se a tarefa foi executada ou não.

Você pode fazer a aplicação com algum framework de sua escolha, assim
como utilizar alguma biblioteca para UI. Seria interessante utilizar algum banco
de dados para persistir as informações, mas pode ser utilizada alguma base
em memória também.

Um diferencial seria a aplicação possuir autenticação, onde o usuário só
poderia acessar sua lista de tarefas caso esteja autenticado com login e senha.

Você pode disponibilizar o código em algum repositório público do GitHub ou
algum similar com as instruções de como executá-lo.
```
