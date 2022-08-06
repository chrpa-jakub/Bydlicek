using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;

namespace LiveBot
{
    public class Emailer
    {
        private SmtpClient _smtpClient;
        private SimilarityChecker _similarity;
        private FileManager _fileManager;
        private (string Email, string Password) _emailLogin;

        public Emailer()
        {
            var emailLoginRaw = File.ReadAllLines("logininfo.txt");
            _emailLogin.Email = emailLoginRaw[0];
            _emailLogin.Password = emailLoginRaw[1];
            _fileManager = new FileManager("flats.txt");
            _similarity = new SimilarityChecker();
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_emailLogin.Email, _emailLogin.Password),
                Timeout = 20000
            };
        }

        public void Loop(List<FlatStorage> values, string[] links)
        {
            while (true)
            {
                try
                {
                    for (var i = 0; i < values.Count; i++)
                    {
                        values[i].Flat = Flat.UseCorrectParser(i, links[i]);
                        // Console.WriteLine($"{i+1} Old: {values[i].OldId} Found: {values[i].Flat.Id}");
                        if (values[i].Flat.Id != values[i].OldId && values[i].Flat.Id != "-1")
                        {
                            if(_similarity.IsDuplicate(values[i].Flat.GetRaw(),_fileManager.GetRawFlats()))
                                continue;
                        
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
            var from = new MailAddress(_emailLogin.Email, "Od Bydlíčka");
            var sendTo = new List<MailAddress>
            {
                new MailAddress("vobo470@gmail.com"),
                new MailAddress("diahexik@gmail.com"),
                new MailAddress("michkocze@gmail.com")
            };
            foreach (var address in sendTo)
            {
                _smtpClient.Send(new MailMessage(from, address)
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