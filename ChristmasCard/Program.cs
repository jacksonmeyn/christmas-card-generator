//***************************************************************************************************
//Program Title: Christmas Card Generator
//Programmer: Jackson Meyn (22558031)
//Version: 0.1
//Decription: Generates ASCII art Christmas cards with custom borders, sender and recipient names, and messages
//through use of a Christmas card class and a ChristmasCardTest program
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasCard
{
    class ChristmasCardTest
    {
        static void Main(string[] args)
        {
            //First display card with default settings...
            ChristmasCard card = new ChristmasCard("Rebecca","Jackson");
            //...And no message set so we can see DoPrompt at work
            card.Display();

            card.ClearMessage();
            card.AddMessage("Merry Christmas");
            card.AddMessage("This is a test of AddMessage");
            card.AddMessage(""); //This line should be ignored by the method because it is blank
            card.AddMessage("This line should be rejected because it is more than 30 characters");
            card.AddMessage("And a happy new year");
            card.AddMessage("Oh and one other thing..."); //This line should be rejected because the max number of lines has already been added

            //Test custom characters and decorations
            try
            {
                Console.WriteLine("Enter a char to test SetBorderCharacter, or enter a string to test FormatException");
                char input = Convert.ToChar(Console.ReadLine());
                card.SetBorderCharacter(input);
            } catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("Enter an integer (1 or 2) to test SetDecoration, or enter a string to test FormatException");
                int decoration = Convert.ToInt16(Console.ReadLine());
                card.SetDecoration(decoration);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            card.Display();

            //Hold the screen
            Console.ReadLine();

        }
    }

    class ChristmasCard
    {
        private const int NUMBER_OF_COLUMNS = 96;
        private string[] decoration1Array = new string[] {@"                                                ",
                                                    @"                    .------,                    ",
                                                    @"      .\/.          |______|                    ",
                                                    @"    _\_}{_/_       _|_Ll___|_                   ",
                                                    @"     / }{ \       [__________]          .\/.    ",
                                                    @"      '/\'        /          \        _\_\/_/_  ",
                                                    @"                 ()  o  o    ()        / /\ \   ",
                                                    @"                  \~~~   .  /           '/\'    ",
                                                    @"             _\/   \'...'  /     \/_            ",
                                                    @"              \\   {`------'}    //             ",
                                                    @"               \\  /`---/',`\\  //              ",
                                                    @"                \/'  o  | |\ \`//               ",
                                                    @"                /'      | | \/ /\               ",
                                                    @"   __,. -- ~~ ~|    o   `\|      |~ ~~ -- . __  ",
                                                    @"               |                 |              ",
                                                    @"               \    o            /              ",
                                                    @"                `._           _.'               ",
                                                    @"                   ^~- . -  ~^                  ",
                                                    @"                                                "
            };
        private const int DECORATION_1_WIDTH = 48;
        private const int DECORATION_1_WHITESPACE_WIDTH = 46;
        private string[] decoration2Array = new string[] {
            @"   ||::|:||   .--------,                   ",
            @"   |:||:|:|   |_______ /        .-.        ",
            @"   ||::|:|| .'`  ___  `'.    \|('v')|/     ",
            @"   \\\/\///:  .'`   `'.  ;____`(   )'____  ",
            @"    \====/ './  o   o  \|~     ^' '^   //  ",
            @"     \\//   |   ())) .  |   Season's    \  ",
            @"      ||     \ `.__.'  /|              //  ",
            @"      ||   _{``-.___.-'\|   Greetings   \  ",
            @"      || _.' `-.____.- '`|    ___       // ",
            @"      ||`        __ \   |___/   \_______\  ",
            @"    .' || (__) \    \|     /               ",
            @"   /   `\/       __   vvvvv'\___/          ",
            @"   |     |      (__)        |              ",
            @"    \___/\                 /               ",
            @"      ||  |     .___.     |                ",
            @"      ||  |       |       |                ",
            @"      ||.-'       |       '-.              ",
            @"      ||          |          )             ",
            @"      || ----------'---------'             "
        };
        private const int DECORATION_2_WIDTH = 43;
        private const int DECORATION_2_WHITESPACE_WIDTH = 51;
        private int decoration;
        private char borderCharacter;
        private string to;
        private string from;
        private string[] messageLinesArray = new string[3];

        public int GetDecoration()
        {
            return decoration;
        }

        public int SetDecoration(int number)
        {
            if (number > 0 && number <= 2)
            {
                this.decoration = number;
                return 0;
            }
            else
            {
                this.decoration = 1;
                Console.WriteLine("Invalid input. Decoration set to 1");
                return -1;
            }
;
        }

        public char GetBorderCharacter()
        {
            return borderCharacter;
        }

        public void SetBorderCharacter(char character)
        {
            this.borderCharacter = character;
        }

        private string To { get => to; set => to = value; }
        private string From { get => from; set => from = value; }

        //Constructor definitions
        public ChristmasCard()
        {
            SetDecoration(1);
            SetBorderCharacter('*');

        }

        public ChristmasCard(string recipient, string sender)
        {
            SetDecoration(1);
            SetBorderCharacter('*');
            To = recipient;
            From = sender;
        }

        //End constructor definitions

        public void ClearMessage()
        {
            messageLinesArray[0] = null;
            messageLinesArray[1] = null;
            messageLinesArray[2] = null;
            Console.WriteLine("Message successfully cleared.");
        }

        public void AddMessage(string message)
        {
            //If the message is more than 30 characters, don't add it.
            try
            {
                if (message.Length > 30)
                {
                    throw new MessageTooLongException();
                }
            } catch (MessageTooLongException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            

            //If the message is blank, don't add it
            if (message == "")
            {
                Console.WriteLine("Message line blank so won't be added.");
                return;
            }

            //Add the message if there's an available place to add it
            bool messageAdded = false;
            for (int i = 0; i < messageLinesArray.Length; i++)
            {
                if(messageLinesArray[i] == "" || messageLinesArray[i] == null)
                {
                    messageLinesArray[i] = message;
                    Console.WriteLine("Message added to line {0} of {1}", i+1, messageLinesArray.Length);
                    messageAdded = true;
                    break;
                }
            }

            //Return an error if the message was not added
            try
            {
                if (!messageAdded)
                {
                    throw new MessageArrayFullException();
                }
            } catch (MessageArrayFullException e)
            {
                Console.WriteLine(e.Message);
            }



        }

        private void DoPrompt()
        {
            
            
            bool firstLineAdded = false;
            string input = "";
            while (!firstLineAdded)
            {
                try
                {
                    Console.WriteLine("You must enter at least one line of message");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        throw new MessageLineBlankException();
                    }
                    else if (input.Length > 30)
                    {
                        throw new MessageTooLongException();
                    }
                    else
                    {
                        AddMessage(input);
                        firstLineAdded = true;
                    }
                } catch (MessageLineBlankException e)
                {
                    Console.WriteLine("Error message : " + e.GetErrorMessage());
                    Console.WriteLine("Error code : " + e.GetErrorCode());
                } catch (MessageTooLongException e)
                {
                    Console.WriteLine("Error message : " + e.GetErrorMessage());
                    Console.WriteLine("Error code : " + e.GetErrorCode());
                }
                
            }

            //Then loop through other non-compulsory lines
            for (int i = 1; i < messageLinesArray.Length; i++)
            {
                Console.Write("Enter line " + (i+1) + " of the message (can be left blank)");

                input = Console.ReadLine();

                AddMessage(input);
            }
            
        }

        public void Display()
        {
            if (messageLinesArray[0] == "" || messageLinesArray[0] == null)
            {
                DoPrompt();
            }

            for (int i = 0; i < NUMBER_OF_COLUMNS - 1; i++)
            {
                Console.Write(GetBorderCharacter());
            }

            Console.WriteLine(GetBorderCharacter());

            if (GetDecoration() == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration1Array[i]);
                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }

                //On Line 5 write the To Text
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[4]);
                string toLine = "To " + To + ",";
                Console.Write(toLine);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - toLine.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());

                //On line 6, continue the design
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[5]);
                for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(GetBorderCharacter());

                //On lines 7-9 write any message the user wants
                //Line 7
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[6]);
                if (messageLinesArray[0] != "" && messageLinesArray[0] != null)
                {
                    Console.Write(messageLinesArray[0]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - messageLinesArray[0].Length; i++)
                    {
                        Console.Write(" ");
                    }
                    
                } else
                {
                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 7

                //Line 8
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[7]);
                if (messageLinesArray[1] != "" && messageLinesArray[1] != null)
                {
                    Console.Write(messageLinesArray[1]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - messageLinesArray[1].Length; i++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {
                    
                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 8

                //Line 9
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[8]);
                if (messageLinesArray[2] != "" && messageLinesArray[2] != null)
                {
                    Console.Write(messageLinesArray[2]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - messageLinesArray[2].Length; i++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {

                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 9

                //Lines 10-13 continue design as normal
                for (int i = 9; i < 13; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration1Array[i]);
                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }

                //Line 14 contains the From Line
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[13]);
                string fromLine = "From,";
                Console.Write(fromLine);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - fromLine.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 14

                //Line 15 contains the From name
                Console.Write(GetBorderCharacter());
                Console.Write(decoration1Array[14]);
                Console.Write(From);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_1_WHITESPACE_WIDTH - From.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 15

                //Lines 16-19 complete the design
                for (int i = 15; i < 19; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration1Array[i]);
                    for (int j = 0; j < DECORATION_1_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }

                

            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration2Array[i]);
                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }

                //On Line 5 write the To Text
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[4]);
                string toLine = "To " + To + ",";
                Console.Write(toLine);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - toLine.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());

                //On line 6, continue the design
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[5]);
                for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(GetBorderCharacter());

                //On lines 7-9 write any message the user wants
                //Line 7
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[6]);
                if (messageLinesArray[0] != "" && messageLinesArray[0] != null)
                {
                    Console.Write(messageLinesArray[0]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - messageLinesArray[0].Length; i++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {
                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 7

                //Line 8
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[7]);
                if (messageLinesArray[1] != "" && messageLinesArray[1] != null)
                {
                    Console.Write(messageLinesArray[1]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - messageLinesArray[1].Length; i++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {

                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 8

                //Line 9
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[8]);
                if (messageLinesArray[2] != "" && messageLinesArray[2] != null)
                {
                    Console.Write(messageLinesArray[2]);
                    //Then write enough white space to finish the line
                    for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - messageLinesArray[2].Length; i++)
                    {
                        Console.Write(" ");
                    }

                }
                else
                {

                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 9

                //Lines 10-13 continue design as normal
                for (int i = 9; i < 13; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration2Array[i]);
                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }

                //Line 14 contains the From Line
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[13]);
                string fromLine = "From,";
                Console.Write(fromLine);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - fromLine.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 14

                //Line 15 contains the From name
                Console.Write(GetBorderCharacter());
                Console.Write(decoration2Array[14]);
                Console.Write(From);

                //Then write enough white space to finish the line
                for (int i = 0; i < DECORATION_2_WHITESPACE_WIDTH - From.Length; i++)
                {
                    Console.Write(" ");
                }

                //Then the final border character
                Console.WriteLine(GetBorderCharacter());
                //End Line 15

                //Lines 16-19 complete the design
                for (int i = 15; i < 19; i++)
                {
                    Console.Write(GetBorderCharacter());
                    Console.Write(decoration2Array[i]);
                    for (int j = 0; j < DECORATION_2_WHITESPACE_WIDTH; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(GetBorderCharacter());
                }



            }

            //Finally, print one more line of border characters
            for (int i = 0; i < NUMBER_OF_COLUMNS - 1; i++)
            {
                Console.Write(GetBorderCharacter());
            }

            Console.WriteLine(GetBorderCharacter());

            //Hold the screen so user can view finished card
            Console.ReadLine();
        }


    }

    class MessageTooLongException : Exception
    {
        public MessageTooLongException():base("A message line cannot be longer than 30 characters")
        {

        }

        public MessageTooLongException(string errorMessage) : base(errorMessage)
        {

        }
        public string GetErrorMessage()
        {
            return "A message line cannot be longer than 30 characters";
        }

        public int GetErrorCode()
        {
            return 101;
        }
    }

    class MessageLineBlankException : Exception
    {
        public MessageLineBlankException() : base("This message line cannot be blank.")
        {

        }

        public MessageLineBlankException(string errorMessage) : base(errorMessage)
        {

        }
        public string GetErrorMessage()
        {
            return "This message line cannot be blank";
        }

        public int GetErrorCode()
        {
            return 102;
        }
    }

    class MessageArrayFullException : Exception
    {
        public MessageArrayFullException() : base("All lines of the message are already full.")
        {

        }

        public MessageArrayFullException(string errorMessage) : base(errorMessage)
        {

        }
        public string GetErrorMessage()
        {
            return "All lines of the message are already full.";
        }

        public int GetErrorCode()
        {
            return 103;
        }
    }
}
