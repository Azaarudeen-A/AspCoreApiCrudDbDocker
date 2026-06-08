# Base runtime image for the final container.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

# Set the application working directory inside the container.
WORKDIR /app

# Expose the HTTP port used by the API.
EXPOSE 5080

# Configure ASP.NET Core to listen on port 5080.
ENV ASPNETCORE_URLS=http://+:5080

# Run the app as a non-root user for better security.
USER app

# Build image using the .NET SDK.
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Default build configuration.
ARG configuration=Release

# Set the source directory for build steps.
WORKDIR /src

# Copy the project file first to restore dependencies efficiently.
COPY ["AspCoreApiCrudDbDocker/AspCoreApiCrudDbDocker.csproj", "AspCoreApiCrudDbDocker/"]

# Restore NuGet packages for the application.
RUN dotnet restore "AspCoreApiCrudDbDocker/AspCoreApiCrudDbDocker.csproj"

# Copy the full source tree into the build context.
COPY . .

# Move into the project folder for the build commands.
WORKDIR "/src/AspCoreApiCrudDbDocker"

# Build the app in the selected configuration.
RUN dotnet build "AspCoreApiCrudDbDocker.csproj" -c $configuration -o /app/build

# Publish stage to generate the deployable output.
FROM build AS publish

# Default build configuration for publishing.
ARG configuration=Release

# Publish the app to the output folder without an app host.
RUN dotnet publish "AspCoreApiCrudDbDocker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final runtime image.
FROM base AS final

# Set the runtime working directory.
WORKDIR /app

# Copy the published output from the publish stage.
COPY --from=publish /app/publish .

# Start the API when the container launches.
ENTRYPOINT ["dotnet", "AspCoreApiCrudDbDocker.dll"]
