using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
    public static class Output
    {
        public static void DisplayPlayerTurn(Grid GameBoard)
        {
            if (GameBoard.Parent is Grid parent)
            {
                foreach (var child in parent.Children)
                {
                    if (child is VerticalStackLayout stack)
                    {
                        var playerTurn = stack.FindByName<Label>("WhichPlayerTurn");
                        playerTurn.Text = $"Player {UIManager.PlayerTurn}'s Turn";
                    }
                }
            }
        }
        public static void DisplayScore(Grid GameBoard)
        {
            if (GameBoard.Parent is Grid parent)
            {
                foreach (var child in parent.Children)
                {
                    if (child is VerticalStackLayout stack)
                    {
                        // Find Player1Score Label
                        var player1Score = stack.FindByName<Label>("Player1Score");
                        if (player1Score != null)
                        {
                            player1Score.Text = $"Player 1 Score: {UIManager.Player1Score}";
                        }

                        // Find Player2Score Label
                        var player2Score = stack.FindByName<Label>("Player2Score");
                        if (player2Score != null)
                        {
                            player2Score.Text = $"Player 2 Score: {UIManager.Player2Score}"; // Example logic
                        }

                        if (UIManager.Player1Score == 12)
                        {
                            player1Score.Text = $"Player 1 is the winner!";
                            player2Score.Text = "";
                        }
                        if (UIManager.Player2Score == 12)
                        {
                            player2Score.Text = $"Player 2 is the winner!";
                            player1Score.Text = "";
                        }

                    }
                }
            }
        }
        public static void DisplayGamePieces(Board board, Grid GameBoard)
        {
            foreach (Tile tile in board.tile)
            {
                foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                {
                    if ((gamepiece.posY == tile.BoardPosY) && (gamepiece.posX == tile.BoardPosX) && (gamepiece.isActive))
                    {
                        GameBoard.Add(gamepiece.gamePieceButton);
                    }

                }
            }
        }
        public static void DisplayPossibleMoves(int xCord, int yCord, Grid GameBoard)
        {

            if (Input.PieceSelected)
            {
                Player.CheckIfAdjacentToGamePiece(UIManager.PlayerTurn, xCord, yCord, out bool gamepieceBL, out bool gamepieceTR, out bool gamepieceTL, out bool gamepieceBR);

                GamePiece selectedGamePiece = Player.ReturnGamePiece(xCord, yCord);

                if (selectedGamePiece.ownedBy == 1 && selectedGamePiece.isKing == false && !selectedGamePiece.ThisPieceCanCapture(xCord,yCord))
                {
                    if (xCord + 1 <= 7 && yCord - 1 >= 0) CompletePossibleMoveSubroutine(gamepieceTR, xCord, yCord, +1, -1, GameBoard);
                    if (xCord - 1 >= 0 && yCord - 1 >= 0) CompletePossibleMoveSubroutine(gamepieceTL, xCord, yCord, -1, -1, GameBoard);

                }
                else if (selectedGamePiece.ThisPieceCanCapture(xCord, yCord))
                {
                    if (xCord + 2 <= 7 && yCord - 2 >= 0) CompletePossibleExecuteMoveSubroutine(gamepieceTR, xCord, yCord, +1, -1, GameBoard);
                    if (xCord - 2 >= 0 && yCord - 2 >= 0) CompletePossibleExecuteMoveSubroutine(gamepieceTL, xCord, yCord, -1, -1, GameBoard);
                }


                if (selectedGamePiece.ownedBy == 2 && selectedGamePiece.isKing == false && !selectedGamePiece.ThisPieceCanCapture(xCord, yCord))
                {
                    if (xCord - 1 >= 0 && yCord + 1 <= 7) CompletePossibleMoveSubroutine(gamepieceBL, xCord, yCord, -1, +1, GameBoard);
                    if (xCord + 1 <= 7 && yCord + 1 <= 7) CompletePossibleMoveSubroutine(gamepieceBR, xCord, yCord, +1, +1, GameBoard);
                }
                else if (selectedGamePiece.ThisPieceCanCapture(xCord, yCord))
                {
                    if (xCord - 2 >= 0 && yCord + 2 <= 7) CompletePossibleExecuteMoveSubroutine(gamepieceBL, xCord, yCord, -1, +1, GameBoard);
                    if (xCord + 2 <= 7 && yCord + 2 <= 7) CompletePossibleExecuteMoveSubroutine(gamepieceBR, xCord, yCord, +1, +1, GameBoard);
                }

                if (selectedGamePiece.isKing && !selectedGamePiece.ThisPieceCanCapture(xCord, yCord))
                {
                    if (xCord - 1 >= 0 && yCord + 1 <= 7) CompletePossibleMoveSubroutine(gamepieceBL, xCord, yCord, -1, +1, GameBoard);
                    if (xCord + 1 <= 7 && yCord - 1 >= 0) CompletePossibleMoveSubroutine(gamepieceTR, xCord, yCord, +1, -1, GameBoard);
                    if (xCord - 1 >= 0 && yCord - 1 >= 0) CompletePossibleMoveSubroutine(gamepieceTL, xCord, yCord, -1, -1, GameBoard);
                    if (xCord + 1 <= 7 && yCord + 1 <= 7) CompletePossibleMoveSubroutine(gamepieceBR, xCord, yCord, +1, +1, GameBoard);

                }
                else if (selectedGamePiece.ThisPieceCanCapture(xCord, yCord))
                {
                    if (xCord - 2 >= 0 && yCord + 2 <= 7) CompletePossibleExecuteMoveSubroutine(gamepieceBL, xCord, yCord, -1, +1, GameBoard);
                    if (xCord + 2 <= 7 && yCord - 2 >= 0) CompletePossibleExecuteMoveSubroutine(gamepieceTR, xCord, yCord, +1, -1, GameBoard);
                    if (xCord - 2 >= 0 && yCord - 2 >= 0) CompletePossibleExecuteMoveSubroutine(gamepieceTL, xCord, yCord, -1, -1, GameBoard);
                    if (xCord + 2 <= 7 && yCord + 2 <= 7) CompletePossibleExecuteMoveSubroutine(gamepieceBR, xCord, yCord, +1, +1, GameBoard);
                }
            }

        }
        private static void CompletePossibleMoveSubroutine(bool pieceInDirection, int xCord, int yCord, int xMod, int yMod, Grid GameBoard)
        {
            int xModifier = 2;
            int yModifier = 2;
            xModifier *= xMod;
            yModifier *= yMod;

            //Checking to see if possible move fits within confines of Game Board

            //If there is no gamepiece blocking it in that direction
            if (!pieceInDirection)
            {
                //Create game piece in that direction
                InstantiatePossibleMoveIcon(GameBoard, xCord, yCord, xMod, yMod);
            }
            // if there is an enemy gamepiece in that direction but no gamepiece after
        }
        private static void CompletePossibleExecuteMoveSubroutine(bool pieceInDirection, int xCord, int yCord, int xMod, int yMod, Grid GameBoard)
        {
            int xModifier = 2;
            int yModifier = 2;
            xModifier *= xMod;
            yModifier *= yMod;

            //Checking to see if piece is infront of player
            if (pieceInDirection)
            {

                GamePiece target = Player.ReturnGamePiece(xCord + (xModifier / 2), (yCord + yModifier / 2));

                // checking to see if that piece is not owned by the player
                if (target.ownedBy != UIManager.PlayerTurn)
                {
                    if (Player.IsOccupied(xCord + (xModifier), (yCord + yModifier)) == false)
                        InstantiatePossibleExecuteMoveIcon(GameBoard, target.posX, target.posY, xMod, yMod);
                }

            }
        }
        public static void HidePossibleMove(Grid GameBoard)
        {
            // Iterate over all children of the layout
            foreach (var child in GameBoard.Children.ToList())
            {
                // Check if the child is an Image control
                if (child is ImageButton image)
                {
                    // Check if the Image source matches the name you're looking for
                    if (image.Source.ToString().Contains("selected_icon.png"))
                    {
                        // Remove the image from the layout
                        GameBoard.Children.Remove(image);
                    }
                }
            }
        }
        private static void InstantiatePossibleMoveIcon(Grid GameBoard, int xCord, int yCord, int xMod, int yMod)
        {
            int xModifier = 2;
            int yModifier = 2;
            xModifier *= xMod;
            yModifier *= yMod;

            ImageButton image = new ImageButton();
            image.Source = @"selected_icon.png";
            image.Opacity = 1;
            image.BackgroundColor = Colors.Transparent;
            image.Clicked += (sender, e) => UIManager.MoveToPosition(sender, e, GameBoard);
            GameBoard.SetRow(image, yCord + (yModifier / 2));
            GameBoard.SetColumn(image, xCord + (xModifier / 2));
            GameBoard.Add(image);

        }
        private static void InstantiatePossibleExecuteMoveIcon(Grid GameBoard, int targetsXPos, int targetsYPos, int xMod, int yMod)
        {
            int xModifier = 2;
            int yModifier = 2;
            xModifier *= xMod;
            yModifier *= yMod;

            ImageButton image = new ImageButton();
            image.Source = @"selected_icon.png";
            image.Opacity = 1;
            image.BackgroundColor = Colors.Transparent;
            image.Clicked += (sender, e) => UIManager.ExecuteMove(sender, e, GameBoard, targetsXPos, targetsYPos);
            GameBoard.SetRow(image, targetsYPos + (yModifier / 2));
            GameBoard.SetColumn(image, targetsXPos + (xModifier / 2));
            GameBoard.Add(image);



        }
        public static void CheckAndAutoSelectIfMandatoryMove()
        {
            GamePiece gamePiece = null;
            if (Player.CanCaptureOpponent(UIManager.PlayerTurn, out int playerArrayNumber))
            {
                foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                {
                    if (gamepiece.ownedBy == UIManager.PlayerTurn && gamepiece.arrayNumber == playerArrayNumber)
                    {
                        gamePiece = gamepiece;
                        
                    }
                }
                gamePiece.gamePieceButton.SendClicked();
            }
           
        }
    }
}


