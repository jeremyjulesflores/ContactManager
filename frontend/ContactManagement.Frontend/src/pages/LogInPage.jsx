import Header from "../components/Header"
import LogIn from "../components/LogIn"
const LogInPage = () => {
  return (
    <div className = "min-h-full h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
      <div className = "max-w-md w-full space-y-8">
        <Header
          heading="Login"
          paragraph="Don't have an account yet? "
          linkName="Signup"
          linkUrl="/signup"
          />
        <LogIn/>
      </div>
      
    </div>
  )
};

export default LogInPage
