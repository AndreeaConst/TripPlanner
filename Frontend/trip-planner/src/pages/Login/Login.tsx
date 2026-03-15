import React, { useContext, useState } from 'react';
import { TextField, Card, CardContent, CardHeader, Button, InputAdornment, Typography, IconButton } from "@mui/material";
import { Link } from 'react-router-dom';
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import './Login.less';
import type { LoginUserRequest } from '../../types/LoginUserRequest';
import { login } from '../../services/authService';
import { AuthContext } from '../../context/authContext';
import { Alert } from "@mui/material";
import { VisibilityOff, Visibility } from '@mui/icons-material';

const Login: React.FC = () => {
    const { setAuthenticated } = useContext(AuthContext);
    const [form, setForm] = useState({
        email: '',
        password: ''
      });
    const [errors, setErrors] = useState<Record<string, string>>({});
    const [errorMessage, setErrorMessage] = useState<string>("");
    const [showPassword, setShowPassword] = useState(false);

    const handleClickShowPassword = () => setShowPassword((prev) => !prev);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement, Element>) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    };
  
    const handleSubmit = async (e: React.SubmitEvent) => {
      e.preventDefault();
  
      const payload: LoginUserRequest = {
        email: form.email,
        password: form.password
      };
      try {
        await login(payload, setAuthenticated);
        setErrorMessage("");
        setErrors({});
      } catch (error: any) {
          const apiErrors = error?.errors;
          const apiMessage = error?.message;
          if (apiErrors || error?.message) {
            setErrors(apiErrors)
            setErrorMessage(apiMessage);
          }else {
            alert(error.message);
          }
      }
    };

  return (
      <div className="login-container">
        <Card variant="outlined" sx={{ boxShadow: 3, width: '400px' }}>
          <CardHeader title="Welcome Back!" align="center" />
                {/* Form-wide error */}
          {errorMessage && <Alert severity="error">{errorMessage}</Alert>}

          <CardContent>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <TextField
                  id="email"
                  name="email"
                  label="Email"
                  variant="outlined"
                  fullWidth
                  value={form.email}
                  error={!!errors?.email}
                  helperText={errors?.email}
                  required
                  onChange={(e) => handleChange(e)}
                  slotProps={{
                    input: {
                      startAdornment: (
                        <InputAdornment position="start">
                          <EmailIcon />
                        </InputAdornment>
                      )
                    },
                  }} />
              </div>
              <div className="form-group">
                <TextField
                  id="password"
                  name="password"
                  label="Password"
                  type={showPassword ? "text" : "password"} // toggle type
                  variant="outlined"
                  fullWidth
                  value={form.password}
                  error={!!errors?.password}
                  helperText={errors?.password}
                  required
                  onChange={(e) => handleChange(e)}
                  slotProps={{
                    input: {
                      startAdornment: (
                        <InputAdornment position="start">
                          <LockIcon />
                        </InputAdornment>
                      ),
                      endAdornment: (
                        <InputAdornment position="end">
                          <IconButton onClick={handleClickShowPassword} edge="end">
                            {showPassword ? <VisibilityOff /> : <Visibility />}
                          </IconButton>
                        </InputAdornment>
                      ),
                    },
                  }} />
              </div>

              <Typography variant="body2" align="right" className="links">
                <Link to="/forgot-password">Forgot password?</Link>
              </Typography>

              <Button sx={{ marginTop: 5 }} variant="contained" type="submit" fullWidth>Login</Button>

              <Typography className="links" variant="body2" align="center" sx={{ marginTop: 2 }}>
                <Link to="/register">New here? Create an account</Link>
              </Typography>
            </form>
          </CardContent>
        </Card>
      </div>

  );
};

export default Login;