using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class SupportRequestController : ControllerBase
{ 
    private static int _nextId = 1;
    private static List<SupportRequest> _requests = new List<SupportRequest>();
    
    public SupportRequestController()
    {
        if (_requests.Count == 0)
        {
            _requests.Add(new SupportRequest { Id = _nextId++, Description = "Request 1", SubmissionTime = DateTime.Now, ResolutionDeadline = DateTime.Now.AddDays(1), IsResolved = false });
            _requests.Add(new SupportRequest { Id = _nextId++, Description = "Request 2", SubmissionTime = DateTime.Now, ResolutionDeadline = DateTime.Now.AddDays(2), IsResolved = false });
        }
    }
   

    [HttpPost]
    public ActionResult<SupportRequest> PostSupportRequest(SupportRequest request)
    {
        request.Id = _nextId++;
        request.SubmissionTime = DateTime.Now;
        request.IsResolved = false;
        _requests.Add(request);

        return CreatedAtAction(nameof(GetSupportRequest), new { id = request.Id }, request);
    }

    [HttpGet]
    public ActionResult<IEnumerable<SupportRequest>> GetSupportRequests()
    {
        return _requests
            .Where(r => !r.IsResolved)
            .OrderByDescending(r => r.ResolutionDeadline)
            .ToList();
    }

    [HttpPut("{id}")]
    public IActionResult MarkAsResolved(int id)
    {
        var request = _requests.FirstOrDefault(r => r.Id == id);
        if (request == null)
        {
            return NotFound();
        }

        request.IsResolved = true;

        return NoContent();
    }
    [HttpGet("{id}")]
    public ActionResult<SupportRequest> GetSupportRequest(int id)
    {
        var request = _requests.FirstOrDefault(r => r.Id == id);

        if (request == null)
        {
            return NotFound();
        }

        return request;
    }

    
}