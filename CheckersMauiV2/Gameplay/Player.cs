using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
    public class Player
    {
        public int Score = 0;
        public int PlayerNumber;
        public GamePiece[] Piece = new GamePiece[12];
        public Player(int playerNumber, Grid GameBoard)
        {
            SetInitialGamePieceLocations(playerNumber, GameBoard);
        }
        private void SetInitialGamePieceLocations(int playerNumber, Grid GameBoard)
        {
            if (playerNumber == 1)
            {
                int posX = 0;
                int posY = 7;
                for (int i = 0; i < Piece.Length; i++)
                {
                    Piece[i] = new GamePiece(GameBoard);
                    Piece[i].SetLocation(posX, posY);
                    Piece[i].ownedBy = 1;
                    Piece[i].arrayNumber = i;
                    Piece[i].CreateGamePieceButton(GameBoard);


                    posX += 2;

                    if (posX >= 8)
                    {
                        posY -= 1;

                        if (posY % 2 != 0)
                        {
                            posX = 0;
                        }
                        else if (posY % 2 == 0)
                        {
                            posX = 1;
                        }
                        continue;
                    }
                }
            }
            else if (playerNumber == 2)
            {
                int posX = 1;
                int posY = 0;
                for (int i = 0; i < Piece.Length; i++)
                {
                    Piece[i] = new GamePiece(GameBoard);
                    Piece[i].SetLocation(posX, posY);
                    Piece[i].ownedBy = 2;
                    Piece[i].arrayNumber = i;
                    Piece[i].CreateGamePieceButton(GameBoard);

                    posX += 2;

                    if (posX >= 8)
                    {
                        posY += 1;

                        if (posY % 2 != 0)
                        {
                            posX = 0;
                        }
                        else if (posY % 2 == 0)
                        {
                            posX = 1;
                        }
                        continue;
                    }
                }
            }
        }
        public static int ReturnPlayerNumber(int xCord, int yCord)
        {
            foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
            {
                if (gamepiece.isActive)
                {
                    if ((gamepiece.posY == yCord) && (gamepiece.posX == xCord))
                    {
                        return gamepiece.ownedBy;

                    }
                }
            }
            return -1;
        }
        public static bool IsOccupied(int xCord, int yCord)
        {
            foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
            {
                if ((gamepiece.posY == yCord) && (gamepiece.posX == xCord) && (gamepiece.isActive))
                {
                    return true;

                }
            }
            return false;
        }
        public static void CheckIfAdjacentToGamePiece(int playerNumber, int xCord, int yCord, out bool gamepieceBL, out bool gamepieceTR, out bool gamepieceTL, out bool gamepieceBR)
        {
            gamepieceBL = false;
            gamepieceBR = false;
            gamepieceTL = false;
            gamepieceTR = false;
            {
                foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                {
                        if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord - 1, yCord + 1)) gamepieceBL = true;

                        if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord + 1, yCord + 1)) gamepieceBR = true;

                        if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord - 1, yCord - 1)) gamepieceTL = true;

                        if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord + 1, yCord - 1)) gamepieceTR = true;
                    
                }
                Debug.WriteLine($"BL = {gamepieceBL} ; BR = {gamepieceBR} ; TL = {gamepieceTL} ; TR = {gamepieceTR} ");
            }

        }
        public static bool CanCaptureOpponent(int playerNumber, out int playerArrayNumber)
        {
            playerArrayNumber = 0;
            foreach (GamePiece playerGamePiece in GamePiece.AllGamePieces)
            {
                if (playerGamePiece.ownedBy == playerNumber && playerGamePiece.isActive)
                {
                    playerArrayNumber = playerGamePiece.arrayNumber;
                    int xCord = playerGamePiece.posX;
                    int yCord = playerGamePiece.posY;

                    //IF NOT KING
                    //Player 1
                    if (playerGamePiece.isKing == false && playerGamePiece.ownedBy == 1)
                    {
                        foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                        {
                            if (gamepiece.ownedBy != playerNumber && gamepiece.isActive)
                            {

                                if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord - 1, yCord - 1) && (xCord >= 2) && (yCord >= 2) && (!IsOccupied(xCord - 2, yCord - 2)))
                                    return true;
                                if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord + 1, yCord - 1) && (xCord <= 5) && (yCord >= 2) && (!IsOccupied(xCord + 2, yCord - 2)))
                                    return true;
                            }
                        }
                    }
                    //Player 2
                    else if (playerGamePiece.isKing == false && playerGamePiece.ownedBy == 2)
                    {
                        foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                        {
                            if (gamepiece.ownedBy != playerNumber && gamepiece.isActive)
                            {

                                if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord - 1, yCord + 1) && (xCord >= 2) && (yCord <= 5) && (!IsOccupied(xCord - 2, yCord + 2)))
                                    return true;
                                if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord + 1, yCord + 1) && (xCord <= 5) && (yCord <= 5) && (!IsOccupied(xCord + 2, yCord + 2)))
                                    return true;
                            }
                        }
                    }

                    //IF KING
                    else if (playerGamePiece.isKing)
                    {
                        foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
                        {
                            if (gamepiece.ownedBy != playerNumber && gamepiece.isActive)
                            {
                                if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord - 1, yCord + 1) && (xCord >= 2) && (yCord <= 5) && (!IsOccupied(xCord - 2, yCord + 2)))
                                    return true;
                                if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord + 1) && IsOccupied(xCord + 1, yCord + 1) && (xCord <= 5) && (yCord <= 5) && (!IsOccupied(xCord + 2, yCord + 2)))
                                    return true;
                                if ((gamepiece.posX == xCord - 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord - 1, yCord - 1) && (xCord >= 2) && (yCord >= 2) && (!IsOccupied(xCord - 2, yCord - 2)))
                                    return true;
                                if ((gamepiece.posX == xCord + 1) && (gamepiece.posY == yCord - 1) && IsOccupied(xCord + 1, yCord - 1) && (xCord <= 5) && (yCord >= 2) && (!IsOccupied(xCord + 2, yCord - 2)))
                                    return true;
                            }
                        }
                    }
                }
                else continue;
            }
            return false;
        }
        public static GamePiece ReturnGamePiece(int xCord, int yCord)
        {
            foreach (GamePiece gamepiece in GamePiece.AllGamePieces)
            {
                if (gamepiece.posX == xCord && gamepiece.posY == yCord && gamepiece.isActive)  return gamepiece; 
            }
            return null;
        }
    }
}
