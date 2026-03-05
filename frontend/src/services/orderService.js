import { apiFetch } from './api';

const ENDPOINT = '/orders';

export const orderService = {
  // Get all orders
  async getAll() {
    return apiFetch(ENDPOINT);
  },

  // Get order by ID
  async getById(id) {
    return apiFetch(`${ENDPOINT}/${id}`);
  },

  // Create new order
  async create(orderData) {
    return apiFetch(ENDPOINT, {
      method: 'POST',
      body: JSON.stringify(orderData),
    });
  },
};
