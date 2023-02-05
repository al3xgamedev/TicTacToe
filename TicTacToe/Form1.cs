using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class FormHome : Form
    {

        int playerTurn = 0; // This integer keeps track of which turn it is
        bool gameEnd = false; // This checks if the game has been finished

        int[] gameGrid = new int[9]; // This is an array to keep track of if the board has an X or a O
        Button[] buttonGrid = new Button[9]; // This will house the actuall buttons for the game
                                             

        List<int[]> winningCombos = new List<int[]>
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 },
        };


 
        /// <summary>
        /// This is a function that will create the grid for the game
        /// </summary>
        private void CreateGameGrid()
        {
            int xSize = 100;
            int ySize = 100;
            int fontSize = xSize / 2;
            int xLocation = xSize; // This holds the X axis for the buttons
            int yLocation = ySize; // This holds the Y axis for the buttons
            int btnArrayPos = 0; // This will keep track of where in the array the buttons are added, and they're name

            for (int i = 0; i < 3; i++) // This loops 3 times to change to the 2nd then 3rd row of buttons
            {
                for (int x = 0; x < 3; x++) // This loops 3 times to create the firs row of buttons
                {
                    Button button = new Button // Creates a new button
                    {
                        Name = "gridButton " + btnArrayPos, // Gives it the name plus the array position
                        Tag = btnArrayPos,
                        Location = new Point(xLocation, yLocation), // Sets the location of the button
                        Size = new Size(xSize, ySize), // Sets its size
                        Font = new Font(Font.FontFamily, fontSize),
                        BackColor = Color.White, // Makes the buttons white on the inside
                        Visible = true // Makes them visible
                    };
                    this.Controls.Add(button); // Adds the buttons to the form
                    buttonGrid[btnArrayPos] = button; // Adds the buttons to the array
                    btnArrayPos++; // Increments the array position by 1
                    xLocation += xSize; // Increments the X position
                }
                xLocation -= (xSize*3); // Sets xLocation back to 25
                yLocation += ySize; // Increments the Y position
            }
            Button startButton = new Button
            {
                Name = "start_button",
                Location = new Point(10,10),
                Size = new Size(100,25),
                BackColor = Color.White,
                Text = "Start Game!",
                Visible = true
            };
            this.Controls.Add(startButton);
            startButton.Click += startButton_Click;

            for (int i = 0; i < 9; i++)
            {
                int index = i; // Creates a pointer for which button is pressed in the array
                buttonGrid[i].Click += button_Click;

            }
        }

                
        public FormHome()
        {
            InitializeComponent();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            CreateGameGrid();
        }

        private void CheckForWin()
        {
            int xCount = 0;
            int oCount = 0;

            foreach (int[] winningCombo in winningCombos)
            {
                xCount = 0;
                oCount = 0;

                foreach (int thisSquare in winningCombo)
                {

                    if (gameGrid[thisSquare] == 1)
                    {
                        xCount++;
                    }
                    else if (gameGrid[thisSquare] == -1)
                    {
                        oCount++;
                    }
                }

                if (xCount == 3 || oCount == 3)
                {
                    break;
                }
            }

            if(xCount == 3)
            {
                MessageBox.Show("X wins");
                gameEnd = true;
                return;
            }


            if (oCount == 3)
            {
                MessageBox.Show("O wins");
                gameEnd = true;
                return;
            }




  
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (!gameEnd)
            {
                int tag = (int)((Button)sender).Tag;
                if (playerTurn == 0) // If the playerTurn int is 0, then it's X's turn and the button pressed is set to "X
                {
                    if (gameGrid[tag] == 0) // This checks if there is anything on the button already
                    {
                        buttonGrid[tag].Text = "X";
                        gameGrid[tag] = 1;
                        playerTurn++; // playerTurn is set to 1
                    }
                }
                else if (playerTurn == 1) // The opposite of if player turn is 0
                {
                    if (gameGrid[tag] == 0)
                    {
                        buttonGrid[tag].Text = "O";
                        gameGrid[tag] = -1;
                        playerTurn -= 1; // playerTurn is set to 0
                    }
                }

                CheckForWin();
            }
            
            
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                int index = i;
                gameGrid[index] = 0; // Sets all the values in the array back to 0
                buttonGrid[index].Text = "";
            }

            playerTurn = 0;
            gameEnd = false; // Makes game end false
        }
    }
}
