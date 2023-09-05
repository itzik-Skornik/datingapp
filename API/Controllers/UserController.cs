using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] // http/user
public class UserController : ControllerBase
{
    private  DataContext _Context ;
    public UserController(DataContext context)
    {
        _Context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appuser>>> GetUsers()
    {
        var users = await _Context.Users.ToListAsync();
        return users;
    }
    [HttpGet("{id}")] // http/users/2
    public async Task<ActionResult<Appuser>> GetUser(int id)
    {
        var user = await _Context.Users.FindAsync(id);
        return user;

    }
}
