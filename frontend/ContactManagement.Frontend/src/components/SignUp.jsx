
import { useState, Fragment, React , useRef} from 'react';
import { signupFields } from "../constants/formFields"
import FormAction from "./FormAction";
import Input from "./Input";
import { Dialog, Transition } from '@headlessui/react'
import { CheckCircleIcon } from '@heroicons/react/24/outline'
import axios from 'axios';
import Popup from './Popup';

const fields=signupFields;
let fieldsState={};

fields.forEach(field => fieldsState[field.id]='');

export default function Signup(){
  const [open, setOpen] = useState(false)
  const [signupState,setSignupState]=useState(fieldsState);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const navigateToLogIn = () => {
    window.location.href = '/';
  };
  const handleChange=(e)=>setSignupState({...signupState,[e.target.id]:e.target.value});

  const handleSubmit=(e)=>{
    setError('');
    e.preventDefault();
    setIsLoading(true);
    console.log(signupState)
    createAccount()
  }

  //handle Signup API Integration here
  const createAccount= async () => {
      try{
        await axios.post("https://localhost:7274/api/users/register", {
              firstname : signupState.firstname,
              lastname : signupState.lastname,
              username: signupState.username,
              email : signupState.emailaddress,
              password: signupState.password,
              confirmpassword: signupState.confirmpassword
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response =>{
          if (response.status == 200){
            setOpen(true);
          }else{
            setError("Something went wrong");
          }
        })
      }
      catch(error){
        if(error.response){
          if(error.response.status === 400){
            setError("Invalid Credentials");
          }else{
            setError("Something went wrong");
          }
        }else if (error.message === "Network Error"){
          setError("Network Error");
          console.log(error);
        }else{
          setError("Something went wrong");
          console.log(error);
        }
      }
      finally{
        setIsLoading(false);
      }
    
      
  }

    return(
        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
        <div className="">
        {
                fields.map(field=>
                        <Input
                            key={field.id}
                            handleChange={handleChange}
                            value={signupState[field.id]}
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
          <FormAction handleSubmit={handleSubmit} text="Signup" isLoading={isLoading} />
        </div>

         

        <Popup
          open ={open}
          setOpen={setOpen}
          bigText= "Account Created"
          smallText = "Successfully Created an account"
          buttonText = "LogIn"
          onClickHandler = {navigateToLogIn}/>
      </form>




    )
}