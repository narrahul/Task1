# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Create directories for file uploads
RUN mkdir -p wwwroot/uploads wwwroot/profile-images

# Expose port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "MedicalRecordAPI.dll"]