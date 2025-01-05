FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5145
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nSkinShop/nSkinShop.csproj", "nSkinShop/"]
COPY ["nSkinShop.DataAccess/nSkinShop.DataAccess.csproj", "nSkinShop.DataAccess/"]
COPY ["nSkinShop.Models/nSkinShop.Models.csproj", "nSkinShop.Models/"]
COPY ["nSkinShop.Utility/nSkinShop.Utility.csproj", "nSkinShop.Utility/"]
RUN dotnet restore "nSkinShop/nSkinShop.csproj"
COPY . .
WORKDIR "/src/nSkinShop"
RUN dotnet build "nSkinShop.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "nSkinShop.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN chown -R $APP_UID:$APP_UID /app
USER $APP_UID
ENTRYPOINT ["dotnet", "nSkinShop.dll"]
