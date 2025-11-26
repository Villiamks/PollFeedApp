# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["WebFront/WebFront.csproj", "WebFront/"]
COPY ["ClassLibrary/ClassLibrary.csproj", "ClassLibrary/"]
RUN dotnet restore "WebFront/WebFront.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/WebFront"
RUN dotnet build "WebFront.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebFront.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published files
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebFront.dll"]
