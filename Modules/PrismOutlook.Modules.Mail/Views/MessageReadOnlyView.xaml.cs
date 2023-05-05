using System.Windows.Controls;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.Menus;

namespace PrismOutlook.Modules.Mail.Views
{
    /// <summary>
    /// MessageReadOnlyView.xaml 的交互逻辑
    /// </summary>
    [DependentView(RegionNames.RibbonRegion, typeof(MessageReadOnlyTab))]
    public partial class MessageReadOnlyView : UserControl, ISupportDataContext
    {
        public MessageReadOnlyView()
        {
            InitializeComponent();
        }
    }
}
