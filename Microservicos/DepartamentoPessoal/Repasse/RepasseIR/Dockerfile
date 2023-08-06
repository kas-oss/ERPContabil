#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservicos/DepartamentoPessoal/Repasse/RepasseIR/RepasseIR.csproj", "Microservicos/DepartamentoPessoal/Repasse/RepasseIR/"]
RUN dotnet restore "Microservicos/DepartamentoPessoal/Repasse/RepasseIR/RepasseIR.csproj"
COPY . .
WORKDIR "/src/Microservicos/DepartamentoPessoal/Repasse/RepasseIR"
RUN dotnet build "RepasseIR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RepasseIR.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RepasseIR.dll"]