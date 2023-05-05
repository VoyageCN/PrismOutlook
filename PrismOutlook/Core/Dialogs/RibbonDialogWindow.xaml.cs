using Prism.Services.Dialogs;

namespace PrismOutlook.Core.Dialogs
{
    /// <summary>
    /// RibbonDialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RibbonDialogWindow : IDialogWindow
    {
        public RibbonDialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
