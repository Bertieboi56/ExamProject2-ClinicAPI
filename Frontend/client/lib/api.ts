import type { SearchDoctor, Category,Doctor } from "@/types/HealthCare";

//Fetches the Search doctor API that searches for doctor by first or last name.

export async function searchDoctors(name: string): Promise<SearchDoctor[]> {
  const res = await fetch(
    `http://localhost:3000/api/Doctor/search?name=${name}`
  );

  if (!res.ok) {
    throw new Error("Failed to search doctors");
  }

  return res.json();
}

// will take the prop types and fetch the category information from the categoryAPI

export async function getCategories(): Promise<Category[]> {
  const res = await fetch("http://localhost:3000/api/Category");

  if (!res.ok) {
    throw new Error("Failed to fetch categories");
  }

  return res.json();
}
// Will take the prop types and fetch the doctor information from the doctorAPI

export async function getDoctors(): Promise<Doctor[]> {
  const res = await fetch("http://localhost:3000/api/Doctor");

  if (!res.ok) {
    throw new Error("Failed to fetch categories");
  }

  return res.json();
}








