using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

namespace CheckersMAUIV2
{
    public static class Input
    {

        public static bool PieceSelected = false;

        public static int SelectedRow { get => selectedRow; private set => selectedRow = value; }
        private static int selectedRow;

        public static int SelectedColumn { get => selectedColumn; private set => selectedColumn = value; }
        private static int selectedColumn;
        private static ImageButton selectedGamePiece;

        public static void SelectPiece(object? sender, EventArgs e, Grid GameBoard)
        {
            if (sender is ImageButton clickedButton)
            {
                if (!PieceSelected)
                {

                    selectedRow = Grid.GetRow(clickedButton);
                    selectedColumn = Grid.GetColumn(clickedButton);



                    if (UIManager.PlayerTurn == Player.ReturnPlayerNumber(selectedColumn, selectedRow))
                    {
                        
                        PieceSelected = true;
                        Debug.WriteLine($"The selectedPiece is at{selectedRow} and {selectedColumn}");
                        selectedGamePiece = clickedButton;
                        clickedButton.BackgroundColor = Colors.Green;
                        Output.DisplayPossibleMoves(selectedColumn, selectedRow, GameBoard);

                       
                    }

                }
                else if (PieceSelected)
                {
                    int row = Grid.GetRow(clickedButton);
                    int column = Grid.GetColumn(clickedButton);

                    if (row == selectedRow && column == selectedColumn)
                    {
                        clickedButton.BackgroundColor = Colors.Transparent;
                        PieceSelected = false;
                        UIManager.HidePossibleMove(GameBoard);
                    }
                }
            }
        }
        public static void MoveToPosition(object sender, EventArgs e, Grid GameBoard)
        {

            {
                if (sender is ImageButton clickedButton)
                {
                    int destinationRow = Grid.GetRow(clickedButton);
                    int destinationColumn = Grid.GetColumn(clickedButton);

                    GamePiece gamePiece = Player.ReturnGamePiece(SelectedColumn, selectedRow);

                    gamePiece.posX = destinationColumn;
                    gamePiece.posY = destinationRow;
                    gamePiece.CheckKing();

                    Grid.SetColumn(selectedGamePiece, destinationColumn);
                    Grid.SetRow(selectedGamePiece, destinationRow);
                    selectedGamePiece.BackgroundColor = Colors.Transparent;
                    PieceSelected = false;
                    UIManager.HidePossibleMove(GameBoard);


                    if (UIManager.PlayerTurn == 1) UIManager.PlayerTurn = 2;
                    else if (UIManager.PlayerTurn == 2) UIManager.PlayerTurn = 1;
                    UIManager.DisplayPlayersTurn(GameBoard);
                    UIManager.CheckAndAutoSelectIfMandatoryMove();

                }
            }
        }
        public static void ExecuteMove(object sender, EventArgs e, Grid GameBoard, int targetsXPos, int targetsYPos)
        {
            if (sender is ImageButton clickedButton)
            {
                int destinationRow = Grid.GetRow(clickedButton);
                int destinationColumn = Grid.GetColumn(clickedButton);

                GamePiece gamePiece = Player.ReturnGamePiece(SelectedColumn, selectedRow);

                gamePiece.posX = destinationColumn;
                gamePiece.posY = destinationRow;
                gamePiece.CheckKing();

                Grid.SetColumn(selectedGamePiece, destinationColumn);
                Grid.SetRow(selectedGamePiece, destinationRow);
                selectedGamePiece.BackgroundColor = null;
                PieceSelected = false;
                UIManager.HidePossibleMove(GameBoard);

                

                if (UIManager.PlayerTurn == 1) UIManager.Player1Score += 1;
                else if (UIManager.PlayerTurn == 2) UIManager.Player2Score += 1;
                Debug.WriteLine($"Player1 score: {UIManager.Player1Score}. Player 2 score: {UIManager.Player2Score}.");
                UIManager.DisplayScore(GameBoard);
                Player.ReturnGamePiece(targetsXPos, targetsYPos).Deactivate(GameBoard);
                UIManager.CheckAndAutoSelectIfMandatoryMove();

                if (!Player.CanCaptureOpponent(UIManager.PlayerTurn, out int playerArrayNumber))
                {
                    if (UIManager.PlayerTurn == 1) UIManager.PlayerTurn = 2;
                    else if (UIManager.PlayerTurn == 2) UIManager.PlayerTurn = 1;
                    UIManager.DisplayPlayersTurn(GameBoard);
                    UIManager.CheckAndAutoSelectIfMandatoryMove();
                }
            }
        }
    }
}

    



    





