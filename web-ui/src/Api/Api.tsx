import axios from "axios";

const BASE_URL = "https://localhost:7083/api/v1";

//axios.defaults.withCredentials = true
//axios.defaults.headers.common['Content-Type'] = 'application/json;charset=UTF-8';
    

// For public API access
export default axios.create({
  baseURL: BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  withCredentials: true
});