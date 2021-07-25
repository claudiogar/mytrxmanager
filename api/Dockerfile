# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
ENV ASPNETCORE_URLS=http://+:1234
EXPOSE 1234/tcp
EXPOSE 1235/tcp


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN dotnet tool install --global dotnet-ef
COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "api.dll"]
