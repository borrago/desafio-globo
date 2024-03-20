# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia os arquivos de projeto para o diretório de trabalho
COPY src/Comentarios.API/Comentarios.API.csproj src/Comentarios.API/

# Restaura as dependências dos projetos
RUN dotnet restore src/Comentarios.API/Comentarios.API.csproj

# Copia todo o código-fonte para o diretório de trabalho
COPY . ./

# Publica o projeto principal
RUN dotnet publish src/Comentarios.API/Comentarios.API.csproj -c Release -o out

# Define a imagem base para a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia os arquivos publicados para o diretório de trabalho
COPY --from=build /app/out ./

# Define o comando de inicialização da aplicação
ENTRYPOINT ["dotnet", "Comentarios.API.dll"]
