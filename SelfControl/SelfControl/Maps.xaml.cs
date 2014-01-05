using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Tasks;
using System.Device.Location;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using SelfControl.Resources;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Shapes;



namespace SelfControl
{
    public partial class Maps : PhoneApplicationPage
    {
        IsolatedStorageSettings setting;
        private double _accuracy;
        private GeoCoordinate MyCoordinate;
        private GeocodeQuery MyGeocodeQuery;

        private PointOfInterest[] locations = {
                                                  new PointOfInterest("4275 Arville St, Las Vegas, NV 89103","Auntee M's Market", 4, "$58",new GeoCoordinate(36.114646,-115.172816)),
                                                  new PointOfInterest("4155 Spring Mountain Rd, Las Vegas, NV 89102", "99 Ranch Market", 5, "$62", new GeoCoordinate(36.126398,-115.195034)),
                                                  new PointOfInterest("4500 Delancey, Las Vegas, NV 89103", "Hesco Food Intl", 3, "$65", new GeoCoordinate(36.112602,-115.202036))
                                              };

        public Maps()
        {
            InitializeComponent();
            setting = IsolatedStorageSettings.ApplicationSettings;
            this.DataContext = locations;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
              //  Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
              //  _accuracy = currentPosition.Coordinate.Accuracy;

                Dispatcher.BeginInvoke(() =>
                {
                    MyCoordinate = new GeoCoordinate(36.115381, -115.198145); //hard coded the location of Palms casino in Vegas (venue)
                    MyMap.Layers.Clear();

                    //On one layer draw store locations
                    MapLayer layer = new MapLayer();
                    foreach (PointOfInterest p in locations)
                    {   
                        DrawMapMarker(p.coord, Colors.Red, layer);
                    }

                    MyMap.Layers.Add(layer);

                    //On next layer draw my location
                    MapLayer currLocLayer = new MapLayer();
                    DrawMapMarker(MyCoordinate, Colors.Blue, currLocLayer);
                    MyMap.Layers.Add(currLocLayer);

                    //Animate into view
                    MyMap.SetView(MyCoordinate, 13, MapAnimationKind.Linear);
                    
                });
            }
            catch (Exception)
            {
                Debug.WriteLine("Problem setting current position in Map");
            }
        }

        private void DrawMapMarker(GeoCoordinate coordinate, Color color, MapLayer mapLayer)
        {
            // Create a map marker
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(0, 75));
            polygon.Points.Add(new Point(25, 0));
            polygon.Fill = new SolidColorBrush(color);

            // Enable marker to be tapped for location information
            polygon.Tag = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            polygon.Tap += polygon_Tap;
           
            // Create a MapOverlay and add marker.
            MapOverlay overlay = new MapOverlay();
            overlay.Content = polygon;
            overlay.GeoCoordinate = new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
            overlay.PositionOrigin = new Point(0.0, 1.0);
            mapLayer.Add(overlay);
        }

        void polygon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Polygon pressed = sender as Polygon;
            GeoCoordinate coord = pressed.Tag as GeoCoordinate;
            MyMap.SetView(coord, 15, MapAnimationKind.Linear);
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "1bd07e5c-1845-4207-8c2d-68ef2276e8a7";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "FlGmARkhMehqJM40VN4xMQ";
        }

        private void MyMap_ZoomLevelChanged(object sender, Microsoft.Phone.Maps.Controls.MapZoomLevelChangedEventArgs e)
        {

        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Panel panelSelected = sender as Panel;
            PointOfInterest p = panelSelected.DataContext as PointOfInterest;
            MyMap.SetView(p.coord, 15, MapAnimationKind.Linear);
        }
    }
}