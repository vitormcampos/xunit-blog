# Usa a imagem oficial do .NET SDK como build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos .csproj de todos os projetos para restaurar as dependências 
COPY XUnitBlog.Domain/*.csproj XUnitBlog.Domain/
COPY XUnitBlog.Data/*.csproj XUnitBlog.Data/
COPY XUnitBlog.App/*.csproj XUnitBlog.App/

# Restaura as dependências
RUN dotnet restore XUnitBlog.App/XUnitBlog.App.csproj

# Copia todo o código restante para o contêiner
COPY ../ .

# Publica a aplicação (gera binários e dependências)
RUN dotnet publish XUnitBlog.App/XUnitBlog.App.csproj -c Release -o /app/out

# Usa a imagem oficial do .NET runtime como runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .  

# Expõe a porta 2000 e inicia a aplicação
EXPOSE 2000 
ENTRYPOINT ["dotnet", "XUnitBlog.App.dll"]