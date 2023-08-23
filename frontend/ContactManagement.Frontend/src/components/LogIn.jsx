import { useState, useEffect } from 'react';
import { loginFields } from "../constants/formFields";
import FormAction from "./FormAction";
import FormExtra from "./FormExtra";
import Input from "./Input";
import axios from 'axios';




const fields=loginFields;
let fieldsState = {};
fields.forEach(field=>fieldsState[field.id]='');

export default function Login(){
    const [loginState,setLoginState]=useState(fieldsState);
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const[rememberMe, setRememberMe] = useState(false);

    useEffect(()=>{
      const storedRememberMe = localStorage.getItem('rememberMe');
      const token = localStorage.getItem('authToken');
      if(storedRememberMe === 'true'){
        setRememberMe(true);
        if(isAuthTokenValid(token)){
          window.location.href ='/contacts';
        }
      }
    })
    const handleRememberMe = (e) =>{
      setRememberMe(e.target.checked); 
    }

    const handleChange=(e)=>{
        setLoginState({...loginState,[e.target.id]:e.target.value})

    }

    const handleSubmit=(e)=>{
        setError("");
        setIsLoading(true);
        e.preventDefault();
        authenticateUser();
    }


   
    const isAuthTokenValid = async (authToken) =>{
      try{
        const response = await axios.post('http://localhost:7274/api/users/check', {
          token : authToken,
          username : loginState.username
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
          if(response.status === 200){
            return true;
          }else{
            return false;
          }
        })
        .catch(error =>{
          //Handle errors
        });
      }
      catch(error){
        //Handle errors
        return false;
      }
       

    };
    //Authenticate VIA API
    const authenticateUser = async () =>{
      try{
        await axios.post("http://localhost:7274/api/users/login", {
          username: loginState.username,
          password: loginState.password
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response =>{
          if (response.status == 200){
  
            if(isAuthTokenValid(response.data)){
              console.log("Valid Token");
              localStorage.setItem('authToken', response.data)
              window.location.href ='/contacts';
              if(rememberMe){
                localStorage.setItem('rememberMe', rememberMe )
              }else if(!rememberMe){
                localStorage.removeItem('rememberMe');
              }
            }
          }else{
            setError("Something went wrong");
          }
        })
      }
      catch(error){
        if(error.response){
          if(error.response.status === 401){
            setError("Invalid Credentials");
          }else{
            setError("Something went wrong");
          }
        }else if (error.message === "Network Error"){
          setError("Network Error");
          console.log(error);
        }else{
          setError("Something went wrong");
        }
      }
      finally{
        setIsLoading(false);
      }
    }

    return(
        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
        <div className="-space-y-px">
            {
                fields.map(field=>
                        <Input
                            key={field.id}
                            handleChange={handleChange}
                            value={loginState[field.id]}
                            labelText={field.labelText}
                            labelFor={field.labelFor}
                            id={field.id}
                            name={field.name}
                            type={field.type}
                            isRequired={field.isRequired}
                            placeholder={field.placeholder}
                    />
                
                )
            }
            {error && <p className="text-red-500">{error}</p>} {/* Display error message if error is not empty */}
        </div>
        
        <FormExtra handleRememberMe={handleRememberMe}/>
        <FormAction handleSubmit={handleSubmit} text="Login" isLoading = {isLoading}/>

      </form>
    )
}