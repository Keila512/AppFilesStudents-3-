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

namespace AppFilesStudents_3_
{
    public partial class Form1 : Form
    {
        private string filePath;
        private List<Class1> students = new List<Class1>();
        private string fileSequential = "sequential.txt";
        private string fileDirect = "direct.txt";
        private string fileIndex = "index.txt";
        public Form1()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Class1 newStudent = new Class1
            {
                Name = txtName.Text,
                LastName = txtLastName.Text,
                Age = Convert.ToInt32(txtAge.Text),
                Grade = txtGrade.Text
            };

            students.Add(newStudent);

            ShowStudentsInListView();
        }
        private void ShowStudentsInListView()
        {
            foreach (var student in students)
            {
                ListViewItem item = new ListViewItem(new[]
                {
                    student.Name,
                    student.LastName,
                    student.Age.ToString(),
                    student.Grade
                });             
            }
        }
        private void SaveFileSequential()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileSequential))
                {
                    foreach (var student in students)
                    {
                        writer.WriteLine($"{student.Name},{student.LastName},{student.Age},{student.Grade}");
                    }
                }

                MessageBox.Show("The file has been saved sequentially.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the sequential file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSequential_Click(object sender, EventArgs e)
        {
            SaveFileSequential();
        }
        private void SaveFileDirect()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(fileDirect, FileMode.Create)))
                {
                    foreach (var student in students)
                    {
                        writer.Write(student.Name);
                        writer.Write(student.LastName);
                        writer.Write(student.Age);
                        writer.Write(student.Grade);
                    }
                }

                MessageBox.Show("The file has been saved directly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving direct file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDirect_Click(object sender, EventArgs e)
        {
            SaveFileDirect();
        }
        private void SaveFileIndex()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileIndex))
                {
                    foreach (var student in students)
                    {
                        long posicion = writer.BaseStream.Position;
                        writer.WriteLine($"{student.Name},{student.LastName},{student.Age},{student.Grade},{posicion}");
                    }
                }

                MessageBox.Show("The indexed file has been saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the indexed file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnIndex_Click(object sender, EventArgs e)
        {
            SaveFileIndex();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName);
                    ShowInListBox(content);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading the file: " + ex.Message);
                }
            }
        }
        private void ShowInListBox(string content)
        {
            listBox1.Items.Clear();

            string[] lines = content.Split('\n');
            foreach (string line in lines)
            {
                listBox1.Items.Add(line.Trim()); 
            }
        }
    }
}
