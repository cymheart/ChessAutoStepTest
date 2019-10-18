using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class Tools
    {
        private static Tools instance = null;
        public static Tools Instance
        {
            get
            {
                if (instance == null)
                    instance = new Tools();
                return instance;
            }
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            T obj = (T)Activator.CreateInstance(type);
            return obj;
        }

        public T DeepCopyByBinary<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public Piece CreatePiece(PieceType type)
        {
            switch(type)
            {
                case PieceType.King: return new King();
                case PieceType.Queen: return new Queen();
                case PieceType.Knight: return new Knight();
                case PieceType.Rook: return new Rook();
                case PieceType.Bishop: return new Bishop();
                case PieceType.Pawn: return new Pawn();
            }

            return null;
        }

        public Random Rand()
        {
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            return ra;
        }

    }
}
