export default function ConfirmationPage(){
    return(
        <main className="flex min-h-screen items-center justify-center text-black">
            <div className="rounded-xl border bg-white p-8 text-center shadow">
                <h1 className="text-center mb-4 font-bold">Appointment Confirmed!</h1>
                <p className="mb-6">Your appointment was booked, Exicted to see you there!</p>
                <a href="/" className="rounded bg-blue-500 px-4 py-2 text-white hover:bg-blue-500">Take me back home!</a>
            </div>
        </main>
    )
}

