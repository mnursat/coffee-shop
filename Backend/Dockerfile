# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS Build

WORKDIR /src

# Restore
COPY ["src/CoffeeShop/CoffeeShop.csproj", "CoffeeShop/"]
RUN dotnet restore "CoffeeShop/CoffeeShop.csproj"

# Build
COPY ["src/CoffeeShop", "CoffeeShop/"]
WORKDIR /src/CoffeeShop
RUN dotnet build "CoffeeShop.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "CoffeeShop.csproj" -c Release -o /app/publish

# Stage 3: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeShop.dll"]