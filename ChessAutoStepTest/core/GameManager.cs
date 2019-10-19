//面试试题测试: by蔡业民 开始于 2019/10/17 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAutoStepTest
{
    public class GameManager
    {
        public Player[] players;
        public Chessboard chessBoard;

        /// <summary>
        /// 初始棋盘数据
        /// </summary>
        public Chessboard orgChessBoard;

        /// <summary>
        /// 走棋记录存储
        /// </summary>
        public RecordManager recordMgr;

      

        public int ChessBoardXCount = 8;
        public int ChessBoardYCount = 8;

        /// <summary>
        ///注意: 是否随机棋子初始位置,这个设置为true,
        ///可能导致开时时，王就在对方棋子旁边，而至使出现只有1轮就结束了
        /// </summary>
        public bool IsRandomPiecesPos = false;

        public bool IsRandomPiecesCount = false;

        int firstPlayPlayerIdx = 0;
        public ChessColor[] playerPieceColor = { ChessColor.White, ChessColor.Black};
        public int winPlayerIdx = -1;
        int playerCount = 2;
        int curtPlayPlayerIdx;

        int turnCount = 100;

        List<int> allPiecesIdx = new List<int>();
        List<int> pawn0Idx = new List<int>();
        List<int> pawn1Idx = new List<int>();


        public GameManager()
        {
        }

        public void CreateGame()
        {
            recordMgr = new RecordManager(this);

            chessBoard = new Chessboard();
            chessBoard.XCount = ChessBoardXCount;
            chessBoard.YCount = ChessBoardYCount;
            chessBoard.Create();

            players = new Player[2]
           {
                new Player(chessBoard),
                new Player(chessBoard)
           };



            curtPlayPlayerIdx = -1;
            CreateRandomIdxTable();          
            CreatePlayersBoardPieces();
            orgChessBoard = Tools.Instance.DeepCopyByBinary<Chessboard>(chessBoard);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Play()
        {
            int result = 0;
            for (int i = 0; i < turnCount; i++)
            {
                TurnToNextPlayer();
                AIPlay(players[curtPlayPlayerIdx]);

                int nextPlayerIdx = GetNextPlayerIdx();
                BoardIdx[] kingBoardIdx = players[nextPlayerIdx].GetPieceBoardIdxsByType(PieceType.King);
                if (kingBoardIdx == null)
                {
                    result = 1;
                    break;
                }

                TurnToNextPlayer();
                AIPlay(players[curtPlayPlayerIdx]);
                nextPlayerIdx = GetNextPlayerIdx();
                kingBoardIdx = players[nextPlayerIdx].GetPieceBoardIdxsByType(PieceType.King);
                if (kingBoardIdx == null)
                {
                    result = 1;
                    break;
                }
            }

            if(result == 1)
            {
                winPlayerIdx = curtPlayPlayerIdx;
            }
            else
            {
                winPlayerIdx = -1;
            }


            return 0;
        }



        /// <summary>
        /// 轮转玩家
        /// </summary>
        void TurnToNextPlayer()
        {
            curtPlayPlayerIdx = GetNextPlayerIdx();
        }

        /// <summary>
        /// 获取下一个玩家
        /// </summary>
        int GetNextPlayerIdx()
        {
            if (curtPlayPlayerIdx == -1)
                return firstPlayPlayerIdx;
            else
                return (curtPlayPlayerIdx + 1) % playerCount;
        }

        /// <summary>
        /// 吃子
        /// </summary>
        /// <param name="player">当前吃子玩家</param>
        /// <param name="eatBoardIdx"></param>
        /// <param name="beEatBoardIdx"></param>
        void Eat(Player player, BoardIdx eatBoardIdx, BoardIdx beEatBoardIdx, string tips = null)
        {
            int nextPlayerIdx = GetNextPlayerIdx();

            recordMgr.AppendRecord(
                curtPlayPlayerIdx, nextPlayerIdx, 
                eatBoardIdx, beEatBoardIdx, tips, ChessRecordType.Eat);

            player.EatOrMoveBoardPiece(eatBoardIdx, beEatBoardIdx);
            players[nextPlayerIdx].DelBoardPieceRef(beEatBoardIdx.x, beEatBoardIdx.y);
        }

        /// <summary>
        /// 移动棋子
        /// </summary>
        /// <param name="player">当前移子玩家</param>
        /// <param name="moveBoardIdx"></param>
        /// <param name="dstBoardIdx"></param>
        void Move(Player player, BoardIdx moveBoardIdx, BoardIdx dstBoardIdx, string tips = null)
        {
            int nextPlayerIdx = GetNextPlayerIdx();

           recordMgr.AppendRecord(curtPlayPlayerIdx, nextPlayerIdx,
                moveBoardIdx, dstBoardIdx, tips, ChessRecordType.Move);

            player.EatOrMoveBoardPiece(moveBoardIdx, dstBoardIdx);       
        }

        /// <summary>
        /// 玩家策略走棋
        /// </summary>
        /// <param name="player"></param>
        void AIPlay(Player player)
        {
            int nextPlayerIdx = GetNextPlayerIdx();

            //1.优先吃对方的王
            BoardIdx[] kingBoardIdx = players[nextPlayerIdx].GetPieceBoardIdxsByType(PieceType.King);
            BoardIdx? canEatKingBoardIdx = player.GetCanEatBoardIdx(kingBoardIdx[0]);
            if(canEatKingBoardIdx != null)
            {
                Eat(player, canEatKingBoardIdx.Value, kingBoardIdx[0], "(策略:优先吃对方的王)");
                return;
            }

            //2.王下回合被吃的情况下，优先移动王
            kingBoardIdx = player.GetPieceBoardIdxsByType(PieceType.King);
            canEatKingBoardIdx = players[nextPlayerIdx].GetCanEatBoardIdx(kingBoardIdx[0]);
            if (canEatKingBoardIdx != null)
            {
                Piece piece = chessBoard.GetPiece(kingBoardIdx[0]);
                BoardIdx[] eatPos = piece.ComputeEatPos(kingBoardIdx[0].x, kingBoardIdx[0].y, chessBoard);
                BoardIdx[] movePos = piece.ComputeMovePos(kingBoardIdx[0].x, kingBoardIdx[0].y, chessBoard);

                if (eatPos.Length != 0 || movePos.Length != 0)
                {
                    for (int i = 0; i < eatPos.Length; i++)
                    {
                        Eat(player, kingBoardIdx[0], eatPos[i], "(策略:王下回合被吃的情况下，优先移动王吃子)");
                        canEatKingBoardIdx = players[nextPlayerIdx].GetCanEatBoardIdx(eatPos[i]);
                        if (canEatKingBoardIdx == null)
                        {
                            return;
                        }
                        else
                        {
                             recordMgr.CancelRecord();
                        }
                    }

                    for (int i = 0; i < movePos.Length; i++)
                    {
                        Move(player, kingBoardIdx[0], movePos[i], "(策略:王下回合被吃的情况下，优先移动王)");
                        canEatKingBoardIdx = players[nextPlayerIdx].GetCanEatBoardIdx(movePos[i]);
                        if (canEatKingBoardIdx == null)
                        {
                            return;
                        }
                        else
                        {
                            recordMgr.CancelRecord();
                        }
                    }
                }
            }


            //3.吃棋子
            BoardIdx? boardIdx = player.GetRandomCanEatBoardIdx();
            if (boardIdx != null)
            {
                Piece piece = chessBoard.GetPiece(boardIdx.Value);
                BoardIdx[] canEatBoardIdxs = piece.ComputeEatPos(boardIdx.Value.x, boardIdx.Value.y, chessBoard);
                Random ra = Tools.Instance.Rand();
                int eatIdx = ra.Next(0, canEatBoardIdxs.Length - 1);
                BoardIdx eatBoardIdx = canEatBoardIdxs[eatIdx];

                Eat(player, boardIdx.Value, eatBoardIdx, "(策略:优先吃子)");
            }
            else
            { 
                //4.没有吃的情况下，随机移动棋子
                boardIdx = player.GetRandomCanMoveBoardIdx();
                Piece piece = chessBoard.GetPiece(boardIdx.Value);
                BoardIdx[] canMoveBoardIdxs = piece.ComputeMovePos(boardIdx.Value.x, boardIdx.Value.y, chessBoard);
                Random ra = Tools.Instance.Rand();
                int moveIdx = ra.Next(0, canMoveBoardIdxs.Length - 1);
                BoardIdx moveBoardIdx = canMoveBoardIdxs[moveIdx];

                Move(player, boardIdx.Value, moveBoardIdx, "(策略:没有吃的情况下，随机移动棋子)");
            }
        }

        
        /// <summary>
        /// 生成玩家棋盘棋子
        /// </summary>
        void CreatePlayersBoardPieces()
        {
            if(IsRandomPiecesPos)
                CreateRandomPlayersBoardPieces();
            else
                CreateStandardPlayersBoardPieces();
        }

        /// <summary>
        /// 生成标准的玩家棋盘棋子
        /// </summary>
        void CreateStandardPlayersBoardPieces()
        {
            CreateStandardPlayerBoardPieces(players[0], playerPieceColor[0], BoardDirection.Forward);
            CreateStandardPlayerBoardPieces(players[1], playerPieceColor[1], BoardDirection.Reverse);
        }

        void CreateStandardPlayerBoardPieces(Player player, ChessColor color, BoardDirection dir)
        {
            int pawnY = 1;
            int kingY = 0;

            if (dir == BoardDirection.Reverse)
            {
                kingY = 7;
                pawnY = 6;
            }

            //pawn
            for (int i = 0; i < 8; i++)
            {
                Pawn pawn = new Pawn();
                pawn.Color = color;
                pawn.PieceAtBoardDir = dir;
                pawn.IsFirstMove = true;
                chessBoard.AppendPiece(pawn, i, pawnY);
                player.AddBoardPieceRef(i, pawnY);
                 
            }

            //Rook
            Piece piece = Tools.Instance.CreatePiece(PieceType.Rook);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 0, kingY);
            player.AddBoardPieceRef(0, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Rook);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 7, kingY);
            player.AddBoardPieceRef(7, kingY);

            //Knight
            piece = Tools.Instance.CreatePiece(PieceType.Knight);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 1, kingY);
            player.AddBoardPieceRef(1, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Knight);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 6, kingY);
            player.AddBoardPieceRef(6, kingY);

            //Bishop
            piece = Tools.Instance.CreatePiece(PieceType.Bishop);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 2, kingY);
            player.AddBoardPieceRef(2, kingY);

            piece = Tools.Instance.CreatePiece(PieceType.Bishop);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 5, kingY);
            player.AddBoardPieceRef(5, kingY);

            //King
            piece = Tools.Instance.CreatePiece(PieceType.King);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 3, kingY);
            player.AddBoardPieceRef(3, kingY);

            //Queen
            piece = Tools.Instance.CreatePiece(PieceType.Queen);
            piece.Color = color;
            chessBoard.AppendPiece(piece, 4, kingY);
            player.AddBoardPieceRef(4, kingY);
        }

        

        /// <summary>
        /// 生成随机位置的玩家棋盘棋子
        /// </summary>
        void CreateRandomPlayersBoardPieces()
        {
            int[] piecesCount0 = CreatePiecesCount();
            int[] piecesCount1 = CreatePiecesCount();

            int pawnCount0 = piecesCount0[(int)PieceType.Pawn];
            int pawnCount1 = piecesCount1[(int)PieceType.Pawn];
            int totalPiecesCount = 0;

            for (int i = 0; i < piecesCount0.Length; i++)
            {
                if(i != (int)PieceType.Pawn)
                    totalPiecesCount += piecesCount0[i];
            }

            for (int i = 0; i < piecesCount1.Length; i++)
            {
                if (i != (int)PieceType.Pawn)
                    totalPiecesCount += piecesCount1[i];
            }
          
            //
           

            //兵不能在出现在第一行
            Random ra = Tools.Instance.Rand();

            for (int i=0; i<pawnCount0; i++)
            {
                Pawn pawn = new Pawn();
                pawn.IsFirstMove = true;
                pawn.Color = playerPieceColor[0];
                pawn.PieceAtBoardDir = BoardDirection.Forward;
                int n = ra.Next(0, pawn0Idx.Count);
                int randIdx = pawn0Idx[n];
                chessBoard.AppendPiece(pawn, randIdx >> 12, randIdx & 0xfff);
                players[0].AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);

                RemoveRandomIdxInTable(randIdx);
            }

            for (int i = 0; i < pawnCount1; i++)
            {
                Pawn pawn = new Pawn();
                pawn.IsFirstMove = true;
                pawn.Color = playerPieceColor[1];
                pawn.PieceAtBoardDir = BoardDirection.Reverse;
                int n = ra.Next(0, pawn1Idx.Count);
                int randIdx = pawn1Idx[n];
                chessBoard.AppendPiece(pawn, randIdx >> 12, randIdx & 0xfff);
                players[1].AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);

                RemoveRandomIdxInTable(randIdx);
            }


            //两个象不能在同一种色块格中
            if(piecesCount0[(int)PieceType.Bishop] == 2)
                CreateTowBishop(players[0], playerPieceColor[0]);

            if (piecesCount1[(int)PieceType.Bishop] == 2)
                CreateTowBishop(players[1], playerPieceColor[1]);


            //其它玩家的剩余棋子加入到棋盘
            int count;
            for (PieceType i = PieceType.King; i < PieceType.Pawn; i++)
            {
                count = piecesCount0[(int)i];
                if (count == 2 && i == PieceType.Bishop)
                    continue;

                for(int j = 0; j < count; j++)
                {
                    Piece piece = Tools.Instance.CreatePiece(i);
                    piece.Color = playerPieceColor[0];
                    int n = ra.Next(0, allPiecesIdx.Count);
                    int randIdx = allPiecesIdx[n];
                    RemoveRandomIdxInTable(randIdx);
                    chessBoard.AppendPiece(piece, randIdx >> 12, randIdx & 0xfff);
                    players[0].AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);
                }
            }


            for (PieceType i = PieceType.King; i < PieceType.Pawn; i++)
            {
                count = piecesCount1[(int)i];
                if (count == 2 && i == PieceType.Bishop)
                    continue;

                for (int j = 0; j < count; j++)
                {
                    Piece piece = Tools.Instance.CreatePiece(i);
                    piece.Color = playerPieceColor[1];
                    int n = ra.Next(0, allPiecesIdx.Count);
                    int randIdx = allPiecesIdx[n];
                    RemoveRandomIdxInTable(randIdx);
                    chessBoard.AppendPiece(piece, randIdx >> 12, randIdx & 0xfff);
                    players[1].AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);
                }
            }
        }

        public void CreateRandomIdxTable()
        {
            allPiecesIdx.Clear();
            pawn0Idx.Clear();
            pawn1Idx.Clear();


            for (int i = 0; i < ChessBoardXCount; i++)
            {
                for (int j = 0; j < ChessBoardXCount; j++)
                {
                    allPiecesIdx.Add(i << 12 | j);
                }
            }

            for (int i = 0; i < ChessBoardXCount; i++)
            {
                for (int j = 1; j < ChessBoardXCount; j++)
                {
                    pawn0Idx.Add(i << 12 | j);
                }
            }

            for (int i = 0; i < ChessBoardXCount; i++)
            {
                for (int j = 0; j < ChessBoardXCount - 1; j++)
                {
                    pawn1Idx.Add(i << 12 | j);
                }
            }
        }

        public void RemoveRandomIdxInTable(int randIdx)
        {
            pawn0Idx.Remove(randIdx);
            allPiecesIdx.Remove(randIdx);
            pawn1Idx.Remove(randIdx);
        }


        int[] CreatePiecesCount()
        {    
            int[] piecesCount = {0, 1 ,1, 2, 2, 2, 8 };

            if (IsRandomPiecesCount)
            {
                Random ra = Tools.Instance.Rand();
                piecesCount[(int)PieceType.Queen] = ra.Next(0, 1);
                piecesCount[(int)PieceType.Knight] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Rook] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Bishop] = ra.Next(0, 2);
                piecesCount[(int)PieceType.Pawn] = ra.Next(0, 8);
            }

            return piecesCount;
        }

        //两个象不能在同一种色块格中
        void CreateTowBishop(Player player, ChessColor chessColor)
        {
            Random ra = Tools.Instance.Rand();
            int n = ra.Next(0, allPiecesIdx.Count);
            int randIdx = allPiecesIdx[n];
            RemoveRandomIdxInTable(randIdx);

            Bishop bishop = new Bishop();
            bishop.Color = chessColor;
            chessBoard.AppendPiece(bishop, randIdx >> 12, randIdx & 0xfff);
            player.AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);
            ChessColor color1 = chessBoard.GetCellColor(randIdx >> 12, randIdx & 0xfff);

            ChessColor color2;
            while(true)
            {
                n = ra.Next(0, allPiecesIdx.Count);
                randIdx = allPiecesIdx[n];
                color2 = chessBoard.GetCellColor(randIdx >> 12, randIdx & 0xfff);
                if (color2 != color1)
                {
                    bishop = new Bishop();
                    bishop.Color = chessColor;
                    chessBoard.AppendPiece(bishop, randIdx >> 12, randIdx & 0xfff);
                    player.AddBoardPieceRef(randIdx >> 12, randIdx & 0xfff);

                    RemoveRandomIdxInTable(randIdx);
                    break;
                }
            }
        }

    }
}
