import { useParams } from "react-router-dom";

const ContactDetails = () => {
    const {Id} = useParams();
  return (
    <div>
      <p> ContactId {Id}</p>
    </div>
  )
};

export default ContactDetails
