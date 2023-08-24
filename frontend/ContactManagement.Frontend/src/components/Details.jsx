import { HandThumbDownIcon, PencilIcon, PencilSquareIcon, PlusIcon, TrashIcon } from "@heroicons/react/24/outline";
import { useState } from "react";
import DropBox from "./DropBox";
import Slider from "./Slider";
import Input from "./Input";
import FormAction from "./FormAction";
import { useParams } from "react-router-dom";
import Popup from "./Popup";



const Details = ({
    title,
    object,
    NoText,
    details,
    choices,
    field,
    create,
    update,
    del,
}) => {
    const {Id} = useParams();
    const [inputText, setInputText]=useState('');
    const [chosen, setChosen] = useState(null);
    const [error, setError] = useState('');
    const [isLoading, setIsLoading]= useState(false);
    const [open, setOpen] = useState(false)
    const [updateOpen, setUpdateOpen] = useState(false);
    const [popOpen, setPopOpen] = useState(false);
    const [currObject, setCurrObject]= useState({});
    const [updatePopOpen, setUpdatePopOpen] = useState(false);
    const [deletePopOpen, setDeletePopOpen] = useState(false);
    
   

    const handleChange=(e)=>{
        setInputText(e.target.value)
    }
    const handleSubmit= async(e)=>{
        setError("");
        setIsLoading(true);
        e.preventDefault();
        const request = {
            [details] : inputText,
            type : chosen.name
        }
        
        if(await create(request) !== false){
            setPopOpen(true);
            setInputText('');
        }else{
            setError(`Failed to Create ${title}`);
        }
        setIsLoading(false);
        

    }

    const handleUpdateSubmit = async(e) =>{
        setError("");
        setIsLoading(true);
        e.preventDefault();
        const request = {
            [details] : inputText,
            type :chosen.name,
            childId : currObject.id
        }
        console.log(`request: ${update(request)}`);
        if(await update(request) !== false){
            setUpdatePopOpen(true);
            setInputText('');
        }else{
            setError(`Failed to Update ${title}`);
        }
        setIsLoading(false);
        
    }
    const handleDeleteSubmit = async()=>{
        setIsLoading(true);
        if(await del(currObject.id) !== false){
            setIsLoading(false);
        }
        if(isLoading){

        }else{
            window.location.href = `/contacts/${Id}`
        }
    }
    
  return (
    <dd className="mt-1 text-xs leading-6 text-gray-700 sm:col-span-2 sm:mt-0 flex">{object.length === 0 ?
        <div>
          {NoText}
          <PlusIcon className ="mt-2 h-5 w-5 text-gray-500 hover:text-green-400" onClick={()=>{setError(''); setInputText('');{setOpen(true)}}}/>
        </div> :  
          <div className="flex-1 overflow-y-auto">
            {object.map((object) => (   
              <div key = {object.id}>
                <div className="text-gray-900 text-sm flex-1 flex">
                  <p className="w-4/5">{object[details]}</p>
                  <PencilSquareIcon className="h-7 w-4 text-gray-500 hover:text-green-400" onClick = {() =>{setError(''); setInputText(object[details]); setUpdateOpen(true); {setCurrObject(object)}}}/>
                  <TrashIcon className="h-7 w-4 text-gray-500 hover:text-red-400" onClick={()=>{setCurrObject(object);{setDeletePopOpen(true)}}}/>
                </div>
                <p className="text-gray-400">{object.type}</p>
              </div>
            ))}
            <PlusIcon className ="mt-5 h-5 w-5 text-gray-500 hover:text-green-400" onClick={()=>{setInputText(''); setError(''); {setOpen(true)}}}/>
          </div>
        
        }
        <Slider
        open = {open}
        setOpen = {setOpen}
        title = {`Create ${title}`}
        body = {
            <div>
                <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                    <div className="-space-y-px">                
                        <Input
                            handleChange={handleChange}
                            value={inputText}
                            labelText={field.labelText}
                            labelFor={field.labelFor}
                            id={field.id}
                            name={field.name}
                            type={field.type}
                            isRequired={field.isRequired}
                            placeholder={field.placeholder}/>
                    </div>
                    <DropBox
                    choices = {choices}
                    label = "Type"
                    setChosen = {setChosen}/>

                    {error && <p className="text-red-500">{error}</p>} {/* Display error message if error is not empty */}
                    <FormAction handleSubmit={handleSubmit} text={title} isLoading = {isLoading}/>
                    <Popup
                        open ={popOpen}
                        setOpen={setPopOpen}
                        bigText= {`Created ${title}`}
                        body = {
                        <p className="text-sm text-gray-500">
                            {title} Created Successfully
                        </p>}
                        buttonText = "Ok"
                        onClickHandler = {()=> {setOpen(false);{window.location.href = `/contacts/${Id}`}}}/>
                    </form>                        
            </div>
                }/>
        <Slider
        open = {updateOpen}
        setOpen={setUpdateOpen}
        title = {`Update ${title}`}
        body ={
            <div>
                <form className="mt-8 space-y-6" onSubmit={handleUpdateSubmit}>
                    <div className="-space-y-px">                
                        <Input
                            handleChange={handleChange}
                            value={inputText}
                            labelText={field.labelText}
                            labelFor={field.labelFor}
                            id={field.id}
                            name={field.name}
                            type={field.type}
                            isRequired={field.isRequired}
                            placeholder={field.placeholder}/>
                    </div>
                    <DropBox
                    choices = {choices}
                    initialChoice={currObject.type}
                    label = "Type"
                    setChosen = {setChosen}/>

                    {error && <p className="text-red-500">{error}</p>} {/* Display error message if error is not empty */}
                    <FormAction handleSubmit={handleUpdateSubmit} text={`Update ${title}`} isLoading = {isLoading}/>
                    <Popup
                        open ={updatePopOpen}
                        setOpen={setUpdatePopOpen}
                        bigText= {`Updated ${title}`}
                        body = {
                        <p className="text-sm text-gray-500">
                            {title} Updated Successfully
                        </p>}
                        buttonText = "Ok"
                        onClickHandler = {()=> {setOpen(false);{window.location.href = `/contacts/${Id}`}}}/>
                    </form>                        
            </div>
        }/>
        <Popup 
            open ={deletePopOpen}
            setOpen={setDeletePopOpen}
            bigText={`Delete ${title} ?`}
            body = {
                <p className="text-sm text-gray-500">
                    This will permanently delete {title}
                </p>}
            buttonText= "Ok"
            onClickHandler={handleDeleteSubmit}
            cancelButton="true"
        />
      </dd>
  )
};

export default Details
