"use server";

import { cookies } from "next/headers";
import { redirect} from "next/navigation";
import type { CreateAppointmentRequest, Appointment, Category, Doctor} from "@/types/HealthCare";

//grabs the envriomental file!

const BASE_URL = process.env.API_BASE_URL;

if (!BASE_URL) {
  throw new Error("API_BASE_URL cant be found in the evviroment! please check");
}



// this function will send a request to the Appointment api, if successful, it will create a guest/user appointment.

export async function postAppointment( _previousState: {errors: string[];}, formData: FormData) {
  const token = cookies().get("token")?.value;
  const appointmentData: CreateAppointmentRequest = {
  date: String(formData.get("date")),
  duration: Number(formData.get("duration")),
  doctorId: Number(formData.get("doctorId")),
  categoryId: Number(formData.get("categoryId")),
};

// if there is no token attached, the person must input the additional information.
// the Registered user will not have to resend this.

if (!token) {
  appointmentData.firstname = String(formData.get("firstname"));
  appointmentData.lastname = String(formData.get("lastname"));
  appointmentData.birthdate = String(formData.get("birthdate"));
}
  const response = await fetch(`${BASE_URL}/api/Appointment`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...(token && {
        Authorization: `Bearer ${token}`,
      }),
    },
    body: JSON.stringify(appointmentData),
  });
  if (!response.ok) {const errorData = await response.json();
  return {
    errors: [ errorData.message || "Failed to create appointment.",],
  };
}
redirect("/Confirmation");
}

// Will fetch the appointment by ID and grab the correct users id based on token.

export async function getAppointmentById(id: number): Promise<Appointment> {
  const token = cookies().get("token")?.value;

  if (!token) {
    throw new Error("You must be logged in.");
  }

  const response = await fetch(`${BASE_URL}/api/Appointment/${id}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
    },
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch appointment.");
  }

  return response.json();
}

// will fetch the get appointments and fetch all users id appointments

export async function getAppointments(): Promise<Appointment[]> {
  const token = cookies().get("token")?.value;

  if (!token) {
    return [];
  }

  const response = await fetch(`${BASE_URL}/api/Appointment`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
    },
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch appointments.");
  }

  return response.json();
}


export async function updateAppointment(id: number,formData: FormData) {
  const token = cookies().get('token')?.value;

   const appointmentData = {
    date: String(formData.get("date")),
    doctorId: Number(formData.get("doctorId")),
    categoryId: Number(formData.get("categoryId")),
    duration: Number(formData.get("duration")),
  };

   const response = await fetch(`${BASE_URL}/api/Appointment/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(appointmentData),
  }
);
if (!response.ok) {
  const errorData = await response.text();
  console.log("Update error:", errorData);
  throw new Error(`Failed to update appointment: ${response.status}`);
}

  redirect('/appointments')


}

export async function deleteAppointment(id: number) {
  const token = cookies().get("token")?.value;
   if (!token) {
    throw new Error("You must be logged in.");
  }

  const response = await fetch(`${BASE_URL}/api/Appointment/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    const errorData = await response.text();
    console.log("Delete error:", errorData);
    throw new Error(`Failed to delete appointment: ${response.status}`);
  }

  redirect("/appointments");

}


export async function getDoctors(): Promise<Doctor[]> {
  const response = await fetch(`${BASE_URL}/api/Doctor`, {
    method: "GET",
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch doctors.");
  }

  return response.json();
}



export async function getCategories(): Promise<Category[]> {
  const response = await fetch(`${BASE_URL}/api/Category`, {
    method: "GET",
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch categories.");
  }

  return response.json();
}

 