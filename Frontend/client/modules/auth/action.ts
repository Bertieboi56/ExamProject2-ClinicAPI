"use server";

import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import type { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from "@/types/auth";

//Function for when the User logs out, token/cookie attached to user will also be deleted.

export async function logoutUser() {
  cookies().delete('token');
  redirect('/login');
}

const BASE_URL = process.env.API_BASE_URL;

if (!BASE_URL) {
  throw new Error("API_BASE_URL is not defined in the enviroment");
}

// this function will send a Login request, fetching the auth API, 
// if succesful will log in the user and will be granted a token, see below.

export async function loginUser(
  _previousState: unknown,
  formData: FormData
) {
  const email = String(formData.get("email"));
  const password = String(formData.get("password"));

  const loginData: LoginRequest = {
    email,
    password,
  };


  const response = await fetch(`${BASE_URL}/api/Auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginData),
  });

  if (!response.ok) {
    return {
      error: "Incorrect email or password.",
    };
  }

  const data: LoginResponse = await response.json();
 // sets cookie with a 60 minute timer
  cookies().set("token", data.token, {
    httpOnly: true,
    secure: process.env.NODE_ENV === "production",
    sameSite: "lax",
    path: "/",
    maxAge: 60 * 60 * 24,
  });

  redirect("/Search");
}

// this function will send a request to register the user,
// on successful login, will be redirected to the login page.

export async function RegisterUser(_previousState: unknown,formData: FormData) {
  const registerData: RegisterRequest = {
    firstname: String(formData.get("firstname")),
    lastname: String(formData.get("lastname")),
    email: String(formData.get("email")),
    password: String(formData.get("password")),
    birthdate: String(formData.get("birthdate")),
    gender: String(formData.get("gender")),
    socialSecurityNumber: String(
      formData.get("socialSecurityNumber")
    ),
    taxnumber: String(formData.get("taxnumber")),
    driversLicence: String(formData.get("driversLicence")),
    religion: String(formData.get("religion")),
    medicalInsuranceMemberNumber: String(
      formData.get("medicalInsuranceMemberNumber")
    ),
  };

  const response = await fetch(`${BASE_URL}/api/Auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(registerData),
  });

 if (!response.ok) {
    const errorData = await response.json();

    const validationErrors = errorData.errors
        ? Object.values(errorData.errors).flat()
        : [errorData.message || "Registration failed."];

    return {
        errors: validationErrors,
    };
}

  const data: RegisterResponse = await response.json();

  redirect("/login");
}

