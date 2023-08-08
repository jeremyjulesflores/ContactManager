import { HeartIcon, MagnifyingGlassCircleIcon, PlusIcon, StarIcon, UserIcon,} from '@heroicons/react/24/outline'
import React, {useState, useEffect} from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
const Contacts = () => {
    const[peopleData, setPeopleData] = useState([]);
    const [filteredPeople, setFilteredPeople] = useState([]);

    const authToken = localStorage.getItem('authToken');
    const headers = {
        'Authorization': `Bearer ${authToken}`,
        'Content-Type': 'application/json'
    };

    useEffect(() => {
        axios.get('https://localhost:7274/api/contacts/', {
            headers: headers
        })
        .then(response => {
            if (response.status === 200) {
                // Update the peopleData state with the response data
                setPeopleData(response.data);
                setFilteredPeople(response.data);
            } else if (response.status === 401) {
                // Handle unauthorized access here (refresh token or redirect to login)
            } else {
                throw new Error('Network response was not ok');
            }
        })
        .catch(error => {
            // Handle errors
        });
    }, []); // Empty dependency array to run the effect once on component mount

    const toggleFavorite = (personId) => {
        setPeopleData((prevPeople) =>
          prevPeople.map((person) =>
            person.id === personId ? { ...person, favorite: !person.favorite } : person
          )
        );
      };

      const toggleEmergency = (personId) => {
        setPeopleData((prevPeople) =>
          prevPeople.map((person) =>
            person.id === personId ? { ...person, emergency: !person.emergency } : person
          )
        );
      };

      const handleSearch = (query)=>{
        const filteredPeople = peopleData.filter(
            (peopleData)=>
                peopleData.firstName.toLowerCase().includes(query.toLowerCase()) ||
                peopleData.lastName.toLowerCase().includes(query.toLowerCase())
        );
        setFilteredPeople(filteredPeople);
      }
    return (
        <div className ="h-screen flex-col">
            <div className="p-4 border-b sticky top-0 z-10 flex justify-between gap-x-1 py-5 items-center bg-white">
                <MagnifyingGlassCircleIcon className ="h-7 w-1/6"/>
                <input 
                type="text"
                placeholder="Search..."
                className="w-5/6 p-2 border rounded"
                onChange={(e) => handleSearch(e.target.value)}
                />
                <PlusIcon className='h-7 w-1/6'/>
            </div>
            <div className="flex-1 overflow-y-auto">
                <ul role="list" className="divide-y divide-gray-100">
                {filteredPeople.map((person) => (
                    <Link to={`/home/contacts/${person.id}` } key={person.id}>
                    <li  className="flex justify-between gap-x-6 py-5 items-center">
                        
                        <div className="flex min-w-0 gap-x-4 w-2/3">
                            <UserIcon className="h-6 w-6" aria-hidden="true" />
                            <div className="min-w-0 flex-auto">
                                <p className="text-sm font-semibold leading-6 text-gray-900">{person.firstName} {person.lastName}</p>
                            </div>
                        </div>
                        <div className="w-1/3 flex min-w-0">
                            <div onClick={() => toggleEmergency(person.id)}>
                                <HeartIcon
                                    className={`h-6 w-6 cursor-pointer ${
                                        person.emergency ? 'text-red-600' : 'text-gray-200'
                                    }`}
                                    aria-hidden="true"
                                
                                />
                            </div>
                            <div onClick={() => toggleFavorite(person.id)}>
                                <StarIcon
                                className={`h-6 w-6 cursor-pointer ${
                                person.favorite ? 'text-yellow-400' : 'text-gray-200'
                                }`}
                                aria-hidden="true"
                            
                                />
                            </div>
                        
                        
                    
                        </div>
                </li>
                </Link>
                ))}
                </ul>
            </div>
        </div>
      )
};

export default Contacts
