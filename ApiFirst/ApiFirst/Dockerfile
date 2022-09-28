#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiFirst/ApiFirst.csproj", "ApiFirst/"]
COPY ["AppServices/AppServices.csproj", "AppServices/"]
COPY ["AppModels/AppModels.csproj", "AppModels/"]
COPY ["DomainServices/DomainServices.csproj", "DomainServices/"]
COPY ["DomainModels/DomainModels.csproj", "DomainModels/"]
RUN dotnet restore "ApiFirst/ApiFirst.csproj"
COPY . .
WORKDIR "/src/ApiFirst"
RUN dotnet build "ApiFirst.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiFirst.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiFirst.dll"]