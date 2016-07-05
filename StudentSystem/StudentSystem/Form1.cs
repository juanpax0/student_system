using System;
using System.Collections;
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
    public partial class Form1 : Form
    {

        Connection conn = new Connection();

        public Form1()
        {
            InitializeComponent();
            get_students();
        }

        private void get_students()
        {
            listView1.Items.Clear();
            List<Student> sts = conn.GetStudents();

            foreach (Student s in sts)
            {
                string ced = s.cedula;
                string nom = s.nombre;
                string eda = s.edad.ToString();

                ListViewItem item = new ListViewItem(new[] { ced, nom, eda });
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ced = textBox1.Text.Replace(" ", "");
            string nom = Regex.Replace(textBox2.Text.Trim().ToUpper(), @"\s+", " ");
            string eda = textBox3.Text.Replace(" ", "");
            Regex regex = new Regex("^[0-9]+$");

            if (!ced.Equals("") && !nom.Equals("") && !eda.Equals("") &&
                regex.IsMatch(ced) && regex.IsMatch(eda))
            {
                DialogResult dialogResult =
                    MessageBox.Show("¿Desea agregar al estudiante?",
                    "¿Seguro?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    conn.InsertStudent(ced, nom, int.Parse(eda));
                    get_students();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //....................
                }
            }
            else {
                DialogResult dialogResult =
                    MessageBox.Show("Verifique que los datos sean correctos", "Error",
                    MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = listView1.SelectedItems.Count;

            if (count > 0)
            {
                DialogResult dialogResult =
                    MessageBox.Show("¿Desea eliminar los estudiantes seleccionados?", "¿Seguro?",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    while (count > 0)
                    {
                        int index = listView1.SelectedItems[0].Index;
                        ListViewItem sRow = listView1.Items[index];
                        string ced = sRow.SubItems[0].Text;
                        string nom = sRow.SubItems[1].Text;

                        conn.DeleteStudent(ced);
                        listView1.Items.RemoveAt(index);

                        richTextBox1.Text += "Eliminando el estudiante " + nom + "\n";
                        count--;
                    }
                    get_students();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //....................
                }
            }
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            int count = listView1.SelectedItems.Count;

            if (count > 0)
            {
                if (count == 1)
                {
                    int index = listView1.SelectedItems[0].Index;
                    ListViewItem sRow = listView1.Items[index];
                    string ced = sRow.SubItems[0].Text;
                    string nom = sRow.SubItems[1].Text;
                    string eda = sRow.SubItems[2].Text;

                    Update update = new Update(ced, nom, eda);
                    update.Show();                
                }
                else {
                    DialogResult dialogResult =
                        MessageBox.Show("Debe actualizar uno a la vez", "Error",
                        MessageBoxButtons.OK);
                }
            }
        }
    }
}
