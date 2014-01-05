using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;

namespace SelfControl
{
    public partial class Stats : PhoneApplicationPage
    {
        public Stats()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer fetchTimer = new DispatcherTimer();
            fetchTimer.Interval = TimeSpan.FromSeconds(2);
            fetchTimer.Tick += (tmrObj, evt) =>
            {
                DispatcherTimer timer = tmrObj as DispatcherTimer;
                timer.Stop();
                spinner.Visibility = System.Windows.Visibility.Collapsed;
                ContentPanel.Visibility = System.Windows.Visibility.Visible;
            };

            fetchTimer.Start();
        }
    }
}