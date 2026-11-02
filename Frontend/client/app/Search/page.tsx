
import DoctorSearch from "@/Components/Searchbar";
import type { Metadata} from "next";

export const metadata: Metadata = {
  title: "Search bar",
  description: "A search function to look for doctor details",
}


export default function Home() {
  return (
    <main className="p-6">
      <h1 className="mb-6 text-center text-3xl font-bold">
        HealthCareApp
      </h1>   
        <DoctorSearch />
    </main>
  );
}

