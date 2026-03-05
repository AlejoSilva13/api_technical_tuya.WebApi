import { useState, useEffect } from 'react'
import toast from 'react-hot-toast'
import { customerService } from '../services'
import { Modal, Loading, EmptyState, ConfirmDialog } from '../components'

function CustomersPage() {
  const [customers, setCustomers] = useState([])
  const [loading, setLoading] = useState(true)
  const [search, setSearch] = useState('')
  
  // Modal states
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const [isEditModalOpen, setIsEditModalOpen] = useState(false)
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false)
  const [selectedCustomer, setSelectedCustomer] = useState(null)
  const [formLoading, setFormLoading] = useState(false)
  
  // Form state
  const [formData, setFormData] = useState({ name: '', email: '' })

  useEffect(() => {
    loadCustomers()
  }, [])

  const loadCustomers = async () => {
    try {
      setLoading(true)
      const data = await customerService.getAll()
      setCustomers(data)
    } catch (error) {
      toast.error('Error al cargar clientes: ' + error.message)
    } finally {
      setLoading(false)
    }
  }

  const handleCreate = async (e) => {
    e.preventDefault()
    if (!formData.name.trim() || !formData.email.trim()) {
      toast.error('Todos los campos son requeridos')
      return
    }

    try {
      setFormLoading(true)
      const newCustomer = await customerService.create(formData)
      setCustomers([...customers, newCustomer])
      setIsCreateModalOpen(false)
      setFormData({ name: '', email: '' })
      toast.success('Cliente creado exitosamente')
    } catch (error) {
      toast.error('Error al crear cliente: ' + error.message)
    } finally {
      setFormLoading(false)
    }
  }

  const handleEdit = async (e) => {
    e.preventDefault()
    if (!formData.name.trim() || !formData.email.trim()) {
      toast.error('Todos los campos son requeridos')
      return
    }

    try {
      setFormLoading(true)
      const updatedCustomer = await customerService.update(selectedCustomer.id, formData)
      setCustomers(customers.map(c => c.id === selectedCustomer.id ? updatedCustomer : c))
      setIsEditModalOpen(false)
      setSelectedCustomer(null)
      setFormData({ name: '', email: '' })
      toast.success('Cliente actualizado exitosamente')
    } catch (error) {
      toast.error('Error al actualizar cliente: ' + error.message)
    } finally {
      setFormLoading(false)
    }
  }

  const handleDelete = async () => {
    try {
      setFormLoading(true)
      await customerService.delete(selectedCustomer.id)
      setCustomers(customers.filter(c => c.id !== selectedCustomer.id))
      setIsDeleteDialogOpen(false)
      setSelectedCustomer(null)
      toast.success('Cliente eliminado exitosamente')
    } catch (error) {
      toast.error('Error al eliminar cliente: ' + error.message)
    } finally {
      setFormLoading(false)
    }
  }

  const openEditModal = (customer) => {
    setSelectedCustomer(customer)
    setFormData({ name: customer.name, email: customer.email })
    setIsEditModalOpen(true)
  }

  const openDeleteDialog = (customer) => {
    setSelectedCustomer(customer)
    setIsDeleteDialogOpen(true)
  }

  const closeModals = () => {
    setIsCreateModalOpen(false)
    setIsEditModalOpen(false)
    setIsDeleteDialogOpen(false)
    setSelectedCustomer(null)
    setFormData({ name: '', email: '' })
  }

  const filteredCustomers = customers.filter(customer =>
    customer.name.toLowerCase().includes(search.toLowerCase()) ||
    customer.email.toLowerCase().includes(search.toLowerCase())
  )

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString('es-ES', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    })
  }

  return (
    <div className="customers-page">
      <div className="page-header">
        <h1 className="page-title">👥 Gestión de Clientes</h1>
        <button 
          className="btn btn-primary"
          onClick={() => setIsCreateModalOpen(true)}
        >
          + Nuevo Cliente
        </button>
      </div>

      {/* Search and Stats */}
      <div className="stats-grid" style={{ gridTemplateColumns: 'repeat(auto-fit, minmax(150px, 1fr))' }}>
        <div className="stat-card">
          <div className="stat-value">{customers.length}</div>
          <div className="stat-label">Total Clientes</div>
        </div>
        <div className="stat-card" style={{ gridColumn: 'span 2' }}>
          <div className="search-box" style={{ maxWidth: '100%' }}>
            <span className="search-icon">🔍</span>
            <input
              type="text"
              className="form-input"
              placeholder="Buscar por nombre o email..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              style={{ paddingLeft: '2.5rem' }}
            />
          </div>
        </div>
      </div>

      {/* Table */}
      <div className="card">
        <div className="card-body" style={{ padding: 0 }}>
          {loading ? (
            <Loading message="Cargando clientes..." />
          ) : filteredCustomers.length === 0 ? (
            <EmptyState
              icon="👥"
              title={search ? "Sin resultados" : "No hay clientes"}
              description={search ? "No se encontraron clientes con ese criterio" : "Comienza agregando tu primer cliente"}
              action={!search && (
                <button className="btn btn-primary" onClick={() => setIsCreateModalOpen(true)}>
                  + Crear Primer Cliente
                </button>
              )}
            />
          ) : (
            <div className="table-container">
              <table className="table">
                <thead>
                  <tr>
                    <th>Nombre</th>
                    <th>Email</th>
                    <th>Fecha de Creación</th>
                    <th style={{ width: '150px' }}>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredCustomers.map(customer => (
                    <tr key={customer.id}>
                      <td>
                        <strong>{customer.name}</strong>
                        <br />
                        <small style={{ color: 'var(--text-secondary)' }}>
                          {customer.id.substring(0, 8)}...
                        </small>
                      </td>
                      <td>{customer.email}</td>
                      <td>{formatDate(customer.createdAtUtc)}</td>
                      <td>
                        <div className="table-actions">
                          <button
                            className="btn btn-secondary btn-sm"
                            onClick={() => openEditModal(customer)}
                            title="Editar"
                          >
                            ✏️ Editar
                          </button>
                          <button
                            className="btn btn-danger btn-sm"
                            onClick={() => openDeleteDialog(customer)}
                            title="Eliminar"
                          >
                            🗑️
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>

      {/* Create Modal */}
      <Modal
        isOpen={isCreateModalOpen}
        onClose={closeModals}
        title="Crear Nuevo Cliente"
      >
        <form onSubmit={handleCreate}>
          <div className="modal-body">
            <div className="form-group">
              <label className="form-label">Nombre *</label>
              <input
                type="text"
                className="form-input"
                placeholder="Ej: Juan Pérez"
                value={formData.name}
                onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                autoFocus
              />
            </div>
            <div className="form-group">
              <label className="form-label">Email *</label>
              <input
                type="email"
                className="form-input"
                placeholder="Ej: juan@email.com"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
              />
            </div>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={closeModals} disabled={formLoading}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={formLoading}>
              {formLoading ? 'Creando...' : 'Crear Cliente'}
            </button>
          </div>
        </form>
      </Modal>

      {/* Edit Modal */}
      <Modal
        isOpen={isEditModalOpen}
        onClose={closeModals}
        title="Editar Cliente"
      >
        <form onSubmit={handleEdit}>
          <div className="modal-body">
            <div className="form-group">
              <label className="form-label">Nombre *</label>
              <input
                type="text"
                className="form-input"
                placeholder="Ej: Juan Pérez"
                value={formData.name}
                onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                autoFocus
              />
            </div>
            <div className="form-group">
              <label className="form-label">Email *</label>
              <input
                type="email"
                className="form-input"
                placeholder="Ej: juan@email.com"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
              />
            </div>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={closeModals} disabled={formLoading}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-success" disabled={formLoading}>
              {formLoading ? 'Guardando...' : 'Guardar Cambios'}
            </button>
          </div>
        </form>
      </Modal>

      {/* Delete Confirmation */}
      <ConfirmDialog
        isOpen={isDeleteDialogOpen}
        onClose={closeModals}
        onConfirm={handleDelete}
        title="Eliminar Cliente"
        message={`¿Estás seguro de eliminar a "${selectedCustomer?.name}"? Esta acción no se puede deshacer.`}
        confirmText="Sí, Eliminar"
        isLoading={formLoading}
      />
    </div>
  )
}

export default CustomersPage
