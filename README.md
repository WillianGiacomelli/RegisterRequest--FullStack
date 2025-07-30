# Sistema de Cadastro de Pedidos (Mini-Projeto Full Stack)

Este é um mini-projeto que demonstra a construção de uma aplicação web Full Stack do zero. A aplicação consiste em um formulário para criar e carregar pedidos de venda, utilizando tecnologias modernas no frontend e no backend.

## ✨ Funcionalidades

-   **Criação de Pedidos**: Formulário para inserir dados do cabeçalho do pedido (datas, observação).
-   **Adição de Itens**: Adicionar dinamicamente itens ao pedido (código, descrição, quantidade e valor).
-   **Cálculo em Tempo Real**: O valor total de cada item e do pedido geral é calculado automaticamente.
-   **Remoção de Itens**: Remover itens da lista antes de salvar o pedido.
-   **Persistência de Dados**: Salvar o pedido completo (cabeçalho e itens) em um banco de dados MySQL através de uma API RESTful.
-   **Carregamento de Dados**: Carregar o último pedido salvo no banco de dados e exibi-lo no formulário.

## 🛠️ Tecnologias Utilizadas

Este projeto é dividido em duas partes principais:

#### **Frontend (Interface do Usuário)**

-   **HTML5**: Estrutura da página.
-   **CSS3**: Estilização customizada.
-   **Bootstrap 5**: Framework CSS para layout responsivo e componentes pré-estilizados.
-   **JavaScript (ES6+)**: Manipulação do DOM e lógica da interface.
-   **jQuery**: Simplificação da manipulação do DOM e chamadas AJAX.

#### **Backend (API e Lógica de Negócios)**

-   **C# e .NET 8**: Plataforma e linguagem para a construção da API.
-   **ASP.NET Core Web API**: Framework para criar a API RESTful.
-   **Entity Framework Core 8**: ORM para mapear os objetos C# para o banco de dados.

#### **Banco de Dados e Ambiente**

-   **MySQL 8.0**: Banco de dados relacional para armazenar os dados dos pedidos.
-   **Docker / Docker Compose**: Para criar e gerenciar o container do banco de dados MySQL de forma isolada e consistente.

## Visualização do Projeto

![image](/frontend/assets/img/execution.png)

## 🚀 Como Executar o Projeto

Para rodar este projeto em sua máquina local, siga os passos abaixo.

### **Pré-requisitos**

Antes de começar, certifique-se de que você tem os seguintes softwares instalados:
-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior.
-   [Docker Desktop](https://www.docker.com/products/docker-desktop).
-   Um editor de código como [Visual Studio Code](https://code.visualstudio.com/).
-   [Git](https://git-scm.com/downloads) para clonar o repositório (opcional).

### **Passo 1: Obter o Projeto**

Clone o repositório ou baixe os arquivos para uma pasta em sua máquina.

### **Passo 2: Iniciar o Banco de Dados com Docker**

O arquivo `docker-compose.yml` na raiz do projeto irá configurar e iniciar um container com o banco de dados MySQL. Em um terminal, navegue até a raiz do projeto e execute:

```bash
docker-compose up -d
```

### **Passo 3: Configurar e Rodar o Backend (API)**

3.1 Navegue até a pasta do projeto da API em um novo terminal:
```bash
cd backend/requestAPI
```

3.2 Restaure os pacotes NuGet necessários:
```bash
dotnet restore
```

3.3 Aplique as migrations para criar as tabelas no banco de dados. O EF Core se conectará ao banco de dados no Docker e criará a estrutura necessária.
```bash
dotnet ef database update
```

3.4 Inicie a API
```bash
dotnet run
```

3.5 O terminal mostrará a URL HTTPS em que a API está rodando em (ex: https://localhost:5080). Anote esta url

### **Passo 4: Configurar e Abrir o Frontend**

4.1 Abra o arquivo frontend/js/script.js no seu editor de código. <br>

4.2 Localize a variável apiUrl no topo do arquivo e substitua o valor pela URL HTTPS exata da sua API que você anotou no passo anterior.
```bash
// Exemplo:
const apiUrl = 'https://localhost:7146/api/Pedidos'; // <-- ATUALIZE ESTA LINHA!
```
4.3 - Finalmente, abra o arquivo frontend/index.html diretamente no seu navegador de preferência. Para uma melhor experiência de desenvolvimento, utilize a extensão "Live Server" do Visual Studio Code.

### **📁 Estrutura do Projeto**
```
├── backend/
│   ├── requestAPI.sln
│   ├── .gitignore
│   └── requestAPI/
│       ├── Controllers/
│       │   └── RequestController.cs
│       ├── Data/
│       │   └── RequestContext.cs
│       ├── Interfaces/
│       │   ├── IRequestRepository.cs
│       │   └── IRequestService.cs
│       ├── Models/
│       │   ├── Base.cs
│       │   ├── Request.cs
│       │   └── RequestItem.cs
│       ├── Repositories/
│       │   └── RequestRepository.cs
│       ├── Services/
│       │   └── RequestService.cs
│       ├── appsettings.json
│       ├── Program.cs
│       └── requestAPI.csproj
│
├── frontend/
│   ├── assets/
│   │   ├── css/
│   │   │   └── style.css
│   │   ├── js/
│   │   │   └── script.js
│   │   └── img/
│   │       └── execution.png
│   └── index.html
│
├── docker-compose.yml
└── README.md       
```   

### **📁 Estrutura do Projeto**
A API expõe os seguintes endpoints:

- POST /api/Pedidos: Cria um novo pedido. Espera um JSON no corpo da requisição com os dados do pedido e seus itens.

- GET /api/Pedidos/ultimo: Retorna o último pedido que foi inserido no banco de dados.