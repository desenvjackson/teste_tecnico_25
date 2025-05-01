# README Técnico - Pasta `frontend`

## Visão Geral

A pasta `frontend` contém o código-fonte do frontend do projeto **DeveloperStore**, desenvolvido em **React** com **Material-UI** para a interface de usuário e **React Router** para o gerenciamento de rotas. Este frontend consome uma API REST para gerenciar vendas, clientes e filiais, seguindo princípios de **DDD (Domain-Driven Design)** para organização do código.

---

## Estrutura de Pastas

A estrutura do projeto foi organizada para seguir o padrão **DDD**, separando responsabilidades em camadas distintas:

```plaintext
frontend/
├── application/          # Camada de aplicação
│   ├── services/         # Serviços que lidam com a lógica de comunicação com a API
│   │   └── saleService.js
├── domain/               # Camada de domínio
│   ├── models/           # Modelos de domínio que encapsulam a lógica de negócios
│   │   └── saleModel.js
├── infrastructure/       # Camada de infraestrutura
│   ├── api/              # Configuração do cliente HTTP
│   │   └── apiClient.js
├── presentation/         # Camada de apresentação
│   ├── pages/            # Páginas React que compõem a interface do usuário
│   │   ├── SalesList.js  # Página para listar vendas
│   │   └── SaleForm.js   # Página para criar/editar vendas
├── App.js                # Configuração principal de rotas
├── index.js              # Ponto de entrada do React
```

---

## Tecnologias Utilizadas

- **React**: Biblioteca para construção de interfaces de usuário.
- **Material-UI**: Biblioteca de componentes para estilização moderna e responsiva.
- **React Router**: Gerenciamento de rotas no frontend.
- **Axios**: Cliente HTTP para comunicação com a API.
- **DDD (Domain-Driven Design)**: Padrão de arquitetura para organizar o código em camadas.

---

## Funcionalidades

### 1. **Listagem de Vendas**
- Página: `SalesList.js`
- Exibe uma lista de vendas em cards organizados em um layout responsivo.
- Cada card contém:
  - Informações da venda (cliente, filial, data, valor total).
  - Botão para editar a venda.
  - Botão para excluir a venda.
  - Opção para expandir/ocultar os itens da venda.

### 2. **Criação/Edição de Vendas**
- Página: `SaleForm.js`
- Permite criar uma nova venda ou editar uma venda existente.
- Funcionalidades:
  - Seleção de cliente e filial.
  - Adição, edição e remoção de itens da venda.
  - Validação de campos obrigatórios.
  - Envio de dados para a API.

### 3. **Exclusão de Vendas**
- Endpoint: `/api/Sales/{id}`
- Botão de exclusão em cada card na página de listagem.
- Confirmação antes de excluir.
- Atualização automática da lista após exclusão.

---

## Configuração do Projeto

### Pré-requisitos
- **Node.js** (versão 14 ou superior)
- **npm** ou **yarn**

### Instalação
1. Navegue até a pasta `frontend`:
   ```bash
   cd frontend
   ```

2. Instale as dependências:
   ```bash
   npm install
   # ou
   yarn install
   ```

3. Inicie o servidor de desenvolvimento:
   ```bash
   npm start
   # ou
   yarn start
   ```

4. Acesse o frontend no navegador:
   ```
   http://localhost:3000
   ```

---

## Configuração da API

O frontend consome uma API REST configurada no arquivo `apiClient.js`. Certifique-se de que a URL base da API está correta:

```javascript
// filepath: /src/infrastructure/api/apiClient.js
const apiClient = axios.create({
  baseURL: "http://localhost:5188/api", // Substitua pela URL da sua API
  headers: {
    "Content-Type": "application/json",
  },
});
```

---

## Principais Arquivos

### 1. **`SalesList.js`**
- Local: `/src/presentation/pages/SalesList.js`
- Responsável por exibir a lista de vendas.
- Principais funcionalidades:
  - Listagem de vendas em cards.
  - Botões para editar e excluir vendas.
  - Expansão/ocultação de itens da venda.

### 2. **`SaleForm.js`**
- Local: `/src/presentation/pages/SaleForm.js`
- Responsável por criar ou editar vendas.
- Principais funcionalidades:
  - Formulário para cliente, filial e itens.
  - Adição e remoção de itens.
  - Envio de dados para a API.

### 3. **`saleService.js`**
- Local: `/src/application/services/saleService.js`
- Contém funções para comunicação com a API:
  - `getSaleById`: Busca uma venda pelo ID.
  - `saveSale`: Cria ou atualiza uma venda.
  - `deleteSale`: Exclui uma venda.

### 4. **`saleModel.js`**
- Local: `/src/domain/models/saleModel.js`
- Modelo de domínio para a entidade `Sale`.
- Encapsula a lógica de negócios, como adicionar, remover ou atualizar itens.

---

## Padrão de Arquitetura

O projeto segue o padrão **DDD (Domain-Driven Design)**, com as seguintes camadas:

1. **Domain (Domínio)**:
   - Contém os modelos de domínio que encapsulam a lógica de negócios.
   - Exemplo: `saleModel.js`.

2. **Application (Aplicação)**:
   - Contém os serviços que lidam com a lógica de comunicação com a API.
   - Exemplo: `saleService.js`.

3. **Infrastructure (Infraestrutura)**:
   - Contém a configuração do cliente HTTP e outras dependências externas.
   - Exemplo: `apiClient.js`.

4. **Presentation (Apresentação)**:
   - Contém os componentes React que compõem a interface do usuário.
   - Exemplo: `SalesList.js`, `SaleForm.js`.

---

## Melhorias Futuras

1. **Validação de Formulários**:
   - Adicionar validações mais robustas no formulário de vendas.

2. **Paginação e Filtros**:
   - Implementar paginação e filtros na listagem de vendas.

3. **Testes Automatizados**:
   - Adicionar testes unitários e de integração para as camadas de domínio e aplicação.

4. **Autenticação**:
   - Implementar autenticação para proteger as rotas do frontend.

---

## Contribuição

1. Faça um fork do repositório.
2. Crie uma branch para sua feature:
   ```bash
   git checkout -b minha-feature
   ```
3. Faça commit das suas alterações:
   ```bash
   git commit -m "Adiciona minha feature"
   ```
4. Envie para o repositório remoto:
   ```bash
   git push origin minha-feature
   ```
5. Abra um Pull Request.

---

## Contato

Para dúvidas ou sugestões, entre em contato com o time de desenvolvimento.

- **Email**: suporte@developerstore.com
- **Slack**: #developerstore-frontend

---

Este README fornece uma visão geral técnica do projeto frontend e serve como guia para desenvolvedores que desejam contribuir ou entender a arquitetura do sistema.