using System;
using System.Collections.Generic;
using System.Text;
// bla bla some comments
// more changes
namespace A15_Ex02
{
    // The Model
    public class Board
    {
        private eCellType[,] m_TheBoard;
        //private int m_NumOfRows;    // enum or int ? 3~9
        //private int m_NumOfCols;    // in case of expanding to non squared matrix.
        private int m_SizeOfBoard;

        // enum for celltype.
        public enum eCellType
        {
            X, O, Empty
        };

        public Board(int i_SizeOfBoard)
	    {
            // what size will board be??
            m_SizeOfBoard = i_SizeOfBoard;
            m_TheBoard = new eCellType[m_SizeOfBoard,m_SizeOfBoard];
            initBoard();
	    }

        // after size is determined 3~9
        public void initBoard()
        {
            int i, j;

            for (i = 0; i < m_SizeOfBoard; i++)
            {
                for (j = 0; j < m_SizeOfBoard; j++)
			    {
                    m_TheBoard[i,j] = eCellType.Empty;
			    }
            }
        }

        public void setValueAtCooridnates(int i_Row, int i_Col, eCellType i_Value)
        {
            m_TheBoard[i_Row, i_Col] = i_Value;
        }

        public eCellType getValueAtCooridnates(int i_Row, int i_Col)
        {
            return m_TheBoard[i_Row, i_Col];
        }

        public int SizeOfBoard
        {
            get
            {
                return m_SizeOfBoard;
            }
            set
            {
                m_SizeOfBoard = value;
            }
        }
    }
}