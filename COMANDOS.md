# Comandos Útiles - Sistema de Logística de Envíos

## Desarrollo

### Backend

```bash
# Compilar el proyecto
dotnet build

# Ejecutar la aplicación
dotnet run --project LogisticaEnvios

# Ejecutar con hot reload
dotnet watch run --project LogisticaEnvios

# Restaurar paquetes NuGet
dotnet restore

# Limpiar build
dotnet clean
```

### Base de Datos

```bash
# Crear migración
dotnet ef migrations add InitialCreate --project LogisticaEnvios

# Aplicar migraciones
dotnet ef database update --project LogisticaEnvios

# Eliminar última migración
dotnet ef migrations remove --project LogisticaEnvios

# Ver historial de migraciones
dotnet ef migrations list --project LogisticaEnvios
```

### Frontend

```bash
# Servidor HTTP con Python
cd Frontend
python -m http.server 8000

# Servidor HTTP con Node.js (si tienes http-server instalado)
cd Frontend
npx http-server -p 8000

# Servidor con PHP (si tienes PHP instalado)
cd Frontend
php -S localhost:8000
```

## Testing

### Pruebas de API con cURL

```bash
# Obtener todos los clientes
curl -X GET "https://localhost:7103/api/Clientes" -k

# Crear un cliente
curl -X POST "https://localhost:7103/api/Clientes" \
  -H "Content-Type: application/json" \
  -d "{\"cedula\":\"1234567890\",\"nombre\":\"Test\",\"direccion\":\"Test\",\"email\":\"test@test.com\"}" \
  -k

# Obtener cliente por cédula
curl -X GET "https://localhost:7103/api/Clientes/1234567890" -k

# Eliminar cliente
curl -X DELETE "https://localhost:7103/api/Clientes/1234567890" -k
```

### Pruebas de API con PowerShell

```powershell
# Obtener todos los clientes
Invoke-RestMethod -Uri "https://localhost:7103/api/Clientes" -Method Get -SkipCertificateCheck

# Crear un cliente
$body = @{
    cedula = "1234567890"
    nombre = "Test"
    direccion = "Test"
    email = "test@test.com"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7103/api/Clientes" `
    -Method Post `
    -Body $body `
    -ContentType "application/json" `
    -SkipCertificateCheck
```

## Git

```bash
# Inicializar repositorio (si es necesario)
git init

# Ver estado
git status

# Agregar todos los archivos
git add .

# Commit
git commit -m "Descripción del commit"

# Configurar remoto
git remote add origin https://github.com/Palagg/LogisticaEnvios.git

# Subir cambios
git push -u origin master

# Pull últimos cambios
git pull origin master

# Ver historial
git log --oneline

# Crear rama
git checkout -b nombre-rama

# Cambiar de rama
git checkout master
```

## SQL Server

```sql
-- Ver todas las bases de datos
SELECT name FROM sys.databases;

-- Ver todas las tablas
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';

-- Ver datos de una tabla
SELECT * FROM Cliente;
SELECT * FROM Bodega;
SELECT * FROM Puerto;
SELECT * FROM TipoProducto;
SELECT * FROM PlanDeEntrega;
SELECT * FROM EnvioTerrestre;
SELECT * FROM EnvioMaritimo;

-- Limpiar una tabla
DELETE FROM Cliente;
DBCC CHECKIDENT ('Cliente', RESEED, 0);

-- Backup de base de datos
BACKUP DATABASE LogisticaDB TO DISK = 'C:\\Backup\\LogisticaDB.bak';

-- Restaurar base de datos
RESTORE DATABASE LogisticaDB FROM DISK = 'C:\\Backup\\LogisticaDB.bak';
```

## Docker (Opcional - para futuras implementaciones)

```bash
# Construir imagen
docker build -t logistica-api .

# Ejecutar contenedor
docker run -d -p 7103:443 --name logistica-api logistica-api

# Ver logs
docker logs logistica-api

# Detener contenedor
docker stop logistica-api

# Eliminar contenedor
docker rm logistica-api
```

## Utilidades

```bash
# Ver qué está usando el puerto 7103
netstat -ano | findstr :7103

# Matar proceso por PID (reemplaza <PID> con el número)
taskkill /PID <PID> /F

# Ver procesos de .NET
dotnet --list-sdks
dotnet --list-runtimes

# Limpiar caché de NuGet
dotnet nuget locals all --clear
```

## Pruebas de Seguridad

### OWASP ZAP

```bash
# Iniciar ZAP en modo daemon
zap.sh -daemon -port 8080 -config api.disablekey=true

# Escaneo rápido
zap-cli quick-scan -s xss,sqli https://localhost:7103

# Escaneo completo
zap-cli active-scan https://localhost:7103
```

### Burp Suite

```bash
# Configurar proxy del sistema para Burp (Windows PowerShell)
netsh winhttp set proxy localhost:8080

# Restaurar configuración de proxy
netsh winhttp reset proxy
```

## Productividad

### Visual Studio Code

```bash
# Abrir proyecto
code .

# Abrir archivo específico
code README.md

# Instalar extensión
code --install-extension ms-dotnettools.csharp
```

### Visual Studio

```bash
# Abrir solución
start LogisticaEnvios.sln

# Compilar desde línea de comandos
msbuild LogisticaEnvios.sln /t:Build /p:Configuration=Release
```

## Limpieza

```bash
# Eliminar archivos de build
Remove-Item -Recurse -Force LogisticaEnvios\bin, LogisticaEnvios\obj

# Limpiar todos los archivos temporales
git clean -fdx

# Ver qué se eliminaría (dry run)
git clean -fdxn
```

## Monitoreo

```bash
# Ver uso de puertos
netstat -ano | findstr LISTENING

# Ver procesos de dotnet
Get-Process -Name dotnet

# Uso de memoria
Get-Process -Name dotnet | Select-Object Name, CPU, WorkingSet
```
