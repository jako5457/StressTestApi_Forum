#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StressTestApi_Forum/StressTestApi_Forum.csproj", "StressTestApi_Forum/"]
RUN dotnet restore "StressTestApi_Forum/StressTestApi_Forum.csproj"
COPY . .
WORKDIR "/src/StressTestApi_Forum"
RUN dotnet build "StressTestApi_Forum.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StressTestApi_Forum.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StressTestApi_Forum.dll"]