#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservicos/DepartamentoContabil/Livros/RegistroEntradas/RegistroEntradas.csproj", "Microservicos/DepartamentoContabil/Livros/RegistroEntradas/"]
RUN dotnet restore "Microservicos/DepartamentoContabil/Livros/RegistroEntradas/RegistroEntradas.csproj"
COPY . .
WORKDIR "/src/Microservicos/DepartamentoContabil/Livros/RegistroEntradas"
RUN dotnet build "RegistroEntradas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RegistroEntradas.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegistroEntradas.dll"]