namespace WFWizard
{
    partial class SelectWorkFlow
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
            this.dllwfList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dllwfList
            // 
            this.dllwfList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dllwfList.FormattingEnabled = true;
            this.dllwfList.Location = new System.Drawing.Point(76, 71);
            this.dllwfList.Name = "dllwfList";
            this.dllwfList.Size = new System.Drawing.Size(204, 20);
            this.dllwfList.TabIndex = 0;
            this.dllwfList.SelectedIndexChanged += new System.EventHandler(this.dllwfList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择流程:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(309, 31);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(99, 60);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "确定";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(76, 31);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(204, 21);
            this.tbSearch.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "关键字:";
            // 
            // clbItems
            // 
            this.clbItems.FormattingEnabled = true;
            this.clbItems.Location = new System.Drawing.Point(18, 138);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(283, 228);
            this.clbItems.TabIndex = 4;
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(330, 138);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(78, 52);
            this.btnUp.TabIndex = 5;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(330, 314);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(78, 52);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // SelectWorkFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 378);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.clbItems);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dllwfList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectWorkFlow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectWorkFlow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox dllwfList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbItems;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label3;
    }
}