using System;
using System.Collections.Generic;
using System.Text;

namespace A15_Ex02
{
    public class Board
    {
        private eCellType[,] m_TheBoard;
        private int m_SizeOfBoard;

        public enum eCellType
        {
            X, O, Empty
        };

        public Board(int i_SizeOfBoard)
	    {
            m_SizeOfBoard = i_SizeOfBoard;
            m_TheBoard = new eCellType[m_SizeOfBoard,m_SizeOfBoard];
            initBoard();
	    }

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