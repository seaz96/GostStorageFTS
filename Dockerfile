FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY GostStorageFTS.sln ./
COPY ["API/API.csproj", "API/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore

WORKDIR "/src/API"
RUN dotnet build "Api.csproj" -c Release -o /app/build
WORKDIR "/src/Core"
RUN dotnet build "Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]