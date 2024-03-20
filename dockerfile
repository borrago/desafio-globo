# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diret�rio de trabalho dentro do cont�iner
WORKDIR /app

# Copia os arquivos de projeto para o diret�rio de trabalho
COPY src/Comentarios.API/Comentarios.API.csproj src/Comentarios.API/

# Restaura as depend�ncias dos projetos
RUN dotnet restore src/Comentarios.API/Comentarios.API.csproj

# Copia todo o c�digo-fonte para o diret�rio de trabalho
COPY . ./

# Publica o projeto principal
RUN dotnet publish src/Comentarios.API/Comentarios.API.csproj -c Release -o out

# Define a imagem base para a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diret�rio de trabalho dentro do cont�iner
WORKDIR /app

# Copia os arquivos publicados para o diret�rio de trabalho
COPY --from=build /app/out ./

# Define o comando de inicializa��o da aplica��o
ENTRYPOINT ["dotnet", "Comentarios.API.dll"]
