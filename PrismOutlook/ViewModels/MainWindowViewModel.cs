using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismOutlook.Core;

namespace PrismOutlook.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand => new DelegateCommand<string>(ExecuteNavigateCommand);

        public MainWindowViewModel(IRegionManager regionMananger, IApplicationCommands applicationCommands)
        {
             _regionManager = regionMananger;
            applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private void ExecuteNavigateCommand(string navigationpath)
        {
            if (string.IsNullOrEmpty(navigationpath))
                throw new ArgumentNullException();

            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationpath);
        }
    }
}
