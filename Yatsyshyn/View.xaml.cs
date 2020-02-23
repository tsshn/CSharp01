using System.Windows.Controls;

namespace Yatsyshyn
{
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}