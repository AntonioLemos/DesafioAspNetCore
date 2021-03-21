# DesafioAspNetCore

DESAFIO .NET CORE
Para simplicidade do desafio, os dados, tanto para inserção/alteração ou consulta, podem
ser armazenados na memória, não existindo a necessidade de se criar um banco de dados.
Todas as rotas, com exceção da de autenticação, devem validar se a requisição possui um
Token autenticado.
Serão avaliados:
● Organização de código;
● Lógica;
● Estrutura da API;
● Padrões REST, uso dos verbos, códigos de retorno e etc. Padronização de código
C# (nome de classes, propriedades etc).
O objeto pessoa proposto no desafio pode ser simples, possuindo apenas código, nome,
CPF, UF e data de nascimento.
O código fonte deve ser enviado a algum repositório Git público de sua preferência.
O DESAFIO
API:
Criar uma API REST utilizando a linguagem C#, contendo as seguintes funcionalidades:
1. Uma rota para autenticação;
2. Uma rota para consulta de pessoas, que deve retornar uma lista de objetos de pessoas.
3. Uma rota para consultar uma pessoa pelo seu código;
4. Uma rota para consultar pessoas de uma determinada UF;
5. Uma rota de gravar pessoa, que deve retornar o objeto “salvo”;
O método deve ser capaz de validar as informações recebidas.
6. Uma rota para atualizar os dados de uma pessoa, que deve retornar o objeto atualizado;
7. Uma rota para excluir uma pessoa;
8. Criar testes unitários para cada rota/endpoint;
FRONT-END:
Criar um projeto .Net Core Web App, com bootstrap e Angular/Blazor/Javascript Vanilla.
1. Página de login;
2. Página que lista as pessoas, com um filtro para consulta de uma pessoa;
3. Página ou botão para criar uma nova pessoa;
4. Página ou botão para excluir uma pessoa (pode ser tanto na lista de pessoas quando um
botão a parte);
5. Página ou botão para atualizar os dados de uma pessoa;

