using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InternetKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            string date = DateTime.Now.ToString("g");
            if (password != String.Empty)
            {
                textBox3.Text = Encrypt(password, date);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox3.Text);
        }

        public static string Encrypt(string Password, string Date)
        {
            string AllString = Password + Date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return Regex.Replace(Convert.ToBase64String(buffer).Substring(0, 86), "[^0-9a-zA-Z]+", ""); // strip padding and special characters
        }
    }
}
