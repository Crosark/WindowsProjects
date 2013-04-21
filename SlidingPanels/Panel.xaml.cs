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

namespace UserControls
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : UserControl
    {
        const int animMoverDist = 50;
        string pathToImage = "";
        SolidColorBrush background;
        const double ratioSizesImageControlX = 0.3;

        public event EventHandler panelSelected;

        public Panel() {
            InitializeComponent();
        }
              
        public void setImage(string path) {
            pathToImage = path;
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(pathToImage);
            img.EndInit();
            imgCharacter.Source = img;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {            
            Background = new SolidColorBrush(Colors.LawnGreen);
            DoubleAnimation animMover = new DoubleAnimation();
            animMover.Duration = TimeSpan.FromMilliseconds(300);
            animMover.From = 0; animMover.To = -(animMoverDist);
            TranslateTransform tt = new TranslateTransform();
            imgCharacter.RenderTransform = tt;
            tt.BeginAnimation(TranslateTransform.XProperty, animMover);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            background = (SolidColorBrush)this.Background;
            this.MouseUp += new MouseButtonEventHandler(Panel_MouseUp);
        }

        void Panel_MouseUp(object sender, MouseButtonEventArgs e) {
            this.blinkBackground();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            Background = background;
            DoubleAnimation animMover = new DoubleAnimation();
            animMover.Duration = TimeSpan.FromMilliseconds(50);
            animMover.To = 0; animMover.From = -(animMoverDist);
            TranslateTransform tt = new TranslateTransform();
            imgCharacter.RenderTransform = tt;
            tt.BeginAnimation(TranslateTransform.XProperty, animMover);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            imgCharacter.Width = this.Width * ratioSizesImageControlX;
            imgCharacter.Height = imgCharacter.Width;
        }

        private void blinkBackground() {
            ColorAnimation colorAnim = new ColorAnimation();
            colorAnim.From = Colors.LawnGreen;
            colorAnim.To = Colors.White;
            colorAnim.AccelerationRatio = 0.6;
            colorAnim.RepeatBehavior = new RepeatBehavior(4);
            colorAnim.Duration = TimeSpan.FromMilliseconds(200);
            colorAnim.Completed += new EventHandler(colorAnim_Completed);
            SolidColorBrush colorBrush = new SolidColorBrush(Colors.LawnGreen);
            this.Background = colorBrush;
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnim);
        }

        void colorAnim_Completed(object sender, EventArgs e) {
            panelSelected(this, new EventArgs());
            this.Background = background;
        }
    }
}
