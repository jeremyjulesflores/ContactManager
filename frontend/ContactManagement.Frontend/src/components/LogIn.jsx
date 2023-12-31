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
    const [fieldErrors, setFieldErrors] = useState({});
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
      const inputId = e.target.id;
      const inputValue = e.target.value;
  
      // Find the corresponding field's maxLength from signupFields array
      const field = fields.find(field => field.id === inputId);
      const maxLengthLimit = field ? field.maxLength : 50; // Default to 50 if field not found
      const minLengthLimit = field? field.minLength : 1;//Default is 1
      if (inputValue.length > maxLengthLimit) {
          return;
      }
  
      if(inputValue.length < minLengthLimit){
          setFieldErrors((prevErrors) => ({
            ...prevErrors,
            [inputId]: `Need atleast ${minLengthLimit} characters for this field`,
        }));
      }else{
        setFieldErrors((prevErrors) => ({
          ...prevErrors,
          [inputId]: '', // Clear the error when within limit
      }));
      }
  

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
          }else if(error.response.status === 500){
            setError("Server Error");          
          }
          else{
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
                  <div key={field.id}>
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
                    {fieldErrors[field.id] && <p className="text-red-500">{fieldErrors[field.id]}</p>}
                    </div>
                
                )
            }
            {error && <p className="text-red-500">{error}</p>} {/* Display error message if error is not empty */}
        </div>
        
        <FormExtra handleRememberMe={handleRememberMe}/>
        <FormAction handleSubmit={handleSubmit} text="Login" isLoading = {isLoading}/>

      </form>
    )
}