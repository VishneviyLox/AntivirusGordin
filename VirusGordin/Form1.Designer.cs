namespace VirusGordin
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
            dataGridView1 = new DataGridView();
            SelectFolderBtn = new Button();
            progressBar1 = new ProgressBar();
            MainLabel = new Label();
            VirusCountText = new Label();
            AdressFolderText = new Label();
            ScanFolderBtn = new Button();
            DeleteSelectedVirusBtn = new Button();
            DeleteAllVirusesBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(274, 105);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(490, 269);
            dataGridView1.TabIndex = 0;
            // 
            // SelectFolderBtn
            // 
            SelectFolderBtn.Location = new Point(55, 105);
            SelectFolderBtn.Name = "SelectFolderBtn";
            SelectFolderBtn.Size = new Size(181, 46);
            SelectFolderBtn.TabIndex = 1;
            SelectFolderBtn.Text = "Выбрать папку";
            SelectFolderBtn.UseVisualStyleBackColor = true;
            SelectFolderBtn.Click += SelectFolderBtn_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(55, 45);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(709, 23);
            progressBar1.TabIndex = 5;
            // 
            // MainLabel
            // 
            MainLabel.AutoSize = true;
            MainLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            MainLabel.ForeColor = SystemColors.HotTrack;
            MainLabel.Location = new Point(154, 0);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new Size(661, 32);
            MainLabel.TabIndex = 6;
            MainLabel.Text = "Программа обработки файлов сигнатурным методом";
            // 
            // VirusCountText
            // 
            VirusCountText.AutoSize = true;
            VirusCountText.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            VirusCountText.ForeColor = Color.Goldenrod;
            VirusCountText.Location = new Point(91, 478);
            VirusCountText.Name = "VirusCountText";
            VirusCountText.Size = new Size(224, 25);
            VirusCountText.TabIndex = 8;
            VirusCountText.Text = "Обнаружено вирусов: ";
            // 
            // AdressFolderText
            // 
            AdressFolderText.AutoSize = true;
            AdressFolderText.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            AdressFolderText.ForeColor = Color.Goldenrod;
            AdressFolderText.Location = new Point(91, 434);
            AdressFolderText.Name = "AdressFolderText";
            AdressFolderText.Size = new Size(141, 25);
            AdressFolderText.TabIndex = 9;
            AdressFolderText.Text = "Адрес папки: ";
            // 
            // ScanFolderBtn
            // 
            ScanFolderBtn.Location = new Point(55, 178);
            ScanFolderBtn.Name = "ScanFolderBtn";
            ScanFolderBtn.Size = new Size(181, 46);
            ScanFolderBtn.TabIndex = 13;
            ScanFolderBtn.Text = "Сканировать папку";
            ScanFolderBtn.UseVisualStyleBackColor = true;
            ScanFolderBtn.Click += ScanFolderBtn_Click;
            // 
            // DeleteSelectedVirusBtn
            // 
            DeleteSelectedVirusBtn.Location = new Point(55, 260);
            DeleteSelectedVirusBtn.Name = "DeleteSelectedVirusBtn";
            DeleteSelectedVirusBtn.Size = new Size(181, 46);
            DeleteSelectedVirusBtn.TabIndex = 14;
            DeleteSelectedVirusBtn.Text = "Удалить выбранный вирус";
            DeleteSelectedVirusBtn.UseVisualStyleBackColor = true;
            DeleteSelectedVirusBtn.Click += DeleteSelectedVirusBtn_Click;
            // 
            // DeleteAllVirusesBtn
            // 
            DeleteAllVirusesBtn.Location = new Point(55, 328);
            DeleteAllVirusesBtn.Name = "DeleteAllVirusesBtn";
            DeleteAllVirusesBtn.Size = new Size(181, 46);
            DeleteAllVirusesBtn.TabIndex = 15;
            DeleteAllVirusesBtn.Text = "Удалить все вирусы";
            DeleteAllVirusesBtn.UseVisualStyleBackColor = true;
            DeleteAllVirusesBtn.Click += DeleteAllVirusesBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Bisque;
            ClientSize = new Size(800, 528);
            Controls.Add(DeleteAllVirusesBtn);
            Controls.Add(DeleteSelectedVirusBtn);
            Controls.Add(ScanFolderBtn);
            Controls.Add(AdressFolderText);
            Controls.Add(VirusCountText);
            Controls.Add(MainLabel);
            Controls.Add(progressBar1);
            Controls.Add(SelectFolderBtn);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button SelectFolderBtn;
        private ProgressBar progressBar1;
        private Label MainLabel;
        private Label VirusCountText;
        private Label AdressFolderText;
        private Button ScanFolderBtn;
        private Button DeleteSelectedVirusBtn;
        private Button DeleteAllVirusesBtn;
    }
}
