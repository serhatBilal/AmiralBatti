using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace AmiralBatti
{
    public partial class Form1 : Form
    {
        int[,] grid = new int[7, 6];
        int remaining_shots = 12;
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                int shipLength;
                if (i == 0) shipLength = 2;
                else if (i == 1) shipLength = 3;
                else shipLength = 4;
                bool placed = false;
                while (!placed)
                {
                    int x = random.Next(7);
                    int y = random.Next(6);
                    int direction = random.Next(2);
                    if (direction == 0) 
                    {
                        if (x + shipLength > grid.GetLength(0)) continue;
                        if (x >= 0 && x < 7 && y >= 0 && y < 6)
                        {
                            if (grid[x, y] != 0) continue;
                        }

                        placed = true;
                        for (int j = 0; j < shipLength; j++)
                        {
                            if (y + j >= 0 && y + j < grid.GetLength(1) && grid[x, y + j] != 0)
                            {
                                placed = false;
                                break;
                            }
                        }

                        if (placed)
                        {
                            for (int j = 0; j < shipLength; j++)
                            {
                                if (y + j >= 0 && y + j < grid.GetLength(1)) grid[x, y + j] = 1;
                            }
                        }
                    }
                    else 
                    {
                        if (y + shipLength > grid.GetLength(1)) continue;
                        if (grid[x, y] != 0) continue;

                        placed = true;
                        for (int j = 0; j < shipLength; j++)
                        {
                            if (grid[x, y + j] != 0)
                            {
                                placed = false;
                                break;
                            }
                        }

                        if (placed)
                        {
                            for (int j = 0; j < shipLength; j++)
                            {
                                grid[x, y + j] = 1;
                            }
                        }
                    }
                }
            }


        }

        public void buttonClick(object sender, EventArgs e)
        {
            Button hitButton = (Button)sender;

            int x = hitButton.Location.X / hitButton.Width;
            int y = hitButton.Location.Y / hitButton.Height;


            if (x >= 0 && x < 7 && y >= 0 && y < 7)
            {
                if (grid[x, y] == 1)
                {
                    hitButton.BackColor = Color.Red;
                    hitButton.Enabled = false;
                    grid[x, y] = -1;
                }
                else
                {
                    hitButton.BackColor = Color.White;
                    hitButton.Enabled = false;
                    hitButton.Text = "-";
                }
                remaining_shots--;
                if (remaining_shots <= 0)
                {
                    MessageBox.Show("Hakkınız bitti");
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            if (grid[i, j] == 1)
                            {
                                foreach (Control control in this.Controls)
                                {
                                    if (control is Button && control.TabIndex == i * 7 + j)
                                    {
                                        control.BackColor = Color.Green;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    foreach (Control control in this.Controls)
                    {
                        if (control is Button)
                        {
                            control.Enabled = false;
                        }
                    }
                }


            }
                        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.Click += new EventHandler(buttonClick);
                }
            }
        }
    }
}
