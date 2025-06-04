# Use .NET SDK to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and set working directory to project folder
COPY . .
WORKDIR /src/TaskManagement.Backend

# Restore and publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Use ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (adjust if needed)
EXPOSE 80
ENTRYPOINT ["dotnet", "TaskManagement.API.dll"]
