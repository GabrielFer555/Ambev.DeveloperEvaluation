# 🚀 Projeto Ambev.DeveloperEvaluation  

O **Ambev.DeveloperEvaluation** é um desafio técnico criado para avaliar os conhecimentos em **.NET** e **EF Core API**. 🎯  
Originalmente voltado para **desenvolvedores seniores**, o desafio foi adaptado para testar as habilidades de **devs juniores** ingressando nos projetos da Ambev Tech.  

O projeto consiste no desenvolvimento de uma API inspirada em um **e-commerce**, com funcionalidades para **carrinho**, **pedido**, **produto** e **usuário**. 🛒📦  

---

## 🛠️ Tecnologias Utilizadas  

As principais tecnologias e práticas adotadas no projeto são:  

- ⚙️ **.NET 8**  
- 🐘 **PostgreSQL**  
- 📋 **Entity Framework Core (EF Core)** como ORM para manipulação de dados  
- 🧩 **Domain-Driven Design (DDD)** para modelagem de entidades  
- ✍️ **Code-First Approach** para criação e gerenciamento do banco de dados  

---

## 📋 Pré-requisitos  

Antes de executar o projeto, você precisará de:  

1. 🖥️ **Visual Studio** instalado na sua máquina  
2. 🐘 Um banco de dados **PostgreSQL** configurado localmente (de preferência sem tabelas existentes)  

---

## 🚀 Como Executar o Projeto  

Siga os passos abaixo para configurar e rodar o projeto:  

1. **Clone o repositório**  
   Use o **Visual Studio** para clonar o projeto localmente.  

2. **Configure o banco de dados**  
   - Acesse a pasta: **`Core > Application > Drivers > WebApi`**  
   - Edite o arquivo **`appsettings.json`** e atualize a string de conexão conforme o ambiente:  

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server={servidor};Database={nome-do-banco};User Id={usuário};Password={senha};TrustServerCertificate=True"
     }
     ```  

3. **Defina o projeto de inicialização**  
   - No **Visual Studio**, defina o projeto **`Ambev.DeveloperEvaluation.WebApi`** como o projeto de inicialização.  

4. **Execute a aplicação**  
   - Inicie o projeto no modo **HTTPS**.  
   - Acesse as rotas da API diretamente no **Swagger**, que será exibido automaticamente no navegador. 🧭  

---

## 📖 Documentação  

- A API utiliza o **Swagger** para expor e documentar suas rotas.  
- Certifique-se de iniciar o projeto no modo **HTTPS** para acessar a documentação de forma visual.  

---

## ⚠️ Pontos de Atenção  
- 🧪 **Testes Unitários**:  
  Testes unitários **ainda não foram implementados**, devido o desenvolvedor ainda estar aprendendo sobre.  

---

## 🔧 Melhorias Futuras  
Sugestões para melhorias no projeto:  
1. 🧪 Desenvolver **testes unitários** para validar o comportamento das APIs.
2. ❌ Alterar lógica para permitir cancelamento de items

---

## 👨‍💻 Autor  

Agradecimento especial a Nathan, Rennã, Lucas e Róger pelo suporte prestado durante o desenvolvimento desse projeto.

💡 **Contribuições e feedbacks são muito bem-vindos!** 🚀  
