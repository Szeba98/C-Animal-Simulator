using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
// 500 x 500

namespace Simulator
{
    public partial class Form1 : Form
    {
        int ragadozok = 30;
        int jelenleg_novenyevo = 10;
        int nap = 0;
        int ragadozo_ehseg = 10;
        int novenyevo_elet = 12;
        Label[,] tomb = new Label[26, 25];
        List<Novenyevo> n = new List<Novenyevo>();
        List<Ragadozo> r = new List<Ragadozo>();

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(ido.Interval);
            textBox5.Text = Convert.ToString(ragadozo_ehseg);
            textBox4.Text = Convert.ToString(novenyevo_elet);
            textBox2.Text = Convert.ToString(jelenleg_novenyevo);
            textBox3.Text = Convert.ToString(ragadozok);
            Alkotas();
        }

        private void Alkotas()
        {
            int bal = 0;
            int fent = 0;
            int meret = 20;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    Label l = new Label();
                    l.Parent = this.Alap;
                    l.BorderStyle = BorderStyle.FixedSingle;
                    l.BackColor = Color.Gray;
                    l.Height = meret;
                    l.Width = meret;
                    l.Left = bal;
                    l.Top = fent;
                    bal += meret;
                    tomb[i, j] = l;
                }
                fent += meret;
                bal = 0;
            }
        }
        private void Ragadozo_elhelyezes()
        {
            int x;
            int y;
            int ragad_szam = 0;
            Random rnd = new Random();
            do
            {
                x = rnd.Next(0, 25);
                y = rnd.Next(0, 25);
                if (tomb[x, y].BackColor == Color.Gray)
                {
                    tomb[x, y].BackColor = Color.Red;
                    Ragadozo z = new Ragadozo();
                    z.x = x;
                    z.y = y;
                    z.ehseg = ragadozo_ehseg;
                    r.Add(z);
                    ragad_szam++;
                }
            } while (ragad_szam != ragadozok);
        }
        private void Novenyevo_elhelyezes()
        {
            int x;
            int y;
            int novenyevo_szam = 0;
            Random rnd = new Random();
            do
            {
                x = rnd.Next(0, 25);
                y = rnd.Next(0, 25);
                if (tomb[x, y].BackColor == Color.Gray)
                {
                    tomb[x, y].BackColor = Color.Green;
                    Novenyevo a = new Novenyevo();
                    a.x = x;
                    a.y = y;
                    a.elet = novenyevo_elet;
                    n.Add(a);
                    novenyevo_szam++;
                }
            } while (novenyevo_szam != jelenleg_novenyevo);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ragadozo_ehseg = Convert.ToInt32(textBox5.Text);
            novenyevo_elet = Convert.ToInt32(textBox4.Text);
            jelenleg_novenyevo= Convert.ToInt32(textBox2.Text);
            ragadozok = Convert.ToInt32(textBox3.Text);
            if (!ido.Enabled)
            {
                ido.Enabled = true;
                button1.Text = "Állj";
                if (nap == 0)
                {
                    Ragadozo_elhelyezes();
                    Novenyevo_elhelyezes();
                }
            }
            else
            {
                button1.Text = "Inditás";
                ido.Enabled = false;
            }
            if (ido.Enabled)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
        }
        private void Mozgas()
        {
            Random rnd = new Random();
            // *
            // Növényevők
            // *
            foreach (Novenyevo a in n)
            {
                switch (rnd.Next(0, 5))
                {
                    case 1:
                        // Fel
                        if (a.y != 0 && a.x != 25)
                        {
                            if (tomb[a.x, a.y - 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y].BackColor = Color.Gray;
                                tomb[a.x, a.y - 1].BackColor = Color.Green;
                                a.y -= 1;
                            }
                        }
                        break;
                    case 2:
                        // Le
                        if (a.y != 24 && a.x != 25)
                        {
                            if (tomb[a.x, a.y + 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y].BackColor = Color.Gray;
                                tomb[a.x, a.y + 1].BackColor = Color.Green;
                                a.y += 1;
                            }
                        }
                        break;
                    case 3:
                        // Bal
                        if (a.x != 0 && a.x != 25)
                        {
                            if (tomb[a.x - 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y].BackColor = Color.Gray;
                                tomb[a.x - 1, a.y].BackColor = Color.Green;
                                a.x -= 1;
                            }
                        }
                        break;
                    case 4:
                        // Jobb
                        if (a.x != 24 && a.x != 25)
                        {
                            if (tomb[a.x + 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y].BackColor = Color.Gray;
                                tomb[a.x + 1, a.y].BackColor = Color.Green;
                                a.x += 1;
                            }
                        }
                        break;
                }
            }
            // *
            // Ragadozók
            // *
            foreach (Ragadozo z in r)
            {
                switch (rnd.Next(0, 5))
                {
                    case 1:
                        if (z.y != 0 && z.x != 25)
                        {
                            if (tomb[z.x, z.y - 1].BackColor == Color.Gray)
                            {
                                tomb[z.x, z.y].BackColor = Color.Gray;
                                tomb[z.x, z.y - 1].BackColor = Color.Red;
                                z.y -= 1;
                            }
                        }
                        break;
                    case 2:
                        if (z.y != 24 && z.x != 25)
                        {
                            if (tomb[z.x, z.y + 1].BackColor == Color.Gray)
                            {
                                tomb[z.x, z.y].BackColor = Color.Gray;
                                tomb[z.x, z.y + 1].BackColor = Color.Red;
                                z.y += 1;
                            }
                        }
                        break;
                    case 3:
                        if (z.x != 0 && z.x != 25)
                        {
                            if (tomb[z.x - 1, z.y].BackColor == Color.Gray)
                            {
                                tomb[z.x, z.y].BackColor = Color.Gray;
                                tomb[z.x - 1, z.y].BackColor = Color.Red;
                                z.x -= 1;
                            }
                        }
                        break;
                    case 4:
                        if (z.x != 24 && z.x != 25)
                        {
                            if (tomb[z.x + 1, z.y].BackColor == Color.Gray)
                            {
                                tomb[z.x, z.y].BackColor = Color.Gray;
                                tomb[z.x + 1, z.y].BackColor = Color.Red;
                                z.x += 1;
                            }
                        }
                        break;
                }
            }
        }
        private void Eves()
        {
            foreach (Ragadozo z in r)
            {
                if (z.y != 0 && z.x != 25)
                {
                    if (tomb[z.x, z.y - 1].BackColor == Color.Green)
                    {
                        z.ehseg = ragadozo_ehseg;
                        novenyevo_eltuntes(z.x, z.y - 1);
                        tomb[z.x, z.y - 1].BackColor = Color.Gray;
                        continue;
                    }
                }
                if (z.y != 24 && z.x != 25)
                {
                    if (tomb[z.x, z.y + 1].BackColor == Color.Green)
                    {
                        z.ehseg = ragadozo_ehseg;
                        novenyevo_eltuntes(z.x, z.y + 1);
                        tomb[z.x, z.y + 1].BackColor = Color.Gray;
                        continue;
                    }
                }
                if (z.x != 0 && z.x != 25)
                {
                    if (tomb[z.x - 1, z.y].BackColor == Color.Green)
                    {
                        z.ehseg = ragadozo_ehseg;
                        novenyevo_eltuntes(z.x - 1, z.y);
                        tomb[z.x - 1, z.y].BackColor = Color.Gray;
                        continue;
                    }
                }
                if (z.x != 24 && z.x != 25)
                {
                    if (tomb[z.x + 1, z.y].BackColor == Color.Green)
                    {
                        z.ehseg = ragadozo_ehseg;
                        novenyevo_eltuntes(z.x + 1, z.y);
                        tomb[z.x + 1, z.y].BackColor = Color.Gray;
                        continue;
                    }
                }
            }
        }
        private void novenyevo_eltuntes(int x, int y)
        {
            foreach (Novenyevo a in n)
            {
                if (a.x == x && a.y == y)
                {
                    a.x = 25;
                    a.y = 0;
                    a.elet = 0;
                    jelenleg_novenyevo--;
                }
            }
        }
        private void Szaporodas()
        {
            List<Novenyevo> n1 = new List<Novenyevo>();
            List<Ragadozo> r1 = new List<Ragadozo>();
            #region szaporodas Növényevő
            foreach (Novenyevo a in n)
            {
                // Fel
                if (a.y != 0 && a.x != 25)
                {
                    if (tomb[a.x, a.y - 1].BackColor == Color.Green)
                    {
                        //Le
                        if (a.y != 24 && a.x != 25)
                        {
                            if (tomb[a.x, a.y + 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y + 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y + 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        //Bal
                        if (a.x != 0 && a.x != 25)
                        {
                            if (tomb[a.x - 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x - 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x - 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        //Jobb
                        if (a.x != 24 && a.x != 25)
                        {
                            if (tomb[a.x + 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x + 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x + 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                    }
                }
                // Le
                if (a.y != 24 && a.x != 25)
                {
                    if (tomb[a.x, a.y + 1].BackColor == Color.Green)
                    {
                        // Fel
                        if (a.y != 0 && a.x != 25)
                        {
                            if (tomb[a.x, a.y - 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y - 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y - 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        // Bal
                        if (a.x != 0 && a.x != 25)
                        {
                            if (tomb[a.x - 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x - 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x - 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        // Jobb
                        if (a.x != 24 && a.x != 25)
                        {
                            if (tomb[a.x + 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x + 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x + 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                    }
                }
                // Bal
                if (a.x != 0 && a.x != 25)
                {
                    if (tomb[a.x - 1, a.y].BackColor == Color.Green)
                    {
                        // Fel
                        if (a.y != 0 && a.x != 25)
                        {
                            if (tomb[a.x, a.y - 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y - 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y - 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        //Le
                        if (a.y != 24 && a.x != 25)
                        {
                            if (tomb[a.x, a.y + 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y + 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y + 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;

                            }
                        }
                        // Jobb
                        if (a.x != 24 && a.x != 25)
                        {
                            if (tomb[a.x + 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x + 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x + 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                    }
                }
                // Jobb
                if (a.x != 24 && a.x != 25)
                {
                    if (tomb[a.x + 1, a.y].BackColor == Color.Green)
                    {
                        // Fel
                        if (a.y != 0 && a.x != 25)
                        {
                            if (tomb[a.x, a.y - 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y - 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y - 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                        //Le
                        if (a.y != 24 && a.x != 25)
                        {
                            if (tomb[a.x, a.y + 1].BackColor == Color.Gray)
                            {
                                tomb[a.x, a.y + 1].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x;
                                b.y = a.y + 1;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;

                            }
                        }
                        // Bal
                        if (a.x != 0 && a.x != 25)
                        {
                            if (tomb[a.x - 1, a.y].BackColor == Color.Gray)
                            {
                                tomb[a.x - 1, a.y].BackColor = Color.Green;
                                Novenyevo b = new Novenyevo();
                                b.x = a.x - 1;
                                b.y = a.y;
                                b.elet = novenyevo_elet;
                                n1.Add(b);
                                jelenleg_novenyevo++;
                                continue;
                            }
                        }
                    }
                }
            }
            #endregion
            #region szaporodas Szaporodás
            foreach (Ragadozo k in r)
            {
                // Fel
                if (k.y != 0 && k.x != 25)
                {
                    if (tomb[k.x, k.y - 1].BackColor == Color.Red)
                    {
                        //Le
                        if (k.y != 24 && k.x != 25)
                        {
                            if (tomb[k.x, k.y + 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y + 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y + 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        //Bal
                        if (k.x != 0 && k.x != 25)
                        {
                            if (tomb[k.x - 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x - 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x - 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        //Jobb
                        if (k.x != 24 && k.x != 25)
                        {
                            if (tomb[k.x + 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x + 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x + 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                    }
                }
                // Le
                if (k.y != 24 && k.x != 25)
                {
                    if (tomb[k.x, k.y + 1].BackColor == Color.Red)
                    {
                        // Fel
                        if (k.y != 0 && k.x != 25)
                        {
                            if (tomb[k.x, k.y - 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y - 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y - 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        // Bal
                        if (k.x != 0 && k.x != 25)
                        {
                            if (tomb[k.x - 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x - 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x - 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        // Jobb
                        if (k.x != 24 && k.x != 25)
                        {
                            if (tomb[k.x + 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x + 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x + 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                    }
                }
                // Bal
                if (k.x != 0 && k.x != 25)
                {
                    if (tomb[k.x - 1, k.y].BackColor == Color.Red)
                    {
                        // Fel
                        if (k.y != 0 && k.x != 25)
                        {
                            if (tomb[k.x, k.y - 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y - 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y - 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        //Le
                        if (k.y != 24 && k.x != 25)
                        {
                            if (tomb[k.x, k.y + 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y + 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y + 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;

                            }
                        }
                        // Jobb
                        if (k.x != 24 && k.x != 25)
                        {
                            if (tomb[k.x + 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x + 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x + 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                    }
                }
                // Jobb
                if (k.x != 24 && k.x != 25)
                {
                    if (tomb[k.x + 1, k.y].BackColor == Color.Red)
                    {
                        // Fel
                        if (k.y != 0 && k.x != 25)
                        {
                            if (tomb[k.x, k.y - 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y - 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y - 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                        //Le
                        if (k.y != 24 && k.x != 25)
                        {
                            if (tomb[k.x, k.y + 1].BackColor == Color.Gray)
                            {
                                tomb[k.x, k.y + 1].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x;
                                b.y = k.y + 1;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;

                            }
                        }
                        // Bal
                        if (k.x != 0 && k.x != 25)
                        {
                            if (tomb[k.x - 1, k.y].BackColor == Color.Gray)
                            {
                                tomb[k.x - 1, k.y].BackColor = Color.Red;
                                Ragadozo b = new Ragadozo();
                                b.x = k.x - 1;
                                b.y = k.y;
                                b.ehseg = ragadozo_ehseg;
                                r1.Add(b);
                                ragadozok++;
                                continue;
                            }
                        }
                    }
                }
            }
#endregion
            foreach (Novenyevo a in n1)
            {
                n.Add(a);
            }
            n1 = null;
            foreach (Ragadozo b in r1)
            {
                r.Add(b);
            }
            r1 = null;
        }
        private void Elet()
        {
            foreach (Novenyevo a in n)
            {
                if (a.x != 25)
                {
                    if (a.elet > 0)
                        a.elet -= 1;
                    else
                    {
                        tomb[a.x, a.y].BackColor = Color.Gray;
                        a.x = 25;
                        a.y = 0;
                        jelenleg_novenyevo--;
                    }
                }
            }
            foreach (Ragadozo z in r)
            {
                if (z.x != 25)
                {
                    if (z.ehseg > 0)
                        z.ehseg -= 1;
                    else
                    {
                        tomb[z.x, z.y].BackColor = Color.Gray;
                        z.x = 25;
                        z.y = 0;
                        ragadozok--;
                    }
                }
            }
        }

        private void ido_Tick(object sender, EventArgs e)
        {
            ido.Interval = Convert.ToInt32(textBox1.Text);
            
            Mozgas();
            Eves();
            Szaporodas();
            Elet();
            nap++;
            label1.Text = "Nap : " + nap + "\n\rNővényevők : " + jelenleg_novenyevo + "\n\rRagadozók : " + ragadozok;
        }
    }
}
