param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../"
### Apps Folders
$angularAppFolder = Join-Path $slnFolder "apps/angular"
$mvcAppFolder = Join-Path $slnFolder "apps/web/src/Veek.Microservices.Web"
$blazorServerAppFolder = Join-Path $slnFolder "apps/blazor/src/Veek.Microservices.Blazor.Server"
$blazorAppFolder = Join-Path $slnFolder "apps/blazor/src/Veek.Microservices.Blazor"
$authserverFolder = Join-Path $slnFolder "apps/auth-server/src/Veek.Microservices.AuthServer"
$publicWebFolder = Join-Path $slnFolder "apps/public-web/src/Veek.Microservices.PublicWeb"

# Microservice Folders
$identityServiceFolder = Join-Path $slnFolder "services/identity/src/Veek.Microservices.IdentityService.HttpApi.Host"
$administrationServiceFolder = Join-Path $slnFolder "services/administration/src/Veek.Microservices.AdministrationService.HttpApi.Host"
$saasServiceFolder = Join-Path $slnFolder "services/saas/src/Veek.Microservices.SaasService.HttpApi.Host"
$productServiceFolder = Join-Path $slnFolder "services/product/src/Veek.Microservices.ProductService.HttpApi.Host"

### Gateway Folders
$webGatewayFolder = Join-Path $slnFolder "gateways/web/src/Veek.Microservices.WebGateway"
$webPublicGatewayFolder = Join-Path $slnFolder "gateways/web-public/src/Veek.Microservices.PublicWebGateway"

### DB Migrator Folder
$dbmigratorFolder = Join-Path $slnFolder "shared/Veek.Microservices.DbMigrator"

### IDENTITY-SERVICE
Write-Host "*** BUILDING DB MIGRATOR ***" -ForegroundColor Green
Set-Location $dbmigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-db-migrator:$version .

Write-Host "===== BUILDING MICROSERVICES =====" -ForegroundColor Yellow 
### IDENTITY-SERVICE
Write-Host "*** BUILDING IDENTITY-SERVICE ***" -ForegroundColor Green
Set-Location $identityServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-service-identity:$version .

### ADMINISTRATION-SERVICE
Write-Host "*** BUILDING ADMINISTRATION-SERVICE ***" -ForegroundColor Green
Set-Location $administrationServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-service-administration:$version .

### SAAS-SERVICE
Write-Host "*** BUILDING SAAS-SERVICE ***" -ForegroundColor Green
Set-Location $saasServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-service-saas:$version .

### PRODUCT-SERVICE
Write-Host "*** BUILDING SAAS-SERVICE ***" -ForegroundColor Green
Set-Location $productServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-service-product:$version .
Write-Host "===== COMPLETED BUILDING MICROSERVICES =====" -ForegroundColor Yellow 


Write-Host "===== BUILDING GATEWAYS =====" -ForegroundColor Yellow 
### WEB-GATEWAY
Write-Host "*** BUILDING WEB-GATEWAY ***" -ForegroundColor Green
Set-Location $webGatewayFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-gateway-web:$version .

### PUBLICWEB-GATEWAY
Write-Host "*** BUILDING WEB-PUBLIC-GATEWAY ***" -ForegroundColor Green
Set-Location $webPublicGatewayFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-gateway-web-public:$version .
Write-Host "===== COMPLETED BUILDING GATEWAYS =====" -ForegroundColor Yellow 


Write-Host "===== BUILDING APPLICATIONS =====" -ForegroundColor Yellow 
### AUTH-SERVER
Write-Host "*** BUILDING AUTH-SERVER ***" -ForegroundColor Green
Set-Location $authserverFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-app-authserver:$version .

### PUBLIC-WEB
Write-Host "*** BUILDING WEB-PUBLIC ***" -ForegroundColor Green
Set-Location $publicWebFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t veek/microservices-app-publicweb:$version .

### Angular WEB App
if (Test-Path -Path $angularAppFolder) {
    Write-Host "*** BUILDING ANGULAR WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $angularAppFolder
    yarn
    # ng build --prod
    npm run build:prod
    docker build -f Dockerfile.local -t veek/microservices-app-angular:$version .
}

### MVC WEB App
if (Test-Path -Path $mvcAppFolder) {
    Write-Host "*** BUILDING MVC WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $mvcAppFolder
    dotnet publish -c Release
    docker build -f Dockerfile.local -t veek/microservices-app-web:$version .
}

### Blazor WEB App
if (Test-Path -Path $blazorAppFolder) {
    Write-Host "*** BUILDING BLAZOR WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $blazorAppFolder
    dotnet publish -c Release
    docker build -f Dockerfile.local -t veek/microservices-app-blazor:$version .
}

### Blazor-Server WEB App
if (Test-Path -Path $blazorServerAppFolder) {
    Write-Host "*** BUILDING BLAZOR SERVER WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $blazorServerAppFolder
    dotnet publish -c Release
    docker build -f Dockerfile.local -t veek/microservices-app-blazor-server:$version .
}
Write-Host "===== COMPLETED BUILDING APPLICATIONS =====" -ForegroundColor Yellow 
### ALL COMPLETED
Write-Host "ALL COMPLETED" -ForegroundColor Green
Set-Location $currentFolder