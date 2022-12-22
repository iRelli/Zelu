using Fusion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Zelu.Classes;

namespace Zelu.UC
{
    /// <summary>
    /// Interaction logic for ucHub.xaml
    /// </summary>
    public partial class ucHub : UserControl
    {
        private static Dictionary<string, Handler> serverInfo;
        public ucHub()
        {
         
            InitializeComponent();
            LoadServers();
            FileManager.LoadData(AlData);
            cbMethods.SelectedIndex = 0;
            cbServer.SelectedIndex = 0;
        }

        private static int btnCounter;
        public string Target;
        public string Port;
        public string Time;
        public string Method;
        public string Server;
        private async void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            Target = tbTarget.Text;
            Port = tbPort.Text;
            Time = tbTime.Text;
            Method = cbMethods.Text;
            Server = cbServer.Text;
            if (string.IsNullOrEmpty(Target) || string.IsNullOrEmpty(Port) || string.IsNullOrEmpty(Time)) return;
            
            var _serverid = serverInfo[Server].Id;
            var Execute = await FusionApp.ExecuteFullAPI(_serverid, $"host={Target}&port={Port}&time={Time}&method={Method}", Convert.ToInt32(Time));
            if(Execute.Error == false)
            {
                btnCounter++;
                FileManager.Attack(Target, Port, Time, Method, Server);
                FileManager._RawResponse(Execute.Response);
                FileManager.LoadData(AlData);
                lblvarinfo.Text = (Int32.Parse(lblvarinfo.Text) + btnCounter).ToString();
                await FusionApp.SetUserVar("attacks", $"{lblvarinfo.Text}");
                await FusionApp.SetUserVar("uhtarget", Target);
                await FusionApp.SetUserVar("uhport", Port);
                await FusionApp.SetUserVar("uhtime", Time);
                await FusionApp.SetUserVar("uhmethod", Method);
                Debug.WriteLine(Execute.Response);
            }
            else
            {
                Debug.WriteLine(Execute.Message);
                FileManager._RawResponse(Execute.Response);
            }
        }

        private async void btnrenewattack_Click(object sender, RoutedEventArgs e)
        {
            var data = AlData.SelectedItem;
            string serverId = serverInfo[(AlData.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text].Id;
            Console.WriteLine(serverId);
            Console.WriteLine($"{(AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text}");
            var response = await FusionApp.ExecuteFullAPI(serverId, $"&host={(AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text}&port={(AlData.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text}&time={(AlData.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text}&method={(AlData.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text}", Convert.ToInt32((AlData.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text));
            if (response.Error == false)
            {
                Debug.WriteLine(response.Message);
                #region Writing to Json
                FileManager.Attack((AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text, (AlData.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text, (AlData.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text, (AlData.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text, (AlData.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text);
                #endregion
                btnCounter++;
                lblvarinfo.Text = (Int32.Parse(lblvarinfo.Text) + btnCounter).ToString();
                await FusionApp.SetUserVar("attacks", $"{lblvarinfo.Text}");
                await FusionApp.SetUserVar("uhtarget", $"{(AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text}");
                await FusionApp.SetUserVar("uhport", $"{(AlData.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text}");
                await FusionApp.SetUserVar("uhtime", $"{(AlData.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text}");
                await FusionApp.SetUserVar("uhmethod", $"{(AlData.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text}");

            }
            else
            {
                Debug.WriteLine(response.Message);
                FileManager._RawResponse(response.Response);
            }
        }


        private void btnPing_Click(object sender, RoutedEventArgs e)
        {
            var data = AlData.SelectedItem;
            Console.WriteLine($"{(AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text}");

            Process.Start("CMD.exe", $"/K ping {(AlData.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text} -t");
        }
        private void btnPingtb_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTarget.Text)) return;
            else
                Process.Start("CMD.exe", $"/K ping {tbTarget.Text} -t");
        }

        private void AlData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e) => e.Cancel = true;

        #region Loading Data
        public void LoadData(ComboBox comboBox, string data)
        {
            comboBox.Items.Clear();
            try
            {
                var strArray = data.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < strArray.Length; i++)
                {
                    comboBox.Items.Add(strArray[i]);
                }
            }
            catch
            {

            }
        }

      

        private void cbServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbServer.SelectedItem == null) return;
            LoadData(cbMethods, serverInfo[cbServer.SelectedItem.ToString()].Methods);
        }

        private async void LoadServers()
        {
            try
            {
                lblvarinfo.Text = await FusionApp.GetUserVar("attacks");
                string serversJson = await FusionApp.GetAppVar("Servers");
                List<Handler> servers = JsonConvert.DeserializeObject<List<Handler>>(serversJson);
                serverInfo = new Dictionary<string, Handler>();
                foreach (Handler server in servers)
                {
                    serverInfo.Add(server.Name, server);
                    cbServer.Items.Add(server.Name);
                }
            }
            catch
            {
                MessageBox.Show("Couldn't fetch data, please restart the application", Fusion.App.AppName);
                return;
            }

        }
        #endregion
    }
}
