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
        public Form1()
        {
            InitializeComponent();
        }
        Button[] btn1 = new Button[25], btn2 = new Button[25];   //玩家Button陣列  //電腦Button陣列
        Random rd = new Random();   //亂數
        int count = 0,column = 0,row = 0;         //計算Button位置 //計算Button在那一行 //計算Button在那一列                    
        int[] tmp = new int[25],tmpp = new int[25];      //紀錄玩家點下的按鈕的值 //紀錄電腦選擇的按鈕的值 
        bool fg = false;  //初始化旗標  
        bool getlineP,getlineC;   //是否已連線 
        //int[] H = new int[5], V = new int[5]; int HL = 0, VL = 0;//智慧

        private void button1_Click(object sender, EventArgs e)   //遊戲重新
        {
            reset();
        }                                                 

        private void btset()  //Button動態陣列設置
        {
            for (int i = 0; i < 25; i++)
            {
                Button newBtn1 = new Button();
                newBtn1.Location = new Point(12 + (column * 75), 75 + ((row - 1) * 75));
                newBtn1.Text = (count + 1).ToString();
                newBtn1.Size = new Size(75, 75);
                newBtn1.Click +=new EventHandler(clk);
                newBtn1.Name = i.ToString();
                btn1[i] = newBtn1;
                this.Controls.Add(newBtn1);
                count += 1;
                column += 1;
                if (count % 5 == 0)
                {
                    column = 0;
                    row += 1;
                }
            }
            for (int i = 0; i < 25; i++)
            {
                Button newBtn2 = new Button();
                newBtn2.Location = new Point(600 + (column * 75), -300+ ((row - 1) * 75));
                newBtn2.Text = (count + 1).ToString();
                newBtn2.Size = new Size(75, 75);
                newBtn2.Click += new EventHandler(clk);
                newBtn2.Name = i.ToString();
                btn2[i] = newBtn2;             
                this.Controls.Add(newBtn2);
                count += 1;
                column += 1;
                if (count % 5 == 0)
                {
                    column = 0;
                    row += 1;
                }
                btn2[i].Enabled = false;
            }
            fg = true;
        } 
        private void getRandom()   //產生1~25不重覆亂數  
        {
            int[] rnd = new int[25];
            int[] rndd = new int[25];

            for (int i = 0; i < 25; i++)
            {
                rnd[i] = rd.Next(1, 26);
                for (int j = 0; j < i; j++)
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
                btn1[i].Text = rnd[i].ToString();
                btn2[i].Text = rndd[i].ToString();
            }
        }                                   
        private void btnRe()  //重設button
        {
            for (int i = 0; i < 25; i++)
            {
                btn1[i].BackColor = default(Color);
                btn1[i].Enabled = true;
                btn1[i].UseVisualStyleBackColor = true;
                btn2[i].BackColor = default(Color);
                btn2[i].UseVisualStyleBackColor = true;
                tmp[i] = 0;
                tmpp[i] = 0;
            }
            //timer1.Enabled = false;
        }                                                  

        void clk(object sender, EventArgs e)  //按鈕點擊判斷
        {
            for (int i = 0; i < 25; i++)
            {
                if (btn2[i].Text == ((Button)sender).Text)
                {
                    btn2[i].BackColor = Color.GreenYellow;
                    btn2[i].Text = "X";
                    tmpp[Convert.ToInt32(btn2[i].Name)] = 2;
                }
            }
            if (!getlineP)
            {
                Button btn = (Button)sender;
                btn.Enabled = false;
                btn.Text = "O";
                btn.BackColor = Color.Gold;
                tmp[Convert.ToInt32(btn.Name)] = 1;
                getlineP = cpl();
                getlineC = ccl();
                win();
            }
            timer1.Enabled = true;
        }
        void win()    //&獲勝判斷
        {
            if (getlineP && getlineC)
            {
                MessageBox.Show("平手");
                reset();
            }
            if (getlineC)
            {
                MessageBox.Show("電腦獲勝");
                reset();
            }
            if (getlineP)
            {
                MessageBox.Show("玩家獲勝");
                reset();
            }
        }
        void com()   //電腦選擇 
        {
            /*for(int i = 0;i < 25;i++)
            while (btn2[i].Text=="X")
            {
                for(int j = 0;j < 5;j++)
                {
                    if(btn2[i].Text=="x")
                    {
                        for (int k = 0; k < 5; k++)
                        {
                           //btn2[k + j * 5].Text = ;
                        }
                    }
                }
            }*/
            

        /*  //智障電腦
            int rnd = rd.Next(0, 25);
            while (btn2[rnd].Text == "X")
            {
                rnd = rd.Next(0, 25);
            }
            for (int i = 0; i < 25; i++)
            {
                if (btn1[i].Text == btn2[rnd].Text)
                {
                    btn1[i].Text = "O";
                    tmp[Convert.ToInt32(btn1[i].Name)] = 1;
                    btn1[i].BackColor = Color.Gold;
                    btn1[i].Enabled = false;
                }
            }
            btn2[rnd].Text = "X";
            tmpp[Convert.ToInt32(btn2[rnd].Name)] = 2;
            btn2[rnd].BackColor = Color.GreenYellow;
            btn2[rnd].Enabled = false;
            win();
        */
        }   

        private bool cpl()   //玩家連線判斷
        {
            int PTL = 0;  //玩家總連線數             
            bool lineP = false;   //是否已達成3條連線  
                     
            for (int i = 0; i < 5; i++)
            {
                if (tmp[0 + i * 5].ToString() + tmp[1 + i * 5].ToString() + tmp[2 + i * 5].ToString() + tmp[3 + i * 5].ToString() + tmp[4 + i * 5].ToString() == "11111")//判斷橫五公格
                {
                    for (int j = 0; j < 5; j++)
                    {
                        btn1[j + i * 5].BackColor = Color.Pink;
                    }
                    PTL += 1;
                }
            }             
            for (int i = 0; i < 5; i++)
            {
                if (tmp[i].ToString() + tmp[5 + i].ToString() + tmp[10 + i].ToString() + tmp[15 + i].ToString() + tmp[20 + i].ToString() == "11111")//判斷直五公格 
                {
                    for (int j = 0; j < 5; j++)
                    {
                        btn1[j * 5 + i].BackColor = Color.Pink;
                    }
                    PTL += 1;
                }
            }
            if (tmp[0].ToString() + tmp[6].ToString() + tmp[12].ToString() + tmp[18].ToString() + tmp[24].ToString() == "11111")//判斷斜線
            {
                for (int j = 0; j < 5; j++)
                {
                    btn1[j * 6].BackColor = Color.Pink;
                }
                PTL += 1;
            }           
            if (tmp[4].ToString() + tmp[8].ToString() + tmp[12].ToString() + tmp[16].ToString() + tmp[20].ToString() == "11111")//判斷斜線
            {
                for (int j = 1; j < 6; j++)
                {
                    btn1[j * 4].BackColor = Color.Pink;
                }
                PTL += 1;
            }             
            if (PTL >= 3)  //是否達成3連線
                lineP = true;
            return lineP;
        }
        private bool ccl()   //電腦連線判斷
        {
            int CTL = 0;  //電腦總連線數            
            bool lineC = false;  //是否已達成3條連線 

            for (int i = 0; i < 5; i++)
            {
                if (tmpp[0 + i * 5].ToString() + tmpp[1 + i * 5].ToString() + tmpp[2 + i * 5].ToString() + tmpp[3 + i * 5].ToString() + tmpp[4 + i * 5].ToString() == "22222")//判斷橫五公格  
                {
                    for (int j = 0; j < 5; j++)
                    {
                        btn2[j + i * 5].BackColor = Color.Purple;
                    }
                    CTL += 1;
                }
            }          
            for (int i = 0; i < 5; i++)
            {
                if (tmpp[i].ToString() + tmpp[5 + i].ToString() + tmpp[10 + i].ToString() + tmpp[15 + i].ToString() + tmpp[20 + i].ToString() == "22222")//判斷直五公格  
                {
                    for (int j = 0; j < 5; j++)
                    {
                        btn2[j * 5 + i].BackColor = Color.Purple;
                    }
                    CTL += 1;
                }
            }          
            if (tmpp[0].ToString() + tmpp[6].ToString() + tmpp[12].ToString() + tmpp[18].ToString() + tmpp[24].ToString() == "22222") //判斷斜線
            {
                for (int j = 0; j < 5; j++)
                {
                    btn2[j * 6].BackColor = Color.Purple;
                }
                CTL += 1;
            }            
            if (tmpp[4].ToString() + tmpp[8].ToString() + tmpp[12].ToString() + tmpp[16].ToString() + tmpp[20].ToString() == "22222")//判斷斜線
            {
                for (int j = 1; j < 6; j++)
                {
                    btn2[j * 4].BackColor = Color.Purple;
                }
                CTL += 1;
            }          
            if (CTL >= 3)    //是否達成3連線  
                lineC = true;
            return lineC;
        }

        private void reset()
        {
            getlineP = false;
            getlineC = false;
            if (!fg)
                btset();
            else
                btnRe();
            getRandom();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!getlineP||!getlineC)
            {
                com();
            }
            timer1.Enabled = false;
        }
    }
}