# Estrutura do Projeto

O projeto segue uma estrutura modular para garantir a separação de responsabilidades e facilitar a manutenção.

## **Diretórios Principais**

- **`src/Ambev.DeveloperEvaluation.WebApi/`**
  - Contém a API principal e os controladores.
  - Expõe os endpoints RESTful para interação com o sistema.

- **`src/Ambev.DeveloperEvaluation.Application/`**
  - Contém a lógica de negócios e os casos de uso.
  - Implementa as regras de negócio e validações.

- **`src/Ambev.DeveloperEvaluation.Infrastructure/`**
  - Gerencia o acesso ao banco de dados e outras dependências externas.
  - Contém os repositórios e o contexto do Entity Framework.

- **`src/Ambev.DeveloperEvaluation.Domain/`**
  - Define as entidades e interfaces do domínio.
  - Contém as regras de negócio específicas do domínio.

- **`src/Ambev.DeveloperEvaluation.ORM/`**
  - Implementa os repositórios utilizando o Entity Framework Core.

## **Outros Diretórios**

- **`tests/`**
  - Contém os testes unitários e de integração.
  - Utiliza xUnit, Moq e FluentAssertions.

- **`doc/`**
  - Contém a documentação do projeto, incluindo visão geral, pilha de tecnologia e estrutura do projeto.