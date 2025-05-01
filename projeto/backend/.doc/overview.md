# Visão Geral

Este projeto foi desenvolvido como parte de uma avaliação técnica para candidatos a desenvolvedores. Ele tem como objetivo avaliar as seguintes competências:

- **Desenvolvimento Back-End:** Implementação de APIs RESTful utilizando .NET.
- **Modelagem de Dados:** Criação de modelos de dados e mapeamento de entidades.
- **Regras de Negócio:** Implementação de regras de negócio específicas no domínio de vendas.
- **Testes:** Criação de testes unitários e de integração para garantir a qualidade do código.
- **Documentação:** Escrita de documentação clara e objetiva para facilitar a compreensão do projeto.

O projeto utiliza um banco de dados em memória (`InMemoryDb`) para simplificar a execução e os testes.

---

## Objetivos do Projeto

1. Criar uma API para gerenciar vendas, incluindo criação, atualização, consulta e cancelamento.
2. Implementar regras de negócio específicas para descontos e limites de vendas.
3. Registrar eventos importantes, como criação e cancelamento de vendas.
4. Garantir a qualidade do código com testes automatizados.

---

## Estrutura Geral

O projeto está dividido em várias camadas para garantir a separação de responsabilidades:

- **Camada de Aplicação:** Contém a lógica de negócios e os casos de uso.
- **Camada de Infraestrutura:** Gerencia o acesso ao banco de dados e outras dependências externas.
- **Camada de API:** Expõe os endpoints RESTful para interação com o sistema.