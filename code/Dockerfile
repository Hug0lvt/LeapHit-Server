FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /server

COPY ./code/server /server

CMD dotnet run -p /server/Server.csproj

EXPOSE 3131
