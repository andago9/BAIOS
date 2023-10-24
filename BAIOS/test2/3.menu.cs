using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por favor lea el manual de uso antes de ejecutar cualquier programa");
            MessageBox.Show("Para mejor Uso algunos aplicativos pueden requerir conexion a internet");
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por favor lea el manual de uso antes de ejecutar cualquier programa");
            MessageBox.Show("Para mejor Uso algunos aplicativos pueden requerir conexion a internet");
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por favor lea el manual de uso antes de ejecutar cualquier programa");
            MessageBox.Show("Para mejor Uso algunos aplicativos pueden requerir conexion a internet");
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             System.Diagnostics.Process.Start(@"adwcleaner.exe");
            }
    }

        //    private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        //  {
        //    public static void Exit();
        // }
}

