#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api/smartalert.api/smartalert.api.csproj", "api/smartalert.api/"]
COPY ["api/smartalert.api.implementation/smartalert.api.implementation.csproj", "api/smartalert.api.implementation/"]
COPY ["api/MySqlConnectionFactory/MySqlConnectionFactory.csproj", "api/MySqlConnectionFactory/"]
COPY ["api/smartalert.api.interfaces/smartalert.api.interfaces.csproj", "api/smartalert.api.interfaces/"]
COPY ["api/smartalert.api.datacontracts/smartalert.api.datacontracts.csproj", "api/smartalert.api.datacontracts/"]
COPY ["api/smartalert.api.models/smartalert.api.models.csproj", "api/smartalert.api.models/"]
COPY ["api/smartalert.api.controllers/smartalert.api.controllers.csproj", "api/smartalert.api.controllers/"]
RUN dotnet restore "api/smartalert.api/smartalert.api.csproj"
COPY . .
WORKDIR "/src/api/smartalert.api"
RUN dotnet build "smartalert.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "smartalert.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "smartalert.api.dll"]