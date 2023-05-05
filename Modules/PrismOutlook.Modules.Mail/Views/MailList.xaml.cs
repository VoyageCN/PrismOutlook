using System.Windows.Controls;
using System.Windows.Navigation;
using Prism.Regions;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.Menus;

namespace PrismOutlook.Modules.Mail.Views
{
    /// <summary>
    /// Interaction logic for MailList
    /// </summary>
    [DependentView(RegionNames.RibbonRegion, typeof(HomeTab))]
    public partial class MailList : UserControl, ISupportDataContext, IRegionMemberLifetime
    {
        public MailList()
        {
            InitializeComponent();
        }

        public bool KeepAlive => false;
    }
}
