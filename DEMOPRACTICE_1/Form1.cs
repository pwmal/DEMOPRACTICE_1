using Npgsql;
using System.Data;

namespace DEMOPRACTICE_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SQLtoDB("SELECT * FROM contact ORDER BY id");
        }

        NpgsqlConnection connString = new NpgsqlConnection("Host=localhost;Port=5432;Database=Contacts;Username=postgres;Password=1234");
        private DataSet informationFromDB = new DataSet();
        private DataTable infTable = new DataTable();

        public void SQLtoDB(string sql)
        {
            connString.Open();
            NpgsqlCommand command = new NpgsqlCommand(sql, connString);
            NpgsqlDataAdapter dataAd = new NpgsqlDataAdapter(sql, connString);
            informationFromDB.Reset();
            dataAd.Fill(informationFromDB);
            infTable = informationFromDB.Tables[0];
            dataGridView1.DataSource = infTable;
            connString.Close();
        }

        public void SQLtoDBwithChanges(string sql)
        {
            connString.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql, connString);
            comm.ExecuteNonQuery();
            connString.Close();
            SQLtoDB("SELECT * FROM contact ORDER BY id");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    SQLtoDBwithChanges($"INSERT INTO contact (name, phone_number) VALUES ('{textBox1.Text}', {textBox2.Text});");
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != "")
                {
                    SQLtoDBwithChanges($"DELETE FROM contact WHERE id = {textBox3.Text};");
                }
                textBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SQLtoDBwithChanges($"UPDATE contact SET name = '{textBox5.Text}', phone_number = '{textBox6.Text}' WHERE id = {textBox4.Text}");
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox4.ReadOnly = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                label8.Visible = false;
                label6.Visible = false;
                button3.Visible = false;
                button6.Visible = false;
                SQLtoDB("SELECT * FROM contact ORDER BY id");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    list.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }

                if (textBox4.Text != "" && list.Contains(textBox4.Text))
                {
                    textBox4.ReadOnly = true;
                    textBox5.Visible = true;
                    textBox6.Visible = true;
                    label8.Visible = true;
                    label6.Visible = true;
                    button3.Visible = true;
                    button6.Visible = true;

                    string sql = $"SELECT * FROM contact WHERE id = {textBox4.Text}";
                    connString.Open();
                    NpgsqlCommand command = new NpgsqlCommand(sql, connString);
                    NpgsqlDataAdapter dataAd = new NpgsqlDataAdapter(sql, connString);
                    informationFromDB.Reset();
                    dataAd.Fill(informationFromDB);
                    infTable = informationFromDB.Tables[0];
                    textBox5.Text = infTable.Rows[0][1].ToString();
                    textBox6.Text = infTable.Rows[0][2].ToString();
                    connString.Close();
                    SQLtoDB("SELECT * FROM contact ORDER BY id");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox4.ReadOnly = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label8.Visible = false;
            label6.Visible = false;
            button3.Visible = false;
            button6.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text != "")
                {
                    SQLtoDB($"SELECT * FROM contact WHERE name LIKE '%{textBox7.Text}%'");
                }
                button7.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SQLtoDB("SELECT * FROM contact ORDER BY id");
            textBox7.Text = "";
            button7.Visible = false;
        }
    }
}