FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR ./
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish ./Finance.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR ./
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Api.dll"]