FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
COPY *.csproj ./
RUN dotnet restore "PikchaWebApp.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "PikchaWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PikchaWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PikchaWebApp.dll"]