import React, { useState } from 'react';
import axios from 'axios';  
import { FaSpinner } from 'react-icons/fa';

const Login = ({onLogin}) =>{
    const[email, setEmail] = useState('');
    const[password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
  
    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        try {
            const response = await axios.post(process.env.REACT_APP_LOGIN_USER, {
              Email: email,
              Password: password
            });
            
            const result = response.data;        
            if (result !== '') {
              localStorage.setItem('token',result.data);
              console.log("Upao u trazenje emaila:");
              console.log(email);
              axios.get(process.env.REACT_APP_GET_USER_BY_EMAIL,{
                    params:{
                        email:email
                    }
                }
                )
                .then((result) => {
                  if (result.data !== null) {
                    const userData = result.data;
                    onLogin(userData);
                  }
                });
            }
          } catch (error) {
            window.alert("Wrong email or password")
          }finally {
            setLoading(false); // Set loading state back to false after API call
          }
      };
  
    
    return(  
        <form onSubmit={handleSubmit} className="login-form">
            <br/>
            <br/>
            <br/>
            <br/>
            <h3 style={{color:'white'}}>Log in</h3>
            <div className="form-group">
                <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="form-input"
                />
            </div>
            <div className="form-group">
                <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="form-input"
                />
            </div>
            <button type="submit" className="submit-button" disabled={loading}>
            {loading ? <FaSpinner className="spinner" /> : 'Log In'}
            </button>
            <a href='/registration'>Dont have an account? Register now!</a>
        </form>
        
    )  
};
export default Login
