using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessConsole;
using ChessConsole.Helpers;
using Chess;
using GameBoard;

namespace UnitTest
{
    [TestClass]
    public class Tests
    {
    

        [TestMethod]
        public void Rank7SetupCorrectly_True_IfAllAreBlackPawns()
        {
            ChessMatch cm = new ChessMatch(2);
            bool valid = false;
            for (int i = 0; i < cm.Board.Columns; i++)
            {
                Piece p = cm.Board.GetPiece(1, i);
                valid = (p.GetType().Equals(typeof(Pawn)) && p.Color == Color.Black);
                
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Rank2SetupCorrectly_True_IfAllAreWhitePawns()
        {
            ChessMatch cm = new ChessMatch(2);
            bool valid = false;
            for (int i = 0; i < cm.Board.Columns; i++)
            {
                Piece p = cm.Board.GetPiece(6, i);
                valid = (p.GetType().Equals(typeof(Pawn)) && p.Color == Color.White);

            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Rank8SetupForTraditional_True_IfPiecesAreInCorrectTraditionalOrderForBlack()
        {
            ChessMatch cm = new ChessMatch(1);
            Board b = cm.Board;
            Color c = Color.Black;

            Piece[] rank8 = {
                new Rook(c,b),
                new Knight(c,b),
                new Bishop(c,b),
                new Queen(c,b),
                new King(c,b,cm),
                new Bishop(c,b),
                new Knight(c,b),
                new Rook(c,b)
            };

            for (int i = 0; i < rank8.Length; i++)
            {
                Piece p = cm.Board.GetPiece(0, i);
                Piece expectedPiece = rank8[i];

                Assert.IsTrue(p.GetType().Equals(expectedPiece.GetType()) && p.Color == Color.Black);   
            }

        }

        [TestMethod]
        public void Rank1SetupForTraditional_True_IfPiecesAreInCorrectTraditionalOrderForWhite()
        {
            ChessMatch cm = new ChessMatch(1);
            Board b = cm.Board;
            Color c = Color.White;

            Piece[] rank8 = {
                new Rook(c,b),
                new Knight(c,b),
                new Bishop(c,b),
                new Queen(c,b),
                new King(c,b,cm),
                new Bishop(c,b),
                new Knight(c,b),
                new Rook(c,b)
            };

            for (int i = 0; i < rank8.Length; i++)
            {
                Piece p = cm.Board.GetPiece(7, i);
                Piece expectedPiece = rank8[i];

                Assert.IsTrue(p.GetType().Equals(expectedPiece.GetType()) && p.Color == Color.White);
            }

        }

        [TestMethod]
        public void Rank8SetupForChess960_True_IfKingAndBishopPlacementsAbideByChess960Rules()
        {
            ChessMatch cm = new ChessMatch(2);
            Board b = cm.Board;
            (int rookLeft, int rookRight) rooks = (-1, -1);
            int king = -1;
            (int bishop1, int bishop2) bishops = (-1, -1);

            for (int i = 0; i < b.Columns; i++)
            {
                Piece p = cm.Board.GetPiece(0, i);

                if (p.GetType().Equals(typeof(Rook)))
                {
                    if(rooks.rookLeft == -1){rooks.rookLeft = p.Position.Column;}
                    else { rooks.rookRight = p.Position.Column; }

                }
                else if (p.GetType().Equals(typeof(King)))
                {
                    king = p.Position.Column;
                }
                else if (p.GetType().Equals(typeof(Bishop)))
                {
                    if (bishops.bishop1 == -1) { bishops.bishop1 = p.Position.Column; }
                    else { bishops.bishop2 = p.Position.Column; }
                }
            }

            bool rookKingRook = (rooks.rookLeft < king && rooks.rookRight > king);
            bool lightAndDarkBishop = (bishops.bishop1 % 2 == 0 && bishops.bishop2 % 2 != 0) || (bishops.bishop1 % 2 != 0 && bishops.bishop2 % 2 == 0);

            Assert.IsTrue(rookKingRook);
            Assert.IsTrue(lightAndDarkBishop);


        }


        [TestMethod]
        public void Rank1SetupForChess960_True_IfKingAndBishopPlacementsAbideByChess960Rules()
        {
            ChessMatch cm = new ChessMatch(2);
            Board b = cm.Board;
            (int rookLeft, int rookRight) rooks = (-1, -1);
            int king = -1;
            (int bishop1, int bishop2) bishops = (-1, -1);

            for (int i = 0; i < b.Columns; i++)
            {
                Piece p = cm.Board.GetPiece(7, i);

                if (p.GetType().Equals(typeof(Rook)))
                {
                    if (rooks.rookLeft == -1) { rooks.rookLeft = p.Position.Column; }
                    else { rooks.rookRight = p.Position.Column; }

                }
                else if (p.GetType().Equals(typeof(King)))
                {
                    king = p.Position.Column;
                }
                else if (p.GetType().Equals(typeof(Bishop)))
                {
                    if (bishops.bishop1 == -1) { bishops.bishop1 = p.Position.Column; }
                    else { bishops.bishop2 = p.Position.Column; }
                }
            }

            bool rookKingRook = (rooks.rookLeft < king && rooks.rookRight > king);
            bool lightAndDarkBishop = (bishops.bishop1 % 2 == 0 && bishops.bishop2 % 2 != 0) || (bishops.bishop1 % 2 != 0 && bishops.bishop2 % 2 == 0);

            Assert.IsTrue(rookKingRook);
            Assert.IsTrue(lightAndDarkBishop);


        }


    }
}
