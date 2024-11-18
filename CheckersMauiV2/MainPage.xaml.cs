using CheckersMAUIV2;

namespace CheckersMauiV2
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage()
        {
            InitializeComponent();


            Board Board = new Board(GameBoard);
            Player Player1 = new Player(1, GameBoard);
            Player Player2 = new Player(2, GameBoard);

            UIManager.DisplayGamePieces(Board, GameBoard);
            UIManager.DisplayScore(GameBoard);
            UIManager.PlayerTurn = 1;
            UIManager.DisplayPlayersTurn(GameBoard);

        }
    }
}
