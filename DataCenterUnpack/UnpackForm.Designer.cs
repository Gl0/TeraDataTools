namespace DataCenterUnpack
{
    partial class UnpackForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Go = new System.Windows.Forms.Button();
            this.DcFileFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Key = new System.Windows.Forms.TextBox();
            this.IV = new System.Windows.Forms.TextBox();
            this.InputFile = new System.Windows.Forms.TextBox();
            this.BrowseInput = new System.Windows.Forms.Button();
            this.FindKeyButton = new System.Windows.Forms.Button();
            this.UnpackDCTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outputDir = new System.Windows.Forms.TextBox();
            this.BrowseOutput = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DownloadedResBrowseDir = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ResourcesOutputDir = new System.Windows.Forms.TextBox();
            this.BrowseResourcesInput = new System.Windows.Forms.Button();
            this.ResourcesUnpack = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ResourcesPathTb = new System.Windows.Forms.TextBox();
            this.ResourcesFileFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DcOutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ResourcesOutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.UnpackDCTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "IV";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Input";
            // 
            // Go
            // 
            this.Go.Location = new System.Drawing.Point(9, 108);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(75, 23);
            this.Go.TabIndex = 4;
            this.Go.Text = "Unpack";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // DcFileFileDialog
            // 
            this.DcFileFileDialog.Filter = "Data Center files|DataCenter_Final*.dat|All Files|*.*";
            // 
            // Key
            // 
            this.Key.Location = new System.Drawing.Point(54, 5);
            this.Key.Name = "Key";
            this.Key.Size = new System.Drawing.Size(329, 20);
            this.Key.TabIndex = 5;
            // 
            // IV
            // 
            this.IV.Location = new System.Drawing.Point(54, 28);
            this.IV.Name = "IV";
            this.IV.Size = new System.Drawing.Size(329, 20);
            this.IV.TabIndex = 6;
            // 
            // InputFile
            // 
            this.InputFile.Location = new System.Drawing.Point(54, 50);
            this.InputFile.Name = "InputFile";
            this.InputFile.ReadOnly = true;
            this.InputFile.Size = new System.Drawing.Size(296, 20);
            this.InputFile.TabIndex = 7;
            // 
            // BrowseInput
            // 
            this.BrowseInput.Location = new System.Drawing.Point(356, 50);
            this.BrowseInput.Name = "BrowseInput";
            this.BrowseInput.Size = new System.Drawing.Size(27, 22);
            this.BrowseInput.TabIndex = 9;
            this.BrowseInput.Text = "...";
            this.BrowseInput.UseVisualStyleBackColor = true;
            this.BrowseInput.Click += new System.EventHandler(this.BrowseInput_Click);
            // 
            // FindKeyButton
            // 
            this.FindKeyButton.Location = new System.Drawing.Point(90, 108);
            this.FindKeyButton.Name = "FindKeyButton";
            this.FindKeyButton.Size = new System.Drawing.Size(114, 23);
            this.FindKeyButton.TabIndex = 11;
            this.FindKeyButton.Text = "Find Key in Memory";
            this.FindKeyButton.UseVisualStyleBackColor = true;
            this.FindKeyButton.Click += new System.EventHandler(this.FindKeyButton_Click);
            // 
            // UnpackDCTab
            // 
            this.UnpackDCTab.Controls.Add(this.tabPage1);
            this.UnpackDCTab.Controls.Add(this.tabPage2);
            this.UnpackDCTab.Location = new System.Drawing.Point(6, 6);
            this.UnpackDCTab.Name = "UnpackDCTab";
            this.UnpackDCTab.SelectedIndex = 0;
            this.UnpackDCTab.Size = new System.Drawing.Size(400, 163);
            this.UnpackDCTab.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.outputDir);
            this.tabPage1.Controls.Add(this.BrowseOutput);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.InputFile);
            this.tabPage1.Controls.Add(this.FindKeyButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BrowseInput);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.IV);
            this.tabPage1.Controls.Add(this.Go);
            this.tabPage1.Controls.Add(this.Key);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(392, 137);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Unpack DC";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // outputDir
            // 
            this.outputDir.Location = new System.Drawing.Point(54, 76);
            this.outputDir.Name = "outputDir";
            this.outputDir.ReadOnly = true;
            this.outputDir.Size = new System.Drawing.Size(296, 20);
            this.outputDir.TabIndex = 13;
            // 
            // BrowseOutput
            // 
            this.BrowseOutput.Location = new System.Drawing.Point(356, 76);
            this.BrowseOutput.Name = "BrowseOutput";
            this.BrowseOutput.Size = new System.Drawing.Size(27, 22);
            this.BrowseOutput.TabIndex = 14;
            this.BrowseOutput.Text = "...";
            this.BrowseOutput.UseVisualStyleBackColor = true;
            this.BrowseOutput.Click += new System.EventHandler(this.BrowseOutput_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Output";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DownloadedResBrowseDir);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.ResourcesOutputDir);
            this.tabPage2.Controls.Add(this.BrowseResourcesInput);
            this.tabPage2.Controls.Add(this.ResourcesUnpack);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.ResourcesPathTb);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(392, 137);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Unpack DownloadedResources";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DownloadedResBrowseDir
            // 
            this.DownloadedResBrowseDir.Location = new System.Drawing.Point(359, 50);
            this.DownloadedResBrowseDir.Name = "DownloadedResBrowseDir";
            this.DownloadedResBrowseDir.Size = new System.Drawing.Size(27, 22);
            this.DownloadedResBrowseDir.TabIndex = 13;
            this.DownloadedResBrowseDir.Text = "...";
            this.DownloadedResBrowseDir.UseVisualStyleBackColor = true;
            this.DownloadedResBrowseDir.Click += new System.EventHandler(this.DownloadedResBrowseDir_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Output dir:";
            // 
            // ResourcesOutputDir
            // 
            this.ResourcesOutputDir.Location = new System.Drawing.Point(70, 50);
            this.ResourcesOutputDir.Name = "ResourcesOutputDir";
            this.ResourcesOutputDir.ReadOnly = true;
            this.ResourcesOutputDir.Size = new System.Drawing.Size(283, 20);
            this.ResourcesOutputDir.TabIndex = 12;
            // 
            // BrowseResourcesInput
            // 
            this.BrowseResourcesInput.Location = new System.Drawing.Point(359, 25);
            this.BrowseResourcesInput.Name = "BrowseResourcesInput";
            this.BrowseResourcesInput.Size = new System.Drawing.Size(27, 22);
            this.BrowseResourcesInput.TabIndex = 10;
            this.BrowseResourcesInput.Text = "...";
            this.BrowseResourcesInput.UseVisualStyleBackColor = true;
            this.BrowseResourcesInput.Click += new System.EventHandler(this.BrowseResourcesInput_Click);
            // 
            // ResourcesUnpack
            // 
            this.ResourcesUnpack.Location = new System.Drawing.Point(11, 91);
            this.ResourcesUnpack.Name = "ResourcesUnpack";
            this.ResourcesUnpack.Size = new System.Drawing.Size(75, 23);
            this.ResourcesUnpack.TabIndex = 8;
            this.ResourcesUnpack.Text = "Unpack";
            this.ResourcesUnpack.UseVisualStyleBackColor = true;
            this.ResourcesUnpack.Click += new System.EventHandler(this.ResourcesUnpack_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "DownloadedResources.dat path:";
            // 
            // ResourcesPathTb
            // 
            this.ResourcesPathTb.Location = new System.Drawing.Point(11, 25);
            this.ResourcesPathTb.Name = "ResourcesPathTb";
            this.ResourcesPathTb.ReadOnly = true;
            this.ResourcesPathTb.Size = new System.Drawing.Size(342, 20);
            this.ResourcesPathTb.TabIndex = 7;
            // 
            // ResourcesFileFileDialog
            // 
            this.ResourcesFileFileDialog.Filter = "DownloadedResources files|DownloadedResources.dat|All Files|*.*";
            // 
            // UnpackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 181);
            this.Controls.Add(this.UnpackDCTab);
            this.Name = "UnpackForm";
            this.Text = "DataCenter Unpacker";
            this.UnpackDCTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.OpenFileDialog DcFileFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox Key;
        private System.Windows.Forms.TextBox IV;
        private System.Windows.Forms.TextBox InputFile;
        private System.Windows.Forms.Button BrowseInput;
        private System.Windows.Forms.Button FindKeyButton;
        private System.Windows.Forms.TabControl UnpackDCTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ResourcesPathTb;
        private System.Windows.Forms.TextBox outputDir;
        private System.Windows.Forms.Button BrowseOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BrowseResourcesInput;
        private System.Windows.Forms.Button ResourcesUnpack;
        private System.Windows.Forms.OpenFileDialog ResourcesFileFileDialog;
        private System.Windows.Forms.FolderBrowserDialog DcOutputFolderDialog;
        private System.Windows.Forms.Button DownloadedResBrowseDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ResourcesOutputDir;
        private System.Windows.Forms.FolderBrowserDialog ResourcesOutputFolderDialog;
    }
}

