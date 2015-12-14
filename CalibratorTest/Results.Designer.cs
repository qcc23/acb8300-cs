namespace FakeColourFinder
{
    partial class Results
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._resultsGrid = new System.Windows.Forms.DataGridView();
            this._colGray = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colBrightness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._layout = new System.Windows.Forms.TableLayoutPanel();
            this._btnClose = new System.Windows.Forms.Button();
            this._resultList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._resultsGrid)).BeginInit();
            this._layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _resultsGrid
            // 
            this._resultsGrid.AllowUserToAddRows = false;
            this._resultsGrid.AllowUserToDeleteRows = false;
            this._resultsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._resultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._resultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colGray,
            this._colBrightness,
            this._colR,
            this._colG,
            this._colB});
            this._layout.SetColumnSpan(this._resultsGrid, 2);
            this._resultsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultsGrid.Location = new System.Drawing.Point(3, 3);
            this._resultsGrid.Name = "_resultsGrid";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._resultsGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this._resultsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._resultsGrid.ShowEditingIcon = false;
            this._resultsGrid.Size = new System.Drawing.Size(580, 318);
            this._resultsGrid.TabIndex = 0;
            this._resultsGrid.SelectionChanged += new System.EventHandler(this._resultsGrid_SelectionChanged);
            // 
            // _colGray
            // 
            this._colGray.HeaderText = "Gray Level";
            this._colGray.Name = "_colGray";
            this._colGray.ReadOnly = true;
            // 
            // _colBrightness
            // 
            this._colBrightness.HeaderText = "Brightness";
            this._colBrightness.Name = "_colBrightness";
            this._colBrightness.ReadOnly = true;
            // 
            // _colR
            // 
            this._colR.HeaderText = "Red";
            this._colR.Name = "_colR";
            this._colR.ReadOnly = true;
            // 
            // _colG
            // 
            this._colG.HeaderText = "Green";
            this._colG.Name = "_colG";
            this._colG.ReadOnly = true;
            // 
            // _colB
            // 
            this._colB.HeaderText = "Blue";
            this._colB.Name = "_colB";
            this._colB.ReadOnly = true;
            // 
            // _layout
            // 
            this._layout.ColumnCount = 2;
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._layout.Controls.Add(this._resultsGrid, 0, 0);
            this._layout.Controls.Add(this._btnClose, 1, 1);
            this._layout.Controls.Add(this._resultList, 0, 1);
            this._layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._layout.Location = new System.Drawing.Point(0, 0);
            this._layout.Name = "_layout";
            this._layout.RowCount = 2;
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layout.Size = new System.Drawing.Size(586, 351);
            this._layout.TabIndex = 1;
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnClose.Location = new System.Drawing.Point(508, 327);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(75, 21);
            this._btnClose.TabIndex = 1;
            this._btnClose.Text = "Done";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // _resultList
            // 
            this._resultList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._resultList.FormattingEnabled = true;
            this._resultList.Location = new System.Drawing.Point(3, 327);
            this._resultList.Name = "_resultList";
            this._resultList.Size = new System.Drawing.Size(499, 21);
            this._resultList.TabIndex = 2;
            this._resultList.SelectedValueChanged += new System.EventHandler(this._resultList_SelectedValueChanged);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 351);
            this.Controls.Add(this._layout);
            this.Name = "Results";
            this.Text = "Results";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Results_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._resultsGrid)).EndInit();
            this._layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _resultsGrid;
        private System.Windows.Forms.TableLayoutPanel _layout;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.ComboBox _resultList;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colGray;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colBrightness;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colR;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colG;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colB;
    }
}