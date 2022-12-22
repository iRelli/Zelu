using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zelu.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                SetProperty(nameof(CurrentChildView));
            }
        }
        public ICommand DashboardCommand { get; }
        public ICommand HubCommand { get; }
        public ICommand ToolCommand { get; }
        public ICommand SettingsCommand { get; }
        public MainViewModel()
        {
            DashboardCommand = new ViewModelCommand(ExecuteDashboard);
            HubCommand = new ViewModelCommand(ExecuteHub);
            ToolCommand = new ViewModelCommand(ExecuteTool);
            SettingsCommand = new ViewModelCommand(ExecuteSettings);


            ExecuteDashboard(null);

        }

        private void ExecuteSettings(object obj)
        {
           //CurrentChildView = new SettingViewModel();
        }

        private void ExecuteTool(object obj)
        {
            //CurrentChildView = new ToolViewModel();
        }

        private void ExecuteHub(object obj)
        {
            CurrentChildView = new HubViewModel();
        }

        private void ExecuteDashboard(object obj)
        {
            CurrentChildView = new DashViewModel();
        }
    }
}
