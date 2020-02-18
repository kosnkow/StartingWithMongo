FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
COPY ["src/JHipsterNet/JHipsterNet.csproj", "src/JHipsterNet/"]
COPY ["src/Jhipster/Jhipster.csproj", "src/Jhipster/"]
RUN dotnet restore "src/Jhipster/Jhipster.csproj"
COPY . ./
WORKDIR /app/src/Jhipster
RUN apt-get update -yq && apt-get install -yq curl
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get update && \
    apt-get install -yq nodejs && \
    rm -rf /var/lib/apt/lists/*
RUN npm install
RUN rm -rf wwwroot && \
    dotnet publish "Jhipster.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "jhipster.dll"]
