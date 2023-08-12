import { Fragment, useState} from 'react'
import { Dialog, Transition } from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/outline'
import { createContactFields } from "../../constants/formFields";
import Input from "../../components/Input";
import axios from 'axios';
import FormAction from "../../components/FormAction"
import Popup from '../../components/Popup';
import { useParams } from 'react-router-dom';


const fields= createContactFields;
let fieldsState = {};
fields.forEach(field=>fieldsState[field.id]='');

export default function CreateContact({isCreateOpen, setIsCreateOpen}) {
    const {Id} = useParams();
    const [contactCreateState,setContactCreateState]=useState(fieldsState);
    const [open, setOpen] = useState(false)
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const handleChange=(e)=>{
        setContactCreateState({...contactCreateState,[e.target.id]:e.target.value})
    }

    const handleSubmit=(e)=>{
        setError("");
        setIsLoading(true);
        e.preventDefault();
        createContact();
    }

    const createContact = async () =>{
        try{
            const authToken = localStorage.getItem('authToken');
            await axios.post("https://localhost:7274/api/contacts", {
              firstname : contactCreateState.firstname,
              lastname : contactCreateState.lastname,
            }, {
                headers: {
                    'Authorization': `Bearer ${authToken}`,
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                if (response.status === 200) {
                    setOpen(true);
                } else {
                    throw new Error('Something went wrong');
                }
            })
        }
        catch(error){
            if(error.response){
                if(error.response.status === 404){
                    setError("User Not Found");
                    
                }else{
                  //Handle
                }
            }else if (error.message === "Network Error"){
                setError("Network Error");
                console.log(error);
            }else{
                setError("Failed To Create User. Try Again Later");
                console.log(error);
            }
        }
        finally{
            setIsLoading(false);
        }
    }



  return (
    <Transition.Root show={isCreateOpen} as={Fragment}>
      <Dialog as="div" className="relative z-10" onClose={()=> {setIsCreateOpen(false); setIsLoading(false)}}>
        <Transition.Child
          as={Fragment}
          enter="ease-in-out duration-500"
          enterFrom="opacity-0"
          enterTo="opacity-100"
          leave="ease-in-out duration-500"
          leaveFrom="opacity-100"
          leaveTo="opacity-0"
        >
          <Dialog.Overlay className="fixed inset-0 bg-black bg-opacity-50" />

        </Transition.Child>

        <div className="fixed inset-0 overflow-hidden">
          <div className="absolute inset-0 overflow-hidden">
            <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
              <Transition.Child
                as={Fragment}
                enter="transform transition ease-in-out duration-500 sm:duration-700"
                enterFrom="translate-x-full"
                enterTo="translate-x-0"
                leave="transform transition ease-in-out duration-500 sm:duration-700"
                leaveFrom="translate-x-0"
                leaveTo="translate-x-full"
              >
                <Dialog.Panel className="pointer-events-auto relative w-screen max-w-md">
                  <Transition.Child
                    as={Fragment}
                    enter="ease-in-out duration-500"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in-out duration-500"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                  >
                    <div className="absolute left-0 top-0 -ml-8 flex pr-2 pt-4 sm:-ml-10 sm:pr-4">
                      <button
                        type="button"
                        className="relative rounded-md text-gray-300 hover:text-white focus:outline-none focus:ring-2 focus:ring-white"
                        onClick={()=> {setIsCreateOpen(false); setIsLoading(false)}}
                      >
                        <span className="absolute -inset-2.5" />
                        <span className="sr-only">Close panel</span>
                        <XMarkIcon className="h-6 w-6" aria-hidden="true" />
                      </button>
                    </div>
                  </Transition.Child>
                  <div className="flex flex-col overflow-y-scroll bg-white py-6 shadow-xl h-screen-3/4">
                    <div className="px-4 sm:px-6">
                      <Dialog.Title className="text-base font-semibold leading-10 text-gray-900">
                        Create Contact
                      </Dialog.Title>
                    </div>
                    <div className="relative mt-6 flex-1 px-4 sm:px-6">{/* Create Contact Form Here */
                   
                        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                            <div className="-space-y-px">
                                {
                                    
                                    fields.map(field=>
                                            <Input
                                                key={field.id}
                                                handleChange={handleChange}
                                                value={contactCreateState[field.id]}
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
                            <FormAction handleSubmit={handleSubmit} text="Create Contact" isLoading = {isLoading}/>
                            <Popup
                                open ={open}
                                setOpen={setOpen}
                                bigText= "Contact Created"
                                body = {
                                <p className="text-sm text-gray-500">
                                  Successfully Created a Contact
                                </p>}
                                buttonText = "Ok"
                                onClickHandler = {()=> {window.location.href = `/contacts/`}}/>
                        </form>
                    
                    
                    }</div>
                  </div>
                </Dialog.Panel>
              </Transition.Child>
            </div>
          </div>
        </div>
      </Dialog>
    </Transition.Root>
  )
}