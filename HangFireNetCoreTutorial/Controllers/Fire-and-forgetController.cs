using Hangfire;

using Microsoft.AspNetCore.Mvc;

namespace HangFireNetCoreTutorial.Controllers;

/// <summary>
/// Fire_and_forget: These jobs are executed only once and almost immediately after they are fired
/// </summary>
public class Fire_and_forgetController : Controller
{

    private readonly IBackgroundJobClient _backgroundJobClient;
    public Fire_and_forgetController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }
    public IActionResult Register()
    {
        var jobId = _backgroundJobClient.Enqueue(() => Helper.SendMail(EmailType.Register));
        return Ok($"Job Id {jobId} Completed");

    }
}
