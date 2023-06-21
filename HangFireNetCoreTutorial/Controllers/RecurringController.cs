using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Mvc;

namespace HangFireNetCoreTutorial.Controllers;

/// <summary>
/// •	Recurring : Recurring jobs are fired many times on the specified CRON schedule.
/// </summary>
public class RecurringController : Controller
{
    private readonly IRecurringJobManager _recurringJobManager;
    public RecurringController(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }

    public IActionResult DiffBackupDB()
    {
        //Every Day 
        _recurringJobManager.AddOrUpdate("DiffBackupDB", () => Helper.DiffBackupDB(), Cron.Daily);

        //Every Day at 03:10
        //_recurringJobManager.AddOrUpdate("DiffBackupDB", () => Helper.DiffBackupDB(), Cron.Daily( 3, 10));

        return Ok($"Job added to wait List");
    }
}
