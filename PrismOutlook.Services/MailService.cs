using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismOutlook.Business;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Services
{
    public class MailService : IMailService
    {
        static List<MailMessage> InboxItems = new List<MailMessage>()
        {
            new MailMessage
            {
                Id = 1,
                From = @"blagunas@infragistics.com",
                To = new ObservableCollection<string>(){ "jane@doe.com", "john@doe.com" },
                Subject = "This is a test email1",
                Body = "This is the body of an email1",
                DateSent = DateTime.Now,
            },
            new MailMessage
            {
                Id = 2,
                From = @"blagunas@infragistics.com",
                To = new ObservableCollection<string>(){ "jane@doe.com", "john@doe.com" },
                Subject = "This is a test email2",
                Body = "This is the body of an email2",
                DateSent = DateTime.Now.AddDays(-1),
            },
            new MailMessage
            {
                Id = 3,
                From = @"blagunas@infragistics.com",
                To = new ObservableCollection<string>(){ "jane@doe.com", "john@doe.com" },
                Subject = "This is a test email3",
                Body = "This is the body of an email3",
                DateSent = DateTime.Now.AddDays(-5),
            },
        };

        static List<MailMessage> SentItems = new List<MailMessage>();

        static List<MailMessage> DeletedItems = new List<MailMessage>();

        public IList<MailMessage> GetInboxItems()
        {
            return InboxItems;
        }

        public IList<MailMessage> GetSentItems()
        {
            return SentItems;
        }

        public IList<MailMessage> GetDeletedItems()
        {
            return DeletedItems;
        }

    }
}
