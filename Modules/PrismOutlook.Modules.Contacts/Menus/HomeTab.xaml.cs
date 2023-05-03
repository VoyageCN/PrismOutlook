using Infragistics.Windows.Ribbon;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Contacts.Menus
{
    /// <summary>
    /// HomeTab.xaml 的交互逻辑
    /// </summary>
    [DependentView(RegionNames.RibbonRegion, typeof(HomeTab))]
    public partial class HomeTab : RibbonTabItem
    {
        public HomeTab()
        {
            InitializeComponent();
        }
    }
}
