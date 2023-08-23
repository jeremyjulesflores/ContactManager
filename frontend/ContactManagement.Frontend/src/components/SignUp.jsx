
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
  const [fieldErrors, setFieldErrors] = useState({});
  const navigateToLogIn = () => {
    window.location.href = '/';
  };
  const handleChange = (e) => {
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


    
    setSignupState(prevState => ({
        ...prevState,
        [inputId]: inputValue
    }));
};

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
        await axios.post("http://localhost:7274/api/users/register", {
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
          }else if(error.response.status === 500){
            setError("Server error occurred");
          }
          else{
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
                  <div key={field.id}>
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
                    {fieldErrors[field.id] && <p className="text-red-500">{fieldErrors[field.id]}</p>}
                    </div>
                    
         
                
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