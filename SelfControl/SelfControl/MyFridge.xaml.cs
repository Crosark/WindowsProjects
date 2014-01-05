using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SelfControl
{
    public partial class MyFridge : PhoneApplicationPage
    {
        const  string baseUrl = "http://umecloud.com/hacks/selfcontrol/";
        const string invParam = "api.php?d=fridge&a=inventory#";
        public MyFridge()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helpers.lstInventory == null || Helpers.lstInventory.Count == 0)
            {
                Helpers.get(baseUrl + invParam, (json) =>
                {
                    Helpers.lstInventory = JsonConvert.DeserializeObject<ObservableCollection<InventoryItem>>(json);
                    Debug.WriteLine("Main Page fetching nearby:" + Helpers.lstInventory.Count);
                    pgBar.Visibility = System.Windows.Visibility.Collapsed;

                    for (int i = 0; i < Helpers.lstInventory.Count; i++){
                        Helpers.lstInventory[i].thumb = baseUrl + Helpers.lstInventory[i].thumb;
                        if (Helpers.lstInventory[i].low) Helpers.lstInventory[i].status = System.Windows.Visibility.Visible;
                        else Helpers.lstInventory[i].status = System.Windows.Visibility.Collapsed;
                    }
                    this.DataContext = Helpers.lstInventory;
                    btnReco.IsEnabled = true;
                });
            }
            else
            {
                pgBar.Visibility = System.Windows.Visibility.Collapsed;
                this.DataContext = Helpers.lstInventory;
                btnReco.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Dinner.xaml", UriKind.Relative));
        }
    }
}