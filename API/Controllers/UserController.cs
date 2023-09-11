using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;
[Authorize]
public class UserController : BaseApicontroller
{
    private  DataContext _Context ;
    public UserController(DataContext context)
    {
        _Context = context;
    }
    [AllowAnonymous]
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
