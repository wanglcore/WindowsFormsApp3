﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        const int N = 4;
        Button[,] buttons = new Button[N, N]; //按钮的数组
        public Form1()
        {
           

            InitializeComponent();
        }
            private void Form1_Load_1(object sender, EventArgs e)
            {
                GenerateAllButtons();
            }
            private void button1_Click(object sender, EventArgs e)
            {
                Shuffle();
            }
            void Shuffle()
            {
                //多次随机交换两个按钮
                Random rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int a = rnd.Next(N);
                    int b = rnd.Next(N);
                    int c = rnd.Next(N);
                    int d = rnd.Next(N);
                    Swap(buttons[a, b], buttons[c, d]);
                }
            }
            void GenerateAllButtons()
            {
                int x0 = 100, y0 = 10, w = 45, d = 50;
                for (int r = 0; r < N; r++)
                    for (int c = 0; c < N; c++)
                    {
                        int num = r * N + c;
                        Button btn = new Button();
                        btn.Text = (num + 1).ToString();
                        btn.Top = y0 + r * d;
                        btn.Left = x0 + c * d;
                        btn.Width = w;
                        btn.Height = w;
                        btn.Visible = true;
                        btn.Tag = r * N + c; //这个数据用来表示它所在行列位置

                        //注册事件
                        btn.Click += new EventHandler(btn_Click);

                        buttons[r, c] = btn; //放到数组中
                        this.Controls.Add(btn); //加到界面上
                    }
                buttons[N - 1, N - 1].Visible = false; //最后一个不可见
            }
            void Swap(Button btna, Button btnb)
            {
                string t = btna.Text;
                btna.Text = btnb.Text;
                btnb.Text = t;

                bool v = btna.Visible;
                btna.Visible = btnb.Visible;
                btnb.Visible = v;
            }
            void btn_Click(object sender, EventArgs e)
            {
                Button btn = sender as Button; //当前点中的按钮
                Button blank = FindHiddenButton(); //空白按钮

                //判断是否与空白块相邻，如果是，则交换
                if (IsNeighbor(btn, blank))
                {
                    Swap(btn, blank);
                    blank.Focus();
                }

                //判断是否完成了
                if (ResultIsOk())
                {
                    MessageBox.Show("ok");
                }
            }
            Button FindHiddenButton()
            {
                for (int r = 0; r < N; r++)
                    for (int c = 0; c < N; c++)
                    {
                        if (!buttons[r, c].Visible)
                        {
                            return buttons[r, c];
                        }
                    }
                return null;
            }
            bool IsNeighbor(Button btnA, Button btnB)
            {
                int a = (int)btnA.Tag; //Tag中记录是行列位置
                int b = (int)btnB.Tag;
                int r1 = a / N, c1 = a % N;
                int r2 = b / N, c2 = b % N;

                if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //左右相邻
                                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                    return true;
                return false;
            }

            //检查是否完成
            bool ResultIsOk()
            {
                for (int r = 0; r < N; r++)
                    for (int c = 0; c < N; c++)
                    {
                        if (buttons[r, c].Text != (r * N + c + 1).ToString())
                        {
                            return false;
                        }
                    }
                return true;
            }
        
       
    }
}
