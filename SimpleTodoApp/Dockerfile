﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleTodoApp/SimpleTodoApp.csproj", "SimpleTodoApp/"]
RUN dotnet restore "SimpleTodoApp/SimpleTodoApp.csproj"
COPY . .
WORKDIR "/src/SimpleTodoApp"
RUN dotnet build "SimpleTodoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleTodoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleTodoApp.dll"]
