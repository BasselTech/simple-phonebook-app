using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Phonebook_Tutorial
{
    public partial class AddContact : Form
    {
        public AddContact()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.FileName = "";
            OD.Filter = "Supported Images|*.jpg;*.jpeg;*.png";
            if(OD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(OD.FileName);
            }
        }

        private SqlConnection con = new SqlConnection("Data Source=BASSEL-DESKTOP;Initial Catalog=Phonebook;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();

            command.Parameters.AddWithValue("@photo", new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[])));
            command.Parameters.AddWithValue("@firstname", textBox1.Text);
            command.Parameters.AddWithValue("@lastname", textBox2.Text);
            command.Parameters.AddWithValue("@mobile", textBox3.Text);
            command.Parameters.AddWithValue("@notes", textBox4.Text);

            command.CommandText = "INSERT INTO Contacts (firstname, lastname, mobile, notes, photo) VALUES(@firstname, @lastname, @mobile, @notes, @photo)";
            if(command.ExecuteNonQuery() > 0)
            {
                //All good
                MessageBox.Show("Contact was added!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to add contact!");
                con.Close();
            }
        }
    }
}
