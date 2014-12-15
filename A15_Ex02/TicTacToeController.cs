using System;
using System.Collections.Generic;
using System.Text;

namespace A15_Ex02
{
    public enum eGameType
    {
        PlayerVsPlayer = 1,
        PlayerVsComputer = 2
    };

    public class TicTacToeController
    {
        private Board m_GameBoard = null;
        private Player m_Player1;
        private Player m_Player2;
        private eGameType m_GameType;
        Random random = new Random();

        //private TicTacToeView m_GameView = new TicTacToeView();


        public TicTacToeController()
        {
            //this.startGame();
        }

        public Board GameBoard
        {
            get
            {
                return m_GameBoard;
            }
            set
            {
                m_GameBoard = value;
            }
        }

        public eGameType GameType
        {
            get
            {
                return m_GameType;
            }
            set
            {
                m_GameType = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
            set
            {
                m_Player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
            set
            {
                m_Player2 = value;
            }
        }

        /*
        public void startGame()
        {
            int sizeOfBoard;
            sizeOfBoard = getValidSizeOfBoardFromUser();
            m_GameBoard = new Board(sizeOfBoard, sizeOfBoard);
            m_GameTypeChoice = chooseGameType();      // m_Players has 2 options. need to know game vs pc or 2 players

            m_GameView.displayMessage("You chose : " + m_GameTypeChoice);
            switch (m_GameTypeChoice)
            {
                case eGameType.PlayerVsPlayer:
                    startPlayerVsPlayerGame();
                    break;
                case eGameType.PlayerVsComputer:
                    startPlayerVsPcGame();
                    break;
            }

            m_GameView.displayMessage("Press enter to exit");
            m_GameView.getInputStringFromUser();
        }*/

        public void createBoard(int i_SizeOfBoard)
        {
            m_GameBoard = new Board(i_SizeOfBoard);
        }

        public void randomChoosePlayer()
        {
            if (random.Next(1, 3) == 1)
            {
                m_Player1.IsMyTurn = true;
                m_Player1.PlayerSymbol = ePlayerSymbol.X;
                m_Player2.PlayerSymbol = ePlayerSymbol.O;
            }
            else
            {
                m_Player2.IsMyTurn = true;
                m_Player2.PlayerSymbol = ePlayerSymbol.X;
                m_Player1.PlayerSymbol = ePlayerSymbol.O;
            }
        }

        public void gameProgress ()
        {

        }

        public bool checkIfCoordinateIsTaken(int i_row, int i_col, Board i_CurrentBoard)
        {
            bool isTaken = false;

            if (!(i_CurrentBoard.getValueAtCooridnates(i_row - 1, i_col - 1).Equals(Board.eCellType.Empty)))
            {
                isTaken = true;
            }
            return isTaken;
        }
        
        /*
        public void startPlayerVsPcGame()
        {
            m_GameView.displayMessage("Player, please enter your name :");
            Player humanPlayer = new Player(m_GameView.getInputStringFromUser());
            PcPlayer pcPlayer = new PcPlayer();
            Random random = new Random();

            if (random.Next(1, 3) == 1)
            {
                humanPlayer.IsMyTurn = true;
                m_GameView.displayMessage(humanPlayer.PlayerName + " you have been randomly chosen to start the game! You are X.");
            }
            else
            {
                pcPlayer.IsMyTurn = true;
                m_GameView.displayMessage("The PC has been randomly chosen to start the game!");
                m_GameView.displayMessage(humanPlayer.PlayerName + " you are O.");
            }

            m_GameView.displayBoard(m_GameBoard);
        }
        */

        // debug this.
        public bool isCurrentPlayerLoser(ePlayerSymbol i_CurrentPlayerSymbol, int i_CurrentPlayerLastMoveRow, int i_CurrentPlayerLastMoveCol)    // consider reduce to 4 functions
        {
            // coordinates start from 1 not 0
            bool isLoser = true;
            int rowIndex;
            int colIndex;

            //wrap in while or do while to avoid going through all the unnecessary loops
            while(true)
            {
                for(rowIndex = 0; rowIndex < m_GameBoard.SizeOfBoard; rowIndex++)
                {
                    if (!(m_GameBoard.getValueAtCooridnates(rowIndex, i_CurrentPlayerLastMoveCol - 1).Equals((Board.eCellType)i_CurrentPlayerSymbol)))
                    {
                        isLoser = false;   // symbols not equal - he didn't lose
                        break;
                    }
                }

                if(isLoser == true)
                {
                    break;
                }

                isLoser = true;

                for(colIndex = 0; colIndex < m_GameBoard.SizeOfBoard; colIndex++)
                {
                    if(!(m_GameBoard.getValueAtCooridnates(i_CurrentPlayerLastMoveRow - 1, colIndex).Equals((Board.eCellType)i_CurrentPlayerSymbol)))
                    {
                        isLoser = false;   // symbols not equal - he didn't lose
                        break;
                    }
                }

                if(isLoser == true)
                {
                    break;
                }

                isLoser = true;

                if(i_CurrentPlayerLastMoveRow == i_CurrentPlayerLastMoveCol)     // might be diagnol win
                {
                    for(rowIndex = 0; rowIndex < m_GameBoard.SizeOfBoard; rowIndex++)
                    {
                        if(!(m_GameBoard.getValueAtCooridnates(rowIndex, rowIndex).Equals((Board.eCellType)i_CurrentPlayerSymbol)))
                        {
                            isLoser = false;   // symbols not equal - he didn't lose
                            break;
                        }
                    }

                    //break ? nah
                }
                else   // if condition is not necessarily true
                {
                    isLoser = false;
                    // break;  ??bad. cant break cuz we need to check 2nd diagnol
                }

                if(isLoser == true)
                {
                    break;
                }

                isLoser = true;

                // other diangol now
                if((i_CurrentPlayerLastMoveRow + i_CurrentPlayerLastMoveCol) == (m_GameBoard.SizeOfBoard + 1))
                {
                    for(rowIndex = 0, colIndex = m_GameBoard.SizeOfBoard - 1; rowIndex < m_GameBoard.SizeOfBoard; rowIndex++, colIndex--)
                    {
                        if(!(m_GameBoard.getValueAtCooridnates(rowIndex, colIndex).Equals((Board.eCellType)i_CurrentPlayerSymbol)))
                        {
                            isLoser = false;   // symbols not equal - he didn't lose
                            break;
                        }
                    }

                    break; //... if he didn't lose here.. we need to exit.
                }
                else   // if condition is not necessarily true
                {
                    isLoser = false;
                    break;
                }
            } // end while(true)

            return isLoser;
        }
    }
}