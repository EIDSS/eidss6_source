using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eidss.openapi.wintest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
            textBoxUrl.Text = "http://localhost:64763/";
            textBoxOrganization.Text = "test_org";
            textBoxLang.Text = "en";
            textBoxUsername.Text = "test_admin";
            textBoxPassword.Text = "test";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
            var api = new Api(textBoxUrl.Text, textBoxLang.Text, textBoxOrganization.Text, textBoxUsername.Text, textBoxPassword.Text);
            new HumanCaseList(api).ShowDialog();
            Close();
        }
    }
}
