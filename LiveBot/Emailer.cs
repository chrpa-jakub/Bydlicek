using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace LiveBot
{
    public class Emailer
    {
        private SmtpClient _smtpClient;

        public Emailer()
        {
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                // Heslo
                Timeout = 20000
            };
        }

        public void Loop(List<Values> values, string[] links)
        {
            while (true)
            {
                try
                {
                    for (var i = 0; i < values.Count; i++)
                    {
                        values[i].Flat = values[i].DecideParse(i, links[i]);
                        Console.WriteLine($"Old: {values[i].OldId} Found: {values[i].Flat.Id}");
                        if (values[i].Flat.Id != values[i].OldId)
                        {
                            Console.WriteLine($"Nový byt! {values[i].Flat.Link}");
                            SendFlat(values[i].Flat);
                            values[i].OldId = values[i].Flat.Id;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            
            
                Thread.Sleep(10000);
            }
        }
        

        public void SendFlat(Flat flat)
        {
            var from = new MailAddress("bydlicekpraha@gmail.com", "Od Bydlíčka");
            var sendTo = new List<MailAddress>
            {
                new MailAddress("vobo470@gmail.com"),
                new MailAddress("diahexik@gmail.com"),
                new MailAddress("michkocze@gmail.com")
            };
            foreach (var adress in sendTo)
            {
                _smtpClient.Send(new MailMessage(from, adress)
                {
                    Subject = GenerateSubject(flat),
                    Body = GenerateBody(flat)
                });
            }
        }

        private string GenerateSubject(Flat flat)
        {
            return $"Nový byt - {flat.Size}, {flat.Locality} za {flat.Price} Kč";
        }

        private string GenerateBody(Flat flat)
        {
            return $@"Ahoj, naskytla se nabídka na nové bydlení! {flat.Link}

Byt má dostupnost {flat.Size}, lokalita: {flat.Locality}. Jeho cena je {flat.Price} Kč.


{string.Join(", ", flat.Labels)}
";
        }
    }
}