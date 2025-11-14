# Frontend - Sistema de Logística de Envíos

Frontend sencillo para consumir la API de Logística de Envíos.

## Características

- ✅ Gestión de Clientes (CRUD)
- ✅ Gestión de Envíos Terrestres (CRUD)
- ✅ Gestión de Envíos Marítimos (CRUD)
- ✅ Gestión de Bodegas (CRUD)
- ✅ Gestión de Puertos (CRUD)
- ✅ Interfaz responsive
- ✅ Notificaciones visuales
- ✅ Diseño moderno con gradientes

## Instalación y Uso

### 1. Configurar CORS en la API

Antes de usar el frontend, necesitas habilitar CORS en tu API. Agrega esto en `Program.cs`:

```csharp
// Antes de var app = builder.Build();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Después de var app = builder.Build();
app.UseCors("AllowAll");
```

### 2. Verificar la URL de la API

Abre `app.js` y verifica que la URL de la API sea correcta:

```javascript
const API_BASE_URL = 'https://localhost:7047/api'; // Ajusta el puerto si es diferente
```

### 3. Ejecutar la API

```bash
cd LogisticaEnvios
dotnet run
```

### 4. Abrir el Frontend

Simplemente abre `index.html` en tu navegador o usa Live Server en VS Code:

- Clic derecho en `index.html` → "Open with Live Server"

O desde PowerShell:

```powershell
cd Frontend
start index.html
```

## Estructura

```
Frontend/
├── index.html    # Estructura HTML principal
├── styles.css    # Estilos CSS
├── app.js        # Lógica JavaScript para consumir la API
└── README.md     # Este archivo
```

## Funcionalidades por Módulo

### Clientes
- Agregar nuevos clientes con cédula, nombre, dirección y email
- Ver lista de todos los clientes
- Eliminar clientes

### Envíos Terrestres
- Crear envíos con placa, bodega, plan, precio y descuento
- Listar todos los envíos terrestres
- Eliminar envíos

### Envíos Marítimos
- Crear envíos con número de flota, puerto y plan
- Listar todos los envíos marítimos
- Eliminar envíos

### Bodegas
- Agregar bodegas con nombre y dirección
- Ver todas las bodegas
- Eliminar bodegas

### Puertos
- Agregar puertos con nombre y ubicación
- Ver todos los puertos
- Eliminar puertos

## Tecnologías Utilizadas

- HTML5
- CSS3 (Flexbox, Grid, Animaciones)
- JavaScript Vanilla (ES6+)
- Fetch API para consumir la API REST

## Notas

- El frontend no requiere ninguna instalación de dependencias
- Funciona directamente en el navegador
- Asegúrate de que la API esté corriendo antes de usar el frontend
- Si ves errores de CORS, verifica la configuración en la API
