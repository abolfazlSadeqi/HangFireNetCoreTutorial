using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace HangFireNetCoreTutorial.Controllers;

/// <summary>
/// •	Delayed : Delayed jobs are executed only once too, but not immediately – only after the specified time interval.
/// </summary>

public class DelayedJobsController : Controller
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    public DelayedJobsController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }
    public IActionResult Register()
    {
        var jobId = _backgroundJobClient.Schedule(() => Helper.SendSMS(SMSType.Register), TimeSpan.FromHours(12));

        return Ok($"Job Add");
    }

    public IActionResult LogBackupDB()
    {
        DateTime dateAndTime = DateTime.Parse("2023-06-21 12:00:00");
        var jobId = _backgroundJobClient.Schedule(() => Helper.SendSMS(SMSType.Register), dateAndTime);

        return Ok($"Job Add");
    }

}
