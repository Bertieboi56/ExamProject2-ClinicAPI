// data types

export interface SearchDoctor {
  fullName: string;
  clinicName: string;
  specialityName: string;
}


export interface Appointment {
  id: number;
  date: string;
  patientId: number;
  doctorId: number;
  categoryId: number;
  duration: number;
  firstname: string;
  lastname: string;
  doctorName: string;
  clinicName: string;
  categoryName: string;
}

export interface CreateAppointmentRequest {
  date: string;
  duration: number;
  doctorId: number;
  categoryId: number;
  firstname?: string;
  lastname?: string;
  birthdate?: string;
}

export interface Category {
  id: number;
  name: string;
}

export interface Doctor{
  id: number;
  firstname: string;
  lastname: string;
}



export interface BookingFormProps {
  categories: Category[];
  doctors: Doctor[];
  isLoggedIn: boolean;
}

