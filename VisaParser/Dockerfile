FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY VisaParser.sln ./
COPY VisaParser/VisaParser.csproj VisaParser/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/VisaParser
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VisaParser.dll"]
