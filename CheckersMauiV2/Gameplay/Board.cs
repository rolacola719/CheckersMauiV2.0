using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
     public class Board
    {
        public Tile[,] tile = new Tile[8, 8];
        public Board(Grid GameBoard)
        {
            InitializeTiles(GameBoard);
        }
        public void InitializeTiles(Grid GameBoard)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (j % 2 == 0)
                        {
                            tile[i, j] = new Tile()
                            {
                                BoardPosX = j,
                                BoardPosY = i
                            };
                            tile[i, j].CreateBlueTile(GameBoard, i, j);
                        }
                        else if (j % 2 == 1)
                        {
                            tile[i, j] = new Tile()
                            {
                                BoardPosX = j,
                                BoardPosY = i
                            };
                            tile[i, j].CreateYellowTile(GameBoard, i, j);
                        }
                    }
                }
                else if (i % 2 == 1)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (j % 2 == 1)
                        {
                            tile[i, j] = new Tile()
                            {
                                BoardPosX = j,
                                BoardPosY = i
                            };
                            tile[i, j].CreateBlueTile(GameBoard, i, j);
                        }
                        else if (j % 2 == 0)
                        {
                            tile[i, j] = new Tile()
                            {
                                BoardPosX = j,
                                BoardPosY = i
                            };
                            tile[i, j].CreateYellowTile(GameBoard, i, j);
                        }
                    }
                }
            }
        }
    }
}
