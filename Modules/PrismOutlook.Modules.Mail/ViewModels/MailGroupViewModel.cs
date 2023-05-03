using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using PrismOutlook.Business;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private DelegateCommand<NavigationItem> _selectedCommand;
        private readonly IApplicationCommands _applicationCommand;

        public DelegateCommand<NavigationItem> SelectedCommand => _selectedCommand ??= new DelegateCommand<NavigationItem>(ExecuteSelectedCommand);

        public MailGroupViewModel(IApplicationCommands applicationCommand)
        {
            GenerateMenu();
            _applicationCommand = applicationCommand;
        }

        private void ExecuteSelectedCommand(NavigationItem navigationItem)
        {
            if (navigationItem != null)
                _applicationCommand.NavigateCommand.Execute(navigationItem.NavigationPath);        
        }

        public void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem() { Caption = "Personal Folder", NavigationPath = "MailList?id=Default" };
            root.Items.Add(new NavigationItem() { Caption = "Inbox", NavigationPath = "MailList?id=Inbox" });
            root.Items.Add(new NavigationItem() { Caption = "Deleted", NavigationPath = "MailList?id=Deleted" });
            root.Items.Add(new NavigationItem() { Caption = "Sent", NavigationPath = "MailList?id=Sent" });

            Items.Add(root);

        }
    }
}
