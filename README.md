# 🚚 Sistema de Logística de Envíos

Sistema completo de gestión logística para envíos terrestres y marítimos, desarrollado con ASP.NET Core 6 y SQL Server.

## 📋 Tabla de Contenidos

- [Características](#características)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Configuración](#configuración)
- [Ejecución](#ejecución)
- [Uso](#uso)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [API Endpoints](#api-endpoints)
- [Pruebas de Seguridad](#pruebas-de-seguridad)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)

## ✨ Características

- ✅ Gestión completa de clientes
- ✅ Administración de bodegas y puertos
- ✅ Control de tipos de productos
- ✅ Gestión de planes de entrega
- ✅ Envíos terrestres con información de vehículos
- ✅ Envíos marítimos con control de flotas
- ✅ API REST completa
- ✅ Frontend responsive con interfaz moderna
- ✅ Base de datos relacional con SQL Server

## 🔧 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

### Backend
- **[.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)** (versión 6.0 o superior)
- **[SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)** (2019 o superior)
  - SQL Server Express es suficiente
  - O SQL Server LocalDB
- **Visual Studio 2022** (opcional, recomendado) o Visual Studio Code

### Frontend
- Navegador web moderno (Chrome, Firefox, Edge)
- **[Python 3.x](https://www.python.org/downloads/)** (opcional, para servidor local)

### Herramientas Opcionales
- **[Postman](https://www.postman.com/downloads/)** - Para pruebas de API
- **[OWASP ZAP](https://www.zaproxy.org/download/)** - Para pruebas de seguridad

## 📥 Instalación

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Palagg/LogisticaEnvios.git
cd LogisticaEnvios
```

### 2. Configurar la Base de Datos

#### Opción A: Usando SQL Server Management Studio (SSMS)

1. Abre SSMS y conéctate a tu instancia de SQL Server
2. Abre el archivo `database_setup.sql` que se encuentra en la raíz del proyecto
3. Ejecuta el script completo (incluye creación de tablas y datos de prueba)

**O ejecuta manualmente:**

```sql
-- El script completo está disponible en database_setup.sql
-- Incluye:
-- - Creación de base de datos LogisticaDB
-- - Creación de 7 tablas (Cliente, TipoProducto, Bodega, Puerto, PlanDeEntrega, EnvioTerrestre, EnvioMaritimo)
-- - Inserción de datos de prueba para cada tabla
```

#### Opción B: Usando Entity Framework Core Migrations

```bash
cd LogisticaEnvios
dotnet ef database update
```

### 3. Configurar la Cadena de Conexión

Edita el archivo `LogisticaEnvios/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "LogisticaCS": "Server=localhost;Database=LogisticaDB;Integrated Security=true;TrustServerCertificate=True"
  }
}
```

**Configuraciones comunes:**

- **SQL Server Express:**
  ```
  Server=localhost\\SQLEXPRESS;Database=LogisticaDB;Integrated Security=true;TrustServerCertificate=True
  ```

- **SQL Server LocalDB:**
  ```
  Server=(localdb)\\mssqllocaldb;Database=LogisticaDB;Integrated Security=true;TrustServerCertificate=True
  ```

- **Con usuario y contraseña:**
  ```
  Server=localhost;Database=LogisticaDB;User Id=tu_usuario;Password=tu_contraseña;TrustServerCertificate=True
  ```

### 4. Restaurar Dependencias

```bash
cd LogisticaEnvios
dotnet restore
```

## ⚙️ Configuración

### Configuración del Puerto de la API

Por defecto, la API se ejecuta en el puerto `7103`. Para cambiarlo, edita:

**`LogisticaEnvios/Properties/launchSettings.json`:**

```json
{
  "profiles": {
    "LogisticaEnvios": {
      "applicationUrl": "https://localhost:7103;http://localhost:5103"
    }
  }
}
```

### Configuración del Frontend

Si cambias el puerto de la API, actualiza el archivo `Frontend/app.js`:

```javascript
const API_BASE_URL = 'https://localhost:7103/api'; // Cambia el puerto aquí
```

## 🚀 Ejecución

### Iniciar el Backend (API)

#### Opción 1: Usando Visual Studio
1. Abre `LogisticaEnvios.sln`
2. Presiona `F5` o click en "Run"

#### Opción 2: Usando la Terminal

```bash
cd LogisticaEnvios
dotnet run
```

La API estará disponible en:
- **HTTPS**: `https://localhost:7103`
- **HTTP**: `http://localhost:5103`
- **Swagger UI**: `https://localhost:7103/swagger`

### Iniciar el Frontend

#### Opción 1: Abrir directamente
Abre el archivo `Frontend/index.html` en tu navegador

#### Opción 2: Servidor HTTP con Python (Recomendado)

```bash
cd Frontend
python -m http.server 8000
```

Luego abre: `http://localhost:8000`

#### Opción 3: Live Server (VS Code)
1. Instala la extensión "Live Server" en VS Code
2. Click derecho en `index.html` → "Open with Live Server"

## 📱 Uso

### Acceso al Sistema

1. **Frontend Web**: Abre `http://localhost:8000` (o el archivo HTML directamente)
2. **API REST**: Accede a `https://localhost:7103/swagger` para ver la documentación interactiva

### Funcionalidades Principales

#### 1. Gestión de Clientes
- Registrar nuevos clientes con cédula, nombre, dirección y email
- Consultar clientes registrados
- Eliminar clientes

#### 2. Tipos de Producto
- Crear tipos de productos con nombre y descripción
- Listar todos los tipos
- Eliminar tipos de producto

#### 3. Planes de Entrega
- Crear planes asociados a un tipo de producto y cliente
- Especificar fechas, cantidades, precios y número de guía
- Consultar y eliminar planes

#### 4. Envíos Terrestres
- Registrar envíos con placa del vehículo
- Asociar a bodega y plan de entrega
- Seguimiento de envíos terrestres

#### 5. Envíos Marítimos
- Registrar envíos con número de flota
- Asociar a puerto y plan de entrega
- Seguimiento de envíos marítimos

#### 6. Bodegas y Puertos
- Gestionar ubicaciones de entrega
- Control de capacidad de bodegas
- Administración de puertos marítimos

## 📁 Estructura del Proyecto

```
LogisticaEnvios/
├── LogisticaEnvios/              # Backend API
│   ├── Controllers/              # Controladores REST
│   │   ├── ClienteController.cs
│   │   ├── BodegaController.cs
│   │   ├── PuertoController.cs
│   │   ├── TipoProductoController.cs
│   │   ├── PlanDeEntregaController.cs
│   │   ├── EnvioTerrestreController.cs
│   │   └── EnvioMaritimoController.cs
│   ├── Models/                   # Modelos de datos
│   │   ├── Cliente.cs
│   │   ├── Bodega.cs
│   │   ├── Puerto.cs
│   │   ├── TipoProducto.cs
│   │   ├── PlanDeEntrega.cs
│   │   ├── EnvioTerrestre.cs
│   │   ├── EnvioMaritimo.cs
│   │   └── *Context.cs          # DbContext para cada entidad
│   ├── Program.cs                # Configuración principal
│   ├── appsettings.json          # Configuración de la aplicación
│   └── LogisticaEnvios.csproj
├── Frontend/                     # Frontend Web
│   ├── index.html                # Interfaz principal
│   ├── styles.css                # Estilos CSS
│   ├── app.js                    # Lógica JavaScript
│   └── README.md                 # Documentación del frontend
├── LogisticaEnvios.sln           # Solución de Visual Studio
├── .gitignore
└── README.md                     # Este archivo
```

## 🔌 API Endpoints

### Clientes
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Clientes` | Obtener todos los clientes |
| GET | `/api/Clientes/{cedula}` | Obtener cliente por cédula |
| POST | `/api/Clientes` | Crear nuevo cliente |
| DELETE | `/api/Clientes/{cedula}` | Eliminar cliente |

### Bodegas
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Bodega` | Obtener todas las bodegas |
| GET | `/api/Bodega/{id}` | Obtener bodega por ID |
| POST | `/api/Bodega` | Crear nueva bodega |
| DELETE | `/api/Bodega/{id}` | Eliminar bodega |

### Puertos
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Puerto` | Obtener todos los puertos |
| GET | `/api/Puerto/{id}` | Obtener puerto por ID |
| POST | `/api/Puerto` | Crear nuevo puerto |
| DELETE | `/api/Puerto/{id}` | Eliminar puerto |

### Tipos de Producto
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/TipoProducto` | Obtener todos los tipos |
| GET | `/api/TipoProducto/{id}` | Obtener tipo por ID |
| POST | `/api/TipoProducto` | Crear nuevo tipo |
| DELETE | `/api/TipoProducto/{id}` | Eliminar tipo |

### Planes de Entrega
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/PlanDeEntrega` | Obtener todos los planes |
| GET | `/api/PlanDeEntrega/{id}` | Obtener plan por ID |
| POST | `/api/PlanDeEntrega` | Crear nuevo plan |
| DELETE | `/api/PlanDeEntrega/{id}` | Eliminar plan |

### Envíos Terrestres
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/EnvioTerrestre` | Obtener todos los envíos terrestres |
| GET | `/api/EnvioTerrestre/{id}` | Obtener envío por ID |
| POST | `/api/EnvioTerrestre` | Crear nuevo envío |
| DELETE | `/api/EnvioTerrestre/{id}` | Eliminar envío |

### Envíos Marítimos
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/EnvioMaritimo` | Obtener todos los envíos marítimos |
| GET | `/api/EnvioMaritimo/{id}` | Obtener envío por ID |
| POST | `/api/EnvioMaritimo` | Crear nuevo envío |
| DELETE | `/api/EnvioMaritimo/{id}` | Eliminar envío |

### Ejemplo de Payload: Crear Cliente

```json
POST /api/Clientes
Content-Type: application/json

{
  "cedula": "1234567890",
  "nombre": "Juan Pérez",
  "direccion": "Calle Principal 123",
  "email": "juan.perez@email.com"
}
```

## 🔒 Pruebas de Seguridad

Este proyecto está diseñado para pruebas de seguridad en ambientes de desarrollo.

### Vulnerabilidades Conocidas (Para Testing)

⚠️ **ADVERTENCIA**: Este sistema tiene vulnerabilidades intencionales para fines educativos:

- Sin autenticación ni autorización
- CORS completamente permisivo
- Sin rate limiting
- Sin validación robusta de entrada
- Sin cifrado de datos sensibles

### Herramientas Recomendadas

#### OWASP ZAP
```bash
# Instalar OWASP ZAP
# Descargar de: https://www.zaproxy.org/download/

# URL objetivo
https://localhost:7103
```

#### Burp Suite
```bash
# Descargar Burp Suite Community
# https://portswigger.net/burp/communitydownload

# Configurar proxy en localhost:8080
# Interceptar tráfico del frontend
```

### Tipos de Pruebas Sugeridas

1. **SQL Injection**
   - Probar en búsquedas por cédula
   - Intentar inyección en campos de texto

2. **Cross-Site Scripting (XSS)**
   - Inyectar scripts en nombres y descripciones
   - Validar sanitización de entrada

3. **IDOR (Insecure Direct Object Reference)**
   - Acceder a recursos modificando IDs en URLs
   - Probar con IDs secuenciales

4. **Rate Limiting**
   - Hacer múltiples peticiones rápidas
   - Intentar DoS básico

5. **Validación de Entrada**
   - Strings muy largos
   - Caracteres especiales
   - Tipos de datos incorrectos

## 🛠️ Tecnologías Utilizadas

### Backend
- **ASP.NET Core 6** - Framework web
- **Entity Framework Core** - ORM
- **SQL Server** - Base de datos
- **Swagger/OpenAPI** - Documentación de API

### Frontend
- **HTML5** - Estructura
- **CSS3** - Estilos (Flexbox, Grid, Animaciones)
- **JavaScript ES6+** - Lógica
- **Fetch API** - Comunicación con la API

### Herramientas de Desarrollo
- **Visual Studio 2022 / VS Code** - IDE
- **Git** - Control de versiones
- **Postman** - Pruebas de API
- **OWASP ZAP** - Pruebas de seguridad

## 🧪 Pruebas

### Ejecutar Pruebas Unitarias (Próximamente)

```bash
cd LogisticaEnvios.Tests
dotnet test
```

### Pruebas Manuales

1. Accede a Swagger: `https://localhost:7103/swagger`
2. Prueba cada endpoint con diferentes payloads
3. Verifica las respuestas y códigos de estado

## 🐛 Solución de Problemas

### Error: "No se puede conectar a SQL Server"
- Verifica que SQL Server esté corriendo
- Comprueba la cadena de conexión en `appsettings.json`
- Asegúrate de tener los permisos correctos

### Error: "Puerto 7103 ya en uso"
- Cambia el puerto en `launchSettings.json`
- O detén la aplicación que esté usando ese puerto

### Error de CORS en el frontend
- Verifica que la API esté corriendo
- Comprueba que el CORS esté configurado en `Program.cs`
- Reinicia la API después de cambios

### Frontend no se conecta a la API
- Verifica que `API_BASE_URL` en `app.js` sea correcto
- Comprueba que la API esté corriendo en el puerto especificado
- Revisa la consola del navegador (F12) para errores

## 📝 Próximas Mejoras

- [ ] Implementar autenticación JWT
- [ ] Agregar pruebas unitarias
- [ ] Implementar rate limiting
- [ ] Mejorar validación de entrada
- [ ] Agregar logging robusto
- [ ] Dockerizar la aplicación
- [ ] Implementar CI/CD
- [ ] Agregar paginación en listados

## 👤 Autor

**Palagg**
- GitHub: [@Palagg](https://github.com/Palagg)

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 🙏 Agradecimientos

Proyecto desarrollado con fines educativos para el curso de Pruebas y Calidad de Software.

---

⭐ Si este proyecto te fue útil, considera darle una estrella en GitHub!
