namespace WFWizard
{
    partial class DBFormSelect
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
            this.btnYes = new System.Windows.Forms.Button();
            this.dvList = new System.Windows.Forms.DataGridView();
            this.CbSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activites = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(443, 217);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 65);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // dvList
            // 
            this.dvList.AllowUserToAddRows = false;
            this.dvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CbSelect,
            this.RowName,
            this.activites});
            this.dvList.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dvList.Location = new System.Drawing.Point(12, 12);
            this.dvList.Name = "dvList";
            this.dvList.RowHeadersVisible = false;
            this.dvList.RowTemplate.Height = 23;
            this.dvList.Size = new System.Drawing.Size(412, 270);
            this.dvList.TabIndex = 2;
            // 
            // CbSelect
            // 
            this.CbSelect.HeaderText = "";
            this.CbSelect.Name = "CbSelect";
            this.CbSelect.Width = 50;
            // 
            // RowName
            // 
            this.RowName.HeaderText = "显示名称";
            this.RowName.Name = "RowName";
            this.RowName.Width = 200;
            // 
            // activites
            // 
            this.activites.HeaderText = "对应字段名";
            this.activites.Name = "activites";
            this.activites.ReadOnly = true;
            this.activites.Width = 150;
            // 
            // DBFormSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 294);
            this.Controls.Add(this.dvList);
            this.Controls.Add(this.btnYes);
            this.Name = "DBFormSelect";
            this.Text = "选择生成FORM的字段";
            ((System.ComponentModel.ISupportInitialize)(this.dvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.DataGridView dvList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CbSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowName;
        private System.Windows.Forms.DataGridViewTextBoxColumn activites;
    }
}