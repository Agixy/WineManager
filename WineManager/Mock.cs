using System.Drawing;
using System.Windows.Forms;

namespace View
{
    public partial class Mock : Form
    {
        public Mock()
        {
            InitializeComponent();

        }

        private void Mock_Load(object sender, System.EventArgs e)
        {
            int ekranX = 10;
            int ekranY = 10;

            int guzikSzer = 2;
            int guzikWys = 2;

            int guzikPozX = 2;
            int guzikPozY = 0;

            var button = new Button();
            button.Width = panel2.Width / ekranX * guzikSzer;
            button.Height = panel2.Height / ekranY * guzikWys;
            button.Location = new System.Drawing.Point(panel2.Width / ekranX * guzikPozX, panel2.Height / ekranY * guzikPozY);
            button.Text = "CZERWONE";
            button.BackColor = Color.FromArgb(-65536);
            button.ForeColor = Color.FromArgb(-16777216);
            button.Font = new Font("Tahoma", 12);
            button.Click += Button_Click;
            button.Tag = 520004;

            panel2.Controls.Add(button);

        


        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            LoadEkran((int)button.Tag);
        }

        private void LoadEkran(int id)
        {

        }
    }
}
