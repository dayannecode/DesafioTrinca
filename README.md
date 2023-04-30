# Solução para Churrasco.

Esse projeto é um sistema backend que foi feito em .NET 6.0 pra gerenciar os churrascos da Trinca!

O projeto segue os princípios de Clean Architecture, SOLID e Clean Code!

## Como Usar

Para utilizar o projeto, é necessario baixar o .NET 6.0 SDK e usar o Visual Studio 2019 ou uma versão mais nova. Depois, é só clonar o projeto na sua máquina e abrir a solução no Visual Studio.


O projeto foi organizado em cinco camadas pra deixar tudo mais fácil de entender:

+ **Domain:** Esta camada contém as entidades e modelos de negócio da aplicação.
+ **Interfaces:** Nesta camada, são definidas as interfaces públicas que permitem a interação com outras camadas.
+ **Repositories:** Esta camada se comunica com o banco de dados da aplicação.
+ **Service:** Nesta camada se encontra a lógica de negócio da aplicação.
+ **UseCases:** Esta camada contém as classes que orquestram as chamadas aos serviços da aplicação para atender às funcionalidades específicas da aplicação.

Obrigada.
