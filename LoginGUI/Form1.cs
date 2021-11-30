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
using System.Xml.Serialization;

namespace LoginGUI
{
    public partial class Form1 : Form
    {
        XmlSerializer xmlSerializer;
        List<UserDetails> users;
        List<UserDetails> registeredUsers;

        public Form1()
        {
            InitializeComponent();
            users = new List<UserDetails>();
            xmlSerializer = new XmlSerializer(typeof(List<UserDetails>));
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            FileStream filestream = new FileStream("C:/Users/Acer/source/repos/LoginGUI/LoginGUI/Users.xml", FileMode.Open, FileAccess.Read);
            registeredUsers = (List<UserDetails>)xmlSerializer.Deserialize(filestream);
           
            foreach(var x in registeredUsers)
            {
                if(x.userName == userNameTxtBox.Text && x.pw == pwTxtBox.Text)
                {
                    MessageBox.Show("Login Successful");
                    
                }
                else if (x.userName == "" || x.pw == "")
                {
                    MessageBox.Show("Please provide a Username and Password!");
                }
                else
                {
                    MessageBox.Show("Please provide a valid Username and Password!");
                }
            }
            filestream.Close();
            userNameTxtBox.Clear();
            pwTxtBox.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pwTxtBox_TextChanged(object sender, EventArgs e)
        {
            pwTxtBox.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream filestream = new FileStream("C:/Users/Acer/source/repos/LoginGUI/LoginGUI/Users.xml", FileMode.Create, FileAccess.Write);
            UserDetails user = new UserDetails();
            user.userName = userNameTxtBox.Text;
            user.pw = pwTxtBox.Text;
            users.Add(user);
            xmlSerializer.Serialize(filestream, users);
            MessageBox.Show("User successfully added! Please click on the check button to verify.");
            filestream.Close();
            userNameTxtBox.Clear();
            pwTxtBox.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream filestream = new FileStream("C:/Users/Acer/source/repos/LoginGUI/LoginGUI/Users.xml", FileMode.Open, FileAccess.Read);
            users = (List<UserDetails>)xmlSerializer.Deserialize(filestream);

            dataGridView1.DataSource = users;
            filestream.Close();
        }
    }
}
