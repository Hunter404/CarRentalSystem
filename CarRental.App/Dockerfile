FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarRental.App/CarRental.App.csproj", "CarRental.App/"]
RUN dotnet restore "CarRental.App/CarRental.App.csproj"
COPY . .
WORKDIR "/src/CarRental.App"
RUN dotnet build "CarRental.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRental.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.App.dll"]
