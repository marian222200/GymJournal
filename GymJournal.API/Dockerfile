#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GymJournal.API/GymJournal.API.csproj", "GymJournal.API/"]
COPY ["GymJournal.API/GymJournal.Data.csproj", "GymJournal.Data/"]
COPY ["GymJournal.API/GymJournal.Domain.csproj", "GymJournal.Domain/"]
RUN dotnet restore "GymJournal.API/GymJournal.API.csproj"
COPY . .
WORKDIR "/src/GymJournal.API"
RUN dotnet build "GymJournal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymJournal.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymJournal.API.dll"]