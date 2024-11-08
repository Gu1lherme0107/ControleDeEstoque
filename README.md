# Sistema de Inventário com SQLite

Este projeto implementa um **Sistema de Inventário** simples utilizando o banco de dados **SQLite** para o gerenciamento de produtos. O sistema permite adicionar, atualizar, remover e listar produtos em um inventário de forma interativa no console.

## Funcionalidades

- **Adicionar Produto**: Permite adicionar um novo produto ao inventário com um nome e quantidade definidos.
- **Atualizar Produto**: Atualiza as informações de um produto existente, como nome e quantidade.
- **Remover Produto**: Remove um produto do inventário utilizando seu ID.
- **Listar Produtos**: Exibe todos os produtos cadastrados no inventário com seus respectivos IDs, nomes e quantidades.
- **Persistência de Dados**: Utiliza o SQLite para armazenar as informações de produtos de maneira simples e eficiente.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal utilizada para o desenvolvimento do sistema.
- **SQLite**: Banco de dados leve utilizado para armazenar as informações do inventário.
- **Console**: Interface de usuário simples e interativa via console para a manipulação dos dados.

## Requisitos para Execução

Para rodar este programa no seu computador, você precisará garantir que os seguintes requisitos estejam atendidos:

1. **.NET Framework ou .NET Core**:
   - O programa é desenvolvido em C#, portanto, é necessário ter o **.NET** instalado. Você pode baixar o SDK mais recente do .NET [aqui](https://dotnet.microsoft.com/download).

2. **SQLite**:
   - O programa utiliza o banco de dados **SQLite**, que é embutido no projeto. Não é necessário instalar o SQLite manualmente, pois ele é gerenciado diretamente pela aplicação via o pacote `System.Data.SQLite` do NuGet.
   
3. **Editor de Código/IDE**:
   - Recomendamos o uso do [Visual Studio](https://visualstudio.microsoft.com/) ou qualquer editor que suporte C#, como o [VS Code](https://code.visualstudio.com/) com a extensão C#.

4. **Sistema Operacional**:
   - O programa foi desenvolvido para ser executado em sistemas operacionais Windows. Para rodar em outros sistemas operacionais (Linux, macOS), é necessário garantir que o .NET e o SQLite estejam configurados corretamente no seu sistema.

## Como Usar

1. **Clonar o repositório**:
   ```bash
   git clone https://github.com/Gu1lherme0107/ControleDeEstoque.git
