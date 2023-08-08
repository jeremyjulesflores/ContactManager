import { useState } from 'react';
import { loginFields } from "../constants/formFields";
import FormAction from "./FormAction";
import FormExtra from "./FormExtra";
import Input from "./Input";

const fields=loginFields;
let fieldsState = {};
fields.forEach(field=>fieldsState[field.id]='');

export default function Login(){
    const [loginState,setLoginState]=useState(fieldsState);
    const [error, setError] = useState('');


    const handleChange=(e)=>{
        setLoginState({...loginState,[e.target.id]:e.target.value})

    }

    const handleSubmit=(e)=>{
        e.preventDefault();
        authenticateUser();
    }

    //Authenticate VIA API
    const authenticateUser = () =>{
      const url = `https://localhost:7274/api/users/login`;
        fetch(url,{
          method: 'POST',
          headers:{
            'Content-Type': 'application/json'
          },
          body:JSON.stringify({
            username: loginState.username,
            password: loginState.password
          })
        }).then(response=>response.text())
        .then(data=>{
          if(data.length <= 30){
            setError(data)
          }else{
            localStorage.setItem('authToken', data)
            window.location.href = '/home';
          }
        })
        .catch(e=> {
          console.log(e)
          setError(e)
        })
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
        
        <FormExtra/>
        <FormAction handleSubmit={handleSubmit} text="Login"/>

      </form>
    )
}