# Youtube Channel Videos List
Robô em Console Application que lê canais do Youtube e baixa os links dos vídeos, bem como a thumbnail e informações disponíveis, salvando as informações em um banco de dados

## Tecnologias utilizadas
- .Net Core 6.0
- C# 10
- MySql
- Repository Pattern

## Configurações necessárias antes de rodar o projeto
- Altere a API Key do youtube no arquivo *appSettings.json*, na chave *Youtube/APIKey*
- Altere as informações de versão do MySql no arquivo *appSettings.json*, na chave *MySql* com informações *Major*, *Minor* e *Build*

## Criação do banco
Para criar o banco da API, siga os seguintes passos:
- Crie um banco vazio no MySql
- Altere a string de conexão no arquivo *appSettings.json*
- Comente as linhas 17 a 20 do arquivo *YoutubeDataContext.cs*
- Descomente as linhas 11 a 14 do arquivo *YoutubeDataContext.cs*
- No seu terminal, rode o comando `update-database`
- Descomente as linhas 17 a 20 do arquivo *YoutubeDataContext.cs*
- Comente as linhas 11 a 14 do arquivo *YoutubeDataContext.cs*

## Servidor de desenvolvimento
Para rodar local, rode o comando `dotnet run` no seu terminal ou clique em "Run" no Visual Studio. Uma tela do prompt deverá ser iniciada logando o que está sendo executado.
