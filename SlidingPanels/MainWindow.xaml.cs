using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Interop;

namespace VideoAndQuestion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string assetsUrl = "pack://application:,,,/VideoAndQuestion;component/assets/";
        const int numPanels = 4;
        string[] panelImagePaths = { "1.png", "2.png", "3.png", "4.png" };
        Border[] arrItems = new Border[numPanels];

        public MainWindow() {
            InitializeComponent();            
        }

        private void adjustPanelSizes() {
            double margin = ((double)this.ActualWidth / numPanels);
            int index = 0;

            foreach (Border item in arrItems) {
                if (item.Child != null) {
                    UserControls.Panel panel = (UserControls.Panel)item.Child;
                    item.Margin = new Thickness(index * margin, 0, 0, 0);
                    panel.setImage(assetsUrl + panelImagePaths[index]);
                    panel.Width = this.ActualWidth;
                    panel.panelSelected += new EventHandler(panel_panelSelected);
                    index++;
                }
            }
        }

        void panel_panelSelected(object sender, EventArgs e) {
            hideMenuContainer();
        }

        void hideMenuContainer() {
            int index = 0;
            foreach (Border item in arrItems) {
                DoubleAnimation animSlideOut = new DoubleAnimation();
                animSlideOut.From = 0;
                animSlideOut.To = item.ActualWidth + (40);
                animSlideOut.FillBehavior = FillBehavior.HoldEnd;
                animSlideOut.EasingFunction = new CubicEase();
                animSlideOut.Duration = TimeSpan.FromMilliseconds(800);
                TranslateTransform tt = new TranslateTransform();
                item.RenderTransform = tt;
                tt.BeginAnimation(TranslateTransform.XProperty, animSlideOut);
                index++;
            }
        }

        void showMenuContainer() {
            int index = 0;
            foreach (Border item in arrItems) {
                DoubleAnimation animSlideIn = new DoubleAnimation();
                animSlideIn.From = item.ActualWidth + 40;
                animSlideIn.To = 0;
                animSlideIn.FillBehavior = FillBehavior.HoldEnd;
                animSlideIn.EasingFunction = new CubicEase();
                animSlideIn.Duration = TimeSpan.FromMilliseconds(800);
                TranslateTransform tt = new TranslateTransform();
                item.RenderTransform = tt;
                tt.BeginAnimation(TranslateTransform.XProperty, animSlideIn);
                index++;
            }
        }
                
        private void Window_Loaded(object sender, RoutedEventArgs e) {            
            arrItems[0] = panel1;
            arrItems[1] = panel2;
            arrItems[2] = panel3;
            arrItems[3] = panel4;
            adjustPanelSizes();
            this.SizeChanged += new SizeChangedEventHandler(MainWindow_SizeChanged);
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            adjustPanelSizes();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            showMenuContainer();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Escape: this.Close(); break;
            }
        }
    }
}
