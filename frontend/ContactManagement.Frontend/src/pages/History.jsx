import {format} from 'date-fns';
import NavBar from "../components/header/Navbar";
import axios from "axios";
import {
    Card,
    CardHeader,
    Typography,
    Button,
    CardBody,
    Chip,
    CardFooter,
    Avatar,
    IconButton,
    Tooltip,
    Input,
  } from "@material-tailwind/react";
import { useEffect, useState } from "react";
const TABLE_HEAD = ["Date", "Action", "UserName", "Contact", "Details"];


 
const History =() => {
    const [tableRows, setTableRows] = useState([]);
    const ITEMS_PER_PAGE = 10;
    const [currentPage, setCurrentPage] = useState(1);
    const fetchLogs = async () =>{
      try{
      
          const authToken = localStorage.getItem('authToken');
          return await axios.get(`http://localhost:7274/api/auditlogs`,{
              headers: {
                  'Authorization': `Bearer ${authToken}`,
                  'Content-Type': 'application/json'
              }
          })
          .then(response => {
              console.log(response);
              if(response.status === 200){
                  setTableRows(response.data)
              }else{
                  console.log(error);
              }
          })
          
        }
        catch(error){
            console.log(error);
        }  
    }
    useEffect(()=>{
      fetchLogs();
         
    }, [])
    
    const totalItems = tableRows.length;
    const totalPages = Math.ceil(totalItems / ITEMS_PER_PAGE);
    const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
    const endIndex = startIndex + ITEMS_PER_PAGE;
    const currentPageData = tableRows.slice(startIndex, endIndex);

    

    
  return (
    <>
    <NavBar/>
    <Card className="h-full w-full">
      <CardHeader floated={false} shadow={false} className="rounded-none">
        <div className="mb-4 flex flex-col justify-between gap-8 md:flex-row md:items-center">
          <div className="flex w-full shrink-0 gap-2 md:w-max">
          </div>
        </div>
      </CardHeader>
      <CardBody className="overflow-x-auto px-0">
        <table className="w-full min-w-max table-auto text-left">
          <thead>
            <tr>
              {TABLE_HEAD.map((head) => (
                <th
                  key={head}
                  className="border-y border-blue-gray-100 bg-blue-gray-50/50 p-4"
                >
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal leading-none opacity-70"
                  >
                    {head}
                  </Typography>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {currentPageData.map(
              (
                {
                    id,
                    timeStamp,
                    action,
                    userName,
                    contactName,
                    details
                },
                index,
              ) => {
                const formattedTimeStamp = format(new Date(timeStamp), 'MM-dd-yyyy HH:mm:ss');
                const isLast = index === tableRows.length - 1;
                const classes = isLast
                  ? "p-4"
                  : "p-4 border-b border-blue-gray-50";
 
                return (
                  <tr key={id}>
                    <td className={classes}>
                      <div className="flex items-center gap-3">
                    
                        <Typography
                          variant="small"
                          color="blue-gray"
                          className="font-bold"
                        >
                          {formattedTimeStamp}
                        </Typography>
                      </div>
                    </td>
                    <td className={classes}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {action}
                      </Typography>
                    </td>
                    <td className={classes}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {userName}
                      </Typography>
                    </td>
                    <td className={classes}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {contactName}
                      </Typography>
                    </td>
                    <td className={classes}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {details}
                      </Typography>
                    </td>
                   
                    <td className={classes}>
                      
                    </td>
                  </tr>
                );
              },
            )}
          </tbody>
        </table>
      </CardBody>
      <CardFooter className="flex items-center justify-between border-t border-blue-gray-50 p-4">
        <Button variant="outlined" 
        size="sm"
        onClick={() => setCurrentPage(currentPage-1)}
        disabled ={currentPage===1}>
          Previous
        </Button>
        {/* <div className="flex items-center gap-2">
            {Array.from({ length: totalPages }, (_, index) => (
                <IconButton
                    key={index}
                    variant="text"
                    size="sm"
                    onClick={() => setCurrentPage(currentPage + 1)}
                    // Apply the active class based on the currentPage
                    className={`${
                        currentPage === index + 1 ? "bg-blue-500 text-gray-500" : ""
                    }`}
                >
                    {index + 1}
                </IconButton>
            ))}
        </div> */}
        <Button variant="outlined" 
        size="sm"
        onClick={()=> setCurrentPage(currentPage+1)}
        disabled = {currentPage === totalPages}
        >
          Next
        </Button>
      </CardFooter>
    </Card>
    </>
  );
}

export default History