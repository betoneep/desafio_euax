# EUAX
Desafio EUAX

Projeto

Tecnologias e escolhas

API:
    WebApi.netCore 3.1
    Entity Framework
    ModelBuilder com uso do modelo SSnake
    JWT
    Injeção de Dependencia
    Swagger

FRONT: Vue sobre o template do CoreUI Boostrap (https://coreui.io/?affChecked=1).

DATABASE:
    Banco de dados postgres

Via Swagger add usuario inicial para teste.


Infelzimente AINDA não consegui colocar todo o ambiente em docker e fazer o link da API (Nginx), Banco (Postgres) e Front (Node), via um docker-compose. Fica pra um outro momento em breve.

A Api tem um dockerfile fncional pra levantar o .net gerado pelo proprio vs.
O Banco é um container padrao PGSQL com os dados de conexao do projeto.. root/root.
O Front tive dificuldade em levanter o node.

Então por hora será preciso levanter a API via VS.
E o front Vue via Node fisico ( npm run serve )



