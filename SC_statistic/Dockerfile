#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SC_statistic/SC_statistic.csproj", "SC_statistic/"]
COPY ["SC_statistic.DataLayer/SC_statistic.DataLayer.csproj", "SC_statistic.DataLayer/"]
COPY ["SC_statistic.Services/SC_statistic.Services.csproj", "SC_statistic.Services/"]
COPY ["SC_statistic.DataAccessLayer/SC_statistic.DataAccessLayer.csproj", "SC_statistic.DataAccessLayer/"]
RUN dotnet restore "SC_statistic/SC_statistic.csproj"
COPY . .
WORKDIR "/src/SC_statistic"
RUN dotnet build "SC_statistic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SC_statistic.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SC_statistic.dll"]