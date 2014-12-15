using System;
using System.Collections.Generic;
using System.Text;

namespace A15_Ex02
{
    public class TicTacToeView
    {
        private TicTacToeController m_GameController = new TicTacToeController();
        private int numberOfRound = 1;

        public TicTacToeView()
        {
            //while ( !islose && !isTRiwe)
            //m_GameControllerObj.
            this.startGame();
        }

        public void startGame()
        {
            int sizeOfBoard;
            int i;
            string outputStr;
            bool userQuit = false;
            bool anotherRound = true;
                 // reduce code to function ?

            sizeOfBoard = getValidSizeOfBoardFromUser();
            m_GameController.createBoard(sizeOfBoard);
            m_GameController.GameType = chooseGameType();      // m_Players has 2 options. need to know game vs pc or 2 players

            Console.WriteLine("You chose : " + m_GameController.GameType);   //string format

            Console.WriteLine("Player 1, please enter your name :");
            m_GameController.Player1 = new Player(Console.ReadLine());
            Console.WriteLine("Player 2, please enter your name :");
            m_GameController.Player2 = new Player(Console.ReadLine());
            Ex02.ConsoleUtils.Screen.Clear();
            m_GameController.randomChoosePlayer();

            // decice who starts - function
            if (m_GameController.Player1.PlayerSymbol.Equals(ePlayerSymbol.X))
            {
                Console.WriteLine(m_GameController.Player1.PlayerName + " you have been randomly chosen to start the game! You are X.");
                Console.WriteLine(m_GameController.Player2.PlayerName + " you are O.\n");   //string format?
            }
            else
            {
                Console.WriteLine(m_GameController.Player2.PlayerName + " you have been randomly chosen to start the game! You are X.");
                Console.WriteLine(m_GameController.Player1.PlayerName + " you are O.\n");     //string format?
            }

            displayBoard(m_GameController.GameBoard);

            // start filling board. start the game... 
            while (anotherRound == true)
            {
                for (i = 1; i <= m_GameController.GameBoard.SizeOfBoard * m_GameController.GameBoard.SizeOfBoard; i++)    // ways of ending this loop : found winner=break, one of users quit=break. draw= ?
                {
                    if (m_GameController.Player1.IsMyTurn == true)
                    {
                        userQuit = playerMove(m_GameController.Player1, i);       //record player last move if.... DONE
                    }
                    else
                    {
                        userQuit = playerMove(m_GameController.Player2, i);
                    }

                    if (userQuit == true)  // && pvp game
                    {
                        if (m_GameController.Player2.IsMyTurn == true)
                        {
                            m_GameController.Player2.Score++;
                            outputStr = String.Format(m_GameController.Player1.PlayerName + " has quit the game." + Environment.NewLine + m_GameController.Player2.PlayerName + " is the winner of this round !");
                            Console.WriteLine(outputStr);
                        }
                        else
                        {
                            m_GameController.Player1.Score++;
                            outputStr = String.Format(m_GameController.Player2.PlayerName + " has quit the game." + Environment.NewLine + m_GameController.Player1.PlayerName + " is the winner of this round !");
                            Console.WriteLine(outputStr);
                        }

                        break;
                    }

                    // check if we have a winner only after 5/9 moves have been made. 11/25. 4x4: 9/16  
                    // reduce to function
                    if ((i >= (m_GameController.GameBoard.SizeOfBoard * 2) - 1))
                    {
                        if (m_GameController.Player1.IsMyTurn == true)    // player1 might be the loser. player2 might be winner.
                        {
                            if (m_GameController.isCurrentPlayerLoser(m_GameController.Player1.PlayerSymbol, m_GameController.Player1.LastMoveRow, m_GameController.Player1.LastMoveCol) == true)     // coordinates of last move.  only after 5. kabel player!notsym
                            {
                                m_GameController.Player2.Score++;
                                Console.WriteLine(m_GameController.Player2.PlayerName + " is the winner of this round !");
                                break;
                            }
                        }
                        else    //player2 might be the loser. player1 might be the winner
                        {
                            if (m_GameController.isCurrentPlayerLoser(m_GameController.Player2.PlayerSymbol, m_GameController.Player2.LastMoveRow, m_GameController.Player2.LastMoveCol) == true)
                            {
                                m_GameController.Player1.Score++;
                                Console.WriteLine(m_GameController.Player1.PlayerName + " is the winner of this round !");
                                break;
                            }
                        }
                    }

                    if (i == (m_GameController.GameBoard.SizeOfBoard * m_GameController.GameBoard.SizeOfBoard)) //reached end... = draw ?
                    {
                        Console.WriteLine("Game Draw.");
                    }

                    // swap turns :
                    if (m_GameController.Player1.IsMyTurn == true)
                    {
                        m_GameController.Player1.IsMyTurn = false;
                        m_GameController.Player2.IsMyTurn = true;
                    }
                    else
                    {
                        m_GameController.Player2.IsMyTurn = false;
                        m_GameController.Player1.IsMyTurn = true;
                    }
                }  // end for - game moves
                // who starts next round ?

                outputStr = String.Format("Current score :\n{0} : {1}\n{2} : {3}\nWould you guys like to play another round? Press Y for yes or N for no.", m_GameController.Player1.PlayerName, m_GameController.Player1.Score, m_GameController.Player2.PlayerName, m_GameController.Player2.Score);
                Console.WriteLine(outputStr);
                anotherRound = verifyUserInputYesOrNo(m_GameController.GameBoard);
                if (anotherRound == true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    outputStr = String.Format("Round {0} - FIGHT !\n", ++numberOfRound);
                    Console.WriteLine(outputStr);
                    m_GameController.GameBoard.initBoard();
                    displayBoard(m_GameController.GameBoard);
                }
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public int getValidSizeOfBoardFromUser()
        {
            string boardSizeStr;
            int boardSize;
            bool goodInput = true;

            Console.WriteLine("Welcome. Enter size of Board (between 3 and 9) :");

            do
            {
                boardSizeStr = Console.ReadLine();
                goodInput = int.TryParse(boardSizeStr, out boardSize);
                if (boardSize < 3 || boardSize > 9)
                {
                    goodInput = false;
                }

                if (goodInput == false)
                {
                    Console.WriteLine("Wrong input. Please enter a number between 3 and 9 :");
                }
            }
            while (goodInput == false);

            return boardSize;
        }

        public eGameType chooseGameType()
        {
            eGameType gameOption;
            string playersOptionStr;
            string outputStr;
            bool goodInput;

            outputStr = String.Format(@"Please choose: 
1) Player vs. Player
2) Player vs. Computer");

            Console.WriteLine(outputStr);

            do
            {
                goodInput = true;
                playersOptionStr = Console.ReadLine();

                if (playersOptionStr != "1" && playersOptionStr != "2")   // compare to enum? use equals ?
                {
                    goodInput = false;
                }

                if (goodInput == false)
                {
                    Console.WriteLine("Wrong input. choose 1 or 2 :");
                }
            }
            while (goodInput == false);

            gameOption = (eGameType)Enum.Parse(typeof(eGameType), playersOptionStr);

            return gameOption;
        }

        public bool playerMove(Player i_CurrentPlayer, int i_MoveNumber)
        {
            int row = 0;
            int col = 0;
            bool isCoordinateTaken = true;
            bool userQuit;

            do
            {
                userQuit = getValidCoordinatePoint(m_GameController.GameBoard, ref row, "row", i_CurrentPlayer);   // send row or col.

                if (userQuit == true)
                {
                    break;
                }

                userQuit = getValidCoordinatePoint(m_GameController.GameBoard, ref col, "column", i_CurrentPlayer);

                if (userQuit == true)
                {
                    break;
                }

                isCoordinateTaken = m_GameController.checkIfCoordinateIsTaken(row, col, m_GameController.GameBoard);
                if(isCoordinateTaken == true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    displayBoard(m_GameController.GameBoard);
                    Console.WriteLine("The mentioned coordinates already taken! Please enter alternative coordinates.");
                }

            }
            while (isCoordinateTaken == true);

            if (userQuit == false)
            {
                m_GameController.GameBoard.setValueAtCooridnates(row - 1, col - 1, (Board.eCellType)i_CurrentPlayer.PlayerSymbol); //Check while debugging
                Ex02.ConsoleUtils.Screen.Clear();
               displayBoard(m_GameController.GameBoard);   // move this to setvalueatcoord function
            }

            if ((i_MoveNumber >= (m_GameController.GameBoard.SizeOfBoard * 2) - 1))    //save curren't players last move to check if winners
            {
                i_CurrentPlayer.LastMoveRow = row;   //proeblem ?
                i_CurrentPlayer.LastMoveCol = col;
            }

            return userQuit;
        }

        // public void getValidCoordinate
        public bool getValidCoordinatePoint(Board i_CurrentBoard, ref int io_CoordinatePoint, string i_RowOrCol, Player i_Player)// (no q) (call other func) [reuse]
        {
            string strCoordinatePoint;
            string outputStr;
            bool goodCoordinatePoint = false;
            bool userQuit = false;

            do
            {
                outputStr = String.Format(i_Player.PlayerName + ", please enter {0} number for " + i_Player.PlayerSymbol + " :", i_RowOrCol);
                Console.WriteLine(outputStr);
                strCoordinatePoint = Console.ReadLine();
                if (strCoordinatePoint.Equals("q") || strCoordinatePoint.Equals("Q"))
                {
                    goodCoordinatePoint = false;   // check this........... y is recognized as good input ! fix
                    Ex02.ConsoleUtils.Screen.Clear();
                    displayBoard(i_CurrentBoard);
                    Console.WriteLine("Are you sure you want to quit? press Y for yes. N for no.");
                    if (verifyUserInputYesOrNo(i_CurrentBoard) == true)
                    {
                        userQuit = true;
                        break;
                    }

                    Ex02.ConsoleUtils.Screen.Clear();
                    displayBoard(i_CurrentBoard);
                    continue;   // if presed Q and chose N, re loop
                }

                goodCoordinatePoint = int.TryParse(strCoordinatePoint, out io_CoordinatePoint);
                if (goodCoordinatePoint == false || io_CoordinatePoint > i_CurrentBoard.SizeOfBoard || io_CoordinatePoint < 1)   // change to sizeofmatrix or something
                {
                    goodCoordinatePoint = false;
                    Ex02.ConsoleUtils.Screen.Clear();
                    displayBoard(i_CurrentBoard);
                    Console.WriteLine("Invalid input! Please enter alternative coordinates.");
                    // continue; ??
                }
            }
            while (goodCoordinatePoint == false && userQuit == false);

            return userQuit;
        }

        public bool verifyUserInputYesOrNo(Board i_CurrentBoard)
        {
            string strUserYesOrNoChoice;
            bool userYesOrNoChoice = false;
            bool goodInput = true;

            do
            {
                strUserYesOrNoChoice = Console.ReadLine();
                if (strUserYesOrNoChoice.Equals("Y") || strUserYesOrNoChoice.Equals("y"))
                {
                    goodInput = true;
                    userYesOrNoChoice = true;
                }
                else if ((strUserYesOrNoChoice.Equals("N") || strUserYesOrNoChoice.Equals("n")))
                {
                    goodInput = true;
                    userYesOrNoChoice = false;
                }
                else
                {
                    goodInput = false;
                    Console.WriteLine("Invalid input. Please choose Y or N.");
                }
            }
            while (goodInput == false);

            return userYesOrNoChoice;
        }

        public void displayBoard(Board i_Board)
        {
            int i, j;
            StringBuilder boardString = new StringBuilder();
            
            for (i = 1; i <= i_Board.SizeOfBoard; i++)
            {
                boardString.Append("   " + i);
            }

            boardString.Append(Environment.NewLine);

            for(i = 0; i < i_Board.SizeOfBoard; i++)
            {
                boardString.Append(i + 1 + "|");
                for(j = 0; j < i_Board.SizeOfBoard; j++)
                {
                    Board.eCellType temp = i_Board.getValueAtCooridnates(i, j);
                    switch (temp)
                    {
                        case Board.eCellType.X:
                            boardString.Append(" " + Board.eCellType.X + " |");
                            break;
                        case Board.eCellType.O:
                            boardString.Append(" " + Board.eCellType.O + " |");
                            break;
                        case Board.eCellType.Empty:
                            boardString.Append("   |");
                            break;
                    }
                }

                boardString.Append(Environment.NewLine + " =");

                for(j = 0; j < i_Board.SizeOfBoard; j++)
                {
                    boardString.Append("====");
                }
                
                boardString.Append(Environment.NewLine);
            }
            
            Console.WriteLine(boardString);
        }
        /*
        public void displayMessage(string i_MsgStr)
        {
            Console.WriteLine(i_MsgStr);
        }

        public string getInputStringFromUser()
        {
            return Console.ReadLine();
        }*/
    }
}
