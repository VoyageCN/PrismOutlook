using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModelBase : ViewModelBase
    {
        private MailMessage _message;

        protected IMailService MailService;

        protected IRegionDialogService RegionDialogService;

        public MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DelegateCommand _deleteMessageCommand;
        public DelegateCommand DeleteMessageCommand => _deleteMessageCommand ??= new DelegateCommand(ExecuteDeleteMessage);

        private DelegateCommand<string> _messageCommand;
        public DelegateCommand<string> MessageCommand =>
            _messageCommand ?? (_messageCommand = new DelegateCommand<string>(ExecuteMessageCommand));

        public MessageViewModelBase(IMailService mailService, IRegionDialogService regionDialogService)
        {
            MailService = mailService;
            RegionDialogService = regionDialogService;
        }

        void ExecuteMessageCommand(string parameter)
        {
            if (Message == null)
                return;

            var parameters = new DialogParameters();
            var viewName = "MessageView";
            MessageMode replyType = MessageMode.Read;

            switch (parameter)
            {
                case nameof(MessageMode.Read):
                    {
                        viewName = "MessageReadOnlyView";
                        replyType = MessageMode.Read;
                        break;
                    }
                case nameof(MessageMode.Reply):
                    {
                        replyType = MessageMode.Reply;
                        break;
                    }
                case nameof(MessageMode.ReplyAll):
                    {
                        replyType = MessageMode.ReplyAll;
                        break;
                    }
                case nameof(MessageMode.Forward):
                    {
                        replyType = MessageMode.Forward;
                        break;
                    }
            }
            parameters.Add(MailParameters.MessageId, Message.Id);
            parameters.Add(MailParameters.MessageMode, replyType);

            RegionDialogService.Show(viewName, parameters, (result) =>
            {
                HandleMessageCallback(result);
            });
        }

        protected virtual void ExecuteDeleteMessage()
        {
            if (Message == null)
                return;

            MailService.DeleteMessage(Message.Id);
        }

        protected virtual void HandleMessageCallback(IDialogResult result)
        {

        }
    }

}
