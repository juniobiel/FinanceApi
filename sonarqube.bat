dotnet sonarscanner begin /k:"finance-api" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="sqp_702e5c887245152719822687cecb17c4fd6037ee" /d:sonar.cs.opencover.reportsPaths=coverage.xml
dotnet build --no-incremental
coverlet .\src\UnitTests\bin\Debug\net6.0\UnitTests.dll --target "dotnet" --targetargs "test --no-build" -f=opencover -o="coverage.xml"
dotnet sonarscanner end /d:sonar.login="sqp_702e5c887245152719822687cecb17c4fd6037ee"