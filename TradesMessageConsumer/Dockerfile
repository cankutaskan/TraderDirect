# Use the official .NET 8 runtime image for the base layer
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Use the .NET 8 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies
COPY ["TradesMessageConsumer/TradesMessageConsumer.csproj", "TradesMessageConsumer/"]
RUN dotnet restore "TradesMessageConsumer/TradesMessageConsumer.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/TradesMessageConsumer"

# Build the application in Release mode
RUN dotnet build "TradesMessageConsumer.csproj" -c Release -o /app/build

# Publish the application as a self-contained package
FROM build AS publish
RUN dotnet publish "TradesMessageConsumer.csproj" -c Release -o /app/publish

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entry point to run the application
ENTRYPOINT ["dotnet", "TradesMessageConsumer.dll"]
