import Link from "next/link";
import { deleteAppointment, getAppointments } from "@/modules/appointment/actions";

export default async function appointmentPage(){
  const appointments = await getAppointments();

    return (
        <main className="min-h-screen px-4 pt-12 text-black">
        <div className="mx-auto w-full max-w-xl">
          <h1 className="mb-8 text-center text-4xl font-bold"> My Appointments </h1>
            
            <div className="space-y-6">
            {appointments.map((appointment) => (
                <div key={appointment.id} className="rounded-xl border bg-white p-6 shadow-s text-center">
                    
                     <p>Doctor: {appointment.doctorName}</p>
                     <p>Clinic: {appointment.clinicName}</p>
                     <p>Category: {appointment.categoryName}</p>
                     <p>{new Date(appointment.date).toLocaleString("en-GB", {day: "2-digit",month: "2-digit",year: "numeric",hour: "2-digit",minute: "2-digit",})}</p>
                     <p>Duration: {appointment.duration} minutes</p>
                     
                    
                     <div className="mt-6 flex justify-center gap-3 pt-4">
               <Link href={`/appointments/edit/${appointment.id}`}>
                <button className="text-center rounded bg-blue-500 px-4 py-2 text-white hover:bg-blue-600">Edit</button>
              </Link>
              <form action={deleteAppointment.bind(null, appointment.id)}>
               <button type="submit" className="text-center rounded bg-blue-500 px-4 py-2 text-white hover:bg-blue-600">delete</button>
              </form>
                </div>
                 </div>
            ))}
            </div>
            </div>
    
        </main>
    )
}
