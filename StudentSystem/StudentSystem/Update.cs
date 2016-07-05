using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSystem
{
    public partial class Update : Form
    {
        private string cedula;
        private Connection conn;
        private static Regex regex = new Regex("^[0-9]+$");

        public Update(Connection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        public void setComponents(string cedula, string nombre, string edad)
        {
            this.cedula = cedula;
            textBox1.Text = nombre;
            textBox2.Text = edad;
        }


        public void button1_Click()
        {
            string nom = Regex.Replace(textBox1.Text.Trim().ToUpper(), @"\s+", " ");
            string eda = textBox2.Text.Replace(" ", "");

            if (!nom.Equals("") && !eda.Equals("") && regex.IsMatch(eda))
            {
                DialogResult dialogResult =
                    MessageBox.Show("¿Desea actualizar los datos?",
                    "¿Seguro?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    conn.UpdateStudent(cedula, nom, int.Parse(eda));
                    Hide();
                }
            }
            else {
                DialogResult dialogResult =
                    MessageBox.Show("Verifique que los datos sean correctos", "Error",
                    MessageBoxButtons.OK);
            }
        }
    }
}
