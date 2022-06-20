import axios from "axios";

const BASE_URL = "https://localhost:7083/api/v1";

export default axios.create({
  baseURL: BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  withCredentials: true
});