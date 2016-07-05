using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystem
{
    public partial class Update : Form
    {
        public string cedula;

        public Update(string cedula, string nombre, string edad)
        {
            InitializeComponent();
            this.cedula = cedula;
            textBox1.Text = nombre;
            textBox2.Text = edad;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Hide();
        }
    }
}
