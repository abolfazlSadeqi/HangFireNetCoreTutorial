# Overview
This is very esay simple application HangFire with  .Net7 and HangFire For Learn

# Hangfire
   Hangfire is an open-source framework that  An easy way to perform background processing in .NET and .NET Core applications. No Windows Service or separate process required.
   
https://www.hangfire.io/

# Where is it used?
To background jobs with time for these scenarios:

•	Send Email 

•	Send SMS

•	maintaining  DB

•	batch import 

•	file(Video,Pic,..) processing

•	Huge reports

## Steps Setup

### Install the below NuGet packages :
•	Hangfire

•	Hangfire.AspNetCore

•	Hangfire.SqlServer

### Create DB

CREATE DATABASE [HangFireNetCoreTutorial]

### Add Configure the connection string into the appsettings.json file.
```
{
  "ConnectionStrings": {
    "HangfireConnection": "Password=123;Persist Security Info=True;User ID=sad;Initial Catalog=HangFireNetCoreTutorial;Data Source=.;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Hangfire": "Information"
    }
  }
}
```

### Add Configure things in Program.cs or Startup related to Hangfire, like SQL Server Database Connection and middleware
```
builder.Services.AddHangfire(x => x.UseSqlServerStorage("<connection string>"));
builder.Services.AddHangfireServer();
```
###  Add Configure Hangfire dashboard in Program.cs or Startup

Monitoring UI allows you to see and control any aspect of background job processing, including statistics, exceptions and background job history.
```
app.UseHangfireDashboard();
app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "default",
              pattern: "{controller=Reports}/{action=Index}/{id?}");

            // HangFire Dashboard endpoint
            endpoints.MapHangfireDashboard();
        });
```
### Add IBackgroundJobClient  or  IRecurringJobManager in Controller

a)For  Fire-and-forget or Delayed Type or  Continuations
```
  private readonly IBackgroundJobClient _backgroundJobClient;
    public Fire_and_forgetController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }
```
b)To  Recurring Type
```
private readonly IRecurringJobManager _recurringJobManager;
    public RecurringController(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }
```

## Types of Jobs and Usage in Hangfire

### Fire-and-forget : 
These jobs are executed only once and almost immediately after they are fired.

Example(Send Email immediately)
```
 _backgroundJobClient.Enqueue(() => Helper.SendMail(EmailType.Register));
```
### Delayed 
Delayed jobs are executed only once too, but not immediately – only after the specified time interval.

Example(Send Email After 12 Hours)
```
_backgroundJobClient.Schedule(() => Helper.SendSMS(SMSType.Register), TimeSpan.FromHours(12));
```
### Recurring 
Recurring jobs are fired many times on the specified CRON schedule.

  Example(Backup Diff DB every Daily)
```
_recurringJobManager.AddOrUpdate("DiffBackupDB", () => Helper.DiffBackupDB(), Cron.Daily);
```

### RContinuations 
Continuations are executed when parent job has finished.

Example(Send Email purchase After 45 Second after SendSMS purchase)
```
var jobId = BackgroundJob.Schedule(() => Helper.SendMail(EmailType.purchase), TimeSpan.FromSeconds(45));
BackgroundJob.ContinueJobWith(jobId, () => Helper.SendSMS(SMSType.purchase));
```


