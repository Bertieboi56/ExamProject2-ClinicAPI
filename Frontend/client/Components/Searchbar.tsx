'use client';
//Search bar Component

// will Seach and grab the doctors name that matches the value
//of the letters given in the input search.

import { useState } from "react";
import { searchDoctors } from "@/lib/api";
import type { SearchDoctor } from "@/types/HealthCare";

export default function DoctorSearch() {

  //Will remember what the user types

  const [search, setSearch] = useState("");

  // will remember the search results!

  const [doctors, setDoctors] = useState<SearchDoctor[]>([]);

// extra prevention of blank or white spacing with trim

  async function handleSearch() {
    if (!search.trim()) return;

    try {
      const results = await searchDoctors(search);
      setDoctors(results);
    } catch {    
    }
  }

  return (
    <div>
      <h2 className="mb-4 text-xl font-semibold text-black">Find a Doctor</h2>

      <div className="flex gap-2">
        <input
          type="text"
          placeholder="Search for a doctor..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          className="rounded border p-2 text-black"
        />

        <button
          onClick={handleSearch}
          className="rounded bg-blue-500 px-4 py-2 text-white"
        >
          Search
        </button>
      </div>

      <div className="mt-6">
        {doctors.map((doctor, index) => (
          <div key={index} className="rounded-xl border bg-white p-6 text-black text-center">
            <h3 className="font-semibold">Doctor: {doctor.fullName}</h3>
            <p>Clinic: {doctor.clinicName}</p>
            <p>Speciality: {doctor.specialityName}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
