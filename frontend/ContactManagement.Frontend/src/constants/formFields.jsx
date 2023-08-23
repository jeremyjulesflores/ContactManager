const loginFields=[
    {
        labelText:"Username",
        labelFor:"username",
        id:"username",
        name:"username",
        type:"text",
        autoComplete:"username",
        isRequired:true,
        placeholder:"Username"   
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password"   
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
        placeholder:"First Name"   
    },
    {
        labelText:"Last Name",
        labelFor:"lastname",
        id:"lastname",
        name:"lastname",
        type:"text",
        autoComplete:"lastname",
        isRequired:true,
        placeholder:"Last Name"   
    },
    {
        labelText:"Username",
        labelFor:"username",
        id:"username",
        name:"username",
        type:"text",
        autoComplete:"username",
        isRequired:true,
        placeholder:"Username"   
    },
    {
        labelText:"Email address",
        labelFor:"email-address",
        id:"emailaddress",
        name:"email",
        type:"email",
        autoComplete:"email",
        isRequired:true,
        placeholder:"Email address"   
    },
    {
        labelText:"Password",
        labelFor:"password",
        id:"password",
        name:"password",
        type:"password",
        autoComplete:"current-password",
        isRequired:true,
        placeholder:"Password"   
    },
    {
        labelText:"Confirm Password",
        labelFor:"confirm-password",
        id:"confirmpassword",
        name:"confirm-password",
        type:"password",
        autoComplete:"confirm-password",
        isRequired:true,
        placeholder:"Confirm Password"   
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
        placeholder:"First Name"   
    },
    {
        labelText:"Last Name",
        labelFor:"lastname",
        id:"lastname",
        name:"lastname",
        type:"text",
        autoComplete:"lastname",
        isRequired:true,
        placeholder:"Last Name"   
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
        placeholder:"Email address"   
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
    placeholder:"Contact Number"   
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
    placeholder:"Address"   
}



export {loginFields,signupFields,createContactFields, createChildrenEmail, createChildrenAddress, createChildrenNumber}