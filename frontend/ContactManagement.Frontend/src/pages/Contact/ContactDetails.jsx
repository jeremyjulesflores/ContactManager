import { EnvelopeOpenIcon, FolderIcon, HomeIcon, HomeModernIcon, PencilIcon, PencilSquareIcon, PhoneIcon, PlusIcon, TrashIcon, UserCircleIcon } from "@heroicons/react/24/outline";
import { useParams } from "react-router-dom";
import { EnvelopeIcon, UserIcon } from '@heroicons/react/20/solid'
import { useState, useEffect } from "react";
import axios from "axios";
import Details from "../../components/Details";
import { addressTypes, emailTypes, numberTypes } from "../../constants/childrenTypes";
import { createChildrenAddress, createChildrenEmail, createChildrenNumber } from "../../constants/formFields";
import { createAddress, createEmail, createNumber, deleteAddress, deleteEmail, deleteNumber, getEmail, patchNotes, updateAddress, updateEmail, updateNumber } from "../../API service/ApiService";



const ContactDetails = () => {
    const {Id} = useParams();
    const [address, setAddress] = useState([]);
    const [email, setEmail] = useState([]);
    const [number, setNumber] = useState([]);
    const [contact, setContact]= useState([]);
    const [note, setNote] = useState('');
    const [editing, setEditing] = useState(false);
  



  const fetchContact = async () => {
    const authToken = localStorage.getItem('authToken');
    const headers = {
        'Authorization': `Bearer ${authToken}`,
        'Content-Type': 'application/json'
    }
    return await axios.get(`https://localhost:7274/api/contacts/${Id}`, { headers })
      .then(response => {
        if (response.status === 200) {
            return response.data;
        } else if (response.status === 401) {
            // Handle unauthorized access here (refresh token or redirect to login)
            return null;
        } else {
            throw new Error('Network response was not ok');
        }
      })
      .catch(error => {
          // Handle errors
          console.log(error);
      });
  };


  useEffect(() => {
    if(Id){
      fetchContact()
      .then(response => {
          setContact(response)
          setAddress(response.addresses);
          setNumber(response.numbers);
          setEmail(response.emails);
          setNote(response.note)
          console.log(response);
      })
    }
  }, [Id]); 

//NOTE HANDLERS
const toggleEditing = () => {
  setEditing(!editing);
};

const handleNoteChange = (e) => {
  setNote(e.target.value);
};

const handleSaveNote = () => {
  // Implement your save logic here
  updateNote();
  toggleEditing();
};

const updateNote = () =>{
  const request = {
    contactId : Id,
    notes : [{
      path : "/note",
      op : "replace",
      value : note
    }]
  }
  console.log(request);

  return(patchNotes(request));
  
}
//EMAIL HANDLERS

  const emailCreationHandler = (req) =>{
    const request = {
      emailAddress : req.emailAddress,
      type : req.type,
      Id : Id
    }
    console.log(request);
    return createEmail(request)
  }

  const emailUpdationHandler = (req) =>{
    const request = {
      emailAddress : req.emailAddress,
      type : req.type,
      contactId : Id,
      emailId : req.emailId
    }
    return updateEmail(request);
    
  }
  const emailDeletionHandler = (req) => {
    const request = {
      contactId : Id,
      emailId : req
    }

    return(deleteEmail(request));
  }
  //NUMBER HANDLERS
  const numberCreationHandler = (req) =>{
    const request = {
      contactNumber : req.contactNumber,
      type: req.type,
      Id:Id
    }
    console.log(request);
    return createNumber(request);
  }

  const numberUpdationHandler = (req) =>{
    const request = {
      contactNumber : req.contactNumber,
      type: req.type,
      contactId :Id,
      numberId : req.childId
    }
    return updateNumber(request);
  }
  const numberDeletionHandler = (req)=>{
    const request = {
      contactId : Id,
      numberId : req
    }

    return(deleteNumber(request));
  }
  const addressCreationHandler = (req) =>{
    const request ={
      addressDetails : req.addressDetails,
      type: req.type,
      Id:Id
    }

    return createAddress(request);
  }
  const addressUpdationHandler = (req) =>{
    const request ={
      addressDetails : req.addressDetails,
      type: req.type,
      addressId : req.childId,
      contactId:Id
    }
    return updateAddress(request);
  }
  const addressDeletionHandler = (req)=>{
    const request = {
      contactId : Id,
      addressId : req
    }
    return(deleteAddress(request));
  }

  

  return (
    <div>
      {Id === undefined ? 
      <>
        <main className="grid min-h-full place-items-center bg-white px-6 py-24 sm:py-32 lg:px-8">
          <div className="text-center items-center justify-center flex flex-col">
            <UserCircleIcon className= "h-20 w-20"/>
            <h1 className="mt-4 text-3xl font-bold tracking-tight text-gray-900 sm:text-5xl">No Contact Selected</h1>
            <p className="mt-6 text-base leading-7 text-gray-600">Select a contact on the contacts list</p>
          </div>
        </main>
      </>
    :

    <div className="mt-10 mb-10">
    <div className="flex justify-center">
        <UserIcon className="h-14 w-14" />
    </div>
    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
       {contact.firstName} {contact.lastName}
    </h2>
      <div className="mt-6 border-t border-gray-100">
          <dl className="divide-y divide-gray-100">
            <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
              <dt className="text-xl font-medium leading-6 text-gray-900 flex">
                <EnvelopeOpenIcon className="h-7 w-10"/>
                Emails
              </dt>
              <Details 
                title ="Email"
                object={email}
                NoText="No Emails"
                details="emailAddress"
                choices={emailTypes}
                field= {createChildrenEmail}
                create={emailCreationHandler}
                update={emailUpdationHandler}
                del ={emailDeletionHandler}/>
            </div>
            <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
              <dt className="text-xl font-medium leading-6 text-gray-900 flex">
                <PhoneIcon className ="h-7 w-10"/>
                Numbers
              </dt>
              <Details
                title = "Number"
                object = {number}
                NoText = "No Numbers"
                details ="contactNumber"
                choices={numberTypes}
                field = {createChildrenNumber}
                create= {numberCreationHandler}
                update= {numberUpdationHandler}
                del ={numberDeletionHandler}
              />
            </div>
            <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
              <dt className="text-xl font-medium leading-6 text-gray-900 flex">
                <HomeIcon className ="h-7 w-10"/>
                Address
              </dt>
              <Details
                title = "Address"
                object = {address}
                NoText = "No Address"
                details ="addressDetails"
                choices={addressTypes}
                field = {createChildrenAddress}
                create= {addressCreationHandler}
                update= {addressUpdationHandler}
                del ={addressDeletionHandler}
              />
            </div>
            <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
              <dt className="text-xl font-medium leading-6 text-gray-900 flex">
                <HomeIcon className ="h-7 w-10"/>
                Note
              </dt>
              <dd className="mt-1 text-sm leading-6 text-gray-700 sm:col-span-2 sm:mt-0">
              {!editing ? (
          <>
            <PencilSquareIcon
              className="h-7 w-4 hover:text-green-400 cursor-pointer"
              onClick={toggleEditing}
            />
      
    
            <p>{note}</p>
            
          </>
        ) : (
          <>
            <textarea
              className="w-full p-2 border border-gray-300 rounded-md"
              rows="4"
              value={note}
              onChange={handleNoteChange}
            />
            <div className="mt-2">
              <button
                className="px-4 py-2 bg-blue-600 text-white rounded-md mr-2"
                onClick={handleSaveNote}
              >
                Save
              </button>
              <button
                className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md"
                onClick={toggleEditing}
              >
                Cancel
              </button>
            </div>
          </>
        )}
                
                
                
              </dd>
            </div>
          </dl>
      </div>
    
    </div>
    
    }
    </div>
  )
};

export default ContactDetails
