using Microsoft.AspNetCore.Mvc;
using AssignmentScheduler.Entity;
using AssignmentScheduler.Repository.Interfaces;

namespace AssignmentScheduler.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public UserController(IUserRepository userRepository,
                          ILogger<UserController> logger )
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        return await _userRepository.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<User> Get(Guid id)
    {
        return await _userRepository.GetAsync(id);
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut()]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _userRepository.RemoveAsync(id);
    }
}
