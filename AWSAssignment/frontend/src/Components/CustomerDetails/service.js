import axios from "axios";
import { BASE_URL } from "../../variables";

const getCustomersById = (id) => {
  return axios.get(`${BASE_URL}/${id}`);
};

const putCustomer = (id, {name, email, contact, status}) => {
  let payload = new FormData();
  payload.append('name', name);
  payload.append('email', email);
  payload.append('contact', contact);
  payload.append('status', status);
  return axios.put(`${BASE_URL}/${id}`, payload);
};

export { getCustomersById, putCustomer };
