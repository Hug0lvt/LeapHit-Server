FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["server/ApiLeapHit/ApiLeapHit.csproj", "ApiLeapHit/"]
COPY ["server/DTO/DTO.csproj", "DTO/"]
COPY ["server/DataBase/DataBase.csproj", "DataBase/"]
RUN dotnet restore "ApiLeapHit/ApiLeapHit.csproj"
COPY . .
WORKDIR "/src/ApiLeapHit"
RUN dotnet build "ApiLeapHit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiLeapHit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiLeapHit.dll"]