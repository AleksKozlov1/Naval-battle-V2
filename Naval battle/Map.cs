using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naval_battle
{

    internal class Map
    {
        Game game = new Game();

        public Button[] bt = new Button[100];
        public int[] bl1 = { -1, 0, 1 };
        public int[] bl2 = { -1, 0 };
        public int[] bl3 = { 0, 1 };
        public int l = 10;

        //Создание массива кнопок в процессе
        public void massButton(Button[] bt)
        {
            for (int i = 0; i < bt.Length; i++)
            {
                bt[i] = game.tableLayoutPanel1.Controls.Find($"b{i + 1}", true).First() as Button;
            }
        }

        //Счётчик кораблей//
        public void countShip(Label ship, Button bt)
        {
            ship.Text = Convert.ToString(Convert.ToInt32(ship.Text) - 1);

            if (ship.Text == "0")
                bt.Enabled = false;
        }

        //Включение и выключение свойст
        public void allButton(Button[] bt, int f)
        {
            switch (f)
            {
                case 0:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseClick -= game.explosion;
                        bt[i].MouseClick -= game.toPutShip;
                        bt[i].MouseEnter -= game.colorShip1;
                        bt[i].MouseEnter -= game.colorShip2;
                        bt[i].MouseEnter -= game.colorShip3;
                        bt[i].MouseEnter -= game.colorShip4;
                        bt[i].MouseLeave -= game.colorWhite;
                    }
                    break;

                case 1:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += game.colorShip1;
                        bt[i].MouseLeave += game.colorWhite;
                        bt[i].MouseClick += game.toPutShip;
                        bt[i].MouseEnter -= game.colorShip2;
                        bt[i].MouseEnter -= game.colorShip3;
                        bt[i].MouseEnter -= game.colorShip4;


                    }
                    break;

                case 2:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += game.colorShip2;
                        bt[i].MouseLeave += game.colorWhite;
                        bt[i].MouseClick += game.toPutShip;
                        bt[i].MouseEnter -= game.colorShip1;
                        bt[i].MouseEnter -= game.colorShip3;
                        bt[i].MouseEnter -= game.colorShip4;
                    }
                    break;

                case 3:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += game.colorShip3;
                        bt[i].MouseLeave += game.colorWhite;
                        bt[i].MouseClick += game.toPutShip;
                        bt[i].MouseEnter -= game.colorShip1;
                        bt[i].MouseEnter -= game.colorShip2;
                        bt[i].MouseEnter -= game.colorShip4;
                    }
                    break;

                case 4:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseEnter += game.colorShip4;
                        bt[i].MouseLeave += game.colorWhite;
                        bt[i].MouseClick += game.toPutShip;
                        bt[i].MouseEnter -= game.colorShip1;
                        bt[i].MouseEnter -= game.colorShip2;
                        bt[i].MouseEnter -= game.colorShip3;
                    }
                    break;

                case 5:
                    for (int i = 0; i < bt.Length; i++)
                    {
                        bt[i].MouseClick += game.explosion;
                    }
                    break;
            }
        }

        //Окрашивает позиции кораблей в серый
        public void Gray(int p, int k)
        {
            switch (k)
            {
                case 1:
                    bt[p].BackColor = Color.Gray;
                    break;

                case 2:
                    try
                    {
                        bt[p].BackColor = Color.Gray;
                        bt[p + l].BackColor = Color.Gray;
                    }
                    catch (Exception)
                    {
                        bt[p - l].BackColor = Color.Gray;
                    }
                    break;

                case 3:
                    try
                    {
                        bt[p].BackColor = Color.Gray;
                        bt[p + l].BackColor = Color.Gray;
                        bt[p - l].BackColor = Color.Gray;
                    }
                    catch (Exception)
                    {
                        if (p > 10)
                        {
                            bt[p - l].BackColor = Color.Gray;
                            bt[p - l * 2].BackColor = Color.Gray;
                        }
                        else
                        {
                            bt[p + l * 2].BackColor = Color.Gray;
                        }
                    }
                    break;

                case 4:
                    try
                    {
                        if (p < 80)
                        {
                            bt[p].BackColor = Color.Gray;
                            bt[p + l].BackColor = Color.Gray;
                            bt[p + l * 2].BackColor = Color.Gray;
                            bt[p - l].BackColor = Color.Gray;
                        }
                        else
                        {
                            bt[p - l * 2].BackColor = Color.Gray;
                            bt[p - l].BackColor = Color.Gray;
                            bt[p].BackColor = Color.Gray;
                            bt[p + l].BackColor = Color.Gray;
                        }
                    }
                    catch (Exception)
                    {
                        if (p < 10)
                        {
                            bt[p + l * 3].BackColor = Color.Gray;
                        }
                        else
                        {
                            bt[p - l * 3].BackColor = Color.Gray;
                        }
                    }
                    break;
            }
        }

        //Окрашивает все позиции белый
        public void White(int p, int k)
        {
            switch (k)
            {
                case 1:
                    bt[p].BackColor = Color.White;
                    break;

                case 2:
                    try
                    {
                        bt[p].BackColor = Color.White;
                        bt[p + l].BackColor = Color.White;
                    }
                    catch (Exception)
                    {
                        bt[p - l].BackColor = Color.White;
                    }
                    break;

                case 3:
                    try
                    {
                        bt[p].BackColor = Color.White;
                        bt[p + l].BackColor = Color.White;
                        bt[p - l].BackColor = Color.White;
                    }
                    catch (Exception)
                    {
                        if (p > 10)
                        {
                            bt[p - l].BackColor = Color.White;
                            bt[p - l * 2].BackColor = Color.White;
                        }
                        else
                        {
                            bt[p + l * 2].BackColor = Color.White;
                        }
                    }
                    break;

                case 4:
                    try
                    {
                        if (p < 80)
                        {
                            bt[p].BackColor = Color.White;
                            bt[p + l].BackColor = Color.White;
                            bt[p + l * 2].BackColor = Color.White;
                            bt[p - l].BackColor = Color.White;
                        }
                        else
                        {
                            bt[p - l * 2].BackColor = Color.White;
                            bt[p - l].BackColor = Color.White;
                            bt[p].BackColor = Color.White;
                            bt[p + l].BackColor = Color.White;
                        }
                    }
                    catch (Exception)
                    {
                        if (p < 10)
                        {
                            bt[p + l * 3].BackColor = Color.White;
                        }
                        else
                        {
                            bt[p - l * 3].BackColor = Color.White;
                        }
                    }
                    break;
            }
        }

        //Блокировка кнопок
        public void blockButton(int p, int k)
        {
            string pr = Convert.ToString(p.ToString().ElementAt(p.ToString().Length - 1));
            switch (k)
            {
                case 1:
                    try
                    {
                        if (pr != "9" && pr != "0")
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (pr == "0")
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl3.Length; i++)
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (pr == "9")
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl2.Length; i++)
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        p += l;
                        if (p == 0)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                for (int i = 0; i < bl3.Length; i++)
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p == 9)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                for (int i = 0; i < bl2.Length; i++)
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p > 0 && p < 9)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }
                    }
                    break;

                case 2:
                    try
                    {
                        if (p > 80 && p < 89)
                        {
                            p -= l;
                            for (int i = 0; i < bl1.Length; i++)
                            {
                                bt[p + bl1[i]].Enabled = false;
                            }
                        }

                        if (pr != "9" && pr != "0")
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (pr == "0")
                        {
                            if (p == 80)
                            {
                                p -= l;
                                for (int j = 0; j < 3; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }

                        if (pr == "9")
                        {
                            if (p == 89)
                            {
                                p -= l;
                                for (int j = 0; j < 3; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        p += l;
                        if (p == 0)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl3.Length; i++)
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p == 9)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl2.Length; i++)
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p > 0 && p < 9)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }
                    }
                    break;

                case 3:
                    try
                    {
                        if (p > 80 && p < 89)
                        {
                            p -= l;
                            for (int i = 0; i < bl1.Length; i++)
                            {
                                bt[p + bl1[i]].Enabled = false;
                            }
                        }

                        if (pr != "9" && pr != "0")
                        {
                            p -= l;
                            for (int j = 0; j < 5; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (pr == "0")
                        {
                            if (p == 80)
                            {
                                p -= l * 2;
                                for (int j = 0; j < 4; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                p -= l;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }

                        if (pr == "9")
                        {
                            if (p == 89)
                            {
                                p -= l * 2;
                                for (int j = 0; j < 4; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                p -= l;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (p > -21 && p < -10) p += l * 2;
                        else p += l;

                        if (p == 0)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl3.Length; i++)
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p == 9)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl2.Length; i++)
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p > 0 && p < 9)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }
                    }
                    break;

                case 4:
                    try
                    {
                        game.giveUp.Text = p.ToString();
                        if (p > 70 && p < 79)
                        {
                            p -= l;
                            for (int i = 0; i < bl1.Length; i++)
                            {
                                bt[p + bl1[i]].Enabled = false;
                            }
                        }

                        if (pr != "9" && pr != "0")
                        {
                            p -= l;
                            for (int j = 0; j < 6; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (pr == "0")
                        {
                            if (p == 70)
                            {
                                p -= l * 2;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                p -= l;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl3.Length; i++)
                                    {
                                        bt[p + bl3[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }

                        if (pr == "9")
                        {
                            if (p == 79)
                            {
                                p -= l * 2;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                            else
                            {
                                p -= l;
                                for (int j = 0; j < 5; j++)
                                {
                                    for (int i = 0; i < bl2.Length; i++)
                                    {
                                        bt[p + bl2[i]].Enabled = false;
                                    }
                                    p += l;
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        /*
                        if (p > -21 && p < -10) p += l * 2;
                        else p += l;

                        if (p == 0)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl3.Length; i++)
                                {
                                    bt[p + bl3[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p == 9)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl2.Length; i++)
                                {
                                    bt[p + bl2[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }

                        if (p > 0 && p < 9)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                for (int i = 0; i < bl1.Length; i++)
                                {
                                    bt[p + bl1[i]].Enabled = false;
                                }
                                p += l;
                            }
                            break;
                        }*/
                    }
                    break;
            }

        }
    }

}
