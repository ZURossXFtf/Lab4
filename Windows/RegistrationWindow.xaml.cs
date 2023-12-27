using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Lab4.Models;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private HttpClient client;
        private Registration? registration;
        public RegistrationWindow(String token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(()=> LoadAssortiments());
        }
        public RegistrationWindow(String token,Registration registration)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => LoadAssortiments());
            Name.Text = registration.Name;
            Weight.Text = registration.Weight;
            Cost.Text = registration.Cost;
            DateConfirm.SelectedDate = registration.DateConfirm;
            cbAssortiment.SelectedItem = registration.Assortiment!.Name;
        }
        private async void LoadAssortiments()
        {
            List<Assortiment>? list = await client.GetFromJsonAsync<List<Assortiment>>("http://localhost:5224/api/assortiments");
            Dispatcher.Invoke(() =>
            {
                cbAssortiment.ItemsSource = list?.Select(p=>p.Name);
            });
        }
        public string? NameProperty
        {
            get { return Name.Text; }
        }
        public string? WeightProperty
        {
            get { return Weight.Text; }
        }
        public string? CostProperty
        {
            get { return Cost.Text; }
        }
        public DateTime? DateConfirmProperty
        {
            get { return DateTime.Parse(DateConfirm.Text); }
        }
        public async Task<int> getIdAssortiment()
        {
            Assortiment? assortiment= await client.GetFromJsonAsync<Assortiment>("http://localhost:5224/api/assortiment/"+cbAssortiment.Text);
            return assortiment!.Id;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
