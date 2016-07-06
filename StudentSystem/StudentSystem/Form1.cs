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

        private static Connection conn = new Connection();
        private static Update update = new Update(conn);
        private static Regex regex = new Regex("^[0-9]+$");

        public Form1()
        {
            InitializeComponent();
            update.button1.Click += new EventHandler(this.update_method);
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

            if (!ced.Equals("") && !nom.Equals("") && !eda.Equals("") &&
                regex.IsMatch(ced) && regex.IsMatch(eda))
            {
                DialogResult dialogResult =
                    MessageBox.Show("¿Desea agregar al estudiante?",
                    "¿Seguro?", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        conn.InsertStudent(ced, nom, int.Parse(eda));
                        get_students();
                    }
                    catch (Exception ex)
                    {
                        DialogResult dialogError = MessageBox.Show("Verifique que el estudiante no se encuentre previamente registrado",
                            "Ocurrio un error", MessageBoxButtons.OK);
                    }
                }
            }
            else {
                DialogResult dialogResult =
                    MessageBox.Show("Verifique que los datos sean correctos",
                    "Ocurrio un error",
                    MessageBoxButtons.OK);
            }
        }

        private void update_method(object sender, EventArgs e)
        {
            update.button1_Click();
            get_students();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
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

                    update.setComponents(ced, nom, eda);
                    update.ShowDialog();
                }
                else {
                    DialogResult dialogResult =
                        MessageBox.Show("Debe actualizar uno a la vez",
                        "Ocurrio un error",
                        MessageBoxButtons.OK);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
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
            }
        }
        public void metodo()
        {
            Console.WriteLine("blablablabla");
        }

    }
}
