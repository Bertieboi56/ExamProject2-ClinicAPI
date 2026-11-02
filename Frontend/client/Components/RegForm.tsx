'use client';

import { useFormState } from 'react-dom';
import { RegisterUser } from '@/modules/auth/action';

//Registering Component
//will map and list all Missed or unfilled information that is misseing

export default function  registerForm() {const [state, formAction] = useFormState(RegisterUser, {});

  return (
    <form action={formAction} className="mx-auto max-w-md space-y-4 rounded-xl border border-gray-200 bg-gray-100 p-6 text-center shadow-sm">
      

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
      
      
      <div className="mb-4">
        <label htmlFor="email" className="mb-1 block text-black">
          firstname
        </label>

        <input
          id="firstname"
          name="firstname"
          type="text"
          placeholder='Firstname'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
      </div>

      <div className="mb-4">
        <label htmlFor="lastname" className="mb-1 block text-black">
          lastname
        </label>
        <input
          id="lastname"
          name="lastname"
          type="text"
          placeholder='Lastname'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
        <label htmlFor="email" className="mb-1 block text-black">
          email
        </label>
        <input
          id="email"
          name="email"
          type="email"
          placeholder='Email'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
        <label htmlFor="password" className="mb-1 block text-black">
          Password
        </label>
        <input
          id="password"
          name="password"
          type="password"
          placeholder='Password'
          required
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
         <label htmlFor="birthdate" className="mb-1 block text-black">
      Date of Birth</label>
      <input
      id="birthdate"
      name="birthdate"
      type="date"
      required
      className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
      />
       <label htmlFor="gender" className="mb-1 block text-black">
          gender
        </label>
        <select
          id="gender"
          name="gender"
          className="w-full rounded border border-gray-300 bg-white p-2 text-black">
          <option value="">Select Gender</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
          

        </select>
        <label htmlFor="socialSecurityNumber" className="mb-1 block text-black">
          socialSecurityNumber
        </label>
        <input
          id="socialSecurityNumber"
          name="socialSecurityNumber"
          type="text"
          placeholder='SSN'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
         <label htmlFor="taxnumber" className="mb-1 block text-black">
          taxnumber
        </label>
        <input
          id="taxnumber"
          name="taxnumber"
          type="text"
          placeholder='taxnumber'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
        <label htmlFor="driversLicence" className="mb-1 block text-black">
          driversLicence
        </label>
        <input
          id="driversLicence"
          name="driversLicence"
          type="text"
          placeholder='driversLicence'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />


        <label htmlFor="religion" className="mb-1 block text-black">
          religion
        </label>
        <select
          id="religion"
          name="religion"
          className="w-full rounded border border-gray-300 bg-white p-2 text-black">
        <option value="">Select Religion</option>
            <option value="Christianity">Christianity</option>
            <option value="Islam">Islam</option>
            <option value="Buddhism">Buddhism</option>
            <option value="Other">Other</option>
            <option value="None">None</option>
        </select>

        <label htmlFor="medicalInsuranceMemberNumber" className="mb-1 block text-black">
          medicalInsuranceMemberNumber
        </label>
        <input
          id="medicalInsuranceMemberNumber"
          name="medicalInsuranceMemberNumber"
          type="text"
          placeholder='MIMN'
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
      </div>
      <button
        type="submit"
        className="rounded bg-blue-500 px-4 py-2 text-white"
      >
        confirm registration
      </button>
    </form>
  );
}