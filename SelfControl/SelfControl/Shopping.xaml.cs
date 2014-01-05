using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace SelfControl
{
    public partial class Shopping : PhoneApplicationPage
    {
        const string baseUrl = "http://umecloud.com/hacks/selfcontrol/";
        const string invParam = "api.php?d=fridge&a=shoppinglist#";

        public Shopping()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helpers.lstShopping == null || Helpers.lstShopping.Count == 0)
            {
                Helpers.get(baseUrl + invParam, (json) =>
                {
                    Helpers.lstShopping = JsonConvert.DeserializeObject<ObservableCollection<InventoryItem>>(json);
                    Debug.WriteLine("Main Page fetching nearby:" + Helpers.lstShopping.Count);
                    pgBar.Visibility = System.Windows.Visibility.Collapsed;

                    for (int i = 0; i < Helpers.lstShopping.Count; i++)
                    {
                        Helpers.lstShopping[i].thumb = baseUrl + Helpers.lstShopping[i].thumb;
                        if (Helpers.lstShopping[i].low) Helpers.lstShopping[i].status = System.Windows.Visibility.Visible;
                        else Helpers.lstShopping[i].status = System.Windows.Visibility.Collapsed;
                    }
                    this.DataContext = Helpers.lstShopping;
                    btnReco.IsEnabled = true;
                });
            }
            else
            {
                pgBar.Visibility = System.Windows.Visibility.Collapsed;
                this.DataContext = Helpers.lstShopping;
                btnReco.IsEnabled = true;
            }
        }

        private void btnReco_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Maps.xaml", UriKind.Relative));
        }
    }
}