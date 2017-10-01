using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DataCenterUnpack
{
    public partial class UnpackForm : Form
    {
        public UnpackForm()
        {
            InitializeComponent();
        }

        private void BrowseInput_Click(object sender, EventArgs e)
        {
            if (DcFileFileDialog.ShowDialog() == DialogResult.OK)
            {
                InputFile.Text = DcFileFileDialog.FileName;
                outputDir.Text = Path.GetDirectoryName(InputFile.Text);
            }
        }

        private void BrowseResourcesInput_Click(object sender, EventArgs e)
        {
            if (ResourcesFileFileDialog.ShowDialog() == DialogResult.OK)
            {
                ResourcesPathTb.Text = ResourcesFileFileDialog.FileName;
                ResourcesOutputDir.Text = Path.GetDirectoryName(ResourcesPathTb.Text);
            }
        }
        private void Go_Click(object sender, EventArgs e)
        {
            try
            {
                var keyString = Key.Text.Replace(" ", "");
                var ivString = IV.Text.Replace(" ", "");

                var key = DcUnpacker.StringToByteArray(keyString);
                var iv = DcUnpacker.StringToByteArray(ivString);
                DcUnpacker.Unpack(InputFile.Text, outputDir.Text, key, iv);

                GC.Collect();

                MessageBox.Show("Done!");
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindKeyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var candidates = KeyScanner.Find();
                MessageBox.Show("Results:\r\n" + String.Join("\r\n", candidates.Select(x => x.Item1 + "    " + x.Item2)));
                if (candidates.Any())
                {
                    Key.Text = candidates.First().Item1;
                    IV.Text = candidates.First().Item2;
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BrowseOutput_Click(object sender, EventArgs e)
        {
            if (DcOutputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                outputDir.Text = DcOutputFolderDialog.SelectedPath;
            }
        }

        private void DownloadedResBrowseDir_Click(object sender, EventArgs e)
        {
            if (ResourcesOutputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                ResourcesOutputDir.Text = ResourcesOutputFolderDialog.SelectedPath;
            }
        }

        private void ResourcesUnpack_Click(object sender, EventArgs e)
        {
            try
            {
                ResourcesUnpacker.Unpack(ResourcesPathTb.Text, ResourcesOutputDir.Text);

                GC.Collect();

                MessageBox.Show("Done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
