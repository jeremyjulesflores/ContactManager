import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import NavBar from "../components/header/Navbar";
import Contacts from './2.Contacts/Contacts';
import ContactDetails from './4.ContactDetails/ContactDetails';
import Footer from '../components/footer/Footer';
import NoContactDetails from './4.ContactDetails/NoContactDetails';
const HomePage = () => {
  return (
  
    <div className ="h-screen flex flex-col">
        <NavBar/>
            <div className ="flex-1 flex overflow-y-hidden">
                <div className ="w-1/4 overflow-y-auto">
                    <Contacts/>
                </div>
                <div className ="w-3/4 overflow-y-auto">
                    <ContactDetails/>
                </div>
            </div>
        <Footer/>
    </div>
       
            
    
  )
};

export default HomePage
