import React from 'react';
import { TextField, Card, CardContent, CardHeader, Button, InputAdornment, Typography } from "@mui/material";
import { Link } from 'react-router-dom';
import EmailIcon from '@mui/icons-material/Email';
import './ForgotPassword.less';

const ForgotPassword: React.FC = () => {
  return (
    <div className="forgot-password-container">
      <Card variant="outlined" sx={{ boxShadow: 3, width: '400px' }}>
        <CardHeader title="Forgot Password?" align="center" />
        <CardContent>
          <Typography variant="body2" align="center" sx={{ marginBottom: 2 }}>
            Enter your email address below, and we'll send you a link to reset your password.
          </Typography>
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
            <Button sx={{ marginTop: 3 }} variant="contained" type="submit" fullWidth>
              Send Reset Link
            </Button>
          </form>
          <Typography className="links" variant="body2" align="center" sx={{ marginTop: 2 }}>
            <Link to="/">Back to Login</Link>
          </Typography>
        </CardContent>
      </Card>
    </div>
  );
};

export default ForgotPassword;