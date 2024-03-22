namespace Test_FileExplorer
{
    partial class MainForm
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
            this.filesTreeView = new System.Windows.Forms.TreeView();
            this.startDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameRegexTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchStatusLabel = new System.Windows.Forms.Label();
            this.foundFilesCountLabel = new System.Windows.Forms.Label();
            this.totalFilesCountLabel = new System.Windows.Forms.Label();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filesTreeView
            // 
            this.filesTreeView.Location = new System.Drawing.Point(12, 12);
            this.filesTreeView.Name = "filesTreeView";
            this.filesTreeView.Size = new System.Drawing.Size(311, 387);
            this.filesTreeView.TabIndex = 0;
            // 
            // startDirectoryTextBox
            // 
            this.startDirectoryTextBox.Location = new System.Drawing.Point(329, 30);
            this.startDirectoryTextBox.Name = "startDirectoryTextBox";
            this.startDirectoryTextBox.Size = new System.Drawing.Size(293, 23);
            this.startDirectoryTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Стартовая директория:";
            // 
            // fileNameRegexTextBox
            // 
            this.fileNameRegexTextBox.Location = new System.Drawing.Point(329, 80);
            this.fileNameRegexTextBox.Name = "fileNameRegexTextBox";
            this.fileNameRegexTextBox.Size = new System.Drawing.Size(293, 23);
            this.fileNameRegexTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Шаблон:";
            // 
            // searchStatusLabel
            // 
            this.searchStatusLabel.AutoSize = true;
            this.searchStatusLabel.Location = new System.Drawing.Point(329, 171);
            this.searchStatusLabel.Name = "searchStatusLabel";
            this.searchStatusLabel.Size = new System.Drawing.Size(127, 15);
            this.searchStatusLabel.TabIndex = 5;
            this.searchStatusLabel.Text = "Текущяя директория: ";
            // 
            // foundFilesCountLabel
            // 
            this.foundFilesCountLabel.AutoSize = true;
            this.foundFilesCountLabel.Location = new System.Drawing.Point(329, 196);
            this.foundFilesCountLabel.Name = "foundFilesCountLabel";
            this.foundFilesCountLabel.Size = new System.Drawing.Size(117, 15);
            this.foundFilesCountLabel.TabIndex = 6;
            this.foundFilesCountLabel.Text = "Найденные файлы: ";
            // 
            // totalFilesCountLabel
            // 
            this.totalFilesCountLabel.AutoSize = true;
            this.totalFilesCountLabel.Location = new System.Drawing.Point(329, 221);
            this.totalFilesCountLabel.Name = "totalFilesCountLabel";
            this.totalFilesCountLabel.Size = new System.Drawing.Size(86, 15);
            this.totalFilesCountLabel.TabIndex = 7;
            this.totalFilesCountLabel.Text = "Всего файлов:";
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Location = new System.Drawing.Point(329, 384);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(112, 15);
            this.elapsedTimeLabel.TabIndex = 8;
            this.elapsedTimeLabel.Text = "Прошло времени: ";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(329, 119);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(293, 40);
            this.searchButton.TabIndex = 9;
            this.searchButton.Text = "Найти";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Controls.Add(this.totalFilesCountLabel);
            this.Controls.Add(this.foundFilesCountLabel);
            this.Controls.Add(this.searchStatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fileNameRegexTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startDirectoryTextBox);
            this.Controls.Add(this.filesTreeView);
            this.Name = "MainForm";
            this.Text = "`";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView filesTreeView;
        private TextBox startDirectoryTextBox;
        private Label label1;
        private TextBox fileNameRegexTextBox;
        private Label label2;
        private Label searchStatusLabel;
        private Label foundFilesCountLabel;
        private Label totalFilesCountLabel;
        private Label elapsedTimeLabel;
        private Button searchButton;
    }
}