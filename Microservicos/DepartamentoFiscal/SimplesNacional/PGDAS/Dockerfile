#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservicos/DepartamentoFiscal/SimplesNacional/PGDAS/PGDAS.csproj", "Microservicos/DepartamentoFiscal/SimplesNacional/PGDAS/"]
RUN dotnet restore "Microservicos/DepartamentoFiscal/SimplesNacional/PGDAS/PGDAS.csproj"
COPY . .
WORKDIR "/src/Microservicos/DepartamentoFiscal/SimplesNacional/PGDAS"
RUN dotnet build "PGDAS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PGDAS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PGDAS.dll"]