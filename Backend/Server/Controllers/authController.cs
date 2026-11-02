using BackendNetwork.DTOs;
using BackendNetwork.Services;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


        /// <summary>
        /// Registers user.
        /// </summary>
        /// <response code="200">Successfully Registers the user </response>
        /// <response code="400">Bad Request, null values, invalid credentials</response>
   [HttpPost("register")]
public IActionResult Register(RegisterDto request)
{
    try
    {
        var result = _authService.Register(request);

        if (result == null)
        {
            return BadRequest(new
            {
                message = "Patient role not found in DB"
            });
        }

        return Ok(new
        {
            message = "Registration successful",
            result.Value.userId,
            result.Value.patientId
        });
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(new
        {
            message = ex.Message
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            message = "An error has appeared!",
            error = ex.Message
        });
    }
}    


    /// <summary>
    /// Logs the User
    /// </summary>
    /// <response code="200">Returns Cookie token and success message</response>
    /// <response code="400">Bad request, Login invalid </response>
    [HttpPost("login")]
    public IActionResult Login(LoginDto request)
    {
        try
        {
            var user = _authService.Login(request);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Wrong email or password"
                });
            }

            var token = _authService.GenerateToken(user);

            return Ok(new
            {
                message = "Login successful",
                token = token
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error has appeared!",
                error = ex.Message
            });
        }
    }
}