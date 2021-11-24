# Youtube Channel Videos List
Robô em Console Application que lê canais do Youtube e baixa os links dos vídeos, bem como a thumbnail e informações disponíveis, salvando as informações em um banco de dados

## Tecnologias utilizadas
- .Net Core 6.0
- MySql
- Repository Pattern

## Configurações necessárias antes de rodar o projeto
- Altere a API Key do youtube no arquivo *appSettings.json*, na chave *Youtube/APIKey*

## Criação do banco
Para criar o banco da API, siga os seguintes passos:
- Crie um banco vazio no MySql
- Altere a string de conexão no arquivo *appSettings.json*
- No seu terminal, rode o comando `update-database`

## Servidor de desenvolvimento
Para rodar local, rode o comando `dotnet run` no seu terminal ou clique em "Run" no Visual Studio. Navegue para a url `http://localhost:3000/`.
