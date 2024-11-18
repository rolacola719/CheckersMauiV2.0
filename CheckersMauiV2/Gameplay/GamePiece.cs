using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
    public class GamePiece
    {

        public int posX;
        public int posY;
        public int ownedBy;
        public bool isKing;
        public bool isActive;
        public int arrayNumber;
        public GamePiece(Grid GameBoard)
        {
            isActive = true;
            isKing = false;
            AllGamePieces.Add(this);
        }

        public ImageButton gamePieceButton;
        public void CreateGamePieceButton(Grid GameBoard) 
        {
            if (ownedBy == 1 && !isKing)
            {
                gamePieceButton = new ImageButton();
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Gold Gamepiece.png";
                GameBoard.SetRow(gamePieceButton, posY);
                GameBoard.SetColumn(gamePieceButton, posX);
                gamePieceButton.BackgroundColor = Colors.Transparent;
                gamePieceButton.Clicked += (sender, e) => UIManager.SelectPiece(sender, e, GameBoard);

            }
            else if (ownedBy == 1 && isKing)
            {
                gamePieceButton = new ImageButton();
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Gold Gamepiece King.png";
                GameBoard.SetRow(gamePieceButton, posY);
                GameBoard.SetColumn(gamePieceButton, posX);
                gamePieceButton.BackgroundColor = Colors.Transparent;
                gamePieceButton.Clicked += (sender, e) => UIManager.SelectPiece(sender, e, GameBoard);
            }
            else if (ownedBy == 2 && !isKing)
            {
                gamePieceButton = new ImageButton();
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Silver Gamepiece.png";
                GameBoard.SetRow(gamePieceButton, posY);
                GameBoard.SetColumn(gamePieceButton, posX);
                gamePieceButton.BackgroundColor = Colors.Transparent;
                gamePieceButton.Clicked += (sender, e) => UIManager.SelectPiece(sender, e, GameBoard);
            }
            else if (ownedBy == 2 && isKing)
            {
                gamePieceButton = new ImageButton();
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Silver Gamepiece King.png";
                GameBoard.SetRow(gamePieceButton, posY);
                GameBoard.SetColumn(gamePieceButton, posX);
                gamePieceButton.BackgroundColor = Colors.Transparent;
                gamePieceButton.Clicked += (sender, e) => UIManager.SelectPiece(sender, e, GameBoard);
            }
        }
        public void Deactivate(Grid GameBoard)
        {
            isActive = false;
            GameBoard.Remove(gamePieceButton);
        }

        public void SetLocation(int x, int y)
        {
            posX = x;
            posY = y;
        }
        public void CheckKing()
        {
            if (ownedBy == 1 && posY == 0)
            {
                isKing = true;
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Gold Gamepiece King.png";
            }

            if (ownedBy == 2 && posY == 7)
            {
                isKing = true;
                gamePieceButton.Source = @"C:\Users\lewis\source\repos\CheckersMauiV2\CheckersMauiV2\Images\Silver Gamepiece King.png";
            }
        }

        public static List<GamePiece> AllGamePieces = new List<GamePiece>();
        public static List<GamePiece> GetAllInstances()
        {
            return AllGamePieces;
        }

        public bool ThisPieceCanCapture(int xCord, int yCord)
        {
            int playerNumber = UIManager.PlayerTurn;

            foreach (GamePiece playerGamePiece in GamePiece.AllGamePieces)
            {
                // IF NON KINGS
                //player 1
                if ((playerGamePiece.posX == xCord) && (playerGamePiece.posY == yCord) && (playerGamePiece.isKing == false) && (playerGamePiece.ownedBy == 1))
                {
                    foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                    {
                        if (gamepiece.ownedBy != playerNumber)
                        {

                            if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord - 1) && Player.IsOccupied(xCord - 1, yCord - 1) && (xCord >= 2) && (yCord >= 2) && (!Player.IsOccupied(xCord - 2, yCord - 2)))
                                return true;
                            if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord - 1) && Player.IsOccupied(xCord + 1, yCord - 1) && (xCord <= 5) && (yCord >= 2) && (!Player.IsOccupied(xCord + 2, yCord - 2)))
                                return true;
                        }
                    }
                }
                //player 2
                else if ((playerGamePiece.posX == xCord) && (playerGamePiece.posY == yCord) && (playerGamePiece.isKing == false) && (playerGamePiece.ownedBy == 2))
                {
                    foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                    {
                        if (gamepiece.ownedBy != playerNumber)
                        {
                            if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord + 1) && Player.IsOccupied(xCord - 1, yCord + 1) && (xCord >= 2) && (yCord <= 5) && (!Player.IsOccupied(xCord - 2, yCord + 2)))
                                return true;
                            if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord + 1) && Player.IsOccupied(xCord + 1, yCord + 1) && (xCord <= 5) && (yCord <= 5) && (!Player.IsOccupied(xCord + 2, yCord + 2)))
                                return true;
                        }
                    }
                }
                // IF KINGS
                if ((playerGamePiece.posX == xCord) && (playerGamePiece.posY == yCord) && (playerGamePiece.isKing == true))
                {
                    foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                    {
                        if (gamepiece.ownedBy != playerNumber)
                        {

                            if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord - 1) && Player.IsOccupied(xCord - 1, yCord - 1) && (xCord >= 2) && (yCord >= 2) && (!Player.IsOccupied(xCord - 2, yCord - 2)))
                                return true;
                            if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord - 1) && Player.IsOccupied(xCord + 1, yCord - 1) && (xCord <= 5) && (yCord >= 2) && (!Player.IsOccupied(xCord + 2, yCord - 2)))
                                return true;
                            if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord + 1) && Player.IsOccupied(xCord - 1, yCord + 1) && (xCord >= 2) && (yCord <= 5) && (!Player.IsOccupied(xCord - 2, yCord + 2)))
                                return true;
                            if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord + 1) && Player.IsOccupied(xCord + 1, yCord + 1) && (xCord <= 5) && (yCord <= 5) && (!Player.IsOccupied(xCord + 2, yCord + 2)))
                                return true;
                        }
                    }
                }

                else continue;
            }
            return false;
        }
    }
}
