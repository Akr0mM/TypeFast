using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;

namespace TypeFast
{
    internal class TypeFast
    {
        Random rnd = new Random();
        public static Timer aTimer;
        public static double secondsCount = 0f;

        public bool finished = false;
        public bool endOfText = false;
        public bool isTyping = false;
        public bool testAborted = false;
        public bool endedInMenu = false;

        int difficulty = 0;
        int textSize;
        int marge = 20;
        int topMarge = 4;
        int currentWordIndex = 0;
        int correctWordsCount = 0;
        int wrongWordsCount = 0;
        
        string currentWord;
        string[] words;
        string[] text = {};
        string input = "";

        string typeFastLogo = @"
                        _________          _______  _______    _______  _______  _______ _________
                        \__   __/|\     /|(  ____ )(  ____ \  (  ____ \(  ___  )(  ____ \\__   __/
                           ) (   ( \   / )| (    )|| (    \/  | (    \/| (   ) || (    \/   ) (   
                           | |    \ (_) / | (____)|| (__      | (__    | (___) || (_____    | |   
                           | |     \   /  |  _____)|  __)     |  __)   |  ___  |(_____  )   | |   
                           | |      ) (   | (      | (        | (      | (   ) |      ) |   | |   
                           | |      | |   | )      | (____/\  | )      | )   ( |/\____) |   | |   
                           )_(      \_/   |/       (_______/  |/       |/     \|\_______)   )_(   
                                                                                                  
";
        string startLogo = @"
                                                _____ __             __ 
                                               / ___// /_____ ______/ /_
                                               \__ \/ __/ __ `/ ___/ __/
                                              ___/ / /_/ /_/ / /  / /_  
                                             /____/\__/\__,_/_/   \__/  
                                                                        
";

        List<string> textList;

        ConsoleColor fgc;
        ConsoleColor bgcClear;
        ConsoleColor bgcCurrent;
        ConsoleColor fgcCurrent;
        ConsoleColor bgcCorrect;
        ConsoleColor fgcCorrect;
        ConsoleColor bgcWrong;
        ConsoleColor fgcWrong;
        ConsoleColor fgcBackground;

        public void Menu()
        {
            int selection = 0;
            int wordAmount = 15;
            int maxWordAmount = 50;
            int difficultyChosen = 0;
            int maxDifficultyChoice = 9;
            bool start = false;

            while (!start)
            {
                Clear();

                SetCursorPosition(0, 0);
                Write(typeFastLogo);
                SetCursorPosition(50, 11);
                Write("Typing Test in C#");

                if (selection == 0)
                {
                    SetCursorPosition(28, 17);
                    Write(">");
                    SetCursorPosition(44, 16);
                    Write("+");
                    SetCursorPosition(30, 17);
                    Write("Word Amount: 0" + wordAmount);
                    SetCursorPosition(44, 18);
                    Write("-");
                    SetCursorPosition(47, 17);
                    Write("<");
                }
                else
                {
                    SetCursorPosition(30, 17);
                    Write("Word Amount: 0" + wordAmount);
                }

                if (selection == 1)
                {
                    SetCursorPosition(68, 17);
                    Write(">");
                    SetCursorPosition(87, 16);
                    Write("+");
                    SetCursorPosition(70, 17);
                    Write("Text Difficulty: " + (difficultyChosen + 1));
                    SetCursorPosition(87, 18);
                    Write("-");
                    if (difficultyChosen == 9)
                    {
                        SetCursorPosition(90, 17);
                        Write("<");
                    }
                    else
                    {
                        SetCursorPosition(89, 17);
                        Write("<");
                    }
                }
                else
                {
                    SetCursorPosition(70, 17);
                    Write("Text Difficulty: " + (difficultyChosen + 1));
                }

                if (selection == 2)
                { 
                    SetCursorPosition(50, 20);
                    Write(startLogo);
                    SetCursorPosition(43, 23);
                    Write(">");
                    SetCursorPosition(43, 24);
                    Write(">");
                    SetCursorPosition(73, 23);
                    Write("<");
                    SetCursorPosition(73, 24);
                    Write("<");
                }
                else
                {
                    SetCursorPosition(50, 20);
                    Write(startLogo);
                }

                if (selection == 3)
                {
                    SetCursorPosition(54, 27);
                    Write(">");
                    SetCursorPosition(56, 27);
                    Write("Exit");
                    SetCursorPosition(61, 27);
                    Write("<");
                }
                else
                {
                    SetCursorPosition(56, 27);
                    Write("Exit");
                }

                ConsoleKeyInfo keyInfo = ReadKey(true);

                switch (keyInfo.Key)
                {
                    case LeftArrow:
                        if (selection != 0)
                            selection--;
                        break;
                    case RightArrow:
                        if (selection != 3)
                            selection++;
                        break;
                    case UpArrow:
                        if (selection == 0)
                        {
                            if (wordAmount != maxWordAmount)
                                wordAmount++;
                        }
                        else if (selection == 1)
                        {
                            if (difficultyChosen != maxDifficultyChoice)
                                difficultyChosen++;
                        }
                        break;
                    case DownArrow:
                        if (selection == 0)
                        {
                            if (wordAmount != 1)
                                wordAmount--;
                        }
                        else if (selection == 1)
                        {
                            if (difficultyChosen != 0)
                                difficultyChosen--;
                        }
                        break;
                    case Enter:
                        if (selection == 2)
                        {
                            difficulty = difficultyChosen;
                            textSize = wordAmount;
                            start = true;
                        }
                        else if (selection == 3)
                        {
                            finished = true;
                            endOfText = true;
                            endedInMenu = true;
                            start = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public void InitColors()
        {
            fgc = White;
            bgcClear = Black;
            bgcCurrent = DarkGray;
            fgcCurrent = White;
            bgcWrong = Red;
            fgcWrong = Red;
            bgcCorrect = Black;
            fgcCorrect = Green;
            fgcBackground = DarkGray;

            ForegroundColor = fgc;
            BackgroundColor = bgcClear;
            Clear();
        }

        public void InitText()
        {
            int range = (difficulty + 1) * 1000;
            textList = text.ToList();
            textList.Clear();
            for (int i = 0; i < textSize; i++)
            {
                int index = rnd.Next(0, range);
                textList.Add(words[index].ToLower());
            }
            text = textList.ToArray();
        }

        public void StartTimer()
        {
            aTimer = new Timer(100); // 10 times per seconds
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
            aTimer.AutoReset = true;
            aTimer.Start();
        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secondsCount += 0.1f;
            secondsCount = Math.Round(secondsCount, 1);

            // throw new NotImplementedException();
        }

        public void InitAll()
        {
            Menu();
            InitColors();
            InitText();
        }

        public void WriteText()
        {
            /*
                % -> current
                # -> current is wrong
                @ -> wrong
                $ -> correct
            */

            Clear();
            CursorLeft = marge;
            CursorTop = topMarge;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i][0] == '%')
                {
                    text[i] = text[i].Remove(0, 1);
                    ForegroundColor = fgcCurrent;
                    BackgroundColor = bgcCurrent;
                    Write(text[i]);
                }
                else if (text[i][0] == '#')
                {
                    text[i] = text[i].Remove(0, 1);
                    ForegroundColor = fgcCurrent;
                    BackgroundColor = bgcWrong;
                    Write(text[i]);
                }
                else if (text[i][0] == '@')
                {
                    text[i] = text[i].Remove(0, 1);
                    ForegroundColor = fgcWrong;
                    BackgroundColor = bgcClear;
                    Write(text[i]);
                    text[i] = "@" + text[i];
                }
                else if (text[i][0] == '$')
                {
                    text[i] = text[i].Remove(0, 1);
                    ForegroundColor = fgcCorrect;
                    BackgroundColor = bgcCorrect;
                    Write(text[i]);
                    text[i] = "$" + text[i];
                }
                else
                    Write(text[i]);

                ForegroundColor = fgc;
                BackgroundColor = bgcClear;
                if (i != text.Length - 1)
                    Write(" ");
                if (CursorLeft - 1 > WindowWidth - marge - 6)
                {
                    CursorLeft = marge;
                    CursorTop += 1;
                }
            }
        }

        public void UpdateText()
        {
            endOfText = false;
            if (text.Length != currentWordIndex)
                currentWord = text[currentWordIndex];
        
            string wordToTest = "";
            if (input.Length % currentWord.Length == 0 && input.Length != 0)
                wordToTest = currentWord;
            else
                wordToTest = currentWord.Remove(input.Length % currentWord.Length);


            if (input == wordToTest)
            {
                text[currentWordIndex] = "%" + currentWord;
                if (currentWordIndex == text.Length - 1 && wordToTest.Length == currentWord.Length)
                {
                    endOfText = true;
                    finished = true;
                }
            }
            else
                text[currentWordIndex] = "#" + currentWord;
        }

        public void LockWord()
        {
            if (text.Length == currentWordIndex + 1)
            {
                // finish the text
                endOfText = true;
                finished = true;

                // stop timer
                aTimer.AutoReset = false;
            }
            else if (input == text[currentWordIndex])
            {
                text[currentWordIndex] = "$" + text[currentWordIndex];
                correctWordsCount++;
            }
            else
            {
                text[currentWordIndex] = "@" + text[currentWordIndex];
                wrongWordsCount++;
            }
        }

        public void WriteInput()
        {
            CursorLeft = WindowWidth / 2 - input.Length / 2;
            CursorTop  = 10 + topMarge;
            Write(input);

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            if (!finished)
                keyInfo = ReadKey();
            ConsoleKey key = keyInfo.Key;
            char keyChar = keyInfo.KeyChar;

            if (!isTyping)
            {
                isTyping = true;
                StartTimer();
            }

            if (key == Spacebar)
            {
                LockWord();
                currentWordIndex++;
                input = "";
            }
            else if (key == Backspace)
            {
                if (input.Length != 0)
                    input = input.Remove(input.Length - 1);
            }
            else if (key == Enter) { /**/ }
            else if (key == Escape)
            {
                finished = true;
                testAborted = true;
            }
            else
            {
                input += keyChar;
            }
        }

        public int WPM(int wordsCorrect, double seconds)
        {
            double wpm = Math.Round(wordsCorrect / (seconds / 60));
            return int.Parse(wpm.ToString());
        }

        public void EndScreen()
        {
            Clear();
            SetCursorPosition(0, 0);

            if (testAborted || endedInMenu)
                WriteLine("Test was aborted");
            else
            {
                WriteLine("Your WPM is: " + WPM(correctWordsCount, secondsCount));
                WriteLine("Text finished in " + secondsCount + "s");
            }
            WriteLine("\nPress <R> to start a new test");
            WriteLine("\nPress any other key to exit...");

            bool pressed = false;
            while (true)
            {
                ConsoleKeyInfo keyInfo = ReadKey();

                if (keyInfo.Key == R)
                {
                    Stop();
                    Run();
                    break;
                }


                if (keyInfo.Key != Spacebar || keyInfo.Key != Enter || pressed)
                    break;
                else if (keyInfo.Key == Spacebar || keyInfo.Key == Enter)
                    pressed = true;
            }
        }

        public void WriteTimer()
        {
            SetCursorPosition(0, topMarge);
            Write(secondsCount);
        }
         
        private void Run()
        {
            InitAll();
            while (!finished)
            {
                UpdateText();
                WriteText();
                WriteTimer();
                WriteInput();
            }
            EndScreen();
        }

        private void Stop()
        {
            rnd = new Random();
            aTimer.Stop();
            aTimer.Enabled = false;
            aTimer.AutoReset = false;
            aTimer = new Timer();
            secondsCount = 0f;

            finished = false;
            endOfText = false;
            isTyping = false;
            testAborted = false;
            endedInMenu = false;

            difficulty = 0;
            textSize = 0;
            marge = 20;
            topMarge = 4;
            currentWordIndex = 0;
            correctWordsCount = 0;
            wrongWordsCount = 0;

            currentWord = "";
            input = "";
        }


        TypeFast()
        {
            CursorVisible = false;
        }

        static void Main(string[] args)
        {
            Title = "Type Fast";

            TypeFast tf = new TypeFast();

            tf.words = System.IO.File.ReadAllLines(@"../../Words.txt");

            tf.Run();
        }
    }
}
