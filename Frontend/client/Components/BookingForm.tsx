
"use client";

import { useFormState } from "react-dom";
import { postAppointment } from "@/modules/appointment/actions";
import type { BookingFormProps } from "@/types/HealthCare";

// component will get the data from the booking page passed as props below.

//these will be used for error handling and to get the data from the selected modules.

//If user is logged in, Firstname, lastname, DOB will be removed.

//errors have been maped and keyed, will list anything that is not selected or inserted before submitting

export default function BookingForm({categories,doctors, isLoggedIn,}: BookingFormProps) {const [state, formAction] = useFormState(postAppointment, {errors: [],});

  return (
    <form action={formAction}className="mx-auto max-w-md mt-10 space-y-4 rounded-xl border border-gray-200 bg-gray-100 p-6 text-center shadow-sm">
      <h1 className="text-center text-2xl font-bold text-black">
        Booking Form
      </h1>


      {state?.errors && state.errors.length > 0 && (
        <div
          className="rounded-lg bg-red-100 p-4 text-red-700"
          role="alert"
        >
          <ul className="list-disc pl-5">
            {state.errors.map((error: string, index: number) => (
              <li key={index}>{error}</li>
            ))}
          </ul>
        </div>
      )}

      <div>
        
        {!isLoggedIn && (
          <>
            <label
              htmlFor="firstname"
              className="mb-1 block text-black"
            >
              Firstname
            </label>

            <input
              id="firstname"
              name="firstname"
              type="text"
              placeholder="Enter firstname"
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
            />

            <label
              htmlFor="lastname"
              className="mb-1 block text-black"
            >
              Lastname
            </label>

            <input
              id="lastname"
              name="lastname"
              type="text"
              placeholder="Enter lastname"
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
            />

            <label
              htmlFor="birthdate"
              className="mb-1 block text-black"
            >
              Date of Birth
            </label>

            <input
              id="birthdate"
              name="birthdate"
              type="date"
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black"
            />
          </>
        )}


        <label
          htmlFor="duration"
          className="mb-1 block text-black"
        >
          Duration
        </label>

        <input
          id="duration"
          name="duration"
          type="number"
          className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
        />

        <label
          htmlFor="date"
          className="mb-1 block text-black"
        >
          Appointment Date and Time
        </label>

        <input
          id="date"
          name="date"
          type="datetime-local"
          required
          className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black"
        />
      </div>


      <select
        name="categoryId"
        defaultValue=""
        className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-slate-900 outline-none focus:border-blue-400 focus:ring-2 focus:ring-blue-400"
      >
        <option value="" disabled>
          Select a category
        </option>

        {categories.map((category) => (
          <option key={category.id} value={category.id}>
            {category.name}
          </option>
        ))}
      </select>

      <select
        name="doctorId"
        defaultValue=""
        className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-slate-900 outline-none focus:border-blue-400 focus:ring-2 focus:ring-blue-400"
      >
        <option value="" disabled>
          Select a Doctor
        </option>

        {doctors.map((doctor) => (
          <option key={doctor.id} value={doctor.id}>
            {doctor.firstname} {doctor.lastname}
          </option>
        ))}
      </select>


      <div className="flex justify-center">
        <button
          type="submit"
          className="rounded bg-blue-500 px-4 py-2 text-center text-black hover:bg-blue-600"
        >
          Confirm booking
        </button>
      </div>
    </form>
  );
}
