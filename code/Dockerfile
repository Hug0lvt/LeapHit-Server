FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Première étape de la construction
WORKDIR /app/server
RUN dotnet new console
COPY Server/Program.cs Program.cs
RUN dotnet publish -c Release -o out

# Deuxième étape de la construction
WORKDIR /app/api
COPY ApiLeapHit/ApiLeapHit.csproj .
COPY DTO/DTO.csproj DTO/
COPY DataBase/DataBase.csproj DataBase/
RUN dotnet restore
COPY . .
WORKDIR /app/api/ApiLeapHit
RUN dotnet build -c Release -o /app/build

# Troisième étape de la construction
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/server/out .
COPY --from=build /app/api/build .

# Configuration de l'application
EXPOSE 80
EXPOSE 443
EXPOSE 3131
ENTRYPOINT ["dotnet", "ApiLeapHit.dll"]

