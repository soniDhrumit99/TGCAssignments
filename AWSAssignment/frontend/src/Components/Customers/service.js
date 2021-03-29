import axios from 'axios';
import { BASE_URL } from '../../variables';

const getAllCustomers = () => {
  return axios.get(`${BASE_URL}`);
}

export default getAllCustomers;