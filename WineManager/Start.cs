using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WineManagerService;

namespace View
{
    public partial class Start : Form
    {
        private WineDao service { get; set; }
        private LocationsDao locationsService { get; set; }
        private const int homeViewId = 5200003;
        private (int X, int Y) screenSize;
        private IList<WineTypeButtonDto> allButtons { get; set; }
        private WineDto checkedWine;

        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            try
            {
            service = new WineDao(homeViewId);
            locationsService = new LocationsDao();

            allButtons = TryCatchWraper.TrySql(() => service.GetAllButtons());
            if (allButtons == default)
                return;

            screenSize = TryCatchWraper.TrySql(() => service.GetScreenSize());
            if (screenSize == default)
                return;

            LoadView(homeViewId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Brak połączenia z bazą danych");
            }
        }

        private void LoadView(int screenId)
        {
            dataPanel.Controls.Clear();
            if (allButtons == null)
               return;
            var buttons = allButtons.Where(b => b.ParentButtonId == screenId);          

            foreach (var button in buttons)
            {
                AddTypeButton(button);
            }
        }

        private void LoadArticlesView(int categoryId)
        {
            try
            {
                dataPanel.Controls.Clear();

                var articles = TryCatchWraper.TrySql(() => service.GetArticlesByCategoryId(categoryId));
                if (articles == default)
                    return;

                for (int i = 0; i < articles.Count; i++)
                {
                    var button = articles[i];
                    AddWineButton(button, articles.Count, i);

                }
            }
            catch (Exception e)
            {
                NoDataScreen(e.Message);
            }
        }

        private void NoDataScreen(string message)
        {
            dataPanel.Controls.Clear();
            dataPanel.Controls.Add(new Label() { Text = message, Font = new Font("Tahoma", 30), AutoSize = true }); //TODO: Get font data from database
        }

        private void AddWineButton(WineTypeButtonDto wineTypeButton, int  buttonsCount, int buttonIndex)
        {
            var grid = (int)Math.Sqrt(buttonsCount-1)+1;
            var columnIndex = buttonIndex % grid;
            var rowIndex = (int) buttonIndex / grid;

            Button button = new Button();
            button.Text = wineTypeButton.Wine.Name;

            button.Click += delegate { LoadWineLocationView(wineTypeButton.Wine); };

            button.Width = dataPanel.Width /grid;
            button.Height = dataPanel.Height / grid;
            button.Location = new Point(dataPanel.Width / grid * columnIndex, dataPanel.Height / grid * rowIndex);
            button.BackColor = Color.FromArgb(wineTypeButton.ColorCode);
            button.Font = new Font("Tahoma", 12);
            button.Tag = wineTypeButton.Id;

            dataPanel.Controls.Add(button);
        }             

        private void AddTypeButton(WineTypeButtonDto wineTypeButton)
        {
            Button button = new Button();

            if (wineTypeButton.ScreenId > 0)
            {
                button.Click += delegate { LoadView(wineTypeButton.ScreenId); };

            }
            else if (wineTypeButton.CategoryId > 0)
            {
                button.Click += delegate { LoadArticlesView(wineTypeButton.CategoryId); };
            }
            else if (wineTypeButton.ArticleId > 0)
            {
                button.Click += delegate { LoadWineLocationView(wineTypeButton.Wine); };
            }

            button.Text = wineTypeButton.Wine.Name;
            button.Width = dataPanel.Width / screenSize.X * wineTypeButton.Width;
            button.Height = dataPanel.Height / screenSize.Y * wineTypeButton.Height;
            button.Location = new Point(dataPanel.Width / screenSize.X * wineTypeButton.PositionX, dataPanel.Height / screenSize.Y * wineTypeButton.PositionY);
            button.BackColor = Color.FromArgb(wineTypeButton.ColorCode);
            button.ForeColor = Color.FromArgb(wineTypeButton.FontColorCode);

            dataPanel.Controls.Add(button);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            checkedWine = null;
            LoadView(homeViewId);
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if(txtWineNumber.Text.Length <= 4)
            {
                txtWineNumber.AppendText(button.Text);
            }           
        }

        private void LoadWineLocationView(WineDto wineDto)
        {
            checkedWine = wineDto;

            if (wineDto.Nr == null)
            {
                wineDto = TryCatchWraper.TrySql(() => service.GetWineById(wineDto.Id));
                if (wineDto == default)
                    return;
            }

            IList<Location> locations = TryCatchWraper.TrySql(() => locationsService.GetLocationsBySkrot(wineDto.Nr));
            if (locations == null)
                return;

            dataPanel.Controls.Clear();
            var wineLocationView = new WineLocation(wineDto, locations, screenSize, locationsService);
            wineLocationView.WineAddedToLocation += WineLocationView_WineAddedToLocation; 
            dataPanel.Controls.Add(wineLocationView);
            wineLocationView.Dock = DockStyle.Fill;
        }

        private void WineLocationView_WineAddedToLocation(object sender, EventArgs e)
        {
            LoadWineLocationView(checkedWine);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var wineSkrot = txtWineNumber.Text;

            var wine = TryCatchWraper.TrySql(() => service.GetWineBySkrot(wineSkrot));
            if (wine == default)
                return;

            if(wine != null)
            {
                checkedWine = wine;

                dataPanel.Controls.Clear();
                txtWineNumber.Text = String.Empty;

                LoadWineLocationView(wine);
            }
            else
            {
                NoDataScreen("Nie znaleziono wina.");
            }
                  
        }

        private void BtnClean_Click(object sender, EventArgs e)
        {
            txtWineNumber.Text = String.Empty;
        }

        private void ByPlaceButton_Click(object sender, EventArgs e)
        {   
            var allLocations = TryCatchWraper.TrySql(() => locationsService.GetAllLocations());
            if (allLocations == null)
                return;
            LocationSelector locationSelector = new LocationSelector(allLocations.Select(l => l.Name).ToList(), "", locationsService, searcher: true);

            if (locationSelector.ShowDialog() == DialogResult.OK)
            {
                var wines = TryCatchWraper.TrySql(() => locationsService.GetWinesInLocation(locationSelector.ReturnValue));
                if (wines == null)
                    return;

                if (wines.Count() > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, wines));
                }
                else
                {
                    MessageBox.Show("Miejsce puste");
                }
            }
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            try
            {
                service = new WineDao(homeViewId);
                locationsService = new LocationsDao();
                allButtons = service.GetAllButtons();
                screenSize = service.GetScreenSize();
                LoadView(homeViewId);

            }
            catch
            {
                MessageBox.Show("Brak połączenia z bazą danych");

            }
        }
    }
}
