import { getAppointmentById,updateAppointment,} from "@/modules/appointment/actions";
import { getDoctors, getCategories } from "@/modules/appointment/actions";

export default async function EditAppointmentPage({
  params,
}: {
  params: { id: string };
}) {
  const appointment = await getAppointmentById(Number(params.id));
  const doctors = await getDoctors();
  const categories = await getCategories();

  return (
    <main className="flex min-h-screen items-center justify-center text-black">
      <div className="w-full max-w-lg rounded-xl border bg-white p-8 shadow">
        <h1 className="text-center text-2xl font-bold text-black">
          Edit Appointment
        </h1>

        <form
          action={updateAppointment.bind(null, appointment.id)}
          className="space-y-4"
        >
          <div>
            <label htmlFor="date" className="block mb-1">
              Date
            </label>

            <input
              id="date"
              name="date"
              type="datetime-local"
              defaultValue={appointment.date.slice(0, 16)}
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
              required
            />
          </div>

          <div>
            <label htmlFor="doctorId" className="block mb-1">
              Doctor
            </label>

            <select
              id="doctorId"
              name="doctorId"
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
              required
            >
              {doctors.map((doctor) => (
                <option key={doctor.id} value={doctor.id}>
                  {doctor.firstname} {doctor.lastname}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label htmlFor="categoryId" className="block mb-1">
              Category
            </label>

            <select
              id="categoryId"
              name="categoryId"
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
              required
            >
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label htmlFor="duration" className="block mb-1">
              Duration
            </label>

            <input
              id="duration"
              name="duration"
              type="number"
              min="1"
              max="60"
              defaultValue={appointment.duration}
              className="w-full rounded-lg border border-slate-500 bg-white px-4 py-3 text-black placeholder-slate-400"
              required
            />
          </div>
        <div className="flex justify-center">
          <button
            type="submit"
            className="text-center rounded bg-blue-500 px-4 py-2 text-black hover:bg-blue-600">
            Update Appointment
          </button>
        </div>
        </form>
      </div>
    </main>
  );
}