# Projeto de estudos da disciplina de backend da pós em desenvolvimento de software

## comandos para executar o projeto
- Baixar as dependências do projeto (que estão mapeadas no arquivo ```APICatalogo.csproj```)
```bash
dotnet restore
```
- Criar Migration geradora de tabelas
```bash
dotnet ef migrations add CriarTabelas
```

- Criar de fato as tabelas no banco de dados
```bash
dotnet ef database update
```