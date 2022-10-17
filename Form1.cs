using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Değişkenler tanımlanıyor
        System.Drawing.Graphics graphicsObj;
        int s1 = 1;
        int a, b, c, itr;
        int[,] k_xy = new int[11,3];
        double[,] u_matrix = new double[11,11];

        int aj,sr,cr,mr,iter;

        int[] tmp10 = new int[11];
        Random r1 = new Random();
        int tmp1,tmp2,tmp3;
        double t1;

        int[,] antijen;
        double[] aj_duy;

        int[,] klon_antijen;

        double best;
        int en_itr;
        

        //tooltipler yükleniyor
        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label3, "İterasyona girecek toplam antijen sayısı!");
            toolTip1.SetToolTip(label4, "Seçilecek en iyi antijenlerin, toplam antijen sayısına oranı!");
            toolTip1.SetToolTip(label5, "Clone sayısı, toplam antijen sayısına oranı!");
            toolTip1.SetToolTip(label6, "Mutasyon oranı!");
            toolTip1.SetToolTip(label7, "İterasyon sayısı");
            toolTip1.SetToolTip(button1, "Sahnede işaretlenen noktaları temizler!");
            toolTip1.SetToolTip(button2, "Rastgele koordinatlar atar!");
            toolTip1.SetToolTip(button4, "Hazır koordinatları yükler!");
        }

        //sahne temizleme butonu
        private void button1_Click(object sender, EventArgs e)
        {
            
            graphicsObj.Clear(System.Drawing.Color.White);
            richTextBox1.Clear();
            s1 = 1;
            //graphicsObj.Clear(System.Drawing.SystemColors.Control);
            //Form1_Click(sender, e);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.Color.White;
        }

        //x - y  koordinatları alınıyor
        protected override void OnMouseMove(MouseEventArgs mouseEv)
        {
            textBox1.Text = mouseEv.X.ToString();
            textBox2.Text = mouseEv.Y.ToString();
        }

        //x- y koordinatlarına göre noktalar atanıyor
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (s1 <= 10)
            {
                base.OnMouseClick(e);
                //textBox1.Text = e.X.ToString();
                //textBox2.Text = e.Y.ToString();

                graphicsObj = this.CreateGraphics();
                Pen myPen = new Pen(System.Drawing.Color.Red, 2);
                //Rectangle myRectangle = new Rectangle(20, 20, 250, 200);
                graphicsObj.DrawEllipse(myPen, e.X, e.Y, 5, 5);
                k_xy[s1, 1] = e.X;
                k_xy[s1, 2] = e.Y;
                graphicsObj.DrawString(s1.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(e.X + 5, e.Y + 5));
                richTextBox1.Text = richTextBox1.Text + "\r\n" + s1.ToString() + ". Koord : " + k_xy[s1, 1].ToString() + "--" + k_xy[s1, 2].ToString();
                s1 = s1 + 1;
                if (s1 == 11) { button3.Enabled = true; }
            }
        }

        //rastgele 10 nokta belirleniyor
        private void button2_Click(object sender, EventArgs e)
        {

            graphicsObj = this.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Red,2);
            Random ran1 = new Random();
            int rx, ry;

            richTextBox1.Text = "Koord -- x -- y";
            for (int i = 1; i <= 10; i++)
            {
                rx = ran1.Next(1,345);
                ry = ran1.Next(1,335);

                k_xy[i, 1] = rx;
                k_xy[i, 2] = ry;

                richTextBox1.Text = richTextBox1.Text + "\r\n" + i.ToString() + ". Koord : " + k_xy[i,1].ToString() + "--" + k_xy[i,2].ToString();
                graphicsObj.DrawEllipse(myPen, rx, ry, 5, 5);
                graphicsObj.DrawString(i.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(rx+5, ry+5));
            }
            button3.Enabled = true;
        }

        //seçim oranı - klon oranı belirleniyor
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Minimum = numericUpDown1.Value;
        }
        
        //hazır koordinatlar yükleniyor
        private void button4_Click(object sender, EventArgs e)
        {
            graphicsObj = this.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Red, 2);
            richTextBox1.Text = "Koord -- x -- y";

            if (comboBox1.SelectedIndex == 0) {
                k_xy[1, 1] = 100; k_xy[1, 2] = 100;
                k_xy[2, 1] = 150; k_xy[2, 2] = 100;
                k_xy[3, 1] = 200; k_xy[3, 2] = 100;
                k_xy[4, 1] = 100; k_xy[4, 2] = 150;
                k_xy[5, 1] = 200; k_xy[5, 2] = 150;
                k_xy[6, 1] = 100; k_xy[6, 2] = 200;
                k_xy[7, 1] = 200; k_xy[7, 2] = 200;
                k_xy[8, 1] = 100; k_xy[8, 2] = 250;
                k_xy[9, 1] = 150; k_xy[9, 2] = 250;
                k_xy[10, 1] = 200; k_xy[10, 2] = 250;
                for (int i = 1; i <= 10; i++) {
                    graphicsObj.DrawEllipse(myPen,k_xy[i,1],k_xy[i,2],5,5);
                    graphicsObj.DrawString(i.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(k_xy[i,1] + 5, k_xy[i,2] + 5));
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + i.ToString() + ". Koord : " + k_xy[i, 1].ToString() + "--" + k_xy[i, 2].ToString();
                }
            }

            if (comboBox1.SelectedIndex == 1)
            {
                k_xy[1, 1] = 100; k_xy[1, 2] = 100;
                k_xy[2, 1] = 200; k_xy[2, 2] = 100;
                k_xy[3, 1] = 75; k_xy[3, 2] = 125;
                k_xy[4, 1] = 225; k_xy[4, 2] = 125;
                k_xy[5, 1] = 50; k_xy[5, 2] = 150;
                k_xy[6, 1] = 250; k_xy[6, 2] = 150;
                k_xy[7, 1] = 75; k_xy[7, 2] = 175;
                k_xy[8, 1] = 225; k_xy[8, 2] = 175;
                k_xy[9, 1] = 100; k_xy[9, 2] = 200;
                k_xy[10, 1] = 200; k_xy[10, 2] = 200;
                for (int i = 1; i <= 10; i++)
                {
                    graphicsObj.DrawEllipse(myPen, k_xy[i, 1], k_xy[i, 2], 5, 5);
                    graphicsObj.DrawString(i.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(k_xy[i, 1] + 5, k_xy[i, 2] + 5));
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + i.ToString() + ". Koord : " + k_xy[i, 1].ToString() + "--" + k_xy[i, 2].ToString();
                }
            }

            if (comboBox1.SelectedIndex == 2)
            {
                k_xy[1, 1] = 100; k_xy[1, 2] = 100;
                k_xy[2, 1] = 150; k_xy[2, 2] = 75;
                k_xy[3, 1] = 200; k_xy[3, 2] = 100;
                k_xy[4, 1] = 75; k_xy[4, 2] = 150;
                k_xy[5, 1] = 225; k_xy[5, 2] = 150;
                k_xy[6, 1] = 75; k_xy[6, 2] = 200;
                k_xy[7, 1] = 225; k_xy[7, 2] = 200;
                k_xy[8, 1] = 100; k_xy[8, 2] = 250;
                k_xy[9, 1] = 150; k_xy[9, 2] = 275;
                k_xy[10, 1] = 200; k_xy[10, 2] = 250;
                for (int i = 1; i <= 10; i++)
                {
                    graphicsObj.DrawEllipse(myPen, k_xy[i, 1], k_xy[i, 2], 5, 5);
                    graphicsObj.DrawString(i.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(k_xy[i, 1] + 5, k_xy[i, 2] + 5));
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + i.ToString() + ". Koord : " + k_xy[i, 1].ToString() + "--" + k_xy[i, 2].ToString();
                }
            }

            if (comboBox1.SelectedIndex == 3)
            {
                k_xy[1, 1] = 100; k_xy[1, 2] = 100;
                k_xy[2, 1] = 150; k_xy[2, 2] = 120;
                k_xy[3, 1] = 200; k_xy[3, 2] = 100;
                k_xy[4, 1] = 120; k_xy[4, 2] = 150;
                k_xy[5, 1] = 180; k_xy[5, 2] = 150;
                k_xy[6, 1] = 120; k_xy[6, 2] = 200;
                k_xy[7, 1] = 180; k_xy[7, 2] = 200;
                k_xy[8, 1] = 100; k_xy[8, 2] = 250;
                k_xy[9, 1] = 150; k_xy[9, 2] = 230;
                k_xy[10, 1] = 200; k_xy[10, 2] = 250;
                for (int i = 1; i <= 10; i++)
                {
                    graphicsObj.DrawEllipse(myPen, k_xy[i, 1], k_xy[i, 2], 5, 5);
                    graphicsObj.DrawString(i.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(k_xy[i, 1] + 5, k_xy[i, 2] + 5));
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + i.ToString() + ". Koord : " + k_xy[i, 1].ToString() + "--" + k_xy[i, 2].ToString();
                }
            }

            button3.Enabled = true;
        }


        //hesaplamalar başlıyor
        public void hesapla()
        { 
            //değerler alınıyor
            aj = Convert.ToInt32(textBox3.Text);
            sr = Convert.ToInt32(numericUpDown1.Value) * aj / 100; 
            cr = Convert.ToInt32(numericUpDown2.Value) * aj / 100;
            mr = Convert.ToInt32(numericUpDown3.Value) / 10;
            iter = Convert.ToInt32(textBox4.Text);
            antijen = new int[aj + 1, 11];
            aj_duy = new double[aj + 1];
            klon_antijen = new int[cr+1,11];
            best = 10000;
            en_itr = 0;

            //uzaklık matrisi oluşturuluyor
            for (a = 1; a <= 10; a++) {
                for (b = 1; b <= 10; b++) { 
                    u_matrix[a,b] = Math.Sqrt(Math.Pow((k_xy[a,1]-k_xy[b,1]),2) + Math.Pow((k_xy[a,2]-k_xy[b,2]),2) );
                    //richTextBox1.Text = richTextBox1.Text + "\r\n" + u_matrix[a, b].ToString();
                }
            }

            //başlangıç antijenleri oluşturuluyor
            for (a = 1; a <= 10; a++) {
                tmp10[a] = a;
            }

            for (b = 1; b <= aj; b++)
            {
                for (a = 1; a <= 10; a++)
                {
                    tmp1 = r1.Next(1, 11);
                    tmp2 = r1.Next(1, 11);
                    tmp3 = tmp10[tmp1];
                    tmp10[tmp1] = tmp10[tmp2];
                    tmp10[tmp2] = tmp3;

                }
                for (a = 1; a <= 10; a++)
                {
                    antijen[b, a] = tmp10[a];
                }
            }


        //iterasyon başlangıcı

            for (itr = 1; itr <= iter; itr++)
            {
                //antijenlerin duyarlılıkları hesaplanıyor
                for (a = 1; a <= aj; a++)
                {
                    aj_duy[a] = 0;  //duyarlılıklar sıfırlandı
                }

                for (b = 1; b <= aj; b++)
                {
                    for (a = 1; a <= 9; a++)
                    {
                        aj_duy[b] = aj_duy[b] + u_matrix[antijen[b, a], antijen[b, a + 1]];
                    }
                    aj_duy[b] = aj_duy[b] + u_matrix[antijen[b, 10], antijen[b, 1]];
                }


                //antijenler sıralanıyor
                for (b = 1; b <= aj - 1; b++)
                {
                    for (a = b + 1; a <= aj; a++)
                    {
                        if (aj_duy[b] > aj_duy[a])
                        {
                            t1 = aj_duy[b];
                            aj_duy[b] = aj_duy[a];
                            aj_duy[a] = t1;

                            for (c = 1; c <= 10; c++)
                            {
                                tmp10[c] = antijen[b, c];
                            }
                            for (c = 1; c <= 10; c++)
                            {
                                antijen[b, c] = antijen[a, c];
                            }
                            for (c = 1; c <= 10; c++)
                            {
                                antijen[a, c] = tmp10[c];
                            }

                        }
                    }
                }

                //minimum iterayon bilgisi alınıyor
                if (aj_duy[1] < best) {
                    best = aj_duy[1];
                    en_itr = itr;
                }
                
                //en iyi antijen(yol) çizdiriliyor
                graphicsObj.Clear(System.Drawing.Color.White);
                graphicsObj = this.CreateGraphics();
                Pen myPen = new Pen(System.Drawing.Color.Red, 2);
                for (a = 1; a <= 10; a++)
                {
                    graphicsObj.DrawEllipse(myPen, k_xy[a, 1], k_xy[a, 2], 5, 5);
                    graphicsObj.DrawString(a.ToString(), new Font("Tahoma", 8), Brushes.Black, new PointF(k_xy[a, 1] + 5, k_xy[a, 2] + 5));
                }
                Pen cizgi = new Pen(System.Drawing.Color.Blue, 2);
                for (a = 1; a <= 9; a++)
                {
                    graphicsObj.DrawLine(cizgi, k_xy[antijen[1, a], 1], k_xy[antijen[1, a], 2], k_xy[antijen[1, a + 1], 1], k_xy[antijen[1, a + 1], 2]);
                }
                graphicsObj.DrawLine(cizgi, k_xy[antijen[1, 10], 1], k_xy[antijen[1, 10], 2], k_xy[antijen[1, 1], 1], k_xy[antijen[1, 1], 2]);
               
                //klonlama yapılıyor
                for (a = 1; a <= cr; a++) {
                    tmp1 = r1.Next(1, sr + 1);
                    for (b = 1; b <= 10; b++) { 
                        klon_antijen[a,b] = antijen[tmp1,b];
                    }
                }

                //mutasyon işlemi başlıyor
                    for (b = 1; b <= cr; b++)
                    {
                        for (a = 1; a <= mr; a++)
                        {
                            tmp1 = r1.Next(1, 11);
                            tmp2 = r1.Next(1, 11);
                            tmp3 = klon_antijen[b, tmp1];
                            klon_antijen[b, tmp1] = klon_antijen[b, tmp2];
                            klon_antijen[b, tmp2] = tmp3;
                        }
                    }
 
                //mutasyona uğramış bireylerin antijenlere eklenmesi
                for (a = (aj-cr)+1 ; a<=aj ; a++)
                {
                    for (b = 1; b <= 10; b++)
                    {
                        antijen[a, b] = klon_antijen[(a - cr), b];
                    }
                } 
                label8.Text = "İterasyon : " + itr.ToString(); 
            } //iterasyon sonu

            richTextBox1.Text = richTextBox1.Text + "\r\n" + "---";
            richTextBox1.Text = richTextBox1.Text + "\r\n Rota = ";
            for (a = 1; a <= 10; a++) {
                richTextBox1.Text = richTextBox1.Text + "->" + antijen[1, a].ToString();
            }
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "---";
            richTextBox1.Text = richTextBox1.Text +"\r\n Min. Yol = "+ aj_duy[1].ToString() + " piksel";
                
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "---";
            richTextBox1.Text = richTextBox1.Text + "\r\n Min. İtr = " + en_itr.ToString();

            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            Thread kanal1 = new Thread(new ThreadStart(hesapla));
            kanal1.Start();
        }

        

        

    }
}

