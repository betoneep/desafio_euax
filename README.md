Desafio EUAX

Projeto

Tecnologias e escolhas

API: WebApi.netCore 3.1 Entity Framework, ModelBuilder com uso do modelo Snake, JWT, Injeção de Dependência e Swagger

FRONT: Vue sobre o template do CoreUI Boostrap (https://coreui.io/?affChecked=1).

DATABASE: Banco de dados postgres

Via Swagger add usuário inicial para teste.

Infelizmente AINDA não consegui colocar todo o ambiente em docker e fazer o link da API (Nginx), Banco (Postgres) e Front (Node), via um docker-compose. Fica pra um outro momento em breve.

A Api tem um dockerfile funcional pra levantar o .net gerado pelo próprio vs. O Banco é um container padrão PGSQL com os dados de conexão do projeto.. root/root. O Front tive dificuldade em levantar o node.

Então por hora será preciso levantar a API via VS. E o front Vue via node físico (npm run serve)

