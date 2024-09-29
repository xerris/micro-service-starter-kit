FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Library.Challenge/Library.Challenge.csproj", "Library.Challenge/"]
RUN dotnet restore "src/Library.Challenge/Library.Challenge.csproj"
COPY . .
WORKDIR "/src/Library.Challenge"
RUN dotnet build "Library.Challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Challenge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Challenge.dll"]
