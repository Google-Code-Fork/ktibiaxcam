namespace KTibiaX.Cam.Dialogs {
    partial class frm_Clients {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Clients));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSelect = new DevExpress.XtraEditors.DropDownButton();
            this.menuSelect = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.imgLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lstProcess = new DevExpress.XtraEditors.ImageListBoxControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSelect);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(237, 239);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSelect
            // 
            this.btnSelect.DropDownControl = this.menuSelect;
            this.btnSelect.ImageIndex = 1;
            this.btnSelect.ImageList = this.imgLarge;
            this.btnSelect.Location = new System.Drawing.Point(5, 196);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(115, 39);
            this.btnSelect.StyleController = this.layoutControl1;
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // menuSelect
            // 
            this.menuSelect.BottomPaneControlContainer = null;
            this.menuSelect.ItemLinks.Add(this.btnRefresh);
            this.menuSelect.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesTextDescription;
            this.menuSelect.Name = "menuSelect";
            this.menuSelect.Ribbon = this.ribbonControl1;
            this.menuSelect.RightPaneControlContainer = null;
            this.menuSelect.RightPaneWidth = 240;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Refresh Process List";
            this.btnRefresh.Description = "Refreshs the current process list.";
            this.btnRefresh.Id = 0;
            this.btnRefresh.LargeImageIndex = 2;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonKeyTip = "";
            this.ribbonControl1.ApplicationIcon = null;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.None;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnRefresh});
            this.ribbonControl1.LargeImages = this.imgLarge;
            this.ribbonControl1.Location = new System.Drawing.Point(23, 26);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.SelectedPage = this.ribbonPage1;
            this.ribbonControl1.Size = new System.Drawing.Size(10, 115);
            // 
            // imgLarge
            // 
            this.imgLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imgLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgLarge.ImageStream")));
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.KeyTip = "";
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.KeyTip = "";
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 0;
            this.btnCancel.ImageList = this.imgLarge;
            this.btnCancel.Location = new System.Drawing.Point(127, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 39);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lstProcess);
            this.groupControl1.Location = new System.Drawing.Point(7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(224, 180);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Processess";
            // 
            // lstProcess
            // 
            this.lstProcess.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstProcess.DisplayMember = "ProcessName";
            this.lstProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstProcess.ImageIndexMember = "ImageIndex";
            this.lstProcess.ImageList = this.imgLarge;
            this.lstProcess.Location = new System.Drawing.Point(2, 20);
            this.lstProcess.Name = "lstProcess";
            this.lstProcess.Size = new System.Drawing.Size(220, 158);
            this.lstProcess.TabIndex = 0;
            this.lstProcess.ValueMember = "ProcessHandle";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(237, 239);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(235, 191);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(122, 191);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(91, 33);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(113, 46);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSelect;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 191);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(65, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(122, 46);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frm_Clients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 239);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Clients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tibia Clients";
            this.Load += new System.EventHandler(this.frm_Clients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menuSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ImageListBoxControl lstProcess;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DropDownButton btnSelect;
        private DevExpress.Utils.ImageCollection imgLarge;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu menuSelect;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
    }
}