# 🎨 Tuya Frontend - React App

Frontend interactivo para consumir la API de Clientes y Órdenes de Tuya.

## 🚀 Inicio Rápido

### Prerrequisitos
- Node.js 18+ instalado
- La API backend ejecutándose en `https://localhost:5001`

### Instalación

```bash
# Navegar a la carpeta frontend
cd frontend

# Instalar dependencias
npm install

# Iniciar el servidor de desarrollo
npm run dev
```

La aplicación estará disponible en: **http://localhost:3000**

## 📁 Estructura del Proyecto

```
frontend/
├── src/
│   ├── components/        # Componentes reutilizables
│   │   ├── Modal.jsx
│   │   ├── Loading.jsx
│   │   ├── EmptyState.jsx
│   │   └── ConfirmDialog.jsx
│   ├── pages/             # Páginas principales
│   │   ├── HomePage.jsx
│   │   ├── CustomersPage.jsx
│   │   └── OrdersPage.jsx
│   ├── services/          # Servicios API
│   │   ├── api.js
│   │   ├── customerService.js
│   │   └── orderService.js
│   ├── App.jsx            # Componente principal con rutas
│   ├── App.css            # Estilos de la app
│   ├── index.css          # Estilos globales
│   └── main.jsx           # Punto de entrada
├── index.html
├── package.json
└── vite.config.js
```

## ✨ Características

### Gestión de Clientes
- ✅ Listar todos los clientes
- ✅ Crear nuevo cliente
- ✅ Editar cliente existente
- ✅ Eliminar cliente
- ✅ Búsqueda en tiempo real

### Gestión de Órdenes
- ✅ Crear nueva orden
- ✅ Buscar orden por ID
- ✅ Ver órdenes creadas en sesión
- ✅ Selección de cliente para la orden

### UX/UI
- 🎨 Diseño moderno y responsivo
- 🔔 Notificaciones toast
- ⌨️ Soporte para tecla Escape en modales
- 📱 Adaptable a dispositivos móviles

## 🔧 Configuración

El proxy está configurado en `vite.config.js` para redirigir las llamadas `/api/*` al backend:

```javascript
proxy: {
  '/api': {
    target: 'https://localhost:5001',
    changeOrigin: true,
    secure: false
  }
}
```

Si tu API corre en otro puerto, modifica el `target`.

## 🛠️ Scripts Disponibles

| Comando | Descripción |
|---------|-------------|
| `npm run dev` | Inicia servidor de desarrollo |
| `npm run build` | Compila para producción |
| `npm run preview` | Preview de build de producción |

## 📦 Dependencias

- **React 18** - Librería UI
- **React Router DOM 6** - Enrutamiento SPA
- **React Hot Toast** - Notificaciones
- **Vite** - Build tool y dev server

## 🎯 Próximos Pasos

Para extender el frontend puedes:
1. Agregar autenticación
2. Implementar paginación de clientes
3. Agregar gráficos de estadísticas
4. Implementar más filtros y ordenamiento
