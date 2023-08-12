import React from "react";
import { MapPinIcon } from "@heroicons/react/20/solid";
import { BanknotesIcon, BookOpenIcon, BriefcaseIcon, HomeIcon, PhoneIcon,  } from "@heroicons/react/24/outline";

const emailTypes=[
    {
        name : "Work",
        avatar : <BriefcaseIcon className ="h-4 w-4"/>
    },
    {
        name : "Home",
        avatar : <HomeIcon className="h-4 w-4"/>  
    },
    {
        name : "School",
        avatar : <BookOpenIcon className="h-4 w-4"/>  
    }
]
const addressTypes =[
    {
        name : "Delivery",
        avatar : <MapPinIcon className="h-4 w-4"/>
    },
    {
        name : "Billing",
        avatar : <BanknotesIcon className = "h-4 w-4"/>
    },
    {
        name : "Home",
        avatar : <HomeIcon className = "h-4 w-4"/>
    },
    {
        name : "Work",
        avatar : <BriefcaseIcon className ="h-4 w-4"/>
    }
    
]
const numberTypes =[
    {
        name : "Work",
        avatar : <BriefcaseIcon className ="h-4 w-4"/>
    },
    {
        name : "Home",
        avatar : <HomeIcon className = "h-4 w-4"/>
    },
    {
        name : "Mobile",
        avatar : <PhoneIcon className ="h-4 w-4"/>
    }
]
export {emailTypes , addressTypes, numberTypes}