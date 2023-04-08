import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7275/'
});

export default api;