using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Manakov.Nsudotnet.TicTacToe
{
    public partial class Form1 : Form
    {
        public Game game { get; set; }

        public Form1()
        {
            this.Width = Constants.biggerOffset * 5 + (Constants.lesserOffset * 4 + Constants.size * 3) * 3;
            this.Height = this.Width + Constants.biggerOffset;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void addButton(int i, int j, int k, int l)
        { 
            Button button = new Button();
            button.Image = Properties.Resources.Free;

            button.Size = new Size(Constants.size, Constants.size);
            button.Location = new Point(Constants.biggerOffset * (i+1) +
                                         (i) * (Constants.lesserOffset * 4 + Constants.size * 3) +
                                         (Constants.lesserOffset * (k+1)) + (Constants.size * (k)),

                                         Constants.biggerOffset * (j+1) +
                                         (j) * (Constants.lesserOffset * 4 + Constants.size * 3) +
                                         (Constants.lesserOffset * (l+1)) + (Constants.size * (l)));
            button.Tag = new Pack(i, j, k, l);
            button.Click += action;

            Controls.Add(button);
        }

        public void action(object sender, EventArgs e)
        {
            Pack pack = (Pack) (((Control)sender).Tag);
            
            if (game.cells[pack.i, pack.j].currentState == BiggerState.Availible)
            {
                Button button = (Button)((Control)sender);
                if (game.cells[pack.i, pack.j].cells[pack.k, pack.l].currentState == LesserState.Free)
                {
                    if (game.player == Player.PlayerCircle)
                    {
                        button.Image = Properties.Resources.Circle;
                        game.cells[pack.i, pack.j].cells[pack.k, pack.l].currentState = (LesserState.Circle);
                        game.player = Player.PlayerCross;
                    } else
                    {
                        button.Image = Properties.Resources.Cross;
                        game.cells[pack.i, pack.j].cells[pack.k, pack.l].currentState = (LesserState.Cross);
                        game.player = Player.PlayerCircle;
                    }
                    if (game.cells[pack.i, pack.j].recount(pack.k, pack.l))
                    {
                        if (game.recount(pack.i, pack.j) == 1) {
                            MessageBox.Show(this, "Won By Circle", "The End", MessageBoxButtons.OK);
                            this.Close();
                        }
                        if (game.recount(pack.i, pack.j) == 8) {
                            MessageBox.Show(this, "Won By Cross", "The End", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                    
                }

                if (game.cells[pack.k, pack.l].checkForSpace())
                {

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            game.cells[i, j].currentState = (BiggerState.Unavailible);
                        }
                    }

                    game.cells[pack.k, pack.l].currentState = (BiggerState.Availible);
                } else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (game.cells[i, j].checkForSpace())
                            {
                                game.cells[i, j].currentState = (BiggerState.Availible);
                            } else
                            {
                                game.cells[i,j].currentState = (BiggerState.Unavailible);
                            }
                        }
                    }
                }
            }

        }
    }

    public class Pack
    {
        public int i { get; set; }
        public int j { get; set; }
        public int k { get; set; }
        public int l { get; set; }

        public Pack(int i, int j, int k, int l)
        {
            this.i = i;
            this.j = j;
            this.k = k;
            this.l = l;
        }
    }

}
