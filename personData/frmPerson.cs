using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace personData
{
    public partial class frmPerson : Form
    {
        public frmPerson()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmPerson_Load(object sender, EventArgs e)
        {
            //this.Icon = Icon.ExtractAssociatedIcon("personData.exe");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text.Trim()) || string.IsNullOrWhiteSpace(txtName.Text.Trim()) || string.IsNullOrWhiteSpace(txtAddress.Text.Trim()))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }
                string FilePath = "personData.txt";
                if (!File.Exists(FilePath))
                {
                    MessageBox.Show("File does not exist. Creating a new file.");
                    File.Create(FilePath).Close();

                    MessageBox.Show("File created and person data added successfully!");
                    StreamWriter sw = new StreamWriter(FilePath, true);
                    sw.WriteLine(txtID.Text + ";" + txtName.Text + ";" + txtAddress.Text);
                    sw.Close();

                  

                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            c.Text = "";
                        }
                    }
                    txtID.Focus();
                }
                else
                {
                    StreamReader srCheck = new StreamReader("personData.txt");
                    string strCheck = srCheck.ReadToEnd();
                    srCheck.Close();


                    if (strCheck.Contains(txtID.Text + ";"))
                    {
                        MessageBox.Show("Person ID already exists. Please enter a unique ID.");
                        txtID.Focus();
                        txtID.SelectAll();
                        return;
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter("personData.txt", true);
                        sw.WriteLine(txtID.Text + ";" + txtName.Text + ";" + txtAddress.Text);
                        sw.Close();
                        MessageBox.Show("Person data added successfully!");
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Text = "";
                            }
                        }
                        txtID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text.Trim()))
            {
                MessageBox.Show("Please enter an ID to find.");
                txtID.Focus();
                return;
            }
            else
            {
                try
                {
                    StreamReader sr = new StreamReader("personData.txt");
                    string Line;
                    bool found = false;
                    while ((Line = sr.ReadLine()) != null)
                    {
                        string[] arrData = Line.Split(';');
                        if (arrData[0] == txtID.Text)
                        {
                            txtName.Text = arrData[1];
                            txtAddress.Text = arrData[2];
                            found = true;
                            break;
                        }
                    }
                    sr.Close();
                    if (!found)
                    {
                        MessageBox.Show("Person ID not found.");
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox && c != txtID)
                            {
                                c.Text = "";
                            }
                        }
                        txtID.Focus();
                        txtID.SelectAll();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Form frmShow = new Form();
            TextBox txtShow = new TextBox();
            frmShow.StartPosition= FormStartPosition.CenterScreen;
            frmShow.Font = this.Font;
            frmShow.Icon = this.Icon;
            frmShow.Size = this.Size;
            frmShow.Text = "All Person Data";
            txtShow.Multiline = true;
            txtShow.Dock = DockStyle.Fill;
            frmShow.Controls.Add(txtShow);

            try
            {
                StreamReader sr = new StreamReader("personData.txt");
                string strAll = sr.ReadToEnd();
                sr.Close();
                txtShow.Text = strAll;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            frmShow.ShowDialog();
        }

        
    }
}
