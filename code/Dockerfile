FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build1
WORKDIR /app/server
RUN dotnet new console
COPY server/Server/Program.cs Program.cs
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS build2
WORKDIR /app/api
COPY ["server/ApiLeapHit/ApiLeapHit.csproj", "ApiLeapHit/"]
COPY ["server/DTO/DTO.csproj", "DTO/"]
COPY ["server/DataBase/DataBase.csproj", "DataBase/"]
RUN dotnet restore "server/ApiLeapHit/ApiLeapHit.csproj"
COPY . .
WORKDIR "/src/ApiLeapHit"
RUN dotnet build "ApiLeapHit.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build1 /app/server/build .
COPY --from=build2 /app/api/publish .

ENTRYPOINT ["dotnet", "ApiLeapHit.dll"]


