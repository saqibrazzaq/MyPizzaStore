import axios from "axios";

const BASE_URL = "https://localhost:7083/api/v1";

axios.defaults.withCredentials = true

// For public API access
export default axios.create({
  baseURL: BASE_URL
});