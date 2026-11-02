'use client';

import { useFormState } from 'react-dom';
import { loginUser } from '@/modules/auth/action';

//Basic login form componenet that will log user in, 
// set cookie and redirect to admin page if given the correct information.

export default function LoginForm() {
  const [state, formAction] = useFormState(loginUser, {});

  return (
    <form action={formAction} className="mx-auto max-w-md space-y-4 rounded-xl border border-gray-200 bg-gray-100 p-6 text-center shadow-sm">
      <div className="mb-4 text-black">
        <label htmlFor="email" className="mb-1 block">
          Email Address
        </label>

        <input
          id="email"
          name="email"
          placeholder='Enter Email'
          required
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
      </div>

      <div className="mb-4 text-black">
        <label htmlFor="password" className="mb-1 block">
          Password
        </label>

        <input
          id="password"
          name="password"
          type="password"
          placeholder="Enter password"
          required
          className="w-full rounded border border-gray-300 bg-white p-2 text-black"
        />
      </div>

      {state.error && (
        <p className="mb-4 text-red-600" role="alert">
          {state.error}
        </p>
      )}

      <button
        type="submit"
        className="rounded bg-blue-500 px-4 py-2 text-white"
      >
        Login
      </button>
    </form>
  );
}