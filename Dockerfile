FROM mcr.microsoft.com/dotnet/sdk:6.0-ubuntu as build
WORKDIR /src/Api
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish src/Api -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src/Api
COPY --from=build /src/Api/out .
ENTRYPOINT ["dotnet", "Api.dll"]