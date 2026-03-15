import type { RegisterUserRequest } from "../types/RegisterUserRequest";
import type { LoginResponse } from "../types/LoginResponse";
import httpClient from "../api/httpClient";
import type { LoginUserRequest } from "../types/LoginUserRequest";
import type { ApiResponse } from "../types/ApiError";

export const register = async (
  payload: RegisterUserRequest,
  setAuthenticated: (authenticated: boolean) => void
): Promise<LoginResponse> => {
    const res  = await httpClient.post<ApiResponse<LoginResponse>>(
      'api/auth/register',
      payload
    );
    if (res.data.error) {
      throw res.data.error;
    }

    setAuthenticated(true);
    return res.data.data;
};

export const login = async (
  payload: LoginUserRequest,
  setAuthenticated: (authenticated: boolean) => void
): Promise<LoginResponse> => {
    const res  = await httpClient.post<ApiResponse<LoginResponse>>(
      'api/auth/login',
      payload
    );
    if (res.data.error) {
      throw res.data.error;
    }

    setAuthenticated(true);
    return res.data.data;
};