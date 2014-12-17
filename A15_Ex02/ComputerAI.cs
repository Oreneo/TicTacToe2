using System;
using System.Collections.Generic;
using System.Text;
/*
tictactoe AI :

1. view matrix as single dimension array
2. isolate the empty array cells                 1[empty] 2[x] 3[empty] 4[O] 5[x]
   ==> maarach monim : 1,3 are good
3.   give nikud to each
4. lose or not lose
5. choose random move from not lose
   */

namespace A15_Ex02
{
    class ComputerAI
    {
        private Board m_GameBoardCopy;
        //private TicTacToeController m_GameController;  // object
        private Random random;
        private Random random2;

        public ComputerAI(Board i_GameBoard)
        {
            //m_GameController = 
            m_GameBoardCopy = i_GameBoard;   //same board, different ref var
        }

        public void computerMove(TicTacToeController i_GameController, Player i_CurrentPlayer, int index)
        {
            bool validMove;
            //int randomRow; 
            //int randomCol;

            // sometimes computer doesn;t make a move - not displayed on board............. FIX, computer not doing turn
            // save available options in array..
            // force him to lose if no other option.

            do
            {
                validMove = true;
                random = new Random();
                random2 = new Random();
                i_CurrentPlayer.LastMoveRow = random.Next(1, m_GameBoardCopy.SizeOfBoard + 1); // check if random range is good 1~3
                i_CurrentPlayer.LastMoveCol = random2.Next(1, m_GameBoardCopy.SizeOfBoard + 1);
                Console.WriteLine("LastMoveRow = " + i_CurrentPlayer.LastMoveRow);
                Console.WriteLine("LastMoveCol = " + i_CurrentPlayer.LastMoveCol);

                if ((m_GameBoardCopy.getValueAtCooridnates(i_CurrentPlayer.LastMoveRow - 1, i_CurrentPlayer.LastMoveCol - 1).Equals(Board.eCellType.Empty)))
                {
                    m_GameBoardCopy.setValueAtCooridnates(i_CurrentPlayer.LastMoveRow - 1, i_CurrentPlayer.LastMoveCol - 1, (Board.eCellType)i_CurrentPlayer.PlayerSymbol);

                    if (index >= (i_GameController.GameBoard.SizeOfBoard * 2) - 1) //only now need to check if losing
                    {
                        if (i_GameController.isCurrentPlayerLoser(i_CurrentPlayer.PlayerSymbol, i_CurrentPlayer.LastMoveRow, i_CurrentPlayer.LastMoveCol))
                        {
                            validMove = false;
                            m_GameBoardCopy.setValueAtCooridnates(i_CurrentPlayer.LastMoveRow - 1, i_CurrentPlayer.LastMoveCol - 1, Board.eCellType.Empty);
                        }
                        else
                        {
                            validMove = true;
                        }
                    }
                }
                else
                {
                    validMove = false;
                }
            }
            while(validMove == false);
            //m_GameController


            // if only one cell is left - computer must take that option
            //choose random one number or 2. 2. check if lose. lose = bad = dont make that move

        }
    }
}
