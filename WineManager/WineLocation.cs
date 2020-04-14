using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model;
using WineManagerService;
using WineManagerService.Interfaces;

namespace View
{
    public partial class WineLocation : UserControl
    {
        private readonly WineDto wine;
        private readonly IList<Location> avaliabeLocations;
        private readonly IList<Location> allLocations;
        private (int X, int Y) screenSize;
        private Location checkedLocation;
        private ILocationsDao service;

        public ILocationsDao Service { get; }

        public WineLocation(WineDto wineDto, IList<Location> locations, (int, int) screenSize, ILocationsDao service)
        {
            this.screenSize = screenSize;
            this.service = service;
            wine = wineDto;
            this.avaliabeLocations = locations;
            allLocations = service.GetAllLocations();
            InitializeComponent();
            lblWineName.Text = wine.Name;
            lblWineCategry.Text = wine.CategoryName;
            lblWineNumber.Text = wine.Nr;

            AddLocationsToPanel();         

        }

        private void AddLocationsToPanel()
        {                  
            if(avaliabeLocations.Count == 0)
            {
                ErrorScreen("Brak dostępnych lokalizacji");
            }
            else
            {
                foreach (var location in avaliabeLocations)
                {
                    AddLocationButton(location);
                }
            }
        }

        private void AddLocationButton(Location location)
        {
            Button button = new Button();

            button.Click += delegate { SetLocationToGetWine(button); };

            button.Text = $"{location.Name}";
            button.Width = mainPanel.Width / screenSize.X * 2;
            button.Height = mainPanel.Height / screenSize.Y * 2;
            button.Font = new Font("Tahoma", 30);
            button.Tag = location;

            flowLayoutPanel1.Controls.Add(button);
        }

        private void SetLocationToGetWine(object sender)
        {
            btnGetWine.Enabled = true;
            var button = (Button)sender;
            checkedLocation = (Location)button.Tag;
            lblCheckedLocation.Text = $"Wybrana lokalizacja: {checkedLocation.Name}";
        }

        private void ErrorScreen(string message)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(new Label() { Text = message });
        }

        private void BtnGetWine_Click(object sender, EventArgs e)
        {
        //    checkedLocation = new Location() { Id = 1 };
        //    service.GetWineFromLocation(checkedLocation, 222);
            service.GetWineFromLocation(checkedLocation, wine.Id);
            btnGetWine.Enabled = false;
            OnWineAddedToLocation(new EventArgs());
        }

        private void BtnAddWine_Click(object sender, EventArgs e)
        {
            LocationSelector locationSelector = new LocationSelector(allLocations.Select(l => l.Name).ToList(), wine.Name, service);

            if (locationSelector.ShowDialog() == DialogResult.OK)
            {
                var selectedLocationName = locationSelector.ReturnValue;
                var selectedLocation = allLocations.First(l => l.Name.Equals(selectedLocationName));
               // bool isLocationFull = service.CheckIsLocationFull(selectedLocation.Id);

                    service.AddWineToLocation(selectedLocation.Id, wine.Id);
                    OnWineAddedToLocation(new EventArgs());
                              
            }
        }

        protected virtual void OnWineAddedToLocation(EventArgs e)
        {
            WineAddedToLocation?.Invoke(this, e);
        }

        public event EventHandler WineAddedToLocation;
    }
}
