# 🛠️ api_technical_tuya.WebApi

API REST para la gestión de **Clientes** y **Órdenes**, desarrollada con .NET 6 siguiendo principios de **arquitectura limpia** y **hexagonal**

---

## ✅ Tecnologías utilizadas
- **.NET 6** (Core Web API)
- **Entity Framework Core** (ORM)
- **SQL Server** (Base de datos relacional)
- **FluentValidation** (Validación de datos)
- **Swagger / Swashbuckle** (Documentación de API)
- **xUnit** (Pruebas unitarias e integración)

---
## 🗄️ Scripts de Base de Datos

Para preparar la base de datos en SQL Server, utiliza los scripts incluidos en la carpeta scripts/database/:

- create-database.sql
- create-tables.sql
- update-fk-orders-customers.sql

*Cómo ejecutarlos:*

Abre SQL Server Management Studio (SSMS).
Ejecuta los scripts en orden:

- create-database.sql
- create-tables.sql
- (Opcional) update-fk-orders-customers.sql
---

## 🏗️ Arquitectura
- **Presentación**: Controladores API + Validadores (FluentValidation)
- **Aplicación (UseCases)**: Casos de uso (Handlers + Commands + Queries)
- **Dominio**: Entidades y reglas de negocio
- **Infraestructura**: Persistencia (EF Core), Repositorios, Configuración

Separación clara entre capas para facilitar mantenibilidad y pruebas.

---
## ⚙️ Archivos de Configuración (JSON)

Estos archivos permiten configurar la API sin modificar el código:

- appsettings.json
- appsettings.Development.json (Configuración especifica para desarrollo)
- launchSettings.json (en Properties/)
---

## 🚀 Cómo ejecutar el proyecto
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tuusuario/customer-orders-api.git](https://github.com/AlejoSilva13/api_technical_tuya.WebApi.git
   cd api_technical_test_tuya
   
2. Configurar la cadena de conexión en appsettings.json

"ConnectionStrings": {
  "SqlServer": "Server=.;Database=CustomerOrdersDb;Trusted_Connection=True;"
}

3. Compilar Solución
4. Explorar la documentación
URL Swagger: https://localhost:5001/swagger

✅ Características principales

CRUD completo para Clientes y Órdenes
Validación automática con FluentValidation
Manejo centralizado de errores con Middleware
Respuestas claras (400, 404, 409, 500)
Integración con SQL Server
Pruebas unitarias e integración con xUnit

## 📂 Estructura del proyecto




