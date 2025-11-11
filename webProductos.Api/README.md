# Proyecto WebApi HU3 (Simulacro)

## Desarrollado por:
**Sarah Pérez**

---

## Descripción General

Este proyecto corresponde al **Simulacro HU3 — Web API .NET 8**, que implementa un sistema de gestión de usuarios y productos, con autenticación basada en **JWT (JSON Web Token)** y acceso mediante roles.

El objetivo es construir una arquitectura limpia en capas utilizando **Entity Framework Core**, **Fluent API**, **repositorios**, y controladores expuestos mediante **Swagger**.

---

## Arquitectura del Proyecto

El proyecto está organizado en **cuatro capas principales**, siguiendo buenas prácticas de arquitectura limpia:

```
HU3_Simulacro/
│
├── webProductos.Api/              → Capa de presentación (controladores y configuración)
├── webProductos.Application/      → Lógica de negocio (servicios y validaciones)
├── webProductos.Domain/           → Entidades y modelos de dominio
└── webProductos.Infrastructure/   → Acceso a datos (DbContext, repositorios, migraciones)
```

### 🔹 Descripción de cada capa

| Capa | Responsabilidad principal |
|------|----------------------------|
| **Api** | Exponer endpoints HTTP (Auth, Users, Products). Configuración de Swagger, CORS, autenticación JWT. |
| **Application** | Contiene los servicios para manejar la lógica de usuarios y productos. |
| **Domain** | Define las entidades principales (`User`, `Product`) y sus propiedades. |
| **Infrastructure** | Implementa `AppDbContext` con EF Core, repositorios concretos y migraciones de base de datos. |

---

## Tecnologías utilizadas

- **.NET 8 (C#)**
- **Entity Framework Core 8**
- **JWT Authentication**
- **Swagger / OpenAPI**
- **Fluent API**
- **BCrypt.Net** (para hashear contraseñas)

---

## Endpoints Principales

### Autenticación (`/api/auth`)

| Método | Endpoint | Descripción | Requiere Token |
|---------|-----------|--------------|----------------|
| `POST` | `/api/auth/register` | Registro de usuario nuevo | ❌ |
| `POST` | `/api/auth/login` | Inicia sesión y devuelve token JWT | ❌ |

**Ejemplo de registro:**
```json
{
  "userName": "sara",
  "email": "sara@correo.com",
  "password": "12345",
  "role": "Admin"
}
```

**Respuesta de login (JWT):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
```

---

### Usuarios (`/api/users`)

| Método | Endpoint | Descripción | Rol requerido |
|---------|-----------|--------------|----------------|
| `GET` | `/api/users` | Lista todos los usuarios registrados | Admin |
| `GET` | `/api/users/{id}` | Obtiene usuario por ID | Admin |
| `DELETE` | `/api/users/{id}` | Elimina usuario por ID | Admin |

---

### Productos (`/api/products`)

| Método | Endpoint | Descripción | Requiere Token |
|---------|-----------|--------------|----------------|
| `GET` | `/api/product` | Lista todos los productos | ✅ |
| `GET` | `/api/product/{id}` | Obtiene producto por ID | ✅ |
| `POST` | `/api/product` | Crea un nuevo producto | ✅ |
| `PUT` | `/api/product/{id}` | Actualiza un producto existente | ✅ |
| `DELETE` | `/api/product/{id}` | Elimina un producto | ✅ |

**Ejemplo de creación de producto:**
```json
{
  "name": "Teclado mecánico",
  "price": 250000,
  "stock": 10
}
```

---

## Base de datos y migraciones

La conexión a la base de datos se define en `appsettings.json` dentro del proyecto **webProductos.Api**.

Ejemplo:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=webproductosdb;User Id=root;Password=123456;"
}
```
**Tener paquetes instalados**
```bash
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet add package Pomelo.EntityFrameworkCore.MySql
```
**O restaurar paquetes(dependencias)**
```bash
    dotnet restore
```

Para aplicar las migraciones desde la consola del proyecto:

```bash
   dotnet ef migrations add Initial --project webProductos.Infrastructure --startup-project webProductos.Api
   dotnet ef database update --project webProductos.Infrastructure --startup-project webProductos.Api
```

---

## **Seeder de usuario administrador**
Para garantizar el acceso inicial, se implementó un `SeedAdmin` que crea automáticamente un usuario 
administrador al ejecutar la API si la base de datos está vacía.

### Archivo:
`webProductos.Infrastructure/Seed/SeedAdmin.cs`

### Usuario creado automáticamente:

| Campo | Valor |
|---------|-----------|
| Username | admin |
| Email | admin@local | 
| PasswordHash | Admin123! | 
| Rol | Admin| 

El seeder se ejecuta en el `Program.cs` al iniciar la aplicación:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedAdmin.Seed(db);
}
```

---

## Cómo ejecutar el proyecto

1. Abre la solución en **Rider** o **Visual Studio**.
2. Verifica que la base de datos esté configurada en `appsettings.json`.
3. Abre la consola de comandos en el proyecto raíz.
4. Ejecuta:
```bash
  dotnet run --project webProductos.Api
  ```
5. Accede a **Swagger** en: [http://localhost:5116/swagger](http://localhost:5080/swagger)

---

## Colección Postman (Pruebas de API)

Se incluye una colección exportada para realizar pruebas desde **Postman**:

- **Archivo:** `HU3-webProductos.postman_collection.json`
- **Ubicación:** en la raíz del proyecto.
- **Formato:** Collection v2.1 (recomendado)

---

## Contenido de la colección

| Request | Método | Descripción |
|----------|---------|-------------|
| Auth - Register | POST | Registra un nuevo usuario |
| Auth - Login | POST | Inicia sesión y guarda el token JWT |
| Products - Get All | GET | Obtiene todos los productos |
| Products - Create | POST | Crea un nuevo producto (requiere token) |
| Users - Get All | GET | Obtiene todos los usuarios (solo Admin) |

## **Variables incluidas**
| Variable | Ejemplo de valor | Descripción |
|----------|---------|-------------|
| `baseUrl` | `http://localhost:5080` | URL base del servidor local |
| `token` | (se genera automáticamente al hacer login) | JWT para autenticación |

## **Cómo importar la colección**

**1.** Abre Postman.

**2.** Haz clic en **Import** → selecciona el archivo `HU3-webProductos.postman_collection.json`.

**3.** Ejecuta las peticiones en este orden:

 - **Auth - Register**

 - **Auth - Login**

 - **Products - Get All**

 - **Products - Create**

 - **Users - Get All**

---

## Estado del proyecto

| Elemento | Estado |
|-----------|---------|
| Arquitectura en capas | ✅ Completado |
| CRUD de Usuarios | ✅ Implementado |
| CRUD de Productos | ✅ Implementado |
| Autenticación JWT | ✅ Implementado |
| Swagger | ✅ Activo |
| Migraciones EF Core | ✅ Incluidas |
| Seed de usuario Admin | ✅ Implementado |
| Dockerfile y docker-compose | ❌ Pendiente |
| Colección Postman | ✅ Implementado |
| Pruebas unitarias | ❌ Pendiente |
| Despliegue online | ❌ Pendiente |

---

## Próximos pasos sugeridos

1. Implementar pruebas unitarias (xUnit) en la capa Application.
2. Añadir Dockerfile y docker-compose.yml para despliegue local.
3. Desplegar la API en un servicio en la nube (Render, Railway, Azure).

---
