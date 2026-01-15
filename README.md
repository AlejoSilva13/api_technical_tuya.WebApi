# 🛠️ api_technical_tuya.WebApi

API REST para la gestión de **Clientes** y **Órdenes**, desarrollada con .NET 6 siguiendo principios de **arquitectura limpia** y **hexagonal**

---

## ✅ Tecnologías utilizadas
- **.NET 6** (ASP.NET Core Web API)
- **Entity Framework Core** (ORM)
- **SQL Server** (Base de datos relacional)
- **FluentValidation** (Validación de datos)
- **Swagger / Swashbuckle** (Documentación de API)
- **xUnit** (Pruebas unitarias e integración)

---

## 🏗️ Arquitectura
- **Presentación**: Controladores API + Validadores (FluentValidation)
- **Aplicación (UseCases)**: Casos de uso (Handlers + Commands + Queries)
- **Dominio**: Entidades y reglas de negocio
- **Infraestructura**: Persistencia (EF Core), Repositorios, Configuración

Separación clara entre capas para facilitar mantenibilidad y pruebas.

---

## 🚀 Cómo ejecutar el proyecto
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tuusuario/customer-orders-api.git
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

📂 Estructura del proyecto

 src/
├── Presentacion/        # Controladores, Validadores, Middleware
├── Application/         # Casos de uso (Handlers, Commands, Queries)
├── Domain/              # Entidades y lógica de negocio
├── Infrastructure/      # Persistencia, Repositorios, Configuración BD
tests/
├── UnitTests/           # Pruebas unitarias
├── IntegrationTests/    # Pruebas de integración


