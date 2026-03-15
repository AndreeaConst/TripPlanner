export interface ApiError {
  code: string;
  message: string;
  errors?: Record<string, string>;
}

export interface ApiResponse<T> {
  data?: T;
  error?: ApiError;
}