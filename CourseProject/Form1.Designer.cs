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
            Tabs = new TabControl();
            tabPage1 = new TabPage();
            button1 = new Button();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            Tabs.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // Tabs
            // 
            Tabs.Controls.Add(tabPage1);
            Tabs.Controls.Add(tabPage2);
            Tabs.Controls.Add(tabPage3);
            Tabs.Location = new Point(0, 0);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(1122, 634);
            Tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1114, 601);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Меню";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(907, 487);
            button1.Name = "button1";
            button1.Size = new Size(145, 51);
            button1.TabIndex = 1;
            button1.Text = "Сформувати замовлення";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1114, 601);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Інсуючі замовлення";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1114, 601);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "тут шось буде тож";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 646);
            Controls.Add(Tabs);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Tabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl Tabs;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button button1;
    }
}
