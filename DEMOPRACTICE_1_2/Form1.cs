using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;

namespace DEMOPRACTICE_1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SQLtoDB("SELECT * FROM products ORDER BY id");
        }

        NpgsqlConnection connString = new NpgsqlConnection("Host=localhost;Port=5432;Database=shop;Username=postgres;Password=1234");
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

        public void Chart1()
        {
            string sql = "SELECT * FROM products ORDER BY id";
            connString.Open();
            NpgsqlCommand command = new NpgsqlCommand(sql, connString);
            NpgsqlDataAdapter dataAd = new NpgsqlDataAdapter(sql, connString);
            informationFromDB.Reset();
            dataAd.Fill(informationFromDB);
            infTable = informationFromDB.Tables[0];
            connString.Close();

            int firstCategory = 0;
            int secondCategory = 0;
            int thirdCategory = 0;

            for (int i = 0; i < infTable.Rows.Count; i++)
            {
                double x = Convert.ToDouble(infTable.Rows[i][2]);
                if (x < 1000)
                {
                    firstCategory++;
                    continue;
                }
                else if (x >= 1000 && x < 2000)
                {
                    secondCategory++;
                    continue;
                }
                else if (x >= 2000)
                {
                    thirdCategory++;
                    continue;
                }
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(1, firstCategory);
            chart1.Series[0].Points.AddXY(2, secondCategory);
            chart1.Series[0].Points.AddXY(3, thirdCategory);

        }

        public void Chart2()
        {
            string sql = "SELECT * FROM products ORDER BY id";
            connString.Open();
            NpgsqlCommand command = new NpgsqlCommand(sql, connString);
            NpgsqlDataAdapter dataAd = new NpgsqlDataAdapter(sql, connString);
            informationFromDB.Reset();
            dataAd.Fill(informationFromDB);
            infTable = informationFromDB.Tables[0];
            connString.Close();

            int firstCategory = 0;
            int secondCategory = 0;
            int thirdCategory = 0;

            for (int i = 0; i < infTable.Rows.Count; i++)
            {
                double x = Convert.ToDouble(infTable.Rows[i][2]);
                if (x < 1000)
                {
                    firstCategory++;
                    continue;
                }
                else if (x >= 1000 && x < 2000)
                {
                    secondCategory++;
                    continue;
                }
                else if (x >= 2000)
                {
                    thirdCategory++;
                    continue;
                }
            }
            chart2.Series[0].Points.Clear();
            chart2.Series[0].Points.AddXY(1, firstCategory);
            chart2.Series[0].Points.AddXY(2, secondCategory);
            chart2.Series[0].Points.AddXY(3, thirdCategory);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                SQLtoDB("SELECT * FROM products ORDER BY id");
            }
            if (tabControl1.SelectedIndex == 1)
            {
                Chart1();
                Chart2();
            }
        }
    }
}