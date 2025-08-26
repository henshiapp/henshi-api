# ---- Build stage ------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY *.sln ./
COPY Henshi.Flashcards/*.csproj Henshi.Flashcards/
COPY Henshi.Shared/*.csproj Henshi.Shared/
COPY Henshi.Web/*.csproj Henshi.Web/
RUN dotnet restore Henshi.sln --verbosity minimal

COPY . .
WORKDIR /src/Henshi.Web
RUN dotnet build Henshi.Web.csproj -c $BUILD_CONFIGURATION -v minimal

ARG PUBLISH_TRIMMED=false
ARG PUBLISH_RTR=false
RUN dotnet publish Henshi.Web.csproj -c $BUILD_CONFIGURATION -p:SelfContained=false \
    -p:PublishTrimmed=$PUBLISH_TRIMMED -p:PublishReadyToRun=$PUBLISH_RTR \
    -o /app/publish --no-build -v minimal

# ---- Runtime stage ----------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

RUN apt-get update \
 && apt-get install -y --no-install-recommends ca-certificates curl gnupg2 \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish/ ./

ENV DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    ASPNETCORE_URLS=http://+:80 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_UsePlainText=1

USER app

EXPOSE 80

ENTRYPOINT ["dotnet", "Henshi.Web.dll"]