import axios from "axios";

const httpClient = axios.create({
  baseURL: "https://localhost:7285",
  withCredentials: true,
  headers: {
    "Content-Type": "application/json"
  }
});

export default httpClient;
