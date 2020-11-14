using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;


namespace _16
{
    public partial class Form1 : Form
    {
        bool filter = false;    
        ListAtletes list = new ListAtletes(); 

        public Form1()
        {
            InitializeComponent();          
            bindingSource1.DataSource = new ListAtletes() ; 
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ListAtletes));
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    xs.Serialize(sw, bindingSource1.DataSource);
                    sw.Close();
                    MessageBox.Show("Файл успешно сохранен!");
                }
            }
            catch { MessageBox.Show("Ошибка сохранения");}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ListAtletes));
                    System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                    bindingSource1.DataSource = (ListAtletes)xs.Deserialize(sr);
                    sr.Close();
                 
                }
            }
            catch { MessageBox.Show("Ошибка открытия "); }
        }

        private void button3_Click(object sender, EventArgs e)

        {
            try
            {
                ListAtletes A = new ListAtletes();
                A = (ListAtletes)bindingSource1.DataSource;
                ListAtletes at = (ListAtletes)bindingSource1.DataSource;

                int a = 1;
                if (radioButton1.Checked == true)
                    a = -1;

                switch (comboBox1.Text)
                {
                    case "Имени": at.atletes.Sort((x, y) => a * (x.Name.CompareTo(y.Name))); break;
                    case "Фамилии": at.atletes.Sort((x, y) => a * (x.SurName.CompareTo(y.SurName))); break;
                    case "Возрату": at.atletes.Sort((x, y) => a * (x.Age.CompareTo(y.Age))); break;
                    case "Виду спорта": at.atletes.Sort((x, y) => a * (x.Sport.CompareTo(y.Sport))); break;
                    case "Победам": at.atletes.Sort((x, y) => a * (x.Wins.CompareTo(y.Wins))); break;
                }
                bindingSource1.DataSource = at;
                bindingSource1.MoveFirst();
                dataGridView1.DataSource = bindingSource1;
                this.Refresh();
            }
            catch
            {
                MessageBox.Show("Ошибка сортировки");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!filter)
                {
                    list = (ListAtletes)bindingSource1.DataSource;
                    filter = true;
                }
                ListAtletes r2 = new ListAtletes();
                int a = 1;
                if (radioButton3.Checked == true)
                {
                    if (RbAge.Checked == true)
                        r2.atletes = list.atletes.FindAll(x => x.Age > int.Parse(textBox2.Text));
                    else
                        r2.atletes = list.atletes.FindAll(x => x.Wins > int.Parse(textBox2.Text));
                }
                else
                {
                    if (RbWins.Checked == true)
                        r2.atletes = list.atletes.FindAll(x => x.Wins < int.Parse(textBox2.Text));
                    else
                        r2.atletes = list.atletes.FindAll(x => x.Age < int.Parse(textBox2.Text));
                }

                bindingSource1.DataSource = r2;
                bindingSource1.MoveFirst();
                this.Refresh();
            }
            catch { MessageBox.Show("Ошибка сортировки"); }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.CurrentCell = null;
                    dataGridView1.Rows[i].Visible = false;
                    for (int c = 0; c < dataGridView1.Columns.Count; c++)
                    {
                        if (dataGridView1[c, i].Value.ToString() == textBox1.Text)
                        {
                            dataGridView1.Rows[i].Visible = true;
                            break;

                        }
                    }
                }

                if (textBox1.Text == "")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        dataGridView1.Rows[i].Visible = true;
                    }
                }
            }
            catch { MessageBox.Show("Ошибка поиска"); }
        }
     

        private void button1_Click_2(object sender, EventArgs e)
        {
            Close();
        }



        private void button2_Click_2(object sender, EventArgs e)
        {
            bindingSource1.DataSource = list;
        }

   
    }

    public class atletes
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public string Sport { get; set; }
        public int Wins { get; set; }

        public atletes() : this(" "," ",0," ",0) { }

        public atletes(string n, string s, int a, string sp, int w)
        {
            this.Name = n;
            this.SurName = s;
            this.Age = a;
            this.Sport = sp;
            this.Wins = w;

        }
    }

    public class ListAtletes
    {
        public List<atletes> atletes { set; get; }

        public ListAtletes() {
            atletes = new List<atletes>(); }
    }
}

