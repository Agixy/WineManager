namespace View
{
    partial class WineLocation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lblWineNumber = new System.Windows.Forms.Label();
            this.lblWineCategry = new System.Windows.Forms.Label();
            this.btnAddWine = new System.Windows.Forms.Button();
            this.lblCheckedLocation = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGetWine = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWineName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.flowLayoutPanel1);
            this.mainPanel.Controls.Add(this.panel2);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Controls.Add(this.lblCheckedLocation);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(564, 392);
            this.mainPanel.TabIndex = 1;
            // 
            // lblWineNumber
            // 
            this.lblWineNumber.AutoSize = true;
            this.lblWineNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWineNumber.Location = new System.Drawing.Point(18, 64);
            this.lblWineNumber.Name = "lblWineNumber";
            this.lblWineNumber.Size = new System.Drawing.Size(103, 20);
            this.lblWineNumber.TabIndex = 8;
            this.lblWineNumber.Text = "Wine number";
            // 
            // lblWineCategry
            // 
            this.lblWineCategry.AutoSize = true;
            this.lblWineCategry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWineCategry.Location = new System.Drawing.Point(18, 40);
            this.lblWineCategry.Name = "lblWineCategry";
            this.lblWineCategry.Size = new System.Drawing.Size(110, 20);
            this.lblWineCategry.TabIndex = 7;
            this.lblWineCategry.Text = "Wine category";
            // 
            // btnAddWine
            // 
            this.btnAddWine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAddWine.Location = new System.Drawing.Point(3, 178);
            this.btnAddWine.Name = "btnAddWine";
            this.btnAddWine.Size = new System.Drawing.Size(115, 69);
            this.btnAddWine.TabIndex = 6;
            this.btnAddWine.Text = "Dodaj";
            this.btnAddWine.UseVisualStyleBackColor = true;
            this.btnAddWine.Click += new System.EventHandler(this.BtnAddWine_Click);
            // 
            // lblCheckedLocation
            // 
            this.lblCheckedLocation.AutoSize = true;
            this.lblCheckedLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCheckedLocation.Location = new System.Drawing.Point(383, 74);
            this.lblCheckedLocation.Name = "lblCheckedLocation";
            this.lblCheckedLocation.Size = new System.Drawing.Size(0, 18);
            this.lblCheckedLocation.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(123, 105);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(441, 287);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnGetWine
            // 
            this.btnGetWine.Enabled = false;
            this.btnGetWine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnGetWine.Location = new System.Drawing.Point(3, 6);
            this.btnGetWine.Name = "btnGetWine";
            this.btnGetWine.Size = new System.Drawing.Size(113, 69);
            this.btnGetWine.TabIndex = 3;
            this.btnGetWine.Text = "Pobierz";
            this.btnGetWine.UseVisualStyleBackColor = true;
            this.btnGetWine.Click += new System.EventHandler(this.BtnGetWine_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(14, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dostępne lokalizacje:";
            // 
            // lblWineName
            // 
            this.lblWineName.AutoSize = true;
            this.lblWineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblWineName.Location = new System.Drawing.Point(13, 11);
            this.lblWineName.Name = "lblWineName";
            this.lblWineName.Size = new System.Drawing.Size(148, 29);
            this.lblWineName.TabIndex = 0;
            this.lblWineName.Text = "Wine Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblWineName);
            this.panel1.Controls.Add(this.lblWineNumber);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblWineCategry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 105);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGetWine);
            this.panel2.Controls.Add(this.btnAddWine);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(123, 287);
            this.panel2.TabIndex = 10;
            // 
            // WineLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "WineLocation";
            this.Size = new System.Drawing.Size(564, 392);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnGetWine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWineName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblCheckedLocation;
        private System.Windows.Forms.Button btnAddWine;
        private System.Windows.Forms.Label lblWineCategry;
        private System.Windows.Forms.Label lblWineNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
