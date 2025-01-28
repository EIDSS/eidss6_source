using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eidss.winclient.ElectronicDigitalSignature
{
    public partial class EnterEdsPassword : Form
    {
        public EnterEdsPassword()
        {
            InitializeComponent();
        }

        public string GetPIN() {
            return this.textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            submitForm();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(this.textBox1.Text))
            {
                submitForm();
            }
        }

        private void submitForm() 
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EnterEdsPassword_Shown(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }
    }
}
