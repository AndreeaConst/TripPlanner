import React from 'react';
import { TextField, Card, CardContent, CardHeader, Button, InputAdornment } from "@mui/material";
import EmailIcon from '@mui/icons-material/Email';
import LockIcon from '@mui/icons-material/Lock';
import PersonIcon from '@mui/icons-material/Person'; // Import PersonIcon
import './Register.less';

const Register: React.FC = () => {
  return (
    <div className="register-container">
      <Card variant="outlined" sx={{ boxShadow: 3, width: '400px' }}>
        <CardHeader title="Create an Account" />
        <CardContent>
          <form>
            <div className="form-group">
              <TextField 
                id="name" 
                name="name" 
                label="Name" 
                variant="outlined" 
                fullWidth
                InputProps={{
                  startAdornment: (
                    <InputAdornment position="start">
                      <PersonIcon />
                    </InputAdornment>
                  ),
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
            <Button sx={{ marginTop: 5 }}  variant="contained" type="submit" fullWidth>Register</Button>
          </form>
        </CardContent>
      </Card>
    </div>
  );
};

export default Register;