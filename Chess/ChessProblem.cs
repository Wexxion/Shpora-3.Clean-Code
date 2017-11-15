using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class ChessProblem
    {
        private static Board board;
        public static ChessStatus ChessStatus;

        public static void LoadFrom(string[] lines)
        {
            board = new BoardParser().ParseBoard(lines);
        }

        // МИША, все хуйня, давай по новой!
        // Определяет мат, шах или пат белым.
        public static void CalculateChessStatus()
        {
            var isCheck = IsCheck(PieceColor.Black);
            var hasMoves = false;

            foreach (var locFrom in board.GetPieces(PieceColor.White))
            foreach (var locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
                using (board.PerformTemporaryMove(locFrom, locTo))
                {
                    if (!IsCheck(PieceColor.Black))
                        hasMoves = true;
                }
            SetStatus(isCheck, hasMoves);
        }

        private static IEnumerable<Location> GetAllPossibleMoves(PieceColor color)
        {
            return board.GetPieces(color)
                .SelectMany(locFrom => board.GetPiece(locFrom)
                .GetMoves(locFrom, board));
        }

        private static void SetStatus(bool isCheck, bool hasMoves)
        {
            if (isCheck)
                ChessStatus = hasMoves ? ChessStatus.Check : ChessStatus.Mate;
            else if (hasMoves) ChessStatus = ChessStatus.Ok;
            else ChessStatus = ChessStatus.Stalemate;
        }

        // check — это шах
        private static bool IsCheck(PieceColor currentColor)
        {
            var isCheck = false;
            foreach (var loc in board.GetPieces(currentColor))
            {
                var piece = board.GetPiece(loc);
                var moves = piece.GetMoves(loc, board);
                foreach (var destination in moves)
                {
                    var otherColor = currentColor == PieceColor.Black ? PieceColor.White : PieceColor.Black;
                    if (board.GetPiece(destination).Is(otherColor, PieceType.King))
                        isCheck = true;
                }
            }
            return isCheck;
        }
    }
}