using System.Windows.Controls;
using Fusion;
using Zelu.ViewModel;

namespace Zelu.UC
{

    public partial class ucDashboard : UserControl
    {
        public ucDashboard()
        {
            DataContext = new DashViewModel();
            InitializeComponent();
          

        }
    }
}
