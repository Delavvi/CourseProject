namespace CourseProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSearch = new Button();
            btnAddDish = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            txtNewDish = new TextBox();
            txtDelete = new TextBox();
            lblSearchResult = new Label();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(835, 39);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(76, 30);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "search";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnAddDish
            // 
            btnAddDish.Location = new Point(835, 244);
            btnAddDish.Name = "btnAddDish";
            btnAddDish.Size = new Size(94, 29);
            btnAddDish.TabIndex = 1;
            btnAddDish.Text = "add";
            btnAddDish.UseVisualStyleBackColor = true;
            btnAddDish.Click += btnAddDish_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(835, 289);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(680, 41);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(125, 27);
            txtSearch.TabIndex = 6;
            // 
            // txtNewDish
            // 
            txtNewDish.Location = new Point(680, 246);
            txtNewDish.Name = "txtNewDish";
            txtNewDish.Size = new Size(125, 27);
            txtNewDish.TabIndex = 4;
            // 
            // txtDelete
            // 
            txtDelete.Location = new Point(680, 289);
            txtDelete.Name = "txtDelete";
            txtDelete.Size = new Size(125, 27);
            txtDelete.TabIndex = 5;
            // 
            // lblSearchResult
            // 
            lblSearchResult.AutoSize = true;
            lblSearchResult.Location = new Point(738, 85);
            lblSearchResult.Name = "lblSearchResult";
            lblSearchResult.Size = new Size(117, 20);
            lblSearchResult.TabIndex = 7;
            lblSearchResult.Text = "                           ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 593);
            Controls.Add(lblSearchResult);
            Controls.Add(txtDelete);
            Controls.Add(txtNewDish);
            Controls.Add(txtSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnAddDish);
            Controls.Add(btnSearch);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSearch;
        private Button btnAddDish;
        private Button btnDelete;
        private TextBox txtSearch;
        private TextBox txtNewDish;
        private TextBox txtDelete;
        private Label lblSearchResult;
    }
}
