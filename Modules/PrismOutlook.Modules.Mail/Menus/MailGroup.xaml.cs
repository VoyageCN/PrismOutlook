using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.ViewModels;

namespace PrismOutlook.Modules.Mail.Menus
{
    /// <summary>
    /// MailGroup.xaml 的交互逻辑
    /// </summary>
    public partial class MailGroup : OutlookBarGroup, iOutlookBarGroup
    {
        public MailGroup()
        {
            InitializeComponent();

            _dataTree.Loaded += DataTree_Loaded;
        }

        private void DataTree_Loaded(object sender, RoutedEventArgs e)
        {
            _dataTree.Loaded -= DataTree_Loaded;

            var parentNode = _dataTree.Nodes[0];
            var nodeToSelect = parentNode.Nodes[0];
            nodeToSelect.IsSelected = true;
        }

        public string DefaultNavigationPath
        {
            get
            {
                if (_dataTree.SelectionSettings.SelectedNodes[0] is XamDataTreeNode item)
                    return ((NavigationItem)item.Data).NavigationPath;

                return $"MailList?{FolderParameters.FolderKey}={FolderParameters.Inbox}";
            }
        }
    }
}
