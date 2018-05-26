using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manakov.Nsudotnet.TicTacToe
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Game game = new Game(form);
            form.game = game;
            Application.Run(form);
        }
    
    }

    public class Game
    {
        public Player player { get; set; }
        public BiggerCell[,] cells { get; set; }
        
        public Game(Form1 form)
        {
            this.player = Player.PlayerCross;
            this.cells = new BiggerCell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cells[i, j] = new BiggerCell(i, j, form);
                }
            }
        }

        public int recount (int i, int j)
        {
            int state = 1;
            for (int s = 0; s < 3; s++)
            {
                state = state * ((int)cells[s, j].playState);
            }
            if (state == 1) { return state; }
            if (state == 8) { return state; }

            state = 1;

            for (int s = 0; s < 3; s++)
            {
                state = state * ((int)cells[i, s].playState);
            }
            if (state == 1) { return state; }
            if (state == 8) { return state; }

            state = 1;
            for (int s = 0; s < 3; s++)
            {
                state = state * ((int)cells[s, s].playState);
            }
            if (state == 1) { return state; }
            if (state == 8) { return state; }

            state = 1;

            for (int s = 0; s < 3; s++)
            {
                state = state * ((int)cells[s, 2 - s].playState);
            }
            if (state == 1) { return state; }
            if (state == 8) { return state; }

            return -1;
        }
    }

    public class BiggerCell
    {
        public LesserCell[,] cells { get; set; }
        public BiggerState currentState { get; set; }
        public FieldPlayState playState { get; set; }

        public BiggerCell(int i, int j, Form1 form)
        {
            this.cells = new LesserCell[3, 3];
            for (int k = 0; k < 3; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    cells[k, l] = new LesserCell(i, j, k, l, form);
                }
            }
            this.currentState = BiggerState.Availible;
            this.playState = FieldPlayState.Playing;
        }

        public Boolean checkForSpace()
        {
            for (int i = 0; i< 3; i++)
            {
                for (int j = 0; j< 3; j++)
                {
                    if (cells[i,j].currentState == LesserState.Free)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean recount(int k, int l)
        {
            if (playState != FieldPlayState.Playing) return false;

            int state = 1;
            for (int i = 0; i< 3; i++)
            {
                state = state * ((int) cells[i, l].currentState);
            }
            if (state == 1) { playState = FieldPlayState.WonByCircle; return true; }
            if (state == 8) { playState = FieldPlayState.WonByCross; return true; }

            state = 1;

            for (int i = 0; i < 3; i++)
            {
                state = state * ((int)cells[k, i].currentState);
            }
            if (state == 1) { playState = FieldPlayState.WonByCircle; return true; }
            if (state == 8) { playState = FieldPlayState.WonByCross; return true; }

            state = 1;

            for (int i = 0; i < 3; i++)
            {
                state = state * ((int)cells[i, i].currentState);
            }
            if (state == 1) { playState = FieldPlayState.WonByCircle; return true; }
            if (state == 8) { playState = FieldPlayState.WonByCross; return true; }

            state = 1;

            for (int i = 0; i < 3; i++)
            {
                state = state * ((int)cells[i, 2 - i].currentState);
            }
            if (state == 1) { playState = FieldPlayState.WonByCircle; return true; }
            if (state == 8) { playState = FieldPlayState.WonByCross; return true; }

            return false;
        }
        
    }

    public class LesserCell
    {
        public LesserState currentState { get; set; }

        public LesserCell(int i, int j, int k, int l, Form1 form)
        {
            this.currentState = LesserState.Free;
            form.addButton(i, j, k, l);
        }
    }

    public enum LesserState { Free, Circle, Cross };
    public enum BiggerState { Availible, Unavailible };
    public enum FieldPlayState { Playing, WonByCircle, WonByCross};
    public enum Player { PlayerCircle, PlayerCross };
}
