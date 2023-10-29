using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Simple_Code_Editor
{
    public partial class MainWindow : Form
    {
        public string GlobalFileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple-Code-Editor\nCreated by Joseph Patrick on 10/28/2023\nThis program uses UTF8 encoding", "About", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Locate the file that we want to edit
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {

                // We have the string for the filename now storage as "selectedFileName"
                string selectedFileName = openFileDialog.FileName;

                // Save the filename to a global var to be accessed later
                GlobalFileName = selectedFileName;

                // Clear the Text Editor Window
                MainTextBox.Clear();

                // Append all of the text read from the file into the text box
                MainTextBox.AppendText(File.ReadAllText(selectedFileName));
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Save File Diaglog Box
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*) | *.*";
            saveFileDialog.Title = "Save File";
            saveFileDialog.ShowDialog();

            // Check to ensure the location exitts
            if(saveFileDialog.FileName.Length > 0)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                byte[] writeArr = Encoding.UTF8.GetBytes(MainTextBox.Text);
                fs.Write(writeArr, 0,MainTextBox.Text.Length);
                fs.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            System.IO.FileStream fs = (System.IO.FileStream)File.Open(GlobalFileName, FileMode.Create);
            byte[] writeArr = Encoding.UTF8.GetBytes(MainTextBox.Text);
            fs.Write(writeArr, 0, MainTextBox.Text.Length);
            fs.Close();
        }
    }
}
