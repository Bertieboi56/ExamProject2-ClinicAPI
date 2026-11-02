// NavBar component
import Link from "next/link";
import { cookies } from "next/headers";
import { logoutUser } from "@/modules/auth/action";

// will track user token, if user is logged in, will remove log in and register with Logged in! and logout options.

const NavBar = () => {
    const token = cookies().get("token")?.value;
    const isLoggedIn = !!token; 

    return (
        <nav className="flex items-center gap-6 text-sm font-medium">
            <Link href="/Book" className="text-[#cdd6f4] transition hover:text-white">Book</Link>
            <Link href="/Search" className="text-[#cdd6f4] transition hover:text-white">Search</Link>
            
            {isLoggedIn ? (
            <>
            <Link href="/appointments" className="text-[#cdd6f4] transition hover:text-white">Appointments</Link>
            <div>Logged in!</div>
           
            <form action={logoutUser}>
                <button type="submit" className="text-[#cdd6f4] transition hover:text-white">
                    Logout
                </button>
            </form>
           </>
          ) : ( 
          <>
          <Link href="/register" className="text-[#cdd6f4] transition hover:text-white">register</Link>
            <Link href="/login" className="text-[#cdd6f4] transition hover:text-white">Login</Link>
            </>
          )}
        </nav>
    );
};

export default NavBar;