using Infragistics.Windows.OutlookBar;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Contacts.Menus
{
    /// <summary>
    /// ContactsGroup.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsGroup : OutlookBarGroup, iOutlookBarGroup
    {
        public ContactsGroup()
        {
            InitializeComponent();
        }

        public string DefaultNavigationPath => "ViewA";
    }
}
