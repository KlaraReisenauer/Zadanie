FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ZadanieAPI.csproj", "."]
RUN dotnet restore "./ZadanieAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ZadanieAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZadanieAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZadanieAPI.dll"]