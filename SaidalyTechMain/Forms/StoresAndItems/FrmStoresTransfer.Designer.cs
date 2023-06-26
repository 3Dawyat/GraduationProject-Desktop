using DevExpress.XtraReports;

namespace SaidalyTechMain.Forms.StoresAndItems
{
    partial class FrmStoresTransfer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoresTransfer));
            this.lpeFrom = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textDate = new DevExpress.XtraEditors.DateEdit();
            this.textNote = new System.Windows.Forms.TextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lpeTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.GcData = new DevExpress.XtraGrid.GridControl();
            this.dtStoreTransferBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemUnitId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnInsert = new DevExpress.XtraEditors.SimpleButton();
            this.lpeItems = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Avilable2 = new DevExpress.XtraEditors.LabelControl();
            this.Avilable1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lpeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStoreTransferBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // lpeFrom
            // 
            this.lpeFrom.Location = new System.Drawing.Point(374, 65);
            this.lpeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.lpeFrom.Name = "lpeFrom";
            this.lpeFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpeFrom.Properties.Appearance.Options.UseFont = true;
            this.lpeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpeFrom.Properties.PopupView = this.gridView1;
            this.lpeFrom.Size = new System.Drawing.Size(215, 24);
            this.lpeFrom.TabIndex = 13;
            this.lpeFrom.EditValueChanged += new System.EventHandler(this.lpeFrom_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 431;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(630, 69);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 18);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "من";
            // 
            // textDate
            // 
            this.textDate.EditValue = null;
            this.textDate.Location = new System.Drawing.Point(34, 28);
            this.textDate.Margin = new System.Windows.Forms.Padding(4);
            this.textDate.Name = "textDate";
            this.textDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDate.Properties.Appearance.Options.UseFont = true;
            this.textDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textDate.Properties.CalendarTimeProperties.MaskSettings.Set("mask", "g");
            this.textDate.Size = new System.Drawing.Size(215, 24);
            this.textDate.TabIndex = 11;
            // 
            // textNote
            // 
            this.textNote.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNote.Location = new System.Drawing.Point(374, 186);
            this.textNote.Margin = new System.Windows.Forms.Padding(4);
            this.textNote.Multiline = true;
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(214, 158);
            this.textNote.TabIndex = 10;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(596, 190);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 18);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "ملاحظات";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(280, 32);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 18);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "التاريخ";
            // 
            // lpeTo
            // 
            this.lpeTo.Location = new System.Drawing.Point(374, 102);
            this.lpeTo.Margin = new System.Windows.Forms.Padding(4);
            this.lpeTo.Name = "lpeTo";
            this.lpeTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpeTo.Properties.Appearance.Options.UseFont = true;
            this.lpeTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpeTo.Properties.PopupView = this.gridView2;
            this.lpeTo.Size = new System.Drawing.Size(215, 24);
            this.lpeTo.TabIndex = 15;
            this.lpeTo.EditValueChanged += new System.EventHandler(this.lpeTo_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 431;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(628, 106);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 18);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "الي";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textQty);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.GcData);
            this.groupBox1.Controls.Add(this.btnInsert);
            this.groupBox1.Controls.Add(this.lpeItems);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.Avilable2);
            this.groupBox1.Controls.Add(this.Avilable1);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.lpeTo);
            this.groupBox1.Controls.Add(this.lpeFrom);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.textDate);
            this.groupBox1.Controls.Add(this.textNote);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(687, 405);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تحويل بين المخازن";
            // 
            // textQty
            // 
            this.textQty.Location = new System.Drawing.Point(374, 146);
            this.textQty.Margin = new System.Windows.Forms.Padding(4);
            this.textQty.Name = "textQty";
            this.textQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.textQty.Properties.Appearance.Options.UseFont = true;
            this.textQty.Size = new System.Drawing.Size(215, 24);
            this.textQty.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(616, 150);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 18);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "الكميه";
            // 
            // GcData
            // 
            this.GcData.DataSource = this.dtStoreTransferBindingSource;
            this.GcData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.GcData.Location = new System.Drawing.Point(7, 150);
            this.GcData.MainView = this.GvData;
            this.GcData.Margin = new System.Windows.Forms.Padding(4);
            this.GcData.Name = "GcData";
            this.GcData.Size = new System.Drawing.Size(341, 247);
            this.GcData.TabIndex = 22;
            this.GcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvData});
            // 
            // dtStoreTransferBindingSource
            // 
            this.dtStoreTransferBindingSource.DataMember = "DtStoreTransfer";
            // 
            // GvData
            // 
            this.GvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemUnitId,
            this.colItemName,
            this.colQty});
            this.GvData.DetailHeight = 431;
            this.GvData.GridControl = this.GcData;
            this.GvData.Name = "GvData";
            this.GvData.OptionsView.ShowGroupPanel = false;
            // 
            // colItemUnitId
            // 
            this.colItemUnitId.Caption = "Id";
            this.colItemUnitId.FieldName = "ItemUnitId";
            this.colItemUnitId.MinWidth = 23;
            this.colItemUnitId.Name = "colItemUnitId";
            this.colItemUnitId.Visible = true;
            this.colItemUnitId.VisibleIndex = 0;
            this.colItemUnitId.Width = 87;
            // 
            // colItemName
            // 
            this.colItemName.Caption = "الاسم";
            this.colItemName.FieldName = "ItemName";
            this.colItemName.MinWidth = 23;
            this.colItemName.Name = "colItemName";
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 1;
            this.colItemName.Width = 87;
            // 
            // colQty
            // 
            this.colQty.Caption = "الكميه";
            this.colQty.FieldName = "Qty";
            this.colQty.MinWidth = 23;
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 2;
            this.colQty.Width = 87;
            // 
            // btnInsert
            // 
            this.btnInsert.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnInsert.Appearance.Options.UseFont = true;
            this.btnInsert.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnInsert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnInsert.ImageOptions.SvgImage")));
            this.btnInsert.Location = new System.Drawing.Point(374, 352);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(215, 46);
            this.btnInsert.TabIndex = 10;
            this.btnInsert.Text = "ادراج";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // lpeItems
            // 
            this.lpeItems.Location = new System.Drawing.Point(374, 28);
            this.lpeItems.Margin = new System.Windows.Forms.Padding(4);
            this.lpeItems.Name = "lpeItems";
            this.lpeItems.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpeItems.Properties.Appearance.Options.UseFont = true;
            this.lpeItems.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpeItems.Properties.PopupView = this.gridView3;
            this.lpeItems.Size = new System.Drawing.Size(215, 24);
            this.lpeItems.TabIndex = 21;
            this.lpeItems.EditValueChanged += new System.EventHandler(this.lpeItems_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.DetailHeight = 431;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(616, 32);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(46, 18);
            this.labelControl8.TabIndex = 20;
            this.labelControl8.Text = "الصنف";
            // 
            // Avilable2
            // 
            this.Avilable2.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Avilable2.Appearance.ForeColor = System.Drawing.Color.IndianRed;
            this.Avilable2.Appearance.Options.UseFont = true;
            this.Avilable2.Appearance.Options.UseForeColor = true;
            this.Avilable2.Location = new System.Drawing.Point(130, 106);
            this.Avilable2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Avilable2.Name = "Avilable2";
            this.Avilable2.Size = new System.Drawing.Size(0, 18);
            this.Avilable2.TabIndex = 19;
            // 
            // Avilable1
            // 
            this.Avilable1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Avilable1.Appearance.ForeColor = System.Drawing.Color.IndianRed;
            this.Avilable1.Appearance.Options.UseFont = true;
            this.Avilable1.Appearance.Options.UseForeColor = true;
            this.Avilable1.Location = new System.Drawing.Point(130, 69);
            this.Avilable1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Avilable1.Name = "Avilable1";
            this.Avilable1.Size = new System.Drawing.Size(0, 18);
            this.Avilable1.TabIndex = 18;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(280, 106);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 18);
            this.labelControl7.TabIndex = 17;
            this.labelControl7.Text = "المتاح";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(280, 69);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(40, 18);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "المتاح";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(14, 427);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 46);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmStoresTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 487);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmStoresTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تحويل بين المخازن";
            this.Load += new System.EventHandler(this.FrmStoresTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lpeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStoreTransferBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpeItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit lpeFrom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit textDate;
        private System.Windows.Forms.TextBox textNote;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SearchLookUpEdit lpeTo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl Avilable2;
        private DevExpress.XtraEditors.LabelControl Avilable1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SearchLookUpEdit lpeItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.GridControl GcData;
        private DevExpress.XtraGrid.Views.Grid.GridView GvData;
        private DevExpress.XtraEditors.SimpleButton btnInsert;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.BindingSource dtStoreTransferBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colItemUnitId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.TextEdit textQty;
    }
}