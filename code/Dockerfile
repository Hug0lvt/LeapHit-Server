FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Première étape de la construction
WORKDIR /app/server
RUN dotnet new console
COPY server/Server/Program.cs Program.cs
RUN dotnet publish -c Release -o out

# Deuxième étape de la construction
WORKDIR /app/api
COPY server/ApiLeapHit/ApiLeapHit.csproj .
COPY server/DTO/DTO.csproj DTO/
COPY server/DataBase/DataBase.csproj DataBase/
RUN dotnet restore "ApiLeapHit.csproj"
COPY . .
WORKDIR /app/api/ApiLeapHit
RUN dotnet build "ApiLeapHit.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ApiLeapHit.csproj" -c Release -o /app/publish

# Troisième étape de la construction
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/server/out .
COPY --from=build /app/api/publish .

# Configuration de l'application
EXPOSE 80
EXPOSE 443
EXPOSE 3131
ENTRYPOINT ["dotnet", "app.dll"]

