using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random rd = new Random();
        Button[] btn = new Button[25];
        Button[] btn1 = new Button[25];
        int[] tmp = new int[25];
        int test = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btset();
            rom();
        }

        void  btset() //動態陣列
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    btn[i * 5 + j] = new Button();
                    btn[i * 5 + j].Size = new Size(35, 35);
                    btn[i * 5 + j].Location = new Point((100 + 40 * j), (100 + 40 * i));
                    test++;
                    label1.Text = test.ToString();
                    Controls.Add(btn[i * 5 + j]);
                    btn1[i * 5 + j] = new Button();
                    btn1[i * 5 + j].Size = new Size(35, 35);
                    btn1[i * 5 + j].Location = new Point((700 + 40 * j), (100 + 40 * i));
                    Controls.Add(btn1[i * 5 + j]);
                }
            }
        }

        void rom() //亂數
        {
            int[] rnd = new int[25];
            int[] rndd = new int[25];

            for (int i = 0; i < 25; i++)
            {
                rnd[i] = rd.Next(1,26);
                for(int j = 0; j < i; j++)
                {
                    while (rnd[i] == rnd[j])
                    {
                        rnd[i] = rd.Next(1, 26);
                        j = 0;
                    }
                }

                rndd[i] = rd.Next(1, 26);
                for (int j = 0; j < i; j++)
                {
                    while (rndd[i] == rndd[j])
                    {
                        rndd[i] = rd.Next(1, 26);
                        j = 0;
                    }
                }

            }
            for (int i = 0; i < 25; i++)
            {
                btn[i].Text = rnd[i].ToString();
                btn1[i].Text = rndd[i].ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
