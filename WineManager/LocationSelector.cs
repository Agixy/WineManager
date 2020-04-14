using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WineManagerService.Interfaces;

namespace View
{
    public partial class LocationSelector : Form
    {
        private readonly List<string> locationNames;

        public string ReturnValue { get; set; }
        private ILocationsDao Service;
        private readonly bool allowFulLocation;

        public LocationSelector(List<string> locationNames, string wineName, ILocationsDao service, bool searcher = false)
        {
            InitializeComponent();
            this.locationNames = locationNames;
            Service = service;
            this.allowFulLocation = searcher;
            if (searcher)
            {
                btnAdd.Text = "Szukaj";
            }
            lblWineToAdd.Text = wineName;
            LoadButtons();
        }

        private void LoadButtons()
        {
            flowLayoutPanel1.Controls.Clear();
            var chars = GetAvalibleChars();
            if (chars.Count() == 0)
            {
                btnAdd.Enabled = true;
                return;
            }
            foreach(var @char in chars)
            {
                var button = GenerateButton(@char);
                flowLayoutPanel1.Controls.Add(button);
            }
        }             

        private Button GenerateButton(string text)
        {
            var button = new Button();
            button.Text = text;
            button.Click += Button_Click;
            button.Width = 70;
            button.Height = 70;
            button.Font = new Font("Tahoma", 30);
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            txtLocationName.AppendText(button.Text);
            LoadButtons();
        }

        private IEnumerable<string> GetAvalibleChars()
        {
            var result = locationNames.Where(n => n.StartsWith(txtLocationName.Text))
                .Where(n => n.Length > txtLocationName.Text.Length)
                .Select(n => n.Substring(txtLocationName.Text.Length, 1))
                .Distinct();
            return result;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            lblLocationFull.Visible = false;
            if (txtLocationName.Text.Length > 0)
            {
                txtLocationName.Text = txtLocationName.Text.Substring(0, txtLocationName.Text.Length - 1);
                LoadButtons();
            }
        }

        private void GenerateResult()
        {
            ReturnValue = txtLocationName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancellButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(Service.CheckIsLocationFull(Service.GetLocationByName(txtLocationName.Text).Id) && !allowFulLocation)
            {
                lblLocationFull.Visible = true;
            }
            else
            {
                GenerateResult();
            }           
        }
    }
}
