import React, { useContext, useState } from 'react';
import { TextField, Card, CardContent, CardHeader, Button, InputAdornment, Alert, IconButton } from "@mui/material";
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import PersonIcon from '@mui/icons-material/Person'; 
import './Register.less';
import type { RegisterUserRequest } from '../../types/RegisterUserRequest';
import { register } from '../../services/authService';
import { AuthContext } from '../../context/authContext';
import { Visibility, VisibilityOff } from '@mui/icons-material';

const Register: React.FC = () => {
  const { setAuthenticated } = useContext(AuthContext);
  const [form, setForm] = useState({
    email: '',
    password: '',
    name: ''
  });
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [showPassword, setShowPassword] = useState(false);

  const validatePassword = (pwd: string) => {
    if (pwd.length < 8) return "Password must be at least 8 characters";
    return "";
  };
  const handleClickShowPassword = () => setShowPassword((prev) => !prev);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement, Element>) => {
  const { name, value } = e.target;
  setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.SubmitEvent) => {
    e.preventDefault();

    const payload: RegisterUserRequest = {
      name: form.name,
      email: form.email,
      password: form.password
    };
    const pwdError = validatePassword(form.password);
    setErrors({ password: pwdError });

    if (pwdError) {
      return;
    }

    try {
      await register(payload, setAuthenticated);
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
    <div className="register-container">
      <Card variant="outlined" sx={{ boxShadow: 3, width: '400px' }}>
        <CardHeader title="Create an Account" />
              {/* Form-wide error */}
        {errorMessage && <Alert severity="error">{errorMessage}</Alert>}

        <CardContent>
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <TextField 
                id="name" 
                name="name" 
                label="Name" 
                variant="outlined" 
                fullWidth
                value={form.name}
                error={!!errors?.name}
                helperText={errors?.name}
                required
                onChange={(e) => handleChange(e)}
                slotProps={{
                  input: {
                    startAdornment: (
                      <InputAdornment position="start">
                        <PersonIcon />
                      </InputAdornment>
                    ),
                  },
                }}
              />
            </div>
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
                }} 
              />
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
                required
                helperText={errors?.password}
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
                }}
              />
            </div>
            <Button sx={{ marginTop: 5 }}  variant="contained" type="submit" fullWidth>Register</Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
};

export default Register;