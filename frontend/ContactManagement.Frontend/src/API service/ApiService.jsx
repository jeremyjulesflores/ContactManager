import axios from "axios";

const API_BASE_URL = 'https://localhost:7274/api'

export const createEmail = async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.post(`${API_BASE_URL}/contacts/${requestBody.Id}/emails`, {
          emailAddress : requestBody.emailAddress,
          type : requestBody.type,
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const getEmail = async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');

        const response = await axios.get(`API_BASE_URL/contacts/${requestBody.contactId}/emails/${requestBody.emailId}`)
        return response.data;
    }

    catch(error){
        console.log(error);
    }
}
export const updateEmail = async (requestBody)=>{
    try{
        const authToken = localStorage.getItem('authToken');

        await axios.put(`${API_BASE_URL}/contacts/${requestBody.contactId}/emails/${requestBody.emailId}`, {
            type : requestBody.type,
            emailAddress : requestBody.emailAddress
            
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
    
}

export const deleteEmail = async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.delete(`${API_BASE_URL}/contacts/${requestBody.contactId}/emails/${requestBody.emailId}`, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const createNumber= async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.post(`${API_BASE_URL}/contacts/${requestBody.Id}/numbers`, {
          contactNumber : requestBody.contactNumber,
          type : requestBody.type,
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}
export const updateNumber = async (requestBody)=>{
    try{
        const authToken = localStorage.getItem('authToken');

        await axios.put(`${API_BASE_URL}/contacts/${requestBody.contactId}/numbers/${requestBody.numberId}`, {
            type : requestBody.type,
            contactNumber : requestBody.contactNumber
            
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
    
}

export const deleteNumber= async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.delete(`${API_BASE_URL}/contacts/${requestBody.contactId}/numbers/${requestBody.numberId}`, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return response.data
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const createAddress= async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.post(`${API_BASE_URL}/contacts/${requestBody.Id}/addresses`, {
          addressDetails : requestBody.addressDetails,
          type : requestBody.type,
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const updateAddress= async (requestBody)=>{
    try{
        const authToken = localStorage.getItem('authToken');

        await axios.put(`${API_BASE_URL}/contacts/${requestBody.contactId}/addresses/${requestBody.addressId}`, {
            type : requestBody.type,
            addressDetails : requestBody.addressDetails
            
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
    
}

export const deleteAddress= async (requestBody) =>{
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.delete(`${API_BASE_URL}/contacts/${requestBody.contactId}/addresses/${requestBody.addressId}`, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const patchNotes = async (requestBody) => {
    try{
        const authToken = localStorage.getItem('authToken');
        await axios.patch(`${API_BASE_URL}/contacts/${requestBody.contactId}`, requestBody.notes
        ,{
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (response.status === 200) {
                return true;
            } else {
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}

export const getLogs = async (requestBody)=>{

    try{
        const authToken = localStorage.getItem('authToken');
        await axios.get(`${API_BASE_URL}/auditlogs`, requestBody ,{
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            console.log(response);
            if(response.status === 200){
                
                return response.data
            }else{
                return false;
            }
        })
    }
    catch(error){
        console.log(error);
        return false;
    }
}
// export const loginUser = async (requestBody) => {
//     try{
//         const response = await axios.post(`${API_BASE_URL}/users/login` , {
//             username : requestBody.username,
//             password : requestBody.password
//         }, {
//             headers: {
//                 'Content-Type' : 'application/json'
//             }
//         })
//         return response.data;
//     }
//     catch(error){
//         if(error.response){
//             if(error.response.status === 401){
//               setError("Invalid Credentials");
//             }else{
//               setError("Something went wrong");
//             }
//           }else if (error.message === "Network Error"){
//             setError("Network Error");
//             console.log(error);
//           }else{
//             setError("Something went wrong");
//           }
//     }
// }

// export const checkToken = async (requestBody) => {
//     try{
//         const response = await axios.post(`${API_BASE_URL}/users/check`, {
//             token : requestBody.token,
//             username : requestBody.username
//         }, {
//             headers: {
//                 'Content-Type': 'application/json'
//             }
//         })
//         return response;
//     }
//     catch(error){
//         if(error.response){
//             if(error.response.status === 401){
//               setError("Invalid Credentials");
//             }else{
//               setError("Something went wrong");
//             }
//           }else if (error.message === "Network Error"){
//             setError("Network Error");
//             console.log(error);
//           }else{
//             setError("Something went wrong");
//           }
//     }
// }