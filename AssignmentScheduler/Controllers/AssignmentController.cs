using Microsoft.AspNetCore.Mvc;
using AssignmentScheduler.Repository.Interfaces;
using AssignmentScheduler.Entity;

namespace AssignmentScheduler.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentController : ControllerBase
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly ILogger _logger;

    public AssignmentController(IAssignmentRepository assignmentRepository,
                                ILogger<AssignmentController> logger)
    {
        _assignmentRepository = assignmentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Assignment>> Get()
    {
        return await _assignmentRepository.GetAsync();
    }

    [HttpGet("{id}")]
    public string Get(Guid id)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _assignmentRepository.RemoveAsync(id);
    }
}
