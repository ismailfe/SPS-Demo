using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IDSQL;

namespace Demo_Otomasyon_Sistemi
{
    public partial class Form1 : Form
    {
        SQL SQL = new SQL();
        string SQLServerName = "SQLEXPRESS";
        string DBName = "DBChart";
        string TableName = "tb1";
        string[] TableColumn = new string[4];



    public Form1()
        {
            InitializeComponent();

            //var myImage = new NamedImage("my_image_key", Properties.Resources.line2);
            //chart2.Images.Add(myImage);
            ////  mySeries.MarkerImage = "my_image_key";
            //Image aa = Properties.Resources.line2;
            //chart2.Series[2].MarkerImage = aa;


            SQL.ConnectionPath = "Data Source=.\\" + SQLServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            TableColumn[0] = "C0";
            TableColumn[1] = "C1";
            TableColumn[2] = "C2";
            TableColumn[3] = "C3";
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Depo_Yerleşimi DepoYer = new Depo_Yerleşimi();
            DepoYer.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ÜretimTakip UrtTakip = new ÜretimTakip();
            UrtTakip.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_Layout Form_Layout = new Form_Layout();
            Form_Layout.Show();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void B_CanEgrisiCIZ_Click(object sender, EventArgs e)
        {
            //Random a = new Random();
            //int rnd = a.Next(1 - 100);
            string cmd = "SELECT RAND()*(10-5)+5";
            string cmd1 = "Delete tb2";
            string cmd2 = "DELETE tb2 DECLARE @sayac1 INT = 1 WHILE @sayac1< " + Adt.Text + "BEGIN insert INTO tb2(x) VALUES(RAND() * (" + lmtU.Text + "-" + lmtA.Text + ") +" + lmtA.Text + " );  SET @sayac1 += 1 END";
            string cmd3 = "DECLARE @std float " +
                "DECLARE @avg float " +
                "DECLARE @cnt float " +
                "DECLARE @min float " +
                "DECLARE @max float " +
                "SELECT  @std = STDEV(x) from tb2 " +
                "SELECT @avg = AVG(x) From tb2 " +
                "SELECT @cnt = COUNT(x) From tb2 " +
                "SELECT @min = MIN(x) From tb2 " +
                "SELECT @max = MAX(x) From tb2 " +
                "UPDATE tb3 SET std = @std, avg = @avg, cnt = @cnt, min = @min, max = @max SELECT* FROM tb3";


            string a = SQL.Open_CMD_ResultinDGV(cmd2, dataGridView1);
            string B = SQL.Open_CMD_ResultinDGV(cmd3, dataGridView2);

            float std = float.Parse(dataGridView2.Rows[0].Cells[1].Value.ToString());
            float avg = float.Parse(dataGridView2.Rows[0].Cells[2].Value.ToString());
            int cnt = int.Parse(dataGridView2.Rows[0].Cells[3].Value.ToString());
            int min = int.Parse(dataGridView2.Rows[0].Cells[4].Value.ToString());
            int max = int.Parse(dataGridView2.Rows[0].Cells[5].Value.ToString());
            GrafikCiz(std, avg, cnt, min, max);
        }






        public void GrafikCiz(float std, float avg, int cnt, int min, int max)
        {
            float mean = avg; // float.Parse(txtMean.Text);
            float stddev = std; // float.Parse(txtStdDev.Text);
            float stddevKaresi = stddev * stddev;
            float one_over_2pi = (float)(1.0 / (stddev * Math.Sqrt(2 * Math.PI)));

            float[] NDis = new float[cnt];
            float[] Yex = new float[cnt];
            float Enbuyuk = 0;

            ch.Series[0].Points.Clear();
            ch.ChartAreas[0].RecalculateAxesScale();
            ch.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ch.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            ch.ChartAreas[0].AxisX.Maximum = avg * 2; // 100;
            ch.ChartAreas[0].AxisX.Minimum = 0; // (avg/2) * -1; // - 20;

            //ch.ChartAreas[0].AxisY.Minimum = int.Parse(lmtA.Text); // - 20;


            int Carpan = 2;

            if ((cnt / 20) > 15)
            {
                Carpan = 30;
            }

            else if ((cnt / 20) < 2)
            {
                Carpan = 5;
            }
            else if ((cnt / 20) <= 15 && (cnt / 20) >= 2)
            {
                Carpan = (cnt / 20);
            }
            else
            {
                Carpan = 3;
            }

            for (int i = 0; i < cnt - 1; i++)
            {
                if (i == 0)
                {
                    NDis[i] = mean - (stddev * (Carpan)); //15
                }
                else
                {
                    NDis[i] = (stddev / (Carpan)) + NDis[i - 1];
                }

                Yex[i] = F(NDis[i], one_over_2pi, mean, stddev, stddevKaresi);

                ch.Series[0].Points.AddXY(NDis[i], Yex[i]);


                if (NDis[i] < int.Parse(lmtA.Text) && NDis[i] > ( 1 - (int.Parse(lmtA.Text) ) )  )
                {
                    ch.ChartAreas[0].AxisY.Minimum = Yex[i];
                }

                if(i == cnt - 2)
                {

                }

                if (Enbuyuk < Yex[i])
                {
                    Enbuyuk = Yex[i];
                }
         
            }




            ch.Series[1].Points.Clear();
            ch.Series[1].Points.AddXY(avg + 10, 0);
            ch.Series[1].Points.AddXY(avg + 10, Enbuyuk);

            //min
            ch.Series[2].Points.Clear();
            ch.Series[2].Points.AddXY(min, 0);
            ch.Series[2].Points.AddXY(min, Enbuyuk);

            //max
            ch.Series[3].Points.Clear();
            ch.Series[3].Points.AddXY(max, 0);
            ch.Series[3].Points.AddXY(max, Enbuyuk);

            //max
            ch.Series[4].Points.Clear();
            ch.Series[4].Points.AddXY(avg, 0);
            ch.Series[4].Points.AddXY(avg, Enbuyuk);
        }


        // The normal distribution function.
        private float F(float x, float one_over_2pi, float mean, float stddev, float stddevKaresi)
        {
            return (float)(one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * stddevKaresi)));
        }
















    }
}
