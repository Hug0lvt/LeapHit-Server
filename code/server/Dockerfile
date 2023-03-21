FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiLeapHit/ApiLeapHit.csproj", "ApiLeapHit/"]
COPY ["DTO/DTO.csproj", "DTO/"]
COPY ["DataBase/DataBase.csproj", "DataBase/"]
RUN dotnet restore "ApiLeapHit/ApiLeapHit.csproj"
COPY . .
WORKDIR "/src/ApiLeapHit"
RUN dotnet build "ApiLeapHit.csproj" -c Release -o /app/build

# Utilise l'image ASP.NET Core 6 pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Installation du client MySQL pour .NET Core
RUN apt-get update && apt-get install -y mysql-client && apt-get clean

# Copie les fichiers de l'application dans l'image et définit l'entrée pour lancer l'application
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ApiLeapHit.dll"]