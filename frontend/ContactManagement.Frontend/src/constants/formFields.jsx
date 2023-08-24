const loginFields=[
    {
        labelText:"Username",
        labelFor:"username",
        id:"username",
        name:"username",
        type:"text",
        autoComplete:"username",
        isRequired:true,
        placeholder:"Username",
        minLength: 6,
        maxLength: 15,     
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password",
        minLength: 1  
    }
]

const signupFields=[
    {
        labelText:"First Name",
        labelFor:"firstname",
        id:"firstname",
        name:"firstname",
        type:"text",
        autoComplete:"firstname",
        isRequired:true,
        placeholder:"First Name",
        minLength: 1,
        maxLength: 30,
    },
    {
        labelText:"Last Name",
        labelFor:"lastname",
        id:"lastname",
        name:"lastname",
        type:"text",
        autoComplete:"lastname",
        isRequired:true,
        placeholder:"Last Name",
        minLength: 1,
        maxLength: 25,
           
    },
    {
        labelText:"Username",
        labelFor:"username",
        id:"username",
        name:"username",
        type:"text",
        autoComplete:"username",
        isRequired:true,
        placeholder:"Username",
        minLength: 6,
        maxLength: 15,   
    },
    {
        labelText:"Email address",
        labelFor:"email-address",
        id:"emailaddress",
        name:"email",
        type:"email",
        autoComplete:"email",
        isRequired:true,
        placeholder:"Email address",
        minLength: 3,
        maxLength: 50,      
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password",
        minLength: 1,   
        maxLength: 15, 
    },
    {
        labelText:"Confirm Password",
        labelFor:"confirm-password",
        id:"confirmpassword",
        name:"confirm-password",
        type:"password",
        autoComplete:"confirm-password",
        isRequired:true,
        placeholder:"Confirm Password",
        minLength: 1,   
        maxLength: 15, 
    }


]

const createContactFields=[
    {
        labelText:"First Name",
        labelFor:"firstname",
        id:"firstname",
        name:"firstname",
        type:"text",
        autoComplete:"firstname",
        isRequired:true,
        placeholder:"First Name",
        minLength: 1,   
        maxLength: 30,    
    },
    {
        labelText:"Last Name",
        labelFor:"lastname",
        id:"lastname",
        name:"lastname",
        type:"text",
        autoComplete:"lastname",
        isRequired:true,
        placeholder:"Last Name",
        minLength: 1,   
        maxLength: 25,    
    }
]


const createChildrenEmail=
    {
        labelText:"Email address",
        labelFor:"email-address",
        id:"emailaddress",
        name:"email",
        type:"email",
        autoComplete:"email",
        isRequired:true,
        placeholder:"Email address",
        minLength: 1,   
        maxLength: 30,    
    }

const createChildrenNumber=
{
    labelText:"Contact Number",
    labelFor:"contact-number",
    id:"contactnumber",
    name:"contactnumber",
    type:"number",
    autoComplete:"contactnumber",
    isRequired:true,
    placeholder:"Contact Number",
    minLength: 1,   
    maxLength: 15,    
}

const createChildrenAddress=
{
    labelText:"Address",
    labelFor:"address",
    id:"address",
    name:"address",
    type:"address",
    autoComplete:"address",
    isRequired:true,
    placeholder:"Address",
    minLength: 1,   
    maxLength: 200,    
}



export {loginFields,signupFields,createContactFields, createChildrenEmail, createChildrenAddress, createChildrenNumber}