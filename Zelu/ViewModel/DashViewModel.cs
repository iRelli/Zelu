using Fusion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Zelu.ViewModel
{
    public class DashViewModel : ViewModelBase
    {

        public string _username;
        public string _totalusers;
        public string _totalAPI;
        public string _expiry;
        public string _news;
        public string _appVersion;
        public string _timelimt;
        public string _constraint;
        public string _target;
        public string _port;
        public string _time;
        public string _method;
        public string _totalRequest;

        public string Username
        {
            get { return _username; }
            set { _username = value; SetProperty(nameof(Username)); }
        }
        public string Totalusers
        {
            get { return _totalusers; }
            set { _totalusers = value; SetProperty(nameof(Totalusers)); }
        }
        public string API
        {
            get { return _totalAPI; }
            set { _totalAPI = value; SetProperty(nameof(API)); }
        }
        public string Expiry
        {
            get { return _expiry; }
            set { _expiry = value; SetProperty(nameof(Expiry)); }
        }
        public string News
        {
            get { return _news; }
            set { _news = value; SetProperty(nameof(News)); }
        }
        public string AppVersion
        {
            get { return _appVersion; }
            set { _appVersion = value; SetProperty(nameof(AppVersion)); }
        }
        public string Timelimit
        {
            get { return _timelimt; }
            set { _timelimt = value; SetProperty(nameof(Timelimit)); }
        }
        public string Constraint
        {
            get { return _constraint; }
            set { _constraint = value; SetProperty(nameof(Constraint)); }
        }
        public string Target
        {
            get { return _target; }
            set { _target = value; SetProperty(nameof(Target)); }
        }
        public string Port
        {
            get { return _port; }
            set { _port = value; SetProperty(nameof(Port)); }
        }
        public string Time
        {
            get { return _time; }
            set { _time = value; SetProperty(nameof(Time)); }
        }
        public string Method
        {
            get { return _method; }
            set { _method = value; SetProperty(nameof(Method)); }
        }
        public string TotalRequest
        {
            get { return _totalRequest; }
            set { _totalRequest = value; SetProperty(nameof(TotalRequest)); }
        }

        public DashViewModel()
        {
            LoadUserDetails();
            LoadData();
        }

        async void LoadData()
        {
            var VersionVar = await FusionApp.GetAppVar("Update");
            var VersionCheck = await Task.Run(() => JsonConvert.DeserializeObject<dynamic>(VersionVar));
            string VerifyVersion = VersionCheck["UpdateCheck"]["Version"].ToString();

            AppVersion= VerifyVersion.ToString();
            News = await FusionApp.GetAppVar("news");
            TotalRequest = await FusionApp.GetUserVar("attacks");
            Target = await FusionApp.GetUserVar("uhtarget");
            Port = await FusionApp.GetUserVar("uhport");
            Time = await FusionApp.GetUserVar("uhtime");
            Method = await FusionApp.GetUserVar("uhmethod");
            Totalusers = Fusion.App.UserCount;
            Username = User.Username;
            Expiry = User.Expiry;
            API = Fusion.App.ApiCount;
        }
        void LoadUserDetails()
        {
            switch (Fusion.User.Level)
            {
                case 1:
                    Constraint = "1";
                    Timelimit = "900 Seconds";
                    break;
                case 2:
                    Constraint = "2";
                    Timelimit = "1800";
                    break;
                case 3:
                    Constraint = "2";
                    Timelimit = "2700 Seconds";
                    break;
                case 4:
                    Constraint = "3";
                    Timelimit = "3600";
                    break;
                default:
                    Constraint = "Custom Plan";
                    Timelimit = "Custom Plan";
                    break;
            }
        }


    }
}
