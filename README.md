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

## SonarQube
Após fazer o download da imagem do PGSQL e do SonarQube, rode os seguintes comandos.

### Criar uma rede para o docker pgsql
```
docker network create sonar_network
```

### Rodar um container do pgsql para o SonarQube
```
docker run --name sonar-pgsql 
    -e POSTGRES_PASSWORD=sonar 
    -e POSTGRES_USER=sonar 
    -e POSTGRES_DB=sonar 
    -v pgdata:/var/lib/postgresql/data 
    -p 5433:5433 -it 
    --network sonar_network sonarqube
```
Por padrão, o pgsql utiliza a porta 5432, porém já é utilizada na outra instância do banco de dados utilizado para a aplicação.

### Habilitar o redimensionamento do wsl2 do docker
Quando tentar rodar o container do sonarqube e ele automaticamente parar, rode o seguinte comando:
```
wsl -d docker-desktop
sysctl -w vm.max_map_count=262144
```
### Rodar um container do SonarQube
Por fim, vamos rodar o container do SonarQube
```
docker run -it --name sonarqube
    -p 9000:9000
    -e sonar.jdbc.username=sonar
    -e sonar.jdbc.password=sonar
    -e sonar.jdbc.url=jdbc.postgresql://sonar-pgsql/sonar
    --network sonar_network sonarqube
```
### Rodar análise do SonarQube
Para rodar a análise, é só executar o arquivo sonarqube.bat de dentro do cmd.