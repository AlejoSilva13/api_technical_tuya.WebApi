import { useState, useEffect } from 'react'
import toast from 'react-hot-toast'
import { orderService, customerService } from '../services'
import { Modal, Loading, EmptyState } from '../components'

function OrdersPage() {
  const [customers, setCustomers] = useState([])
  const [loadingCustomers, setLoadingCustomers] = useState(true)
  const [orders, setOrders] = useState([])
  const [loadingOrders, setLoadingOrders] = useState(true)
  
  // Modal states
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const [isSearchModalOpen, setIsSearchModalOpen] = useState(false)
  const [formLoading, setFormLoading] = useState(false)
  const [searchLoading, setSearchLoading] = useState(false)
  const [foundOrder, setFoundOrder] = useState(null)
  
  // Form state
  const [formData, setFormData] = useState({ customerId: '', total: '' })
  const [searchId, setSearchId] = useState('')

  useEffect(() => {
    loadCustomers()
    loadOrders()
  }, [])

  const loadCustomers = async () => {
    try {
      setLoadingCustomers(true)
      const data = await customerService.getAll()
      setCustomers(data)
    } catch (error) {
      toast.error('Error al cargar clientes: ' + error.message)
    } finally {
      setLoadingCustomers(false)
    }
  }

  const loadOrders = async () => {
    try {
      setLoadingOrders(true)
      const data = await orderService.getAll()
      setOrders(data)
    } catch (error) {
      toast.error('Error al cargar órdenes: ' + error.message)
    } finally {
      setLoadingOrders(false)
    }
  }

  const handleCreate = async (e) => {
    e.preventDefault()
    if (!formData.customerId || !formData.total) {
      toast.error('Todos los campos son requeridos')
      return
    }

    const total = parseFloat(formData.total)
    if (isNaN(total) || total < 0) {
      toast.error('El total debe ser un número válido mayor o igual a 0')
      return
    }

    try {
      setFormLoading(true)
      await orderService.create({
        customerId: formData.customerId,
        total: total
      })
      await loadOrders()
      setIsCreateModalOpen(false)
      setFormData({ customerId: '', total: '' })
      toast.success('Orden creada exitosamente')
    } catch (error) {
      toast.error('Error al crear orden: ' + error.message)
    } finally {
      setFormLoading(false)
    }
  }

  const handleSearch = async (e) => {
    e.preventDefault()
    if (!searchId.trim()) {
      toast.error('Ingresa un ID de orden')
      return
    }

    // Validate GUID format
    const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i
    if (!guidRegex.test(searchId.trim())) {
      toast.error('El ID debe ser un GUID válido')
      return
    }

    try {
      setSearchLoading(true)
      setFoundOrder(null)
      const order = await orderService.getById(searchId.trim())
      setFoundOrder(order)
      toast.success('Orden encontrada')
    } catch (error) {
      toast.error('Orden no encontrada: ' + error.message)
      setFoundOrder(null)
    } finally {
      setSearchLoading(false)
    }
  }

  const closeModals = () => {
    setIsCreateModalOpen(false)
    setIsSearchModalOpen(false)
    setFormData({ customerId: '', total: '' })
    setSearchId('')
    setFoundOrder(null)
  }

  const formatDate = (dateString) => {
    if (!dateString) return 'Sin fecha'
    const date = new Date(dateString)
    if (isNaN(date.getTime())) return 'Fecha inválida'
    return date.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    })
  }

  const formatCurrency = (amount) => {
    if (amount === undefined || amount === null || isNaN(amount)) return '$0'
    return new Intl.NumberFormat('es-CO', {
      style: 'currency',
      currency: 'COP',
      minimumFractionDigits: 0
    }).format(amount)
  }

  const getCustomerName = (customerId) => {
    const customer = customers.find(c => c.id === customerId)
    return customer ? customer.name : 'Cliente desconocido'
  }

  return (
    <div className="orders-page">
      <div className="page-header">
        <h1 className="page-title">📦 Gestión de Órdenes</h1>
        <div style={{ display: 'flex', gap: '0.75rem' }}>
          <button 
            className="btn btn-secondary"
            onClick={() => setIsSearchModalOpen(true)}
          >
            🔍 Buscar Orden
          </button>
          <button 
            className="btn btn-primary"
            onClick={() => setIsCreateModalOpen(true)}
            disabled={loadingCustomers || customers.length === 0}
          >
            + Nueva Orden
          </button>
        </div>
      </div>

      {/* Stats */}
      <div className="stats-grid">
        <div className="stat-card">
          <div className="stat-icon">📦</div>
          <div className="stat-value">{loadingOrders ? '...' : orders.length}</div>
          <div className="stat-label">Total Órdenes</div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">👥</div>
          <div className="stat-value">{loadingCustomers ? '...' : customers.length}</div>
          <div className="stat-label">Clientes Disponibles</div>
        </div>
        <div className="stat-card">
          <div className="stat-icon">💰</div>
          <div className="stat-value">
            {loadingOrders ? '...' : formatCurrency(orders.reduce((sum, o) => sum + (o.total || 0), 0))}
          </div>
          <div className="stat-label">Total Vendido</div>
        </div>
      </div>

      {/* Info Card - solo mostrar si no hay clientes */}
      {customers.length === 0 && !loadingCustomers && (
        <div className="card" style={{ marginBottom: '2rem' }}>
          <div className="card-body">
            <div style={{ 
              padding: '1rem', 
              background: '#fef3c7', 
              borderRadius: 'var(--radius)',
              color: '#92400e'
            }}>
              ⚠️ No hay clientes registrados. Debes crear al menos un cliente antes de crear órdenes.
            </div>
          </div>
        </div>
      )}

      {/* Orders Table */}
      <div className="card">
        <div className="card-header">
          <h2>📋 Todas las Órdenes</h2>
        </div>
        <div className="card-body" style={{ padding: 0 }}>
          {loadingOrders ? (
            <Loading message="Cargando órdenes..." />
          ) : orders.length === 0 ? (
            <EmptyState
              icon="📦"
              title="Sin órdenes"
              description="No hay órdenes registradas"
              action={
                customers.length > 0 && (
                  <button className="btn btn-primary" onClick={() => setIsCreateModalOpen(true)}>
                    + Crear Primera Orden
                  </button>
                )
              }
            />
          ) : (
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Cliente</th>
                    <th>Total</th>
                    <th>Estado</th>
                    <th>Fecha</th>
                  </tr>
                </thead>
                <tbody>
                  {orders.map(order => (
                    <tr key={order.id}>
                      <td>
                        <code style={{ fontSize: '0.75rem', background: 'var(--background)', padding: '0.25rem 0.5rem', borderRadius: '4px' }}>
                          {order.id?.substring(0, 8)}...
                        </code>
                      </td>
                      <td>{getCustomerName(order.customerId)}</td>
                      <td><strong>{formatCurrency(order.total)}</strong></td>
                      <td>
                        <span className="badge badge-success">
                          {order.status || 'Created'}
                        </span>
                      </td>
                      <td>{formatDate(order.createdAtUtc)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>

      {/* Create Order Modal */}
      <Modal
        isOpen={isCreateModalOpen}
        onClose={closeModals}
        title="Crear Nueva Orden"
      >
        <form onSubmit={handleCreate}>
          <div className="modal-body">
            <div className="form-group">
              <label className="form-label">Cliente *</label>
              {loadingCustomers ? (
                <Loading message="Cargando clientes..." />
              ) : (
                <select
                  className="form-input form-select"
                  value={formData.customerId}
                  onChange={(e) => setFormData({ ...formData, customerId: e.target.value })}
                >
                  <option value="">Seleccionar cliente...</option>
                  {customers.map(customer => (
                    <option key={customer.id} value={customer.id}>
                      {customer.name} ({customer.email})
                    </option>
                  ))}
                </select>
              )}
            </div>
            <div className="form-group">
              <label className="form-label">Total *</label>
              <input
                type="number"
                step="0.01"
                min="0"
                className="form-input"
                placeholder="Ej: 150000"
                value={formData.total}
                onChange={(e) => setFormData({ ...formData, total: e.target.value })}
              />
              <small style={{ color: 'var(--text-secondary)', marginTop: '0.25rem', display: 'block' }}>
                Ingresa el monto total de la orden
              </small>
            </div>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={closeModals} disabled={formLoading}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={formLoading || loadingCustomers}>
              {formLoading ? 'Creando...' : 'Crear Orden'}
            </button>
          </div>
        </form>
      </Modal>

      {/* Search Order Modal */}
      <Modal
        isOpen={isSearchModalOpen}
        onClose={closeModals}
        title="Buscar Orden por ID"
      >
        <form onSubmit={handleSearch}>
          <div className="modal-body">
            <div className="form-group">
              <label className="form-label">ID de la Orden (GUID) *</label>
              <input
                type="text"
                className="form-input"
                placeholder="Ej: 12345678-1234-1234-1234-123456789012"
                value={searchId}
                onChange={(e) => setSearchId(e.target.value)}
                autoFocus
              />
            </div>

            {searchLoading && <Loading message="Buscando orden..." />}

            {foundOrder && (
              <div style={{ 
                marginTop: '1rem', 
                padding: '1rem', 
                background: 'var(--background)', 
                borderRadius: 'var(--radius)',
                border: '1px solid var(--border-color)'
              }}>
                <h4 style={{ marginBottom: '0.75rem', color: 'var(--secondary-color)' }}>
                  ✅ Orden Encontrada
                </h4>
                <div style={{ display: 'grid', gap: '0.5rem', fontSize: '0.875rem' }}>
                  <div><strong>ID:</strong> {foundOrder.id}</div>
                  <div><strong>Cliente:</strong> {getCustomerName(foundOrder.customerId)}</div>
                  <div><strong>Total:</strong> {formatCurrency(foundOrder.total)}</div>
                  <div><strong>Estado:</strong> <span className="badge badge-success">{foundOrder.status}</span></div>
                  <div><strong>Creada:</strong> {formatDate(foundOrder.createdAtUtc)}</div>
                </div>
              </div>
            )}
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={closeModals}>
              Cerrar
            </button>
            <button type="submit" className="btn btn-primary" disabled={searchLoading}>
              {searchLoading ? 'Buscando...' : 'Buscar'}
            </button>
          </div>
        </form>
      </Modal>
    </div>
  )
}

export default OrdersPage
