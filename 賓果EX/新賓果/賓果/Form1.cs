using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 賓果
{
    public partial class Form1 : Form
    {
        Button[] btn1, btn2;     //玩家Button陣列  //電腦Button陣列
        Random rd;    //亂數
        int[] tmp, tmpp,CP, CC, btnx;    //紀錄玩家點下的按鈕的值 //紀錄電腦選擇的按鈕的值 //記錄點下的按鈕        
        int LP, cpl, ccl;
        int[] H, V;  //連線
        int HL, VL, SL, SR; //智能電腦
        bool PC,over;   //判斷目前點擊方

        public Form1()
        {
            InitializeComponent();
            Re();
        }

        private void Button1_click(object sender, EventArgs e)    //開始遊戲
        {
            timer1.Enabled = true;btset();
            button1.Visible = false;
            over = false; PC = false;
        }
        void Re()
        {
            btn1 = new Button[25]; btn2 = new Button[25];
            rd = new Random();
            tmp = new int[25]; tmpp = new int[25]; btnx = new int[25];
            CP = new int[25]; CC = new int[25];
            LP = 0; cpl = 0; ccl = 0;
            H = new int[5]; V = new int[5];
            HL = 0; VL = 0;PC = false; over = false;
        }
        void Reset()  //重製
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            timer1.Enabled = false;
            button1.Visible = true;
            for (int i = 0; i < btn1.Length; i++)
            {
                flowLayoutPanel1.Controls.Remove(btn1[i]);
                flowLayoutPanel2.Controls.Remove(btn2[i]);
            }
            Re();
        }
        void btset()     //Button動態陣列設置
        {
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = true;
            getRandom();
            for (int i = 0; i < btn1.Length; i++)
            {
                btn1[i] = new Button
                {
                    Tag = i,Text = btnx[i].ToString(),
                    BackColor = Color.LightGray,Width = 70, Height = 70,
                    Location = new Point(10, 30 * i)
                };
                btn1[i].Click += new EventHandler(Pclick);
            }
            flowLayoutPanel1.Controls.AddRange(btn1);
            getRandom();
            for (int i = 0; i < btn2.Length; i++)
            {
                btn2[i] = new Button
                {
                    Tag = i,
                    Text = btnx[i].ToString(),
                    BackColor = Color.LightGray,Width = 70, Height = 70,
                    Location = new Point(10, 30 * i)
                };
            }
            flowLayoutPanel2.Controls.AddRange(btn2);
        }

        void getRandom()  //亂數
        {
            for (int i = 0; i < 25; i++)
            {
                btnx[i] = i + 1;
                tmp[i] = 0;tmpp[i] = 0;
                CP[i] = 0;CC[i] = 0;
            }
            int temp;
            for (int i = 0; i < 25; i++)
            {
                int R = rd.Next(25);
                temp = btnx[i];
                btnx[i] = btnx[R];btnx[R] = temp;
            }
        }
        void Pclick(object sender, EventArgs e)  //玩家點擊
        {
            if (!PC && timer1.Enabled == true)
            {
                LP = Convert.ToInt32(((sender as Button).Tag).ToString());
                btn1[LP].Enabled = false;
                btn1[LP].BackColor = Color.Gold;
                tmp[LP] = 1;
                link(btn1, tmp, CP); 
                for (int i = 0; i < btn2.Length; i++)
                {
                    if (over==false)
                    {
                        if (btn2[i].Text == btn1[LP].Text)
                        {
                            btn2[i].BackColor = Color.Gold;
                            tmpp[i] = 1; PC = true; link(btn2, tmpp, CC);
                            CC[i] = 1; 
                        }
                    }
                }win();
                PC = true; SMC(PC);win();
            }
        }
        void C(bool Jud, int c)//電腦判斷
        {
            if (Jud && timer1.Enabled == true)
            {
                if (btn2[c].BackColor == Color.LightGray)
                {
                    btn2[c].BackColor = Color.GreenYellow;
                    tmpp[c] = 1;
                    link(btn2, tmpp, CC); 
                    for (int i = 0; i < btn1.Length; i++)
                    {
                        if (over==false)
                        {
                            if (btn1[i].Text == btn2[c].Text)
                            {
                                btn1[i].BackColor = Color.GreenYellow;
                                btn1[i].Enabled = false;
                                tmp[i] = 1;PC = false;
                                link(btn1, tmp, CP);
                                CP[i] = 1;
                            }
                        }
                    }win();
                }
                else
                {
                    PC = true;
                    SMC(PC);
                }
                win();
            }
        }
        void link(Button[] bt, int[] cli, int[] ck)//連線判斷
        {
            Horizontal(bt, cli, ck);Vertical(bt, cli, ck);
            SlashL(bt, cli, ck);SlashR(bt, cli, ck);
        }
        void line()
        {
            if (PC) ccl += 1;
            if (!PC) cpl += 1;
        }
        void win()  //獲勝判斷
        {
            if (ccl >= 3 && cpl < 3)
            {
                MessageBox.Show("電腦獲勝");
                Reset();
                over = true;
            }
            if (cpl >= 3 && ccl < 3)
            {
                MessageBox.Show("玩家獲勝");
                Reset();
                over = true;
            }
            if (ccl == 3 && cpl == 3)
            {
                MessageBox.Show("平手");
                Reset();
                over = true;
            }
        }
        void Horizontal(Button[] but, int[] cli, int[] ck)  //橫排
        {
            for (int i = 0; i < 5; i++)
            {
                if (cli[0 + i * 5] + cli[1 + i * 5] + cli[2 + i * 5] + cli[3 + i * 5] + cli[4 + i * 5] == 5
                    && ck[0 + i * 5] + ck[1 + i * 5] + ck[2 + i * 5] + ck[3 + i * 5] + ck[4 + i * 5] < 5)
                {
                    
                    for (int j = 0; j < 5; j++)
                    {
                        but[j + i * 5].BackColor = Color.Purple;
                        ck[j + i * 5] = 1; 
                    }
                    line();
                }
            }
        }
        void Vertical(Button[] bt, int[] cli, int[] ck)  //直排
        {
            for (int i = 0; i < 5; i++)
            {
                if (cli[i] + cli[5 + i] + cli[10 + i] + cli[15 + i] + cli[20 + i] == 5
                    && ck[i] + ck[5 + i] + ck[10 + i] + ck[15 + i] + ck[20 + i] < 5)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        bt[j * 5 + i].BackColor = Color.Purple;
                        ck[j * 5 + i] = 1;
                    }
                    line();
                }
            }
        }
        void SlashL(Button[] but, int[] cli, int[] ck)   //右斜
        {
            if (cli[0] + cli[6] + cli[12] + cli[18] + cli[24] == 5 && ck[0] + ck[6] + ck[12] + ck[18] + ck[24] < 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    but[j * 6].BackColor = Color.Purple;
                    ck[j * 6] = 1;
                }
                line();
            }
        }
        void SlashR(Button[] but, int[] cli, int[] ck)   //左斜
        {
            if (cli[4] + cli[8] + cli[12] + cli[16] + cli[20] == 5 && ck[4] + ck[8] + ck[12] + ck[16] + ck[20] < 5)
            {
                for (int j = 1; j < 6; j++)
                {
                    but[j * 4].BackColor = Color.Purple;
                    ck[j * 4] = 1;
                }
                line();
            }
        }
        void SMC(bool SC)  //智能電腦
        {
            if (SC && timer1.Enabled == true)
            {
                for (int i = 0; i < H.Length; i++)
                {
                    H[i] = tmpp[0 + i * 5] + tmpp[1 + i * 5] + tmpp[2 + i * 5] + tmpp[3 + i * 5] + tmpp[4 + i * 5];
                    V[i] = tmpp[i] + tmpp[5 + i] + tmpp[10 + i] + tmpp[15 + i] + tmpp[20 + i];
                    SL = tmpp[0] + tmpp[6] + tmpp[12] + tmpp[18] + tmpp[24];SR = tmpp[4] + tmpp[8] + tmpp[16] + tmpp[18] + tmpp[20];
                }
                HL = More(H);
                VL = More(V);

                if (H[HL] > V[VL]|| H[HL] == V[VL])
                {
                    if(SL > H[HL])
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (tmpp[i * 6] == 0)
                            {
                                C(SC, i * 6);
                                break;
                            }
                        }
                    }
                    if (SR > H[HL])
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (tmpp[i * 4] == 0)
                            {
                                C(SC, i * 4);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (tmpp[i + HL * 5] == 0)
                            {
                                C(SC, i + HL * 5);
                                break;
                            }
                        }
                    }
                }
                if (V[VL] > H[HL])
                {
                    if (SL > V[VL])
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (tmpp[i * 6] == 0)
                            {
                                C(SC, i * 6);
                                break;
                            }
                        }
                    }
                    if (SL > V[VL])
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (tmpp[i * 4] == 0)
                            {
                                C(SC, i * 4);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                           if (tmpp[VL + i * 5] == 0)
                           {
                              C(SC, VL + i * 5);
                              break;
                           }
                        }
                    }
                   
                }
            }
        }
        int More(int[] m)//判斷直橫哪行多
        {
            int L = 0, M = 0;
            for (int i = 0; i < m.Length; i++)
            {
                if (M < m[i] && m[i]<5)
                {
                    M = m[i];
                    L = i;
                }
            }
            return L;
        }
    }
}
