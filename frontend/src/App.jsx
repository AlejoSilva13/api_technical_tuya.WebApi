import { Routes, Route, NavLink } from 'react-router-dom'
import CustomersPage from './pages/CustomersPage'
import OrdersPage from './pages/OrdersPage'
import HomePage from './pages/HomePage'
import './App.css'

function App() {
  return (
    <div className="app">
      <nav className="navbar">
        <div className="nav-brand">
          <span className="logo">🛒</span>
          <span className="brand-text">Tuya API</span>
        </div>
        <ul className="nav-links">
          <li>
            <NavLink to="/" className={({ isActive }) => isActive ? 'active' : ''}>
              Inicio
            </NavLink>
          </li>
          <li>
            <NavLink to="/customers" className={({ isActive }) => isActive ? 'active' : ''}>
              Clientes
            </NavLink>
          </li>
          <li>
            <NavLink to="/orders" className={({ isActive }) => isActive ? 'active' : ''}>
              Órdenes
            </NavLink>
          </li>
        </ul>
      </nav>

      <main className="main-content">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/customers" element={<CustomersPage />} />
          <Route path="/orders" element={<OrdersPage />} />
        </Routes>
      </main>

      <footer className="footer">
        <p>© 2026 Tuya Technical Test - Gestión de Clientes y Órdenes</p>
      </footer>
    </div>
  )
}

export default App
