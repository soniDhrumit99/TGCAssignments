import axios from 'axios';
import { BASE_URL } from '../../../variables';

const postCustomer = ({ name, email, password, contact, photo }) => {
  let payload = new FormData();
  payload.append('name', name);
  payload.append('email', email);
  payload.append('password', password);
  payload.append('contact', contact);
  payload.append('photo', photo);
  return axios.post(`${BASE_URL}`, payload, {
    headers: {
      "Content-Type": "multipart/form-data",
    }
  });
};

export default postCustomer;