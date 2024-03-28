namespace Naval_battle
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void playerTwo_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void playerOne_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void black_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}