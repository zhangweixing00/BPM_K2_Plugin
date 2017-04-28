namespace WFWizard
{
    partial class ApprovalSetting
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
            this.dvList = new System.Windows.Forms.DataGridView();
            this.CbSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activites = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnTogether = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dllwfList = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dvList)).BeginInit();
            this.SuspendLayout();
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
            this.dvList.Location = new System.Drawing.Point(12, 130);
            this.dvList.Name = "dvList";
            this.dvList.RowHeadersVisible = false;
            this.dvList.RowTemplate.Height = 23;
            this.dvList.Size = new System.Drawing.Size(604, 315);
            this.dvList.TabIndex = 0;
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
            this.RowName.Width = 150;
            // 
            // activites
            // 
            this.activites.HeaderText = "节点们";
            this.activites.Name = "activites";
            this.activites.ReadOnly = true;
            this.activites.Width = 400;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(656, 131);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 41);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "上移";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(656, 198);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 41);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "下移";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnTogether
            // 
            this.btnTogether.Location = new System.Drawing.Point(656, 268);
            this.btnTogether.Name = "btnTogether";
            this.btnTogether.Size = new System.Drawing.Size(75, 41);
            this.btnTogether.TabIndex = 1;
            this.btnTogether.Text = "合并";
            this.btnTogether.UseVisualStyleBackColor = true;
            this.btnTogether.Click += new System.EventHandler(this.btnTogether_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(656, 339);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 41);
            this.btnSplit.TabIndex = 1;
            this.btnSplit.Text = "分离";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(656, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 41);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "完成";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(95, 30);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(204, 21);
            this.tbSearch.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "关键字:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择流程:";
            // 
            // dllwfList
            // 
            this.dllwfList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dllwfList.FormattingEnabled = true;
            this.dllwfList.Location = new System.Drawing.Point(95, 70);
            this.dllwfList.Name = "dllwfList";
            this.dllwfList.Size = new System.Drawing.Size(204, 20);
            this.dllwfList.TabIndex = 4;
            this.dllwfList.SelectedIndexChanged += new System.EventHandler(this.dllwfList_SelectedIndexChanged);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(541, 33);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 41);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "查看";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(656, 404);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 41);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(342, 68);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(112, 23);
            this.btnDB.TabIndex = 9;
            this.btnDB.Text = "...";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // ApprovalSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 457);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dllwfList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.btnTogether);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.dvList);
            this.Name = "ApprovalSetting";
            this.Text = "流程开发向导";
            this.Load += new System.EventHandler(this.ApprovalSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dvList;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnTogether;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dllwfList;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CbSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowName;
        private System.Windows.Forms.DataGridViewTextBoxColumn activites;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDB;


    }
}