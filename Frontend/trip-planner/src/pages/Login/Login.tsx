import React from 'react';
import { TextField, Card, CardContent, CardHeader, Button, InputAdornment, Typography } from "@mui/material";
import { Link } from 'react-router-dom';
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import './Login.less';

const Login: React.FC = () => {
  return (
    <div className="login-container">
      <Card variant="outlined" sx={{ boxShadow: 3, width: '400px' }}>
        <CardHeader title="Welcome Back!" align="center" />
        
        <CardContent>
          <form>
            <div className="form-group">
              <TextField 
                id="email" 
                name="email" 
                label="Email" 
                variant="outlined" 
                fullWidth 
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <EmailIcon />
                    </InputAdornment>
                  ),
                }}
              />
            </div>
            <div className="form-group">
              <TextField 
                id="password" 
                name="password" 
                label="Password" 
                type="password" 
                variant="outlined" 
                fullWidth 
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <LockIcon />
                    </InputAdornment>
                  ),
                }}
              />
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