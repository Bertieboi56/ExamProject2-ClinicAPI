
![](http://images.restapi.co.za/pvt/Noroff-64.png)
# Noroff
# Back-end Development Year 2
#### This folder should be used for the back-end code.

# Things to note
This Health App runs with .NET 9 



# How to set up the HealthcareDB app


Please CD to the Server folder with the following commands:
cd oct24ft-oct24ft-ep2-ca-bertieboi56
cd backend
cd server

then proceed to - Dotnet build

# Database
To set up the Database 
Please copy The ConnectionStrings and JWTsettings below into the 
Appsettings.Json, feel free to change the information within the connecton string and jtwsettings.

{
  
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=HealthCaredb;User=HealthCareAdmin;Password=secretpassword1234;"
  },
  "JwtSettings": {
    "SecretKey": "A-Very-Super-Secret-Key-123456789",
    "Issuer": "HealthCareApp",
    "Audience": "Users",
     "ExpiryMinutes": 60
  },

# Migration
Use Migrations to load the Database onto mySQL
Make sure your on the server root.
dotnet ef migrations add -c DataContext Initial     - To initialize the tables

dotnet ef migrations list                            - To view the migration

dotnet ef database update Initial                    - To implement the database

Once you confirm that it has succesfully inplimented the Database, go ahead and dotnet run


# Backend Dependancies 

Microsoft.AspNetCore.Authentication.JwtBearer 9.0.8
Microsoft.AspNetCore.OpenApi 9.0.16
Microsoft.EntityFrameworkCore.Design 9.0.0
MySql.EntityFrameworkCore 9.0.0
Pomelo.EntityFrameworkCore.MySql 9.0.0
Swashbuckle.AspNetCore 6.6.2
System.IdentityModel.Tokens.Jwt 8.18.0

# "ENDPOINTS"
Description of each end Point
# doctor

The doctor endpoint is designed to Display Doctor information to the patient
example -

Firstname
lastname
SpecialityID - Fetches name of speciality From the Speciality Endpoint
ClinicID - Fetches name of Clinic From the Clinic Endpoint

List of all Doctor CRUDs and what they do below .

GET /api/Doctor
Will return all doctors in the database

GET /api/Doctor/{id}
Will return doctor by its Id

POST /api/Doctor
Will Create a new Doctor and save it to the Database/Display on the frontend.

PUT /api/Doctor/{id}
Will Update the Select Doctors information by its id 

DELETE /api/Doctor{id}
Will delete selected Doctor by its id, Will only delete the Doctor If he is not tied to a booked appointment by the user

GET /api/Doctor/Search
A search endpoint where you can type in a letter or part of a name to search for the desired Doctor.



# Speciality

The Speciality endpoint is designed to display the names of all current Specialities avaliable on the API.
example - 

name: "Neurology"

List of all Speciality CRUDs and what they do below.

GET /api/Speciality
Will return all specialities

Post /api/Speciality
Will create a new Speciality type.

GET /api/Speciality/{id}
Will get selected speciality by its id

DELETE /api/Speciality{id}
Deletes the select Speciality by its id 

PUT /api/Speciality/{id}
Updates the select speciality names by its id


# Clinic

The Clinic endpoint is designed to display the names of all current Clinics avaliable on the api 
Example - 

name: "Hope Clinic"

List of all Clinic CRUDs and what they do below.

GET /api/Clinic
Will return all Clinics

POST /api/Cinics
Will create a new Clinic

GET /api/Clinic/{id}
Will get Select clinic by its id

DELETE /api/Clinic/{id}
Deletes selected clinic by its id

PUT  /api/clinic{id}
Updates the selected Clinics name by its id

# Category 

The Category endpoint will list all current option of names available in the clinics.
Example -

name: "follow up"
name: "check up"

List of all Category CRUDs and what they do

GET /api/Category
Will return all Categories

POST /api/Category
Will create a new Category

GET /api/Category/{id}
will get selected Category by its id

DELETE /api/Category
Deletes selected Category by its id

PUT /api/Category
Updates the selected Category name by its id


# Patient  

The Patient endpoint is in control of the patient Data for Guests and registered.


GET /api/Patient/{id}
An endpoint for Registered patients, will be able to look at their information/Edit

Post /api/Patient
This endpoint will just post a Guest USER patient into the DB

GET /api/Patient 
This end point is used to get ALL Patients within the Database, this will display both guest and registered within the same patient table.

DELETE /api/Patient/{id}
Deletes selected patient by id.

PUT /api/Patient/{id}
Updates the Patient information by id, Only Registered Patients will have access to this endpoint


# Appointment 

The Appointment endpoint is used to maintaine and schedule Appointments for Guest and Registered Patients
However, Guests will have limited Access to what they are able to do unlike Registered Patients, like Managing their appointment.

GET /api/Patient/{id}
Will get the Appointment by its id

DELETE /api/Appointment/{id}
Deletes Selected Patient appointment by id, Will also only delete Appointment Ids that are tied to the USER

GET /api/Appointment
Gets all apointments in the database or if logged in, The appointments of that token USER

POST /api/Appointment
Creates a new post appointment, This is used by Guest and registered users.

 A registred user patient that uses the Auth  Register endpoint, will be given less information to input into the booking form as they place most of it during their registration.

A Guest will input non-pii when booking, they will recieve a confirmation message on their booking. 




# Auth

POST /api/Auth/Register
Will Create a registered USER They will input all their Pii/ most non-pii information when registering
People who Register will become USERS of the app and also set in the patient database, along with the guests.  

Patientid will be placed in the user table as well as their password.

POST /api/Auth/Login
Will log the registered user in the DB, will also be given a JWT token for 60 minutes.



# Sources

I found from https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs

Validation with Data Annotations, which i found can be placed within  the models " DTOs " 
this clears up the Controller files alittle, making it more clear to read for the auth files. as i have done with the logic which has been placed inside the Services folder.


# Things to note

After redesigning the controllers a few times due to misunderstanding and trying to figure out the best way to solve the exam project, I ended up not utilizing and using more service folders, I do see that it would be more modular and scalable by using them, normally it would be fine for most But i do see that AppointmentController could have benefitted greatly
from this.

I also added more logic/valiation than maybe was required inside appointments, like, two patients cannot booked the same time / appointment, from what i understood it was only the user who couldnt book two of the same, I went ahead and did both, it should work fine, this is also the same with the Date booking, you cannot book in the "past".






