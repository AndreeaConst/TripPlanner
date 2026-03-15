import httpClient from "./httpClient";

httpClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const data = error.response?.data;
    
    if (error.response?.status === 401) {
      try {
        await httpClient.post("/auth/refresh");
        return httpClient(error.config);
      } catch {
        window.location.href = "/login";
      }
    }

    if (data?.error?.errors || data?.error?.message) {
      return Promise.reject({
        type: "validation",
        errors: data?.error?.errors,
        message: data?.error?.message
      });
    }

    return Promise.reject({
      type: "generic",
      message: "Something went wrong"
    });
  }
);