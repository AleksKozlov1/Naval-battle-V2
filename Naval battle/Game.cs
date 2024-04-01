using System;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace Naval_battle
{
    public partial class Game : Form
    {
        public Button[] bt = new Button[100];
        public Button[] bta = new Button[100];
        public Label[] labKol = new Label[4];
        public int[] bl1 = { -1, 0, 1 };
        public int[] bl2 = { -1, 0 };
        public int[] bl3 = { 0, 1 };
        public int[] bl4 = { -10, 0, 10 };
        public int[] bl5 = { -10, 0 };
        public int[] bl6 = { 0, 10 };
        public int l = 10;
        public int k;
        public int myNumber;
        public int hod;
         public bool fl = false;
        public bool flag = false;
        public bool flag1 = false;
        public bool flag2 = false;
        public bool isStart = false;
        public bool isReady = false;


        private ServerConnection serverConnection;
        private int[] array;

        public Game()
        {
            serverConnection = new ServerConnection();
            InitializeComponent();
            MassButton();
            allButton(0);

            lb1.Text = "Ожидание второго игрока ...";
        }
        private void InitGame()
        {
            try
            {
                array = serverConnection.ReceiveArray();
                while (array[1] == 0)
                {
                    array = serverConnection.ReceiveArray();
                }
                isStart = true;
                myNumber = array[0];
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private void Game_Shown(object sender, EventArgs e)
        {
            Task.Run(() => InitGame());
            timer1.Start();
        }

        //разблокировка кнопок нужна
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isStart == true)
            {
                if (!flag2)
                {
                    lb1.Text = "Расставьте корабли!";
                    flag2 = true;
                    Task.Run(() => IsReadyOponent());
                }
            }

            if (isReady)
            {
                lb1.Text = "Противник готов к битве!";

                if (flag1)
                {
                    timer1.Stop();
                    if(myNumber == 1)
                    {
                        lb1.Text = "Ваш ход!";
                    }
                    else
                    {
                        OponentTurn();
                    }
                }
            }
        }

        //Свойство для фиксации кораблей на поле
        public void toPutShip(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);

            if (btn.BackColor != Color.White)
            {
                ColorBlockButton(p, k, 0);
                allButton(0);
            }
        }

        //Меняет расстановку с вертикального на горизонтальный
        private void MouseRight(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);

            if (e.Button == MouseButtons.Right)
            {
                if (flag == false)
                {
                    flag = true;

                    bt[p].BackColor = Color.White;

                    Gray(p, k);
                    Color_White(p, l);
                }
                else
                {
                    flag = false;

                    bt[p].BackColor = Color.White;

                    Gray(p, k);
                    Color_White(p, 1);
                }
            }
        }

        //Функция, которая закрашивает в белый оставшиеся фрагменты после изменения 
        public void Color_White(int p, int k)
        {
            int p1 = p;

            for (int i = 0; i < 4; i++)
            {
                if ((p + k) < 100)
                {
                    if (bt[p + k].BackColor == Color.Gray)
                    {
                        bt[p + k].BackColor = Color.White;
                        p += k;
                    }
                    else break;
                }
                else break;
            }

            for (int i = 0; i < 4; i++)
            {
                if ((p1 - k) >= 0)
                {
                    if (bt[p1 - k].BackColor == Color.Gray)
                    {
                        bt[p1 - k].BackColor = Color.White;
                        p1 -= k;
                    }
                    else break;
                }
                else break;
            }
        }

        //Кнопка начала игры
        public void giveUp_Click(object sender, EventArgs e)
        {
            if (flag1 == false)
            {
                if (LabKol() == 4)
                {
                    allButton(5);
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].Enabled = false;
                        bta[i].Enabled = true;
                    }
                    lb1.Text = "Ожидание противника!";
                    giveUp.Text = "Сдаться";
                    flag1 = true;

                    serverConnection.SendArray(new int[] {1});
                }
                else
                {
                    MessageBox.Show("Не все коробли раставлены!");
                }
            }
            else
            {
                this.Close();
            }
        }

        //Кнопка для растановка 4-х палубника
        public void fourShip_Click(object sender, EventArgs e)
        {
            if(fl == false)
            {
                if (Convert.ToInt32(kol1.Text) > 0)
                {
                    kol1.Text = Convert.ToString(Convert.ToInt32(kol1.Text) - 1);
                    allButton(4);
                    fl = true;
                }
            }
            else
            {
                kol1.Text = Convert.ToString(Convert.ToInt32(kol1.Text) + 1);
                allButton(0);
                fl = false;
            }
        }

        //Кнопка для растановка 3-х палубника
        public void threeShip_Click(object sender, EventArgs e)
        {
            if(fl == false)
            {
                if (Convert.ToInt32(kol2.Text) > 0)
                {
                    kol2.Text = Convert.ToString(Convert.ToInt32(kol2.Text) - 1);
                    allButton(3);
                    fl = true;
                }
            }
            else
            {
                kol2.Text = Convert.ToString(Convert.ToInt32(kol2.Text) + 1);
                allButton(0);
                fl = false;
            }
            
        }
        }

        //Кнопка для растановка 2-х палубника
        public void towShip_Click(object sender, EventArgs e)
        {
            if (fl == false)
            {
                if (Convert.ToInt32(kol3.Text) > 0)
                {
                    kol3.Text = Convert.ToString(Convert.ToInt32(kol3.Text) - 1);
                    allButton(2);
                    fl = true;
                }
            }
            else
            {
                kol3.Text = Convert.ToString(Convert.ToInt32(kol3.Text) + 1);
                allButton(0);
                fl = false;
            }
        }

        //Кнопка для растановка 1-но палубника
        public void oneShip_Click(object sender, EventArgs e)
        {
            if(fl == false)
            {
                if (Convert.ToInt32(kol4.Text) > 0)
                {
                    kol4.Text = Convert.ToString(Convert.ToInt32(kol4.Text) - 1);
                    allButton(1);
                    fl = true;
                }
            }
            else
            {
                kol4.Text = Convert.ToString(Convert.ToInt32(kol4.Text) + 1);
                allButton(0);
                fl = false;
            }
        }

        //Свойства при наведении окрашивает 1 серую клетку
        public void colorShip1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);
            k = 1;
            Gray(p, k);
        }

        //Свойства при наведении окрашивает 2 серые клетки
        public void colorShip2(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);
            k = 2;
            Gray(p, k);
        }

        //Свойства при наведении окрашивает 3 серые клетки
        public void colorShip3(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);
            k = 3;
            Gray(p, k);
        }

        //Свойства при наведении окрашивает 4 серые клетки
        public void colorShip4(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);
            k = 4;
            Gray(p, k);
        }

        //Свойства когда курсор уходит с клетки окрашивает в белый
        public void colorWhite(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);
            White(p, k);
        }

        //Создание массива button 
        public void MassButton()
        {
            for (int i = 0; i < bt.Length; i++)
            {
                bt[i] = tableLayoutPanel1.Controls.Find($"b{i + 1}", true).First() as Button;
                bta[i] = tableLayoutPanel4.Controls.Find($"a{i + 1}", true).First() as Button;
                bta[i].Enabled = false;
            }
        }

        //Проверка на количество раставленных кораблей 
        public int LabKol()
        {
            labKol[0] = kol1;
            labKol[1] = kol2;
            labKol[2] = kol3;
            labKol[3] = kol4;

            int k = 0;

            for (int i = 0; i < 4; i++)
            {
                if (labKol[i].Text == "0")
                {
                    k++;
                }
            }
            return k;
        }

        //Счётчик кораблей блокировка кнопки при 0
        public void countShip(Label ship, Button bt)
        {
            if (ship.Text == "0") bt.Enabled = false;
            else bt.Enabled = true;
        }

        //Включение и выключение свойст
        public void allButton(int f)
        {
            switch (f)
            {
                case 0:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseClick -= explosion;
                        bt[i].MouseClick -= toPutShip;
                        bt[i].MouseEnter -= colorShip1;
                        bt[i].MouseEnter -= colorShip2;
                        bt[i].MouseEnter -= colorShip3;
                        bt[i].MouseEnter -= colorShip4;
                        bt[i].MouseLeave -= colorWhite;
                        bt[i].MouseDown -= MouseRight;
                    }

                    countShip(kol1, fourShip);
                    countShip(kol2, threeShip);
                    countShip(kol3, towShip);
                    countShip(kol4, oneShip);
                    giveUp.Enabled = true;
                    break;

                case 1:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += colorShip1;
                        bt[i].MouseLeave += colorWhite;
                        bt[i].MouseClick += toPutShip;
                        bt[i].MouseDown += MouseRight;
                        bt[i].MouseEnter -= colorShip2;
                        bt[i].MouseEnter -= colorShip3;
                        bt[i].MouseEnter -= colorShip4;
                    }

                    fourShip.Enabled = false;
                    threeShip.Enabled = false;
                    towShip.Enabled = false;
                    oneShip.Enabled = true;
                    giveUp.Enabled = false;
                    break;

                case 2:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += colorShip2;
                        bt[i].MouseLeave += colorWhite;
                        bt[i].MouseClick += toPutShip;
                        bt[i].MouseDown += MouseRight;
                        bt[i].MouseEnter -= colorShip1;
                        bt[i].MouseEnter -= colorShip3;
                        bt[i].MouseEnter -= colorShip4;
                    }

                    fourShip.Enabled = false;
                    threeShip.Enabled = false;
                    towShip.Enabled = true;
                    oneShip.Enabled = false;
                    giveUp.Enabled = false;
                    break;

                case 3:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += colorShip3;
                        bt[i].MouseLeave += colorWhite;
                        bt[i].MouseClick += toPutShip;
                        bt[i].MouseDown += MouseRight;
                        bt[i].MouseEnter -= colorShip1;
                        bt[i].MouseEnter -= colorShip2;
                        bt[i].MouseEnter -= colorShip4;
                    }

                    fourShip.Enabled = false;
                    threeShip.Enabled = true;
                    towShip.Enabled = false;
                    oneShip.Enabled = false;
                    giveUp.Enabled = false;
                    break;

                case 4:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += colorShip4;
                        bt[i].MouseLeave += colorWhite;
                        bt[i].MouseClick += toPutShip;
                        bt[i].MouseDown += MouseRight;
                        bt[i].MouseEnter -= colorShip1;
                        bt[i].MouseEnter -= colorShip2;
                        bt[i].MouseEnter -= colorShip3;
                    }

                    fourShip.Enabled = true;
                    threeShip.Enabled = false;
                    towShip.Enabled = false;
                    oneShip.Enabled = false;
                    giveUp.Enabled = false;
                    break;

                case 5:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bta[i].MouseClick += explosion;
                        bt[i].MouseClick -= toPutShip;
                        bt[i].MouseEnter -= colorShip1;
                        bt[i].MouseEnter -= colorShip2;
                        bt[i].MouseEnter -= colorShip3;
                        bt[i].MouseEnter -= colorShip4;
                        bt[i].MouseLeave -= colorWhite;
                        bt[i].MouseDown -= MouseRight;
                    }
                    break;
            }
        }

        //Окрашивает позиции кораблей в серый
        public void Gray(int p, int k)
        {
            if (flag == false)
            {
                switch (k)
                {
                    case 1:
                        bt[p].BackColor = Color.Gray;
                        break;

                    case 2:
                        try
                        {
                            if (p < 10 && bt[p + l].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + l].BackColor = Color.Gray;
                            }

                            if (p > 89 && bt[p - l].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - l].BackColor = Color.Gray;
                            }

                            if (p > 9 && p < 90)
                            {
                                if (bt[p + l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + l].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if (bt[p - l].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - l].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 3:
                        try
                        {
                            if (p < 10 && bt[p + l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + l].BackColor = Color.Gray;
                                bt[p + l * 2].BackColor = Color.Gray;
                            }

                            if (p > 89 && bt[p - l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - l].BackColor = Color.Gray;
                                bt[p - l * 2].BackColor = Color.Gray;
                            }

                            if (p > 9 && p < 90)
                            {
                                if (bt[p + l].Enabled != false && bt[p - l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + l].BackColor = Color.Gray;
                                    bt[p - l].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if (bt[p - l * 2] != null && bt[p - l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - l].BackColor = Color.Gray;
                                        bt[p - l * 2].BackColor = Color.Gray;
                                    }

                                    if (bt[p + l * 2] != null && bt[p + l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + l].BackColor = Color.Gray;
                                        bt[p + l * 2].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 4:
                        try
                        {
                            if (p < 10 && bt[p + l * 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + l].BackColor = Color.Gray;
                                bt[p + l * 2].BackColor = Color.Gray;
                                bt[p + l * 3].BackColor = Color.Gray;
                            }

                            if (p > 89 && bt[p - l * 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - l].BackColor = Color.Gray;
                                bt[p - l * 2].BackColor = Color.Gray;
                                bt[p - l * 3].BackColor = Color.Gray;
                            }

                            if (p > 79 && p < 90 && bt[p - l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + l].BackColor = Color.Gray;
                                bt[p - l].BackColor = Color.Gray;
                                bt[p - l * 2].BackColor = Color.Gray;
                            }

                            if (p > 9 && p < 80)
                            {
                                if (bt[p + l * 2].Enabled != false && bt[p - l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + l].BackColor = Color.Gray;
                                    bt[p + l * 2].BackColor = Color.Gray;
                                    bt[p - l].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if (bt[p + l].Enabled == false && bt[p - l * 3] != null && bt[p - l * 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - l].BackColor = Color.Gray;
                                        bt[p - l * 2].BackColor = Color.Gray;
                                        bt[p - l * 3].BackColor = Color.Gray;
                                    }

                                    if (bt[p + l].Enabled != false && bt[p - l * 2] != null && bt[p - l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + l].BackColor = Color.Gray;
                                        bt[p - l].BackColor = Color.Gray;
                                        bt[p - l * 2].BackColor = Color.Gray;
                                    }

                                    if (bt[p + l * 3] != null && bt[p + l * 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + l].BackColor = Color.Gray;
                                        bt[p + l * 2].BackColor = Color.Gray;
                                        bt[p + l * 3].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;
                }
            }
            else
            {
                int pr = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());

                switch (k)
                {
                    case 1:
                        bt[p].BackColor = Color.Gray;
                        break;

                    case 2:
                        try
                        {
                            if (pr == 0 && bt[p + 1].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + 1].BackColor = Color.Gray;
                            }

                            if (pr == 9 && bt[p - 1].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - 1].BackColor = Color.Gray;
                            }

                            if (pr > 0 && pr < 9)
                            {
                                if (bt[p + 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + 1].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if (bt[p - 1].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - 1].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 3:
                        try
                        {
                            if (pr == 0 && bt[p + 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + 1].BackColor = Color.Gray;
                                bt[p + 1 * 2].BackColor = Color.Gray;
                            }

                            if (pr == 9 && bt[p - 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - 1].BackColor = Color.Gray;
                                bt[p - 2].BackColor = Color.Gray;
                            }

                            if (pr > 0 && pr < 9)
                            {
                                if (bt[p + 1].Enabled != false && bt[p - 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + 1].BackColor = Color.Gray;
                                    bt[p - 1].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if ((pr - 2) != -1 && bt[p - 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - 1].BackColor = Color.Gray;
                                        bt[p - 1 * 2].BackColor = Color.Gray;
                                    }

                                    if ((pr + 2) != 10 && bt[p + 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + 1].BackColor = Color.Gray;
                                        bt[p + 1 * 2].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 4:
                        try
                        {
                            if (pr == 0 && bt[p + 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + 1].BackColor = Color.Gray;
                                bt[p + 2].BackColor = Color.Gray;
                                bt[p + 3].BackColor = Color.Gray;
                            }

                            if (pr == 9 && bt[p - 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p - 1].BackColor = Color.Gray;
                                bt[p - 2].BackColor = Color.Gray;
                                bt[p - 3].BackColor = Color.Gray;
                            }

                            if (pr == 8 && bt[p - 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.Gray;
                                bt[p + 1].BackColor = Color.Gray;
                                bt[p - 1].BackColor = Color.Gray;
                                bt[p - 2].BackColor = Color.Gray;
                            }

                            if (pr > 0 && pr < 8)
                            {
                                if (bt[p + 2].Enabled != false && bt[p - 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.Gray;
                                    bt[p + 1].BackColor = Color.Gray;
                                    bt[p + 2].BackColor = Color.Gray;
                                    bt[p - 1].BackColor = Color.Gray;
                                }
                                else
                                {
                                    if (bt[p + 1].Enabled == false && (pr - 3) >= 0 && bt[p - 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p - 1].BackColor = Color.Gray;
                                        bt[p - 2].BackColor = Color.Gray;
                                        bt[p - 3].BackColor = Color.Gray;
                                    }

                                    if (bt[p + 1].Enabled != false && (pr - 2) >= 0 && bt[p - 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + 1].BackColor = Color.Gray;
                                        bt[p - 1].BackColor = Color.Gray;
                                        bt[p - 2].BackColor = Color.Gray;
                                    }

                                    if ((pr + 3) < 10 && bt[p + 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.Gray;
                                        bt[p + 1].BackColor = Color.Gray;
                                        bt[p + 2].BackColor = Color.Gray;
                                        bt[p + 3].BackColor = Color.Gray;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;
                }
            }

        }

        //Окрашивает все позиции белый
        public void White(int p, int k)
        {
            if (flag == false)
            {
                switch (k)
                {
                    case 1:
                        bt[p].BackColor = Color.White;
                        break;

                    case 2:
                        try
                        {
                            if (p < 90)
                            {
                                bt[p].BackColor = Color.White;

                                if (bt[p + l].Enabled != false)
                                {
                                    bt[p + l].BackColor = Color.White;
                                }
                                else
                                {
                                    bt[p - l].BackColor = Color.White;
                                }
                            }
                            else
                            {
                                if (bt[p - l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p - l].BackColor = Color.White;
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 3:
                        try
                        {
                            if (p < 10 && bt[p + l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + l].BackColor = Color.White;
                                bt[p + l * 2].BackColor = Color.White;
                            }

                            if (p > 89 && bt[p - l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p - l].BackColor = Color.White;
                                bt[p - l * 2].BackColor = Color.White;
                            }

                            if (p > 9 && p < 90)
                            {
                                if (bt[p + l].Enabled != false && bt[p - l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p + l].BackColor = Color.White;
                                    bt[p - l].BackColor = Color.White;
                                }
                                else
                                {
                                    if (bt[p - l * 2] != null && bt[p - l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p - l].BackColor = Color.White;
                                        bt[p - l * 2].BackColor = Color.White;
                                    }

                                    if (bt[p + l * 2] != null && bt[p + l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + l].BackColor = Color.White;
                                        bt[p + l * 2].BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 4:
                        try
                        {
                            if (p < 10 && bt[p + l * 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + l].BackColor = Color.White;
                                bt[p + l * 2].BackColor = Color.White;
                                bt[p + l * 3].BackColor = Color.White;
                            }

                            if (p > 89 && bt[p - l * 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p - l].BackColor = Color.White;
                                bt[p - l * 2].BackColor = Color.White;
                                bt[p - l * 3].BackColor = Color.White;
                            }

                            if (p > 79 && p < 90 && bt[p - l * 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + l].BackColor = Color.White;
                                bt[p - l].BackColor = Color.White;
                                bt[p - l * 2].BackColor = Color.White;
                            }

                            if (p > 9 && p < 80)
                            {
                                if (bt[p + l * 2].Enabled != false && bt[p - l].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p + l].BackColor = Color.White;
                                    bt[p + l * 2].BackColor = Color.White;
                                    bt[p - l].BackColor = Color.White;
                                }
                                else
                                {
                                    if (bt[p + l].Enabled == false && bt[p - l * 3] != null && bt[p - l * 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p - l].BackColor = Color.White;
                                        bt[p - l * 2].BackColor = Color.White;
                                        bt[p - l * 3].BackColor = Color.White;
                                    }

                                    if (bt[p + l].Enabled != false && bt[p - l * 2] != null && bt[p - l * 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + l].BackColor = Color.White;
                                        bt[p - l].BackColor = Color.White;
                                        bt[p - l * 2].BackColor = Color.White;
                                    }

                                    if (bt[p + l * 3] != null && bt[p + l * 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + l].BackColor = Color.White;
                                        bt[p + l * 2].BackColor = Color.White;
                                        bt[p + l * 3].BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                        break;
                }
            }
            else
            {
                int pr = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());

                switch (k)
                {
                    case 1:
                        bt[p].BackColor = Color.White;
                        break;

                    case 2:
                        try
                        {
                            if (pr == 0 && bt[p + 1].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + 1].BackColor = Color.White;
                            }

                            if (pr == 9 && bt[p - 1].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p - 1].BackColor = Color.White;
                            }

                            if (pr > 0 && pr < 9)
                            {
                                if (bt[p + 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p + 1].BackColor = Color.White;
                                }
                                else
                                {
                                    if (bt[p - 1].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p - 1].BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 3:
                        try
                        {
                            if (pr == 0 && bt[p + 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + 1].BackColor = Color.White;
                                bt[p + 1 * 2].BackColor = Color.White;
                            }

                            if (pr == 9 && bt[p - 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p - 1].BackColor = Color.White;
                                bt[p - 2].BackColor = Color.White;
                            }

                            if (pr > 0 && pr < 9)
                            {
                                if (bt[p + 1].Enabled != false && bt[p - 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p + 1].BackColor = Color.White;
                                    bt[p - 1].BackColor = Color.White;
                                }
                                else
                                {
                                    if ((pr - 2) != -1 && bt[p - 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p - 1].BackColor = Color.White;
                                        bt[p - 1 * 2].BackColor = Color.White;
                                    }

                                    if ((pr + 2) != 10 && bt[p + 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + 1].BackColor = Color.White;
                                        bt[p + 1 * 2].BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;

                    case 4:
                        try
                        {
                            if (pr == 0 && bt[p + 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + 1].BackColor = Color.White;
                                bt[p + 2].BackColor = Color.White;
                                bt[p + 3].BackColor = Color.White;
                            }

                            if (pr == 9 && bt[p - 3].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p - 1].BackColor = Color.White;
                                bt[p - 2].BackColor = Color.White;
                                bt[p - 3].BackColor = Color.White;
                            }

                            if (pr == 8 && bt[p - 2].Enabled != false)
                            {
                                bt[p].BackColor = Color.White;
                                bt[p + 1].BackColor = Color.White;
                                bt[p - 1].BackColor = Color.White;
                                bt[p - 2].BackColor = Color.White;
                            }

                            if (pr > 0 && pr < 8)
                            {
                                if (bt[p + 2].Enabled != false && bt[p - 1].Enabled != false)
                                {
                                    bt[p].BackColor = Color.White;
                                    bt[p + 1].BackColor = Color.White;
                                    bt[p + 2].BackColor = Color.White;
                                    bt[p - 1].BackColor = Color.White;
                                }
                                else
                                {
                                    if (bt[p + 1].Enabled == false && (pr - 3) >= 0 && bt[p - 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p - 1].BackColor = Color.White;
                                        bt[p - 2].BackColor = Color.White;
                                        bt[p - 3].BackColor = Color.White;
                                    }

                                    if (bt[p + 1].Enabled != false && (pr - 2) >= 0 && bt[p - 2].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + 1].BackColor = Color.White;
                                        bt[p - 1].BackColor = Color.White;
                                        bt[p - 2].BackColor = Color.White;
                                    }

                                    if ((pr + 3) < 10 && bt[p + 3].Enabled != false)
                                    {
                                        bt[p].BackColor = Color.White;
                                        bt[p + 1].BackColor = Color.White;
                                        bt[p + 2].BackColor = Color.White;
                                        bt[p + 3].BackColor = Color.White;
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                        break;
                }
            }

        }

        //Окрашивает кнопки при уничтожении корабля и блокирует при растановке
        public void ColorBlockButton(int p, int k, int f)
        {
            int poz;

            if (flag == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (p < 90)
                    {
                        if (bt[p].BackColor == Color.Gray)
                        {
                            p += l;
                        }
                        else break;
                    }
                    else break;
                }

                switch (k)
                {
                    case 1:
                        if (p > 89 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray)) poz = 2;
                        else poz = 3;

                        MassBlock(p, poz, f);
                        break;

                    case 2:
                        if (p > 89 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray)) poz = 3;
                        else poz = 4;

                        MassBlock(p, poz, f);
                        break;

                    case 3:
                        if (p > 89 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray)) poz = 4;
                        else poz = 5;

                        MassBlock(p, poz, f);
                        break;

                    case 4:
                        if (p > 89 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray)) poz = 5;
                        else poz = 6;

                        MassBlock(p, poz, f);
                        break;
                }
            }
            else
            {
                int pr = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());

                for (int i = 0; i < 4; i++)
                {
                    if (pr < 9)
                    {
                        if (bt[p].BackColor == Color.Gray)
                        {
                            p++;
                            pr++;
                        }
                        else break;
                    }
                    else break;
                }

                switch (k)
                {
                    case 1:
                        if (pr == 9 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray) || (pr == 1 && bt[p].BackColor == Color.White)) poz = 2;
                        else poz = 3;

                        MassBlock(p, poz, f);
                        break;

                    case 2:
                        if (pr == 9 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray) || (pr == 2 && bt[p].BackColor == Color.White)) poz = 3;
                        else poz = 4;

                        MassBlock(p, poz, f);
                        break;

                    case 3:
                        if (pr == 9 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray) || (pr == 3 && bt[p].BackColor == Color.White)) poz = 4;
                        else poz = 5;

                        MassBlock(p, poz, f);
                        break;

                    case 4:
                        if (pr == 9 && (bt[p].BackColor == Color.Black || bt[p].BackColor == Color.Gray) || (pr == 4 && bt[p].BackColor == Color.White)) poz = 5;
                        else poz = 6;

                        MassBlock(p, poz, f);
                        break;
                }
            }
        }

        //Окрашивает и блокирует кнопоки
        public void MassBlock(int p, int poz, int f)
        {
            int pro = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());

            if (flag == false)
            {
                try
                {
                    if (pro == 0)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl3.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl3[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl3[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                            }
                            p -= l;
                        }
                    }

                    if (pro == 9)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl2.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl2[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl2[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                            }
                            p -= l;
                        }
                    }

                    if (pro != 0 && pro != 9)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl1.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl1[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl1[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                            }
                            p -= l;
                        }
                    }
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    if (p < 10)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl6.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl6[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl6[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl6[i]].Enabled = false;
                                }
                            }
                            p -= 1;
                        }
                    }

                    if (p > 9 && p < 90)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl4.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl4[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl4[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl4[i]].Enabled = false;
                                }
                            }
                            p -= 1;
                        }
                    }

                    if (p > 89)
                    {
                        for (int j = 0; j < poz; j++)
                        {
                            for (int i = 0; i < bl5.Length; i++)
                            {
                                if (f == 1)
                                {
                                    if (bt[p + bl5[i]].BackColor != Color.Black)
                                    {
                                        bt[p + bl5[i]].BackColor = Color.Blue;
                                    }
                                }
                                else
                                {
                                    bt[p + bl5[i]].Enabled = false;
                                }
                            }
                            p -= 1;
                        }
                    }
                }
                catch (Exception) { }
            }

        }





        public void OponentTurn()
        {
            int j = 0;
            lb1.Text = "Ход противника!";
            array = serverConnection.ReceiveArray();

            if (bt[array[1]].BackColor == Color.White)
            {
                bt[array[1]].BackColor = Color.Blue;
                hod = 0;
                serverConnection.SendArray(new int[] { 0 });
                lb1.Text = "Ваш ход!";
            }
            else
            {
                bt[array[1]].BackColor = Color.Black;
                hod = 1;
                serverConnection.SendArray(new int[] { 1 });
                OponentTurn();
            }
        }

        //Свойства для отаки по врагу (оно не сделано)
        public void explosion(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            int p = Convert.ToInt32(btn.Tag);

            btn.Enabled = false;
            int[] arr = { myNumber, p };
            serverConnection.SendArray(arr);
            array = serverConnection.ReceiveArray();

            if (array[0] == 0)
            {
                btn.BackColor = Color.Blue;
                OponentTurn();
            }
            else
            {
                btn.BackColor = Color.Black;
                lb1.Text = "Ваш ход!";
            }

            Victory();

            // ход игрока, позиция выстрела, тип корабля, флаг расположения
            //Проверка на попадание изи промах сделать

            /*
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.Blue;
            }
            else
            if (btn.BackColor == Color.Blue)
            {
                btn.BackColor = Color.White;
            }

            if (btn.BackColor == Color.Gray)
            {
                btn.BackColor = Color.Black;
                lb1.Text = "";
                BorderShip(p, TypeShip(p));
            }
            else
            if (btn.BackColor == Color.Black)
            {
                btn.BackColor = Color.Gray;
            }*/
        }

        public void Victory()
        {
            if (array[2] == 0)
            {
                MessageBox.Show("Вы победили!");
            }
            else
            {
                if (Mass() == 0)
                {
                    MessageBox.Show("Вы проиграли!");
                }
            }
        }

        private void IsReadyOponent()
        {
            try
            {
                array = serverConnection.ReceiveArray();
                isReady = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        //Определение типа корабля
        public int TypeShip(int p)
        {
            int pr = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());
            int type = 0;
            int p1 = p;
            int p2 = p - l;

            try
            {
                flag = false;

                for (int i = 0; i < 4; i++)
                {
                    if (p1 < 90)
                    {
                        if (bt[p1].BackColor == Color.Gray || bt[p1].BackColor == Color.Black)
                        {
                            type++;
                            p1 += l;
                        }
                        else break;
                    }
                    else
                    {
                        if (bt[p1].BackColor == Color.Gray || bt[p1].BackColor == Color.Black) type++;
                        break;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (p2 > 9)
                    {
                        if (bt[p2].BackColor == Color.Gray || bt[p2].BackColor == Color.Black)
                        {
                            type++;
                            p2 -= l;
                        }
                        else break;
                    }
                    else
                    {
                        if (p2 > 0 && (bt[p2].BackColor == Color.Gray || bt[p2].BackColor == Color.Black)) type++;
                        break;
                    }
                }

                if (type == 1)
                {
                    flag = true;
                    p1 = p;
                    p2 = p;

                    for (int i = 0; i < 3; i++)
                    {
                        pr = Convert.ToInt32(p1.ToString().ElementAt(p1.ToString().Length - 1).ToString());
                        if (pr < 9)
                        {
                            if (bt[p1 + 1].BackColor == Color.Gray || bt[p1 + 1].BackColor == Color.Black)
                            {
                                type++;
                                p1 += 1;
                            }
                            else break;
                        }
                        else break;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        pr = Convert.ToInt32(p2.ToString().ElementAt(p2.ToString().Length - 1).ToString());
                        if (pr > 0)
                        {
                            if (bt[p2 - 1].BackColor == Color.Gray || bt[p2 - 1].BackColor == Color.Black)
                            {
                                type++;
                                p2--;
                            }
                            else break;
                        }
                        else break;
                    }
                }

                return type;
            }
            catch (Exception e)
            {
                return type;
            }
        }

        //Проверка на уничтожение корабля
        public void BorderShip(int p, int type)
        {
            int pr;
            int ship = 0;
            int p1 = p;

            try
            {
                if (flag == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (p < 90)
                        {
                            if (bt[p].BackColor == Color.Black)
                            {
                                ship++;
                                p += l;
                            }
                            else break;
                        }
                        else
                        {
                            if (bt[p].BackColor == Color.Black) ship++;
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (p1 > 9)
                        {
                            if (bt[p1 - l].BackColor == Color.Black)
                            {
                                ship++;
                                p1 -= l;
                            }
                            else break;
                        }
                        else
                        {
                            if (bt[p1 - l].BackColor == Color.Black) ship++;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        pr = Convert.ToInt32(p.ToString().ElementAt(p.ToString().Length - 1).ToString());

                        if (pr < 9)
                        {
                            if (bt[p].BackColor == Color.Black)
                            {
                                ship++;
                                p++;
                            }
                            else break;
                        }
                        else
                        {
                            if (bt[p].BackColor == Color.Black) ship++;
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        pr = Convert.ToInt32(p1.ToString().ElementAt(p1.ToString().Length - 1).ToString());

                        if (pr > 0)
                        {
                            if (bt[p1 - 1].BackColor == Color.Black)
                            {
                                ship++;
                                p1--;
                            }
                            else break;
                        }
                        else break;
                    }
                }

                if (ship == type)
                {
                    lb1.Text = "Корабыль потоплен!";
                    ColorBlockButton(p, type, 1);
                }
            }
            catch (Exception e)
            {
                if (ship == type)
                {
                    lb1.Text = "Корабыль потоплен!";
                    ColorBlockButton(p, type, 1);
                }
            }
        }

        //Подчёт количества серых квадратов
        public int Mass()
        {
            int kol = 0;
            for (int i = 0; i < bt.Length; i++)
            {
                if (bt[i].BackColor == Color.Gray)
                {
                    kol++;
                }
            }
            return kol;
        }


    }
}
