import { HeartIcon, MagnifyingGlassCircleIcon, PlusIcon, StarIcon, UserIcon, UserCircleIcon, XCircleIcon, TrashIcon} from '@heroicons/react/24/outline'
import React, {useState, useEffect} from 'react';
import { Link, useParams } from 'react-router-dom';
import axios from 'axios';
import { data } from 'autoprefixer';

const Contacts = ({isCreateOpen, setIsCreateOpen}) => {
    const {Id} = useParams();
    const[peopleData, setPeopleData] = useState([]);
    const [filteredPeople, setFilteredPeople] = useState([]);
    const [emergencyPeople, setEmergencyPeople] = useState([]);
    const localEmergency = localStorage.getItem('emergency');
    const initialEmergency = localEmergency === 'true';
    const[dataFetched, setDataFetched] = useState(false);
    const [emergency, setEmergency]= useState(initialEmergency);

    const fetchContacts = async () => {
        
        const authToken = localStorage.getItem('authToken');
        const headers = {
            'Authorization': `Bearer ${authToken}`,
            'Content-Type': 'application/json'
        }
        return await axios.get('https://localhost:7274/api/contacts/', { headers })
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
                return null;
            });
    };
    
    useEffect(() => {
        if(!dataFetched){
            fetchContacts()
            .then(contacts => {
                setPeopleData(contacts);
                setFilteredPeople(contacts);
                setDataFetched(true);
        });
        }
        if(dataFetched){
            if(emergency){
                showEmergency();
            }
        }
    }, [dataFetched, peopleData]); // Empty dependency array to run the effect once on component mount

    const showEmergency = async () =>{
        try{
            console.log(peopleData);
            const filter = peopleData.filter(
                (peopleData) =>
                    peopleData.emergency === true
            );
            console.log(filter);
            setEmergencyPeople(filter);
            setFilteredPeople(filter);
        }
        catch(error){
            //handle error here
        }
    }
    const toggleShowEmergency = async () =>{
        try{
            console.log(filteredPeople);
            if(emergency){
                setFilteredPeople(peopleData);
            }else{
                showEmergency();
            }

            localStorage.setItem('emergency', !emergency);
            setEmergency((prevEmergency) => !prevEmergency);
        }
        catch(error){
            //handle error here
        }
    }
    
    
    const toggleFavorite = async (person) => {
        try {
            const authToken = localStorage.getItem('authToken');
            const headers = {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            };
    
            const patchDocument = [
                {
                    path: '/favorite',
                    op: 'replace',
                    value: !person.favorite
                }
            ];
    
            await axios.patch(`https://localhost:7274/api/contacts/${person.id}`, patchDocument, { headers });
            // UPDATE THE PERSON TOGGLED
            const updatedPerson = {...person, favorite: !person.favorite};

            //Update the filteredlist here based on the updated person
            const updatedFilter = filteredPeople.map(filteredPerson => 
                filteredPerson.id === person.id ? updatedPerson : filteredPerson);

            // Sort the updatedFilter array first by favorite status (favorites at the top), then alphabetically
            const sortedUpdatedFilter = updatedFilter.sort((a, b) => {
                // First, sort by favorite status (favorites at the top)
                if (b.favorite && !a.favorite) {
                    return 1;
                }
                if (!b.favorite && a.favorite) {
                    return -1;
                }

                // If favorite status is the same, sort alphabetically
                return a.firstName.localeCompare(b.firstName);
            });
            // Fetch and update contacts after successful patch
            const contacts = await fetchContacts(headers);
            setPeopleData(contacts);
            setFilteredPeople(emergency ? sortedUpdatedFilter : contacts);
            
        } catch (error) {
            if(error.response){
                if(error.response.status === 400){
                //Handle
                }else{
                  //Handle
                }
            }else if (error.message === "Network Error"){
                //Handle
                console.log(error);
            }else{
                //Handle
            }
        }
    };
      
    const toggleEmergency = async (person) => {
    try{
        const authToken = localStorage.getItem('authToken');
        const headers = {
            'Authorization': `Bearer ${authToken}`,
            'Content-Type': 'application/json'
        };

        const patchDocument = [
            {
                path: '/emergency',
                op: 'replace',
                value: !person.emergency
            }
        ];
        await axios.patch(`https://localhost:7274/api/contacts/${person.id}`, patchDocument, { headers });

        // UPDATE THE PERSON TOGGLED
        const updatedPerson = {...person, emergency: !person.emergency};

        // UPDATED THE EMERGENCY PEOPLE LIST
        const updatedEmergencyPeople = person.emergency ?
        emergencyPeople.filter(emergencyPerson => emergencyPerson.id !== person.id)
        : [...emergencyPeople, updatedPerson];
        // Fetch and update contacts after successful patch
        const contacts = await fetchContacts(headers);
        setPeopleData(contacts);
        //If emergency is tru then set the filtered people with the updated emergencypeople
        setFilteredPeople(emergency ? updatedEmergencyPeople : contacts);
        setEmergencyPeople(updatedEmergencyPeople);
    }
    catch(error){
        if(error.response){
            if(error.response.status === 400){
            //Handle
            }else{
                //Handle
            }
            }else if (error.message === "Network Error"){
            //Handle
            console.log(error);
            }else{
            //Handle
        }
    }
    };
    const handleSearch = (query)=>{
        if(emergency){
            const filter = emergencyPeople.filter(
                (peopleData)=>
                peopleData.firstName.toLowerCase().includes(query.toLowerCase()) ||
                peopleData.lastName.toLowerCase().includes(query.toLowerCase())
            );
            setFilteredPeople(filter);
        }else{
            const filter = peopleData.filter(
                (peopleData)=>
                    peopleData.firstName.toLowerCase().includes(query.toLowerCase()) ||
                    peopleData.lastName.toLowerCase().includes(query.toLowerCase())
            );
        setFilteredPeople(filter);
        }
        
    }

    const handleDelete = async (person) => {
        try{

            const authToken = localStorage.getItem('authToken');
            await axios.delete(`https://localhost:7274/api/contacts/${person.id}`, {
                headers: {
                    'Authorization': `Bearer ${authToken}`,
                    'Content-Type': 'application/json'
                }
            })
            const updatedEmergencyPeople = emergencyPeople.filter(emergencyPerson => emergencyPerson.id !== person.id);
            setEmergencyPeople(updatedEmergencyPeople);
            const contacts = await fetchContacts();
            setPeopleData(contacts);
            setFilteredPeople(emergency ? updatedEmergencyPeople : contacts);
            window.location.href = "/contacts"
        }
        catch(error){
            console.log(error);
            
        }
    }

    return (
        <div className ="h-screen flex-col">
            <div className="p-4 border-b sticky top-0 z-10 flex justify-between gap-x-1 py-5 items-center bg-white">
                <MagnifyingGlassCircleIcon className ="h-7 w-1/6" />
                <input 
                type="text"
                placeholder="Search..."
                className="w-5/6 p-2 border rounded"
                onChange={(e) => handleSearch(e.target.value)}
                />
                <HeartIcon className={`h-7 w-1/6  ${emergency ? 'text-red-400 hover:text-red-200' : 'hover:text-gray-400' }`} onClick={() => toggleShowEmergency() } />
                <PlusIcon className='h-7 w-1/6 hover:text-gray-400' onClick={()=> setIsCreateOpen(!isCreateOpen)}/>
                
            </div>
            {filteredPeople === null || filteredPeople.length === 0 ? 
            <>
                <main className="grid min-h-full place-items-center bg-white px-6 py-24 sm:py-32 lg:px-8">
                    <div className="text-center items-center justify-center flex flex-col">
                        <XCircleIcon className= 'block h-20 w-20 text-red-800'/>
                        <h1 className="mt-4 text-3xl font-bold tracking-tight text-gray-900 sm:text-5xl">No Contacts</h1>
                        
                        <p className="text-sm font-semibold text-gray-600 underline"
                         onClick={() =>setIsCreateOpen(!isCreateOpen)}>
                        Add a Contact <span aria-hidden="true">&rarr;</span>
                        </p>                    
                    </div>
                </main>
            </>
            : 
            <>
            {filteredPeople.length === 0 && emergency? 
                <>
                    <main className="grid min-h-full place-items-center bg-white px-6 py-24 sm:py-32 lg:px-8">
                        <div className="text-center items-center justify-center flex flex-col">
                            <HeartIcon className= 'block h-20 w-20 text-red-800'/>
                            <h1 className="mt-4 text-3xl font-bold tracking-tight text-gray-900 sm:text-5xl">No Emergency Contacts</h1>
                        </div>
                    </main>
                </>
            
            :
            <div className="flex-1 overflow-y-auto">
            <ul role="list" className="divide-y divide-gray-100">
            {filteredPeople.map((person) => (
                <Link to={`/contacts/${person.id}` } key={person.id}>
                <li  className={`flex justify-between gap-x-6 py-5 items-center border rounded hover:bg-gray-300 ${person.id === Number(Id) ? 'bg-gray-300' : `` }`}>
                    
                    <div className="flex min-w-0 gap-x-4 w-2/3">
                        <UserIcon className="h-6 w-6" aria-hidden="true" />
                        <div className="min-w-0 flex-auto">
                            <p className="text-sm font-semibold leading-6 text-gray-900">{person.firstName} {person.lastName}</p>
                        </div>
                    </div>
                    <div className="w-1/3 flex min-w-0">
                        <div onClick={() => toggleEmergency(person)}>
                            <HeartIcon
                                className={`h-6 w-6 cursor-pointer ${
                                    person.emergency ? 'text-red-600 hover:text-red-300' : 'text-gray-200 hover:text-red-300'
                                }`}
                                aria-hidden="true"
                            
                            />
                        </div>
                        <div onClick={(e) => toggleFavorite(person)}>
                            <StarIcon
                            className={`h-6 w-6 cursor-pointer ${
                            person.favorite ? 'text-yellow-400 hover:text-yellow-300' : 'text-gray-200 hover:text-yellow-300'
                            }`}
                            aria-hidden="true"
                        
                            />
                        </div>

                        <div onClick={(e) => handleDelete(person)}>
                            <TrashIcon className = "h-6 w-6 cursor-pointer text-gray-200 hover:text-red-500" aria-hidden="true"/>
                        </div>
                    
                    
                
                    </div>
            </li>
            </Link>
            ))}
            </ul>
        </div>}</>}
            
        </div>
      )
};

export default Contacts
