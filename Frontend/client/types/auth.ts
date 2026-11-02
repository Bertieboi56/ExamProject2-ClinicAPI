//Auth types

export type RegisterRequest = {
  firstname: string;
  lastname: string;
  email: string;
  password: string;
  birthdate: string;
  gender: string;
  socialSecurityNumber: string;
  taxnumber: string;
  driversLicence: string;
  religion: string;
  medicalInsuranceMemberNumber: string;
};

export type RegisterResponse = {
    userId: number;
    patientId: number;

}


export type LoginRequest = {
    email: string;
    password: string;

}

export type LoginResponse = {
    token: string;

}

