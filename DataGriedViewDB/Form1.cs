using DataGriedViewDB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGriedViewDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult res = MessageBox.Show("Зробіть ваш вибір", "Ви впевнені що хочете вийти?", buttons);
            if (res == DialogResult.OK)
            {
                this.Close();
            }
        }
        private Context context = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Visible = false;
            ChangeVis(false);

            FillDataGried();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeVis(true, false);
            
            textBox2.PlaceholderText = "Назва відділу";
            textBox1.PlaceholderText = "Номер відділення";

            dataGridView1.Visible = false;
            button3.Text = "Добавити";
        }
        private void FillDataGried() 
        {
            dataGridView1.Rows.Clear();
            if (context == null) 
            {
            context = new Context();
            }
            foreach (var item in context.departments)
            {
                string[] strs =
                {
                    item.Id.ToString(),
                    item.Name,
                    item.CabinetNumber.ToString()
                };

                dataGridView1.Rows.Add(strs);
            }
        }
        private void ChangeVis(bool p, bool v= true) 
        {
            textBox1.Visible = p;
            textBox2.Visible = p;
            button3.Visible = p;
            button2.Visible = v;
            button4.Visible = v;
            button5.Visible = v;
            button6.Visible = v;

            linkLabel1.Visible = p;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (context == null) 
            {
                context = new Context();
            }
            switch (button3.Text) 
            {
                case "Добавити": 
                    {
                        string name = textBox2.Text;
                        int cabinetNumber = 0;
                        try
                        {
                            if (!string.IsNullOrEmpty(textBox1.Text))
                            {
                                cabinetNumber = int.Parse(textBox1.Text);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Некоректно введений параметр номер кабінету");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            return;
                        }

                        if (string.IsNullOrEmpty(name))
                        {
                            MessageBox.Show("Ви пропустили поле назва відділення!");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            return;
                        }


                        MedizinDepartment md = new MedizinDepartment();
                        md.Name = name;
                        md.CabinetNumber = cabinetNumber;
                        context.departments.Add(md);
                        
                        textBox1.Clear();
                        textBox2.Clear();

                        MessageBox.Show("Запис збережено!");

                        context.SaveChanges();
                        FillDataGried();
                        break; 
                    }
                case "Знайти":
                    {
                        int Id = 0;
                        string name = "";
                        int cabinetNumber = 0;

                        var query = context.departments.AsQueryable();

                        if (!string.IsNullOrEmpty(textBox2.Text))
                        {
                            try
                            {
                                Id = int.Parse(textBox2.Text);
                                query = query.Where((MedizinDepartment md) => md.Id == Id);
                            }
                            catch
                            {
                                MessageBox.Show("Неправильно введено поле ідентифікатора!");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                return;
                            }

                        }
                        if (!string.IsNullOrEmpty(textBox1.Text))
                        {
                            name = textBox1.Text;
                            query = query.Where((MedizinDepartment md) => md.Name == name);
                        }
                        if (!string.IsNullOrEmpty(textBox3.Text))
                        {
                            try
                            {
                                cabinetNumber = int.Parse(textBox3.Text);
                                query = query.Where((MedizinDepartment md) => md.CabinetNumber == cabinetNumber);
                            }
                            catch
                            {
                                MessageBox.Show("Неправильне введено номер кабінету!");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                return;
                            }
                        }

                        dataGridView1.Rows.Clear();
                        dataGridView1.Visible = true;

                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        linkLabel1.Visible = true;

                        foreach (var item in query)
                        {
                            string[] strs =
                            {
                        item.Id.ToString(),
                        item.Name,
                        item.CabinetNumber.ToString()
                    };
                            dataGridView1.Rows.Add(strs);
                        }

                        button3.Visible = false;

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";



                        break;
                    }
                case "Змінити":
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text))
                        {
                            try
                            {
                                int Id = int.Parse(textBox2.Text);
                                var query = context.departments.AsQueryable().Where(el => el.Id == Id);

                                if (query.Count() > 0)
                                {
                                    MedizinDepartment md = query.First();
                                    if (!string.IsNullOrEmpty(textBox1.Text))
                                    {
                                        md.Name = textBox1.Text;
                                    }
                                    if (!string.IsNullOrEmpty(textBox3.Text))
                                    {
                                        try
                                        {
                                            md.CabinetNumber = int.Parse(textBox3.Text);
                                        }
                                        catch
                                        {
                                            throw new Exception();
                                        }
                                    }

                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Не коректно введені дані");
                            }
                            context.SaveChanges();
                            FillDataGried();
                        }
                        else
                        {
                            MessageBox.Show("Не вказано номер елемента");
                        }
                       
                        break;
                    }
                case "Видалити":
                    {
                        if (!string.IsNullOrEmpty(textBox2.Text))
                        {
                            try
                            {
                                int Id = int.Parse(textBox2.Text);
                                var element = context.departments.AsQueryable().Where(el => el.Id == Id);
                                if (element.Count() > 0)
                                {
                                    context.departments.Remove(element.First());
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Некоректно введені дані!");
                            }
                            context.SaveChanges();
                            FillDataGried();
                        }
                        else
                        {
                            MessageBox.Show("Заповніть поле!");
                        }

                       
                        break;
                    }
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            linkLabel1.Visible = true;

            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangeVis(false,true);
            FillDataGried();
            dataGridView1.Visible = true;
            textBox3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeVis(true, false);
            dataGridView1.Visible = false;
            textBox3.Visible = true;

            textBox2.PlaceholderText = "Ідентифікатор";
            textBox1.PlaceholderText = "Назва відділу";
            textBox3.PlaceholderText = "номер кабінету";

            button3.Text = "Знайти";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeVis(true, false);
            dataGridView1.Visible = false;
            textBox3.Visible = true;

            textBox2.PlaceholderText = "Ідентифікатор";
            textBox1.PlaceholderText = "Назва відділу";
            textBox3.PlaceholderText = "номер кабінету";

            button3.Text = "Змінити";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeVis(true, false);
            dataGridView1.Visible = false;
            textBox3.Visible = false;
            textBox1.Visible = false;

            textBox2.PlaceholderText = "Ідентифікатор";

            button3.Text = "Видалити";
        }
    }
}
