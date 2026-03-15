import { createContext, useState } from "react";

export const AuthContext = createContext({
  isAuthenticated: false,
  setAuthenticated: (_: boolean) => {}
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [isAuthenticated, setAuthenticated] = useState(false);

  return (
    <AuthContext.Provider value={{ isAuthenticated, setAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
};
