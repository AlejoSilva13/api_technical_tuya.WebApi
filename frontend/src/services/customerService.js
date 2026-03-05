import { apiFetch } from './api';

const ENDPOINT = '/customers';

export const customerService = {
  // Get all customers
  async getAll() {
    return apiFetch(ENDPOINT);
  },

  // Get customer by ID
  async getById(id) {
    return apiFetch(`${ENDPOINT}/${id}`);
  },

  // Create new customer
  async create(customerData) {
    return apiFetch(ENDPOINT, {
      method: 'POST',
      body: JSON.stringify(customerData),
    });
  },

  // Update existing customer
  async update(id, customerData) {
    return apiFetch(`${ENDPOINT}/${id}`, {
      method: 'PUT',
      body: JSON.stringify(customerData),
    });
  },

  // Delete customer
  async delete(id) {
    return apiFetch(`${ENDPOINT}/${id}`, {
      method: 'DELETE',
    });
  },
};
