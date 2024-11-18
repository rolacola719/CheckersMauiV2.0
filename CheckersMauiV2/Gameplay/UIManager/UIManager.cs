using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
    public static class UIManager
    {

        // ************ INPUTS ********************* //
        public static int PosX { get => Input.SelectedColumn; }
        public static int PosY { get => Input.SelectedRow; }

        public static int PlayerTurn;

        public static void SelectPiece(object? sender, EventArgs e, Grid GameBoard)
        {

                Input.SelectPiece(sender, e, GameBoard);
            
        }
        public static void MoveToPosition(object sender, EventArgs e, Grid GameBoard)
        {
            Input.MoveToPosition(sender, e, GameBoard);
        }

        public static void ExecuteMove(object sender, EventArgs e, Grid GameBoard, int targetsXPos, int targetsYPos)
        {
            Input.ExecuteMove(sender, e, GameBoard,  targetsXPos, targetsYPos);
        }

        // ************ OUTPUTS ********************* //

        public static int Player1Score = 0;
        public static int Player2Score = 0;

        public static void DisplayScore(Grid GameBoard)
        {
            Output.DisplayScore(GameBoard);
        }
        public static void DisplayGamePieces(Board board, Grid GameBoard)
        {
            Output.DisplayGamePieces(board, GameBoard);
        }
        public static void DisplayPlayersTurn(Grid GameBoard)
        {
            Output.DisplayPlayerTurn(GameBoard);
        }
        public static void HidePossibleMove(Grid GameBoard)
        {
            Output.HidePossibleMove(GameBoard);
        }
        public static void CheckAndAutoSelectIfMandatoryMove()
        {
            Output.CheckAndAutoSelectIfMandatoryMove();
        }

    }
}
