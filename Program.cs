using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;
using static System.Net.Mime.MediaTypeNames;

namespace TypeFast
{
    internal class TypeFast
    {
        Random rnd = new Random();

        public bool finished = false;
        public bool endOfText = false;

        int difficulty = 0;
        int textSize;
        int marge = 20;
        int currentWordIndex = 0;
        
        string currentWord;
        string[] words;
        string[] text = {};
        string input = "";

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
            textSize = 40;
            difficulty = 0;
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
            for (int i = 0; i < textSize; i++)
            {
                int index = rnd.Next(0, range);
                textList.Add(words[index].ToLower());
            }
            text = textList.ToArray();
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
                text[currentWordIndex] = "%" + currentWord;
            else
                text[currentWordIndex] = "#" + currentWord;
        }

        public void LockWord()
        {
            if (text.Length == currentWordIndex + 1)
            {
                endOfText = true;
                finished = true;
            }
            else if (input == text[currentWordIndex])
            {
                text[currentWordIndex] = "$" + text[currentWordIndex];
            }
            else
            {
                text[currentWordIndex] = "@" + text[currentWordIndex];
            }
        }

        public void WriteInput()
        {
            CursorLeft = WindowWidth / 2 - input.Length / 2;
            CursorTop  = 10;
            Write(input);

            ConsoleKeyInfo keyInfo = ReadKey();
            ConsoleKey key = keyInfo.Key;
            char keyChar = keyInfo.KeyChar;

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
            else if (key == Enter) { }
            else if (key == Escape)
                finished = true;
            else
            {
                input += keyChar;
            }
        }

        public void EndScreen()
        {
            Clear();
            SetCursorPosition(0, 0);
            WriteLine("Press any key to exit...");
            ReadKey();
        }

        private void Run()
        {
            InitAll();
            while (!finished)
            {
                UpdateText();
                WriteText();
                WriteInput();
            }
            EndScreen();
        }


        TypeFast()
        {
            CursorVisible = false;
        }

        static void Main(string[] args)
        {
            Title = "Type Fast";

            TypeFast tf = new TypeFast();

            tf.words = System.IO.File.ReadAllLines(@"E:\Program Files (x86)\Visual Studio\Repos\TypeFast\Words.txt");

            tf.Run();
        }
    }
}
