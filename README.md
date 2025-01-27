# Poke Boxes API

Este projeto é uma aplicação ASP.NET Core para a inface de Boxes de Pokemon, incluindo funcionalidades para usuários, treinadores, e Pokémon.

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

## Funcionalidades

### Usuários

- Criação de usuários
- Login de usuários

### Treinadores

- Criação de treinadores

### Pokémon

- Gerenciamento de Pokémon, incluindo habilidades, movimentos e itens

## Configuração

### Banco de Dados

O projeto utiliza MongoDB como banco de dados. As configurações de conexão estão localizadas em `appsettings.Development.json` e `appsettings.json`.

### Autenticação

A autenticação é feita utilizando JWT. As configurações de JWT estão localizadas em `appsettings.Development.json`.

## Executando o Projeto

Para executar o projeto, utilize os seguintes comandos:

```sh
dotnet run
