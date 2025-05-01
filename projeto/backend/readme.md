# Projeto de Avaliação de Desenvolvedores

## Instruções para Configuração e Execução

### Pré-requisitos

- .NET 8.0 SDK instalado
- Banco de dados configurado (opcional, usa `InMemoryDb` por padrão)

### Passos para Executar

1. Clone o repositório:

   git clone <URL_DO_REPOSITORIO>
   cd <NOME_DO_REPOSITORIO>

2. Navegue até o diretório do backend:

   cd src/backend

3. Limpe e compile o projeto:

   dotnet clean Ambev.DeveloperEvaluation.sln
   dotnet build Ambev.DeveloperEvaluation.sln

4. Execute o projeto:

   dotnet run --project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj

5. Acesse o Swagger para testar os endpoints:
   - URL: [https://localhost:5188/swagger/index.html](https://localhost:5188/swagger/index.html)

---

## Endpoints da API

### **1. Criar Venda**

- **URL:** `POST /api/sales`
- **Descrição:** Cria uma nova venda.
- **Exemplo de Requisição:**
  ```json
  {
    "clientId": "12345",
    "branch": "Filial A",
    "items": [
      {
        "productId": "98765",
        "quantity": 5,
        "unitPrice": 10.0,
        "discount": 0.1
      }
    ]
  }
  ```
- **Exemplo de Resposta:**
  ```json
  {
    "saleId": "abc123",
    "totalValue": 45.0,
    "status": "Created"
  }
  ```

---

### **2. Buscar Venda por ID**

- **URL:** `GET /api/sales/{id}`
- **Descrição:** Retorna os detalhes de uma venda específica.
- **Exemplo de Resposta:**
  ```json
  {
    "saleId": "abc123",
    "clientId": "12345",
    "branch": "Filial A",
    "items": [
      {
        "productId": "98765",
        "quantity": 5,
        "unitPrice": 10.0,
        "discount": 0.1,
        "totalValue": 45.0
      }
    ],
    "totalValue": 45.0,
    "status": "Created"
  }
  ```

---

### **3. Atualizar Venda**

- **URL:** `PUT /api/sales/{id}`
- **Descrição:** Atualiza os detalhes de uma venda existente.
- **Exemplo de Requisição:**
  ```json
  {
    "branch": "Filial B",
    "items": [
      {
        "productId": "98765",
        "quantity": 10,
        "unitPrice": 10.0,
        "discount": 0.2
      }
    ]
  }
  ```
- **Exemplo de Resposta:**
  ```json
  {
    "saleId": "abc123",
    "totalValue": 80.0,
    "status": "Updated"
  }
  ```

---

### **4. Cancelar Venda**

- **URL:** `DELETE /api/sales/{id}`
- **Descrição:** Cancela uma venda existente.
- **Exemplo de Resposta:**
  ```json
  {
    "saleId": "abc123",
    "status": "Cancelled"
  }
  ```

---

## Regras de Negócio

- Compras acima de 4 itens idênticos têm 10% de desconto.
- Compras entre 10 e 20 itens idênticos têm 20% de desconto.
- Não é possível vender mais de 20 itens idênticos.
- Compras abaixo de 4 itens não podem ter desconto.

---

## Eventos Registrados

- `VendaCriada`
- `VendaModificada`
- `VendaCancelada`
- `ItemCancelado`

---

## Visão Geral

Esta seção fornece uma visão geral do projeto e das diversas habilidades e competências que ele visa avaliar nos candidatos a desenvolvedores.

Consulte [Visão Geral](/.doc/overview.md)

---

## Pilha de Tecnologia

Esta seção lista as principais tecnologias usadas no projeto, incluindo os componentes de back-end, testes, front-end e banco de dados.

Consulte [Tech Stack](/.doc/tech-stack.md)

---

## Frameworks

Esta seção descreve os frameworks e bibliotecas utilizados no projeto para aprimorar a produtividade e a manutenibilidade do desenvolvimento.

Consulte [Frameworks](/.doc/frameworks.md)

---

## Estrutura do Projeto

Esta seção descreve a estrutura geral e a organização dos arquivos e diretórios do projeto.

Veja [Estrutura do Projeto](/.doc/project-structure.md)
