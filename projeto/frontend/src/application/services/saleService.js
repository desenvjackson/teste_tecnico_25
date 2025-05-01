import apiClient from "../../infrastructure/api/apiClient";

export const getSaleById = async (id) => {
  const response = await apiClient.get(`/sales/${id}`);
  return response.data;
};

export const saveSale = async (sale) => {
  if (sale.id) {
    return await apiClient.put(`/sales/${sale.id}`, sale);
  } else {
    return await apiClient.post("/sales", sale);
  }
};

export const deleteSale = async (id) => {
  return await apiClient.delete(`/sales/${id}`);
};
