FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "Veek.Microservices.sln" "Veek.Microservices.sln"

# Applications
COPY "apps/auth-server/src/Veek.Microservices.AuthServer/Veek.Microservices.AuthServer.csproj" "apps/auth-server/src/Veek.Microservices.AuthServer/Veek.Microservices.AuthServer.csproj"
COPY "apps/public-web/src/Veek.Microservices.PublicWeb/Veek.Microservices.PublicWeb.csproj" "apps/public-web/src/Veek.Microservices.PublicWeb/Veek.Microservices.PublicWeb.csproj"
COPY *"apps/blazor/src/Veek.Microservices.Blazor/Veek.Microservices.Blazor.csproj" "apps/blazor/src/Veek.Microservices.Blazor/Veek.Microservices.Blazor.csproj"
COPY *"apps/blazor/src/Veek.Microservices.Blazor.Server/Veek.Microservices.Blazor.Server.csproj" "apps/blazor/src/Veek.Microservices.Blazor.Server/Veek.Microservices.Blazor.Server.csproj"
COPY *"apps/web/src/Veek.Microservices.Web/Veek.Microservices.Web.csproj" "apps/web/src/Veek.Microservices.Web/Veek.Microservices.Web.csproj"

# Shared Projects
COPY "shared/Veek.Microservices.DbMigrator/Veek.Microservices.DbMigrator.csproj" "shared/Veek.Microservices.DbMigrator/Veek.Microservices.DbMigrator.csproj"
COPY "shared/Veek.Microservices.Shared.Hosting/Veek.Microservices.Shared.Hosting.csproj" "shared/Veek.Microservices.Shared.Hosting/Veek.Microservices.Shared.Hosting.csproj"
COPY "shared/Veek.Microservices.Shared.Hosting.Gateways/Veek.Microservices.Shared.Hosting.Gateways.csproj" "shared/Veek.Microservices.Shared.Hosting.Gateways/Veek.Microservices.Shared.Hosting.Gateways.csproj"
COPY "shared/Veek.Microservices.Shared.Hosting.Microservices/Veek.Microservices.Shared.Hosting.Microservices.csproj" "shared/Veek.Microservices.Shared.Hosting.Microservices/Veek.Microservices.Shared.Hosting.Microservices.csproj"
COPY "shared/Veek.Microservices.Shared.Hosting.AspNetCore/Veek.Microservices.Shared.Hosting.AspNetCore.csproj" "shared/Veek.Microservices.Shared.Hosting.AspNetCore/Veek.Microservices.Shared.Hosting.AspNetCore.csproj"
COPY "shared/Veek.Microservices.Shared.Hosting/Veek.Microservices.Shared.Hosting.csproj" "shared/Veek.Microservices.Shared.Hosting/Veek.Microservices.Shared.Hosting.csproj"
COPY "shared/Veek.Microservices.Shared.Localization/Veek.Microservices.Shared.Localization.csproj" "shared/Veek.Microservices.Shared.Localization/Veek.Microservices.Shared.Localization.csproj"

# Microservices
COPY "services/administration/src/Veek.Microservices.AdministrationService.HttpApi.Host/Veek.Microservices.AdministrationService.HttpApi.Host.csproj" "services/administration/src/Veek.Microservices.AdministrationService.HttpApi.Host/Veek.Microservices.AdministrationService.HttpApi.Host.csproj"
COPY "services/identity/src/Veek.Microservices.IdentityService.HttpApi.Host/Veek.Microservices.IdentityService.HttpApi.Host.csproj" "services/identity/src/Veek.Microservices.IdentityService.HttpApi.Host/Veek.Microservices.IdentityService.HttpApi.Host.csproj"
COPY "services/saas/src/Veek.Microservices.SaasService.HttpApi.Host/Veek.Microservices.SaasService.HttpApi.Host.csproj" "services/saas/src/Veek.Microservices.SaasService.HttpApi.Host/Veek.Microservices.SaasService.HttpApi.Host.csproj"
COPY "services/product/src/Veek.Microservices.ProductService.HttpApi.Host/Veek.Microservices.ProductService.HttpApi.Host.csproj" "services/product/src/Veek.Microservices.ProductService.HttpApi.Host/Veek.Microservices.ProductService.HttpApi.Host.csproj"
# Gateways
COPY "gateways/web/src/Veek.Microservices.WebGateway/Veek.Microservices.WebGateway.csproj" "gateways/web/src/Veek.Microservices.WebGateway/Veek.Microservices.WebGateway.csproj"
COPY "gateways/web-public/src/Veek.Microservices.PublicWebGateway/Veek.Microservices.PublicWebGateway.csproj" "gateways/web-public/src/Veek.Microservices.PublicWebGateway/Veek.Microservices.PublicWebGateway.csproj"

COPY "NuGet.Config" "NuGet.Config"

RUN dotnet restore "Veek.Microservices.sln"

COPY . .
WORKDIR "/src/apps/web/src/Veek.Microservices.Web"
RUN dotnet publish -c Release -o /app
# Should be used after .net6 is out of preview for better performance
# RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Veek.Microservices.Web.dll"]