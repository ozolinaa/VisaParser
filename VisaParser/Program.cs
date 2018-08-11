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

            int utcHourToSendLog = Convert.ToInt32(Environment.GetEnvironmentVariable("utcHourToSendLog"));
            IEnumerable<string> emails = Environment.GetEnvironmentVariable("emails").Split(' ');
            bool interviewRequired = Convert.ToBoolean(Environment.GetEnvironmentVariable("interviewRequired"));

            Logger logger = new Logger(utcHourToSendLog, emails);

            while (true)
            {
                try
                {
                    bool hasPlaces = Parser.HasMskPlaces(interviewRequired, out string parsedString);
                    logger.Log(string.Format("UTC {0} Status: {1} : {2}", DateTime.UtcNow.ToShortTimeString(), hasPlaces.ToString(), parsedString));

                    if (hasPlaces)
                    {
                        using (Notifier notifier = new Notifier())
                        {
                            foreach (string email in emails)
                            {
                                notifier.SendEmail(email, "MSK VISA PLACE !!!", "MSK VISA PLACE AVAILABLE !!!");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Log(string.Format("UTC {0} Error: {1}", DateTime.Now.ToShortTimeString(), e.Message));
                }

                try
                {
                    if (logger.IsNowTimeToSendLog())
                    {
                        logger.SendLogedItems();
                    }
                }
                catch (Exception)
                {
                }


                //Wait 1 minute untill next run
                Task.Delay(1000 * 60 * 1).Wait();
            }
        }


    }
}
