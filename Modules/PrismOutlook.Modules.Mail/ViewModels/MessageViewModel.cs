using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Infragistics.Windows.Ribbon;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand _sendMessageCommand;

        private MailMessage _message;
        private readonly IMailService _mailService;

        public MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand SendMessageCommand =>
            _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(ExecuteSendMessageCommand));

        void ExecuteSendMessageCommand()
        {
            _mailService.SendMessage(Message);

            IDialogParameters parameters = new DialogParameters();
            parameters.Add("messageSent", Message);

            RequestClose.Invoke(new DialogResult(ButtonResult.Yes, parameters));
        }

        public MessageViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        public string Title => "Mail Message";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = new MailMessage() { From = "ieshiaonan@gmail.com " };

            var messageMode = parameters.GetValue<MessageMode>(MailParameters.MessageMode);
            if (messageMode != MessageMode.New)
            {
                var messageId = parameters.GetValue<int>(MailParameters.MessageId);
                var originalMessage = _mailService.GetMessage(messageId);

                Message.To = GetToEmail(messageMode, originalMessage);

                if (messageMode == MessageMode.Reply || messageMode == MessageMode.ReplyAll)
                    Message.CC = originalMessage.CC;

                Message.Subject = GetMessageSubject(messageMode, originalMessage);

                Message.Body = originalMessage.Body;
            }
        }

        string GetMessageSubject(MessageMode mode, MailMessage originalMessage)
        {
            string prefix = string.Empty;

            switch (mode)
            {
                case MessageMode.Reply:
                case MessageMode.ReplyAll:
                    {
                        prefix = "RE:";
                        break;
                    }
                case MessageMode.Forward:
                    {
                        prefix = "FW:";
                        break;
                    }
            }
            return originalMessage.Subject.ToLower().StartsWith(prefix.ToLower()) ? originalMessage.Subject : $"{prefix} {originalMessage.Subject}";
        }

        ObservableCollection<string> GetToEmail(MessageMode mode, MailMessage message)
        {
            var toEmails = new ObservableCollection<string>();

            switch (mode)
            {
                case MessageMode.Reply:
                    {
                        toEmails.Add(message.From);
                        break;
                    }
                case MessageMode.ReplyAll:
                    {
                        toEmails.AddRange(message.To.Where(x => x != "ieshiaonan@gmail.com"));
                        toEmails.Add(message.From);
                        break;
                    }
                case MessageMode.Forward:
                    {
                        break;
                    }
            }
            return toEmails;
        }
    }
}
