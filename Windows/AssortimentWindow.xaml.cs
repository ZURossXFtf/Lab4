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
using System.Windows.Shapes;
using Lab4.Models;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class AssortimentWindow : Window
    {
        private HttpClient client;
        private Assortiment? assortiment;
        public AssortimentWindow(string token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Assortiment>? list = await client.GetFromJsonAsync<List<Assortiment>>("http://localhost:5224/api/assortiments");
            Dispatcher.Invoke(() =>
            {
                ListAssortiments.ItemsSource = null;
                ListAssortiments.Items.Clear();
                ListAssortiments.ItemsSource = list;
            });
        }
        private async Task Save()
        {
            Assortiment assortiment = new Assortiment
            {
                Kod = Kod.Text,
                Name = NameAssortiment.Text,
                Price = Price.Text
            };
            JsonContent content = JsonContent.Create(assortiment);
            using var response = await client.PostAsync("http://localhost:5224/api/assortiment", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }

        private void ListAssortiments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            assortiment=ListAssortiments .SelectedItem as Assortiment;
            Kod.Text=assortiment?.Kod;
            NameAssortiment.Text=assortiment?.Name;
            Price.Text=assortiment?.Price;
        }

        private async Task Edit()
        {
            assortiment!.Kod = Kod.Text;
            assortiment!.Name = NameAssortiment.Text;
            assortiment!.Price = Price.Text;
            JsonContent content = JsonContent.Create(assortiment);
            using var response = await client.PutAsync("http://localhost:5224/api/assortiment", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5224/api/assortiment/" + assortiment?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Edit();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Delete();
        }
    }
}
