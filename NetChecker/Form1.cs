using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Verze v = new Verze();
            richTextBox1.Text = v.verze();
        }

    }
}
