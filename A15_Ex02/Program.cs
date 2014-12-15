using System;
using System.Collections.Generic;
using System.Text;

namespace A15_Ex02
{
    public class Program
// fix - nothing can access the view.
// random? yes or no?
// nullable, access modifiers, const???? row=col -> sizeofmatrix...
// composition ?
// stylecop
// clean project from git files??
// methods from player -> move to controller
    {
        public static void Main()
        {
            //TicTacToeController gameController = new TicTacToeController();
            TicTacToeView gameView = new TicTacToeView();
        }
    }
}


// shit to do :
// determine who starts after round is over (draw, win, quit) - loser/winner or random.
// work on Player vs pc
// delete pcplayer class
// wysiwyg