using System;
using System.Windows.Forms;

namespace test2
{
    public partial class BAIOS : Form
    {
        public BAIOS()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void BAIOS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
