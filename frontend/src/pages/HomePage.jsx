import { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import { customerService } from '../services'

function HomePage() {
  const [stats, setStats] = useState({
    customers: 0,
    loading: true
  })

  useEffect(() => {
    loadStats()
  }, [])

  const loadStats = async () => {
    try {
      const customers = await customerService.getAll()
      setStats({
        customers: customers.length,
        loading: false
      })
    } catch (error) {
      setStats({ customers: 0, loading: false })
    }
  }

  return (
    <div className="home-page">
      <div className="welcome-section">
        <h1 className="page-title">Bienvenido a Tuya API Dashboard</h1>
        <p style={{ color: 'var(--text-secondary)', marginBottom: '2rem' }}>
          Sistema de gestión de clientes y órdenes
        </p>
      </div>

      <div className="stats-grid">
        <div className="stat-card">
          <div className="stat-icon">👥</div>
          <div className="stat-value">{stats.loading ? '...' : stats.customers}</div>
          <div className="stat-label">Clientes Registrados</div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">📦</div>
          <div className="stat-value">∞</div>
          <div className="stat-label">Órdenes Posibles</div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">🚀</div>
          <div className="stat-value">.NET 6</div>
          <div className="stat-label">Tecnología Backend</div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">⚛️</div>
          <div className="stat-value">React 18</div>
          <div className="stat-label">Tecnología Frontend</div>
        </div>
      </div>

      <div className="card" style={{ marginBottom: '2rem' }}>
        <div className="card-header">
          <h2>🎯 Acciones Rápidas</h2>
        </div>
        <div className="card-body">
          <div style={{ display: 'flex', gap: '1rem', flexWrap: 'wrap' }}>
            <Link to="/customers" className="btn btn-primary">
              👥 Ver Clientes
            </Link>
            <Link to="/orders" className="btn btn-success">
              📦 Crear Orden
            </Link>
          </div>
        </div>
      </div>

      <div className="card">
        <div className="card-header">
          <h2>📖 Acerca de la API</h2>
        </div>
        <div className="card-body">
          <div style={{ display: 'grid', gap: '1rem' }}>
            <div>
              <h3 style={{ fontSize: '1rem', marginBottom: '0.5rem' }}>Arquitectura Clean/Hexagonal</h3>
              <p style={{ color: 'var(--text-secondary)', fontSize: '0.875rem' }}>
                Separación clara entre Domain, Application, Infrastructure y Presentation layers.
              </p>
            </div>
            <div>
              <h3 style={{ fontSize: '1rem', marginBottom: '0.5rem' }}>Tecnologías</h3>
              <div style={{ display: 'flex', gap: '0.5rem', flexWrap: 'wrap' }}>
                <span className="badge badge-primary">.NET 6</span>
                <span className="badge badge-primary">Entity Framework Core</span>
                <span className="badge badge-primary">SQL Server</span>
                <span className="badge badge-primary">FluentValidation</span>
                <span className="badge badge-primary">xUnit</span>
              </div>
            </div>
            <div>
              <h3 style={{ fontSize: '1rem', marginBottom: '0.5rem' }}>Endpoints Disponibles</h3>
              <ul style={{ color: 'var(--text-secondary)', fontSize: '0.875rem', marginLeft: '1.5rem' }}>
                <li><code>GET /api/customers</code> - Obtener todos los clientes</li>
                <li><code>GET /api/customers/:id</code> - Obtener cliente por ID</li>
                <li><code>POST /api/customers</code> - Crear cliente</li>
                <li><code>PUT /api/customers/:id</code> - Actualizar cliente</li>
                <li><code>DELETE /api/customers/:id</code> - Eliminar cliente</li>
                <li><code>GET /api/orders/:id</code> - Obtener orden por ID</li>
                <li><code>POST /api/orders</code> - Crear orden</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default HomePage
