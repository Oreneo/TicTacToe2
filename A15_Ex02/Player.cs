using System;
using System.Collections.Generic;
using System.Text;

namespace A15_Ex02
{
    public enum ePlayerSymbol
    {
        X, O
    };

    public enum ePlayerType //nullable
    {
        Human, Computer
    };

    public class Player
    {
        private int m_Score;
        private bool m_IsMyTurn = false;
        private string m_PlayerName = null;     // nullify.....pc has no name.
        private ePlayerSymbol m_PlayerSymbol;
        private ePlayerType m_PlayerType;
        private int? m_LastMoveRow;     // is it null by default? check this 
        private int? m_LastMoveCol;

        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
        }

        public Player()
        {

        }

        public void setPlayerType(ePlayerType i_PlayerType)
        {
            m_PlayerType = i_PlayerType;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public bool IsMyTurn
        {
            get
            {
                return m_IsMyTurn;
            }
            set
            {
                m_IsMyTurn = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }
            set
            {
                m_PlayerName = value;
            }
        }

        public ePlayerSymbol PlayerSymbol
        {
            get
            {
                return m_PlayerSymbol;
            }
            set
            {
                m_PlayerSymbol = value;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return m_PlayerType;
            }
            set
            {
                m_PlayerType = value;
            }
        }

        public int LastMoveRow
        {
            get
            {
                return (int)m_LastMoveRow;    // check this
            }
            set
            {
                m_LastMoveRow = value;
            }
        }

        public int LastMoveCol
        {
            get
            {
                return (int)m_LastMoveCol;    // and this
            }
            set
            {
                m_LastMoveCol = value;
            }
        }
    }
}