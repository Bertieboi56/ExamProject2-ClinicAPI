using BackendNetwork.Models;
using BackendNetwork.DTOs;
using BackendNetwork.Data;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace BackendNetwork.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthService( DataContext context,JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }
        //Registering service logic
        public (int userId, int patientId)? Register(RegisterDto request)
        {

            var existingPatient = _context.Patients
            .FirstOrDefault(p => p.Email == request.Email);

            if (existingPatient != null)
            {
                throw new InvalidOperationException("Email is already registered");
            }
  

            var patientRole = _context.Roles
                .FirstOrDefault(r => r.Name == "Patient");

            if (patientRole == null)
            {
                return null;
            }

            // Creating the new patient
            var newPatient = new Patient
            {
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Birthdate = request.Birthdate,
                Gender = request.Gender,
                SocialSecurityNumber = request.SocialSecurityNumber,
                Taxnumber = request.Taxnumber,
                Religion = request.Religion,
                DriversLicense = request.DriversLicence,
                MedicalInsuranceMemberNumber = request.MedicalInsuranceMemberNumber
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();

            // Creating new user linked to patient table
            var newUser = new User
            {
                Password = request.Password,
                RoleId = patientRole.Id,
                PatientId = newPatient.Id
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return (newUser.Id, newPatient.Id);
        }
 // User login logic!
  public User? Login(LoginDto request)
{
    var user = _context.Users
        .Include(u => u.Patient)
        .FirstOrDefault(u =>
            u.Patient != null &&
            u.Patient.Email == request.Email &&
            u.Password == request.Password);

    return user;
}
       
       //Token generator for user who log in! 
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()
                ),
                new Claim(
                    "patientId",
                    user.PatientId.ToString()
                ),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                )
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    _jwtSettings.ExpiryMinutes
                ),
                
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}