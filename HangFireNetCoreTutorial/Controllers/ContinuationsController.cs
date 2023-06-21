using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireNetCoreTutorial.Controllers;

/// <summary>
/// •	Continuations : Continuations are executed when parent job has finished.
/// </summary>
public class ContinuationsController : Controller
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    public ContinuationsController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }
    public IActionResult purchase()
    {

     
        var jobId = BackgroundJob.Schedule(() => Helper.SendMail(EmailType.purchase), TimeSpan.FromSeconds(45));
        BackgroundJob.ContinueJobWith(jobId, () => Helper.SendSMS(SMSType.purchase));

        return Ok($"Job added to wait List");
     
    }
}
