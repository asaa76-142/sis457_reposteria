namespace CpMielva
{
    partial class FrmPrincipal
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c1Ribbon1 = new C1.Win.Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.Ribbon.RibbonApplicationMenu();
            this.ribbonBottomToolBar1 = new C1.Win.Ribbon.RibbonBottomToolBar();
            this.ribbonConfigToolBar1 = new C1.Win.Ribbon.RibbonConfigToolBar();
            this.ribbonQat1 = new C1.Win.Ribbon.RibbonQat();
            this.ribbonTab1 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup1 = new C1.Win.Ribbon.RibbonGroup();
            this.btnCaProductos = new C1.Win.Ribbon.RibbonButton();
            this.ribbonButton2 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonGroup2 = new C1.Win.Ribbon.RibbonGroup();
            this.ribbonTab2 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup3 = new C1.Win.Ribbon.RibbonGroup();
            this.ribbonButton3 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonGroup4 = new C1.Win.Ribbon.RibbonGroup();
            this.ribbonButton4 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab3 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup5 = new C1.Win.Ribbon.RibbonGroup();
            this.ribbonButton5 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonButton6 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonButton7 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTopToolBar1 = new C1.Win.Ribbon.RibbonTopToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Ribbon1
            // 
            this.c1Ribbon1.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.c1Ribbon1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1Ribbon1.BottomToolBarHolder = this.ribbonBottomToolBar1;
            this.c1Ribbon1.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.c1Ribbon1.Location = new System.Drawing.Point(0, 0);
            this.c1Ribbon1.Name = "c1Ribbon1";
            this.c1Ribbon1.QatHolder = this.ribbonQat1;
            this.c1Ribbon1.Size = new System.Drawing.Size(834, 241);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab1);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab2);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab3);
            this.c1Ribbon1.TopToolBarHolder = this.ribbonTopToolBar1;
            // 
            // ribbonApplicationMenu1
            // 
            this.ribbonApplicationMenu1.Name = "ribbonApplicationMenu1";
            // 
            // ribbonBottomToolBar1
            // 
            this.ribbonBottomToolBar1.Name = "ribbonBottomToolBar1";
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Groups.Add(this.ribbonGroup1);
            this.ribbonTab1.Groups.Add(this.ribbonGroup2);
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "Catálogos";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Items.Add(this.btnCaProductos);
            this.ribbonGroup1.Items.Add(this.ribbonButton2);
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "administración de Catálogos";
            // 
            // btnCaProductos
            // 
            this.btnCaProductos.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnCaProductos.IconSet.Add(new C1.Framework.C1BitmapIcon("TDEffects", new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, "Preset_LargeImages", 313));
            this.btnCaProductos.Name = "btnCaProductos";
            this.btnCaProductos.Text = "Productos";
            this.btnCaProductos.Click += new System.EventHandler(this.btnCaProductos_Click);
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton2.IconSet.Add(new C1.Framework.C1BitmapIcon("Users", new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, "Preset_LargeImages", 332));
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.Text = "Clientes";
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "Group";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Groups.Add(this.ribbonGroup3);
            this.ribbonTab2.Groups.Add(this.ribbonGroup4);
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "Compras/Ventas";
            // 
            // ribbonGroup3
            // 
            this.ribbonGroup3.Items.Add(this.ribbonButton3);
            this.ribbonGroup3.Name = "ribbonGroup3";
            this.ribbonGroup3.Text = "Gestión de Compras";
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton3.Name = "ribbonButton3";
            this.ribbonButton3.Text = "Compras";
            // 
            // ribbonGroup4
            // 
            this.ribbonGroup4.Items.Add(this.ribbonButton4);
            this.ribbonGroup4.Name = "ribbonGroup4";
            this.ribbonGroup4.Text = "Gestión de Ventas";
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton4.Name = "ribbonButton4";
            this.ribbonButton4.Text = "Ventas";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Groups.Add(this.ribbonGroup5);
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Text = "Administración";
            // 
            // ribbonGroup5
            // 
            this.ribbonGroup5.Items.Add(this.ribbonButton5);
            this.ribbonGroup5.Items.Add(this.ribbonButton6);
            this.ribbonGroup5.Items.Add(this.ribbonButton7);
            this.ribbonGroup5.Name = "ribbonGroup5";
            this.ribbonGroup5.Text = "Administración de Sistema";
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton5.IconSet.Add(new C1.Framework.C1BitmapIcon("Users", new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, "Preset_LargeImages", 332));
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.Text = "Empleados y Usuarios";
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton6.Name = "ribbonButton6";
            this.ribbonButton6.Text = "Roles";
            // 
            // ribbonButton7
            // 
            this.ribbonButton7.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton7.Name = "ribbonButton7";
            this.ribbonButton7.Text = "Permisos";
            // 
            // ribbonTopToolBar1
            // 
            this.ribbonTopToolBar1.Name = "ribbonTopToolBar1";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(834, 554);
            this.Controls.Add(this.c1Ribbon1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...::: Mielva - Menú Principal :::...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.Ribbon.C1Ribbon c1Ribbon1;
        private C1.Win.Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.Ribbon.RibbonBottomToolBar ribbonBottomToolBar1;
        private C1.Win.Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.Ribbon.RibbonQat ribbonQat1;
        private C1.Win.Ribbon.RibbonTab ribbonTab1;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup1;
        private C1.Win.Ribbon.RibbonTopToolBar ribbonTopToolBar1;
        private C1.Win.Ribbon.RibbonButton btnCaProductos;
        private C1.Win.Ribbon.RibbonButton ribbonButton2;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup2;
        private C1.Win.Ribbon.RibbonTab ribbonTab2;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup3;
        private C1.Win.Ribbon.RibbonButton ribbonButton3;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup4;
        private C1.Win.Ribbon.RibbonButton ribbonButton4;
        private C1.Win.Ribbon.RibbonTab ribbonTab3;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup5;
        private C1.Win.Ribbon.RibbonButton ribbonButton5;
        private C1.Win.Ribbon.RibbonButton ribbonButton6;
        private C1.Win.Ribbon.RibbonButton ribbonButton7;
    }
}