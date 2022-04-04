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

Write-Host "*** BUILDING DB MIGRATOR ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$dbmigratorFolder/Dockerfile" -t veek/microservices-db-migrator:$version .

Write-Host "===== BUILDING MICROSERVICES =====" -ForegroundColor Yellow 
### IDENTITY-SERVICE
Write-Host "*** BUILDING IDENTITY-SERVICE ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$identityServiceFolder/Dockerfile" -t veek/microservices-service-identity:$version .

### ADMINISTRATION-SERVICE
Write-Host "*** BUILDING ADMINISTRATION-SERVICE ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$administrationServiceFolder/Dockerfile" -t veek/microservices-service-administration:$version .

### SAAS-SERVICE
Write-Host "*** BUILDING SAAS-SERVICE ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$saasServiceFolder/Dockerfile" -t veek/microservices-service-saas:$version .

### PRODUCT-SERVICE
Write-Host "*** BUILDING PRODUCT-SERVICE ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$productServiceFolder/Dockerfile" -t veek/microservices-service-product:$version .
Write-Host "===== COMPLETED BUILDING MICROSERVICES =====" -ForegroundColor Yellow 


Write-Host "===== BUILDING GATEWAYS =====" -ForegroundColor Yellow 
### WEB-GATEWAY
Write-Host "*** BUILDING WEB-GATEWAY ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$webGatewayFolder/Dockerfile" -t veek/microservices-gateway-web:$version .

### PUBLICWEB-GATEWAY
Write-Host "*** BUILDING WEB-PUBLIC-GATEWAY ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$webPublicGatewayFolder/Dockerfile" -t veek/microservices-gateway-web-public:$version .
Write-Host "===== COMPLETED BUILDING GATEWAYS =====" -ForegroundColor Yellow 


Write-Host "===== BUILDING APPLICATIONS =====" -ForegroundColor Yellow 
### AUTH-SERVER
Write-Host "*** BUILDING AUTH-SERVER ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$authserverFolder/Dockerfile" -t veek/microservices-app-authserver:$version .

### PUBLIC-WEB
Write-Host "*** BUILDING WEB-PUBLIC ***" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$publicWebFolder/Dockerfile" -t veek/microservices-app-publicweb:$version .

### Angular WEB App
if (Test-Path -Path $angularAppFolder) {
    Write-Host "*** BUILDING ANGULAR WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $angularAppFolder
    docker build -f "$angularAppFolder/Dockerfile" -t veek/microservices-app-angular:$version .
}

### MVC WEB App
if (Test-Path -Path $mvcAppFolder) {
    Write-Host "*** BUILDING MVC WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $slnFolder
    docker build -f "$mvcAppFolder/Dockerfile" -t veek/microservices-app-web:$version .
}

### Blazor WEB App
if (Test-Path -Path $blazorAppFolder) {
    Write-Host "*** BUILDING BLAZOR WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $slnFolder
    docker build -f "$blazorAppFolder/Dockerfile" -t veek/microservices-app-blazor:$version .
}

### Blazor-Server WEB App
if (Test-Path -Path $blazorServerAppFolder) {
    Write-Host "*** BUILDING BLAZOR SERVER WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $slnFolder
    docker build -f "$blazorServerAppFolder/Dockerfile" -t veek/microservices-app-blazor-server:$version .
}
Write-Host "===== COMPLETED BUILDING APPLICATIONS =====" -ForegroundColor Yellow

### ALL COMPLETED
Write-Host "ALL COMPLETED" -ForegroundColor Green
Set-Location $currentFolder