import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NavBar from "../components/header/Navbar";
import Contacts from './Contact/Contacts';
import ContactDetails from './Contact/ContactDetails';
import Footer from '../components/footer/Footer';
import { useState, Fragment } from 'react';
import { Dialog, Transition} from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/outline'
import CreateContact from './Contact/CreateContact';

const HomePage = () => {
    const [open, setOpen] = useState(false);


  return (
  
    <div className ="h-screen flex flex-col">
        <NavBar/>
            <div className ="flex-1 flex overflow-y-hidden">
                <div className ="w-1/4 overflow-y-auto">
                    <Contacts isCreateOpen = {open}
                    setIsCreateOpen={setOpen}/>
                </div>
                <div className ="w-3/4 overflow-y-auto">
               
                    <ContactDetails/>
                    
                </div>
                
                <CreateContact isCreateOpen = {open}
                setIsCreateOpen={setOpen}/>    
            </div>
        
    </div>
            
    
  )
};

export default HomePage
