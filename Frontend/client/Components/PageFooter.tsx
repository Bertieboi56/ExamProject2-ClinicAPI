import Link from "next/link";
import NavBar from "./NavBar";

//page footer will display fullyear and current date along side the app name

const PageFooter = () => {
    return (
       <footer className="text-center p-4 bg-sky-700 text-[#cdd6f4] text-sm">
        HealthCareApp &copy; {new Date().getFullYear()}

       </footer>
    );
};

export default PageFooter;
