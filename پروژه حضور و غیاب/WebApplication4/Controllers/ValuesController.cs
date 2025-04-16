using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliController : ControllerBase
    {
        private static string TimeFirst;  

        [HttpGet("Start")]
        public IActionResult GetTime()
        {
            TimeFirst = DateTime.Now.ToString("HH:mm:ss");
            string filePath = @"C:\Users\MOHAMADREZA\Desktop\firsttime.txt";
            System.IO.File.WriteAllText(filePath, TimeFirst);
            return Ok(new { message = "!خوش آمدید", Time = TimeFirst });
        }

        [HttpGet("End")]
        public IActionResult GetSecondTime()
        {
            TimeSpan totalTime1 = TimeSpan.Zero;

            string filePath1 = @"C:\Users\MOHAMADREZA\Desktop\firsttime.txt";
            if (System.IO.File.Exists(filePath1))
            {
                using (StreamReader reader = new StreamReader(filePath1))
                {
                    string Timesaved = reader.ReadLine();
                    if (!string.IsNullOrEmpty(Timesaved))
                    {
                        totalTime1 = TimeSpan.Parse(Timesaved);
                    }
                }
            }

            string TimeSecond = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan t2 = TimeSpan.Parse(TimeSecond);

            string diffrenttime = (t2 - totalTime1).ToString(@"hh\:mm\:ss");

            string filePath = @"C:\Users\MOHAMADREZA\Desktop\output.txt";

            TimeSpan totalTime = TimeSpan.Zero;

            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string Timesaved = reader.ReadLine();
                    if (!string.IsNullOrEmpty(Timesaved))
                    {
                        totalTime = TimeSpan.Parse(Timesaved);
                    }
                }
            }

            TimeSpan newTime = TimeSpan.Parse(diffrenttime);
            totalTime += newTime;

            System.IO.File.WriteAllText(filePath, totalTime.ToString(@"dd\:hh\:mm\:ss"));

            return Ok(new
            {
                message = "خسته نباشید-خدانگهدار",
                Time = TimeSecond,
                message2 = "میزان زمان حضور شما در شرکت",
                time2 = diffrenttime,
            });
        }
        [HttpGet("Total")]
        public IActionResult Total()
        {
            string filePath = @"C:\Users\MOHAMADREZA\Desktop\output.txt";
            TimeSpan totalTime = TimeSpan.Zero;
            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string Timesaved = reader.ReadLine();
                    if (!string.IsNullOrEmpty(Timesaved))
                    {
                        totalTime = TimeSpan.Parse(Timesaved);
                    }
                }
            }
            TimeSpan newTime = totalTime;
            string timeday = newTime.ToString(@"dd");
            string timehour = newTime.ToString(@"hh");
            int Hour = Convert.ToInt32(timehour);
            int Day = Convert.ToInt32(timeday) * 24;
            return Ok(new
            {
                messege = "کل زمان جضور شما در شرکت",
                Time = newTime.ToString(@"dd\:hh\:mm\:ss"),
                totalTime = $"{Day + Hour}:{newTime.ToString(@"mm\:ss")}"
            });
        }
    }
}
