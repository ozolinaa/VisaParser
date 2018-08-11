using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VisaParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            const int hourToSendLog = 20;
            string[] emails = new string[] { "anton.ozolin@gmail.com", "azhmurkova@gmail.com", "angubenko@gmail.com" };
            List<string> logItems = new List<string>();
            DateTime lastSentLogTime = DateTime.MinValue;

            while (true)
            {
                try
                {
                    bool hasPlaces = Parser.HasMskPlaces("http://www.gofortravel.ru/usa/visa/application/our-help/latest-news", true, out string parsedString);
                    string logItem = string.Format("{0} Status: {1} : {2}", DateTime.Now.ToShortTimeString(), hasPlaces.ToString(), parsedString);
                    Console.WriteLine(logItem);
                    logItems.Add(logItem);

                    if (hasPlaces)
                    {
                        using (Notifier notifier = new Notifier())
                        {
                            foreach (string email in emails)
                            {
                                notifier.SendEmail(email, "MSK VISA PLACE !!!", logItem);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    string logItem = string.Format("{0} Error: {1}", DateTime.Now.ToShortTimeString(), e.Message);
                    Console.WriteLine(logItem);
                    logItems.Add(logItem);
                }


                DateTime now = DateTime.Now;
                if (now.Hour == hourToSendLog && (now - lastSentLogTime).Hours > 1)
                {
                    string logMsg = string.Join("<br />", logItems);
                    logItems = new List<string>();
                    using (Notifier notifier = new Notifier())
                    {
                        foreach (string email in emails)
                        {
                            notifier.SendEmail(email, "MSK VISA LOG", logMsg);
                        }
                    }
                    lastSentLogTime = now;
                }

                //Wait 1 minute untill next run
                Task.Delay(1000 * 60 * 1).Wait();
            }
        }


    }
}
