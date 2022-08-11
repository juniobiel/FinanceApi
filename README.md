# Finance API
Repositório de estudos. 
Desenvolvimento de aplicação para o gerenciamento financeiro.

## Sobre o Projeto
Este projeto visa a construção de uma API para o gerenciamento financeiro e está sendo usado como base para avanço nos estudos de Engenharia de Software e de tecnologias relacionadas ao desenvolvimento do projeto.

## Tecnologias
1. Linguagem de Programação Backend - C# com .NET Framework
3. Banco de Dados - PostgreSQL com Entity Framework
4. Hospedagem da Api - Google Cloud
5. Hospedagem do Banco de dados - Heroku
6. CI/CD com GitHub Actions

## Práticas de Desenvolvimento
1. Test-Driven Development
2. Desenvolvimento Ágil com Kanban
3. Processo de Software Ágil
    1. Elencar requisito
    2. Modelar o requisito
    3. Elaborar o caso de uso
    4. Desenvolver
    5. Entregar
4. Continuos Integration
5. Continuos Delivery

## Postgres SQL Docker

Após fazer o download da imagem do PGSQL no docker, rode o seguinte comando.

```
docker run -d -p 5432:5432 
    --name financedb-postgres 
    -e POSTGRES_PASSWORD=admin@123456 
    -e POSTGRES_USER=admin 
    -e POSTGRES_DB=finance 
    postgres
```
Após configurar e rodar o container, é só conectar a ferramenta DBMS passando o `localhost:5432` e as credenciais da imagem