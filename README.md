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


src/
├── Presentacion/        # API: Controladores, Validadores, Middleware
│   ├── Controllers/     # Endpoints REST (Customers, Orders)
│   ├── Validators/      # FluentValidation (manual en controladores)
│   └── Filters/         # Middleware global de excepciones
│
├── Application/         # Casos de uso (Handlers, Commands, Queries)
│   ├── Dtos/            # DTOs para salida
│   ├── Interfaces/      # Abstracciones (Repos, UoW, DateTimeProvider)
│   └── UseCases/        # Lógica de aplicación (Handlers)
│
├── Domain/              # Entidades y lógica de negocio
│   └── Entities/        # Customer, Order (invariantes y reglas)
│
├── Infrastructure/      # Persistencia, Repositorios, Configuración EF Core
│   ├── Configurations/  # Mapeo EF Core (Customer, Order)
│   ├── Persistence/     # DbContext
│   ├── Repositories/    # Implementación de repositorios
│   ├── Services/        # Servicios transversales (DateTimeProvider)
│   └── DependencyInjection.cs
│
tests/
├── UnitTests/           # Pruebas unitarias (Handlers, Validadores)
└── IntegrationTests/    # Pruebas de integración (API + BD)


