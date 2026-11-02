// shared Server component logic for both Book and root page

import BookingForm from "@/Components/BookingForm";
import { getCategories, getDoctors } from "@/lib/api";
import { cookies } from "next/headers";

export default async function BookingPage() {
  const categories = await getCategories();
  const doctors = await getDoctors();

  const token = (await cookies()).get("token")?.value;

  return (
    <BookingForm
      categories={categories}
      doctors={doctors}
      isLoggedIn={!!token}
    />
  );
}