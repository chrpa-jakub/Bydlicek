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
        private SimilarityChecker _similarity;
        private FileManager _fileManager;

        public Emailer()
        {
            _fileManager = new FileManager("flats.txt");
            _similarity = new SimilarityChecker();
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
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
                        Console.WriteLine($"{i+1} Old: {values[i].OldId} Found: {values[i].Flat.Id}");
                        if (values[i].Flat.Id != values[i].OldId && values[i].Flat.Id != "-1" && !_similarity.IsDuplicite(values[i].Flat.GetRaw(),_fileManager.GetRawFlats()))
                        {
                            Console.WriteLine($"Email odeslán!");
                            SendFlat(values[i].Flat);
                            _fileManager.AddNewFlat(values[i].Flat.GetRaw());
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