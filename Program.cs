using System;
using System.Collections.Generic;
using System.Linq;
using AE.Net.Mail;

namespace TrelloT
{
    internal class Program
    {
        private const string host = "imap.googlemail.com";
        private const string username = "";
        private const string password = "";

        private static ImapClient imapClient;

        static void Main(string[] args)
        {
            foreach (Lazy<MailMessage> message in GetMessages())
            {
                Console.WriteLine($"From: {message.Value.From}\n" +
                                    $"Subject: {message.Value.Subject}\n" +
                                    $"Date: {message.Value.Date}\n");
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        static List<Lazy<MailMessage>> GetMessages()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            imapClient = new(host, username, password, AuthMethods.Login, 993, true);

            List<Lazy<MailMessage>> mailMessages = imapClient.SearchMessages(SearchCondition.Unseen(), false, false).ToList();

            var mas = imapClient.GetMessage(0);

            return mailMessages;
        }
    }
}
