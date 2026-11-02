import Link from "next/link";
import NavBar from "./NavBar";

// header Component 
// holds inside the nav bar component

const PageHeader = () => {
    return (
        <header className="bg-sky-700 text-[#cdd6f4] shadow-sm">
            <div className="mx-auto flex max-w-6xl items-center justify-between px-4 py-4">
                <Link href="/" className="text-lg font-semibold tracking-wide text-[#f8fafc] transition hover:text-white">
                    HealthCareApp
                </Link>
                <NavBar />
            </div>
        </header>
    );
};

export default PageHeader;