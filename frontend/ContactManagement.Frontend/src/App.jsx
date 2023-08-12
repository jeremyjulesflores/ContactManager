import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'

import SignupPage from './pages/SignupPage';
import LogInPage from './pages/LogInPage';
import HomePage from './pages/HomePage';
import ContactDetails from './pages/Contact/ContactDetails';
import PageNotFound from './pages/PageNotFound';
import History from './pages/History';
import NavBar from './components/header/Navbar';

function App() {

  return (
    // <div className ="h-screen flex flex-col">
    //   <Router>
    //     <NavBar/>
    //     <div className ="flex-1 flex overflow-y-hidden">
    //       <div className ="w-1/4 overflow-y-auto">
    //          <Contacts/>
    //       </div>
    //       <div className ="w-3/4 overflow-y-auto">
    //         <Routes>
    //           <Route exact path='/' element ={<NoContactDetails/>}/>
    //           <Route path ="/contacts/:id" element={<ContactDetails/>}/>
    //         </Routes>
    //       </div>
          
    //     </div>
        
    //     <Footer/>
    //   </Router>
    // </div>
    //min-h-full h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8
//max-w-md w-full space-y-8
    <div className="">
       <div className="">
     <Router>
        
        <Routes>
            <Route path="/" element={<LogInPage/>} />
            <Route path="/signup" element={<SignupPage/>} />
            <Route path="/contacts" element={<HomePage/>}>
              <Route path = ':Id' element = {<ContactDetails/>}/>
            </Route>
            <Route path = '/history' element = {<History/>}/>
            <Route path = '*' element = {<PageNotFound/>}/>
        </Routes>
      </Router>
    </div>
  </div>
  )
}

export default App
