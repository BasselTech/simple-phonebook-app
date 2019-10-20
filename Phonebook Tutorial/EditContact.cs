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
    public partial class EditContact : Form
    {
        private int id;
        public EditContact(Contact mycontact)
        {
            InitializeComponent();
            textBox1.Text = mycontact.firstname;
            textBox2.Text = mycontact.lastname;
            textBox3.Text = mycontact.mobile;
            textBox4.Text = mycontact.notes;
            pictureBox1.Image = mycontact.photo;
            id = mycontact.id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.FileName = "";
            OD.Filter = "Supported Images|*.jpg;*.jpg;*.png";
            if (OD.ShowDialog() == DialogResult.OK)
                pictureBox1.Load(OD.FileName);
        }
        private SqlConnection con = new SqlConnection("Data Source=BASSEL-DESKTOP;Initial Catalog=Phonebook;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@firstname", textBox1.Text);
            command.Parameters.AddWithValue("@lastname", textBox2.Text);
            command.Parameters.AddWithValue("@mobile", textBox3.Text);
            command.Parameters.AddWithValue("@notes", textBox4.Text);
            command.Parameters.AddWithValue("@photo", new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[])));
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "UPDATE Contacts SET firstname=@firstname, lastname=@lastname, mobile=@mobile, notes=@notes, photo=@photo WHERE id=@id";

            if(command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Contact was updated!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to update contact!");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = con.CreateCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "DELETE FROM Contacts WHERE id=@id";
            if(command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Contact was deleted!");
                con.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Unable to delete contact!");
                con.Close();
            }
        }
    }
}
