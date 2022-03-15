// Kory Kinter
using System;


namespace Craps
{
    internal class BaseProgram
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            // starting variables for chips, rools, bets, count, and gos
            int StartingChips = 100;
            int roll = 1;
            int bet = 0;
            int count = 1;
            int go = 2;


            //Call of method to show instructions
            Game();

            //Do loop to ask user if they would like to play again
            do
            {
                //Another do loop to run until bet is placed
                do
                {
                    Console.WriteLine("How many chips would you like to bet? Your current chip total is: " + StartingChips + " Chips");
                    while (!int.TryParse(Console.ReadLine(), out bet))
                    {
                        Console.WriteLine("Invalid input. Please enter an integer value: ");
                    }
                    if (bet > StartingChips)
                    {
                        Console.WriteLine("Error: bet is higher than available chips. Please enter appropriate amount of chips for your bet ");
                    }
                } while (bet > StartingChips);

                // Checks first roll for win, loss, or point
                int result = Set(DiceRolled(rand.Next(1, 7), rand.Next(1, 7)), roll);


                if (result == 0 || result == -1)
                {
                    //if they won or lost updates Chips accordingly
                    StartingChips = Update(result, StartingChips, bet);

                }
                // if player point is set
                else
                {
                    Console.WriteLine("Dice Roll: " + result);
                    Console.WriteLine("Player's 'Point' = " + result);
                    //Do loops till player rolls there player point or a 7
                    count = 0;
                    do
                    {
                        count++;
                        go = ContinueRolling(result, DiceRolled(rand.Next(1, 7), rand.Next(1, 7)));
                        if (go != 0 && go != -1)
                        {
                            Console.WriteLine(count + " Roll Dice Total: " + go);
                        }

                        else if (go == 0)
                        {
                            Console.WriteLine(count + " Roll Dice Total: " + result);
                        }
                        else if (go == -1)
                        {
                            Console.WriteLine(count + " Roll Dice Total: 7");
                        }





                    } while (go != 0 && go != -1);
                    // updates the player chips based on if they got there player point or lost by rolling a 7
                    StartingChips = Update(go,
                                           StartingChips,
                                           bet);



                }




                //prints out the players remaing chips
                Console.WriteLine(StartingChips);
            } while (Continue(StartingChips) != -1);

        }
        // Method to print instructions
        public static void Game()
        {
            Console.WriteLine("KINTER'S CRAPS!");
            Console.WriteLine();
            Console.WriteLine("How to play:");
            Console.WriteLine("Place a wager with your available chips. This is called a 'bet'.");
            Console.WriteLine();
            Console.WriteLine("You Win if your first roll sum of the dice is a total of 7 or 11.");
            Console.WriteLine("You lose if your first roll sum of the dice is a total of 2, 3, or 12.");
            Console.WriteLine();
            Console.WriteLine("If on the first roll anything other than a 2, 3, 7, 11, or 12 is rolled, that roll sum will become the player's 'point'.");
            Console.WriteLine();
            Console.WriteLine("The player will continue to roll the dice until they win by getting the sum to equal their 'point'.");
            Console.WriteLine("However, if a 7 is rolled instead of the player's 'point' this will be a loss.");
            Console.WriteLine();
            Console.WriteLine("If you win, your bet will be doubled and those will be your winnings.");
            Console.WriteLine("For example: Tom has 100 chips and bet 10 then won. Therefore, 20 chips are won and his new total is 120 chips.");
            Console.WriteLine();
            Console.WriteLine("If the bet is lost, the chips will be subtracted from the player's total chip count.");
            Console.WriteLine();
            Console.WriteLine("Good Luck and Have Fun!");
            Console.WriteLine();
        }
        //Method to roll dice and show total
        public static int DiceRolled(int dice1, int dice2)
        {
            int DiceTotal = dice1 + dice2;
            return DiceTotal;
        }

        //Method to see if won, loss, or point needing to be set
        public static int Set(int total, int roll)
        {
            int point = 0;
            if (roll == 1 && total == 7 || total == 11)
            {
                Console.WriteLine("DiceRoll: " + total);
                point = 0;
            }
            else if (roll == 1 && total == 2 || total == 3 || total == 12)
            {
                Console.WriteLine("DiceRoll: " + total);
                point = -1;
            }
            else if (roll == 1 && total == 4 || total == 5 || total == 6 || total == 8 || total == 9 || total == 10)
            {
                point = total;
            }
            return point;
        }
        // Method to continue rolling until a total of 7 or the player's point is rolled
        public static int ContinueRolling(int point, int total)
        {
            if (total == point)
            {
                total = 0;
            }
            else if (total == 7)
            {
                total = -1;
            }
            return total;
        }
        // Method to calculate either win or loss and update chip total
        public static int Update(int point, int NewChips, int bet)
        {

            if (point == 0)
            {
                Console.Write("Congrats! You won: ");
                return NewChips += 2 * bet;

            }
            else if (point == -1)
            {
                Console.Write("You Lost: ");
                return NewChips -= (bet);

            }
            else
            {

                return NewChips;
            }

        }
        // Method to inform user they are out of chips or ask to continue
        public static int Continue(int chips)
        {
            String stop = "";
            do
            {
                if (chips != 0)
                {
                    Console.WriteLine("Continue playing? 'y' , 'n' " + "Your current chip total: " + chips + " Chips");
                    stop = Console.ReadLine();
                }
                if (stop.ToLower().Equals("y"))
                {
                    return 0;
                }
                else if (stop.ToLower().Equals("n") || chips <= 0)
                {
                    if (chips <= 0)
                        Console.WriteLine("Sorry! You are out of money. Better luck next time!");

                    return -1;
                }
                else
                {
                    stop = "NUll";
                    Console.WriteLine("Input was not a valid option. Please try again.");
                }

            } while (stop.Equals("NUll"));
            return -1;
        }
    }
}
