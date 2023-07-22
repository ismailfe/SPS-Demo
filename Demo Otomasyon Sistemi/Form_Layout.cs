using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo_Otomasyon_Sistemi
{
    public partial class Form_Layout : Form
    {
        public Form_Layout()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Grup3.Visible = true;
            Grup10.Visible = false;
            Grup11.Visible = false;

        }

        private void label52_Click(object sender, EventArgs e)
        {
            Grup10.Visible = true;
            Grup3.Visible = false;
            Grup11.Visible = false;
        }

        private void label47_Click(object sender, EventArgs e)
        {
            Grup11.Visible = true;
            Grup3.Visible = false;
            Grup10.Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
