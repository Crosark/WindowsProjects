using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace UserControls
{
    public partial class ChatHead : UserControl
    {
        //Create a property called 'Radius'
        public int Radius { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public ChatHead(){
            InitializeComponent();
        }

        /// <summary>
        /// This creates the circular clip around this control
        /// </summary>
        private void initAppearance() {
            
            //decide the center of the circular region
            double centerX = this.Height / 2;
            double centerY = this.Width / 2;
            
            //create the clip geometry
            EllipseGeometry geom = new EllipseGeometry();
            geom.RadiusY = Radius;
            geom.RadiusX = Radius;
            geom.Center = new Point(centerX, centerY);
            
            //apply the clip to this control
            this.Clip = geom;
        }

        /// <summary>
        /// Invoked when the control loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded_1(object sender, RoutedEventArgs e) {
            initAppearance();
        }
    }
}
