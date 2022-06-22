import axios from "axios";
import Common from "../utility/Common";

export default axios.create({
  baseURL: Common.BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  withCredentials: true
});