using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAUIV2
{
    public class Tile
    { 
        public int BoardPosX;
        public int BoardPosY;
        public void CreateBlueTile(Grid GameBoard, int posY, int posX)
        {
            var tile = new Border
            {
                BackgroundColor = Color.FromArgb("#0a0b4c"),
                Padding = new Thickness(10),
            };

            Grid.SetRow(tile, posY);
            Grid.SetColumn(tile, posX);
            GameBoard.Add(tile);

        }
        public void CreateYellowTile(Grid GameBoard, int posY, int posX)
        {
            var tile = new Border
            {
                BackgroundColor = Color.FromArgb("#f5f4b3"),
                Padding = new Thickness(10),

            };
            Grid.SetRow(tile, posY);
            Grid.SetColumn(tile, posX);
            GameBoard.Add(tile);

        }


    }
}
