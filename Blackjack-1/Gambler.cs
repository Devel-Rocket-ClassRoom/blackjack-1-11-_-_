using System;
using System.Collections.Generic;
using System.Text;

public class Gambler
{
    public Card[] Hands { get; }
    public string Name { get; private set; }

    public int Chip { get; set; }


    public Gambler(string name)
    {
        Hands = new Card[21];

        Name = name;
        Chip = 1000;
    }


    public int SumScore()
    {
        int sum = 0;
        int oneCount = 0;

        for (int i = 0; i < Hands.Length && Hands[i] != null; i++)
        {
            if (Hands[i].num == 0)
            {
                oneCount++;
                continue;
            }

            if (Hands[i].num >= 10)
            {
                sum += 10;
                continue;
            }

            sum += Hands[i].num + 1;
        }

        if (oneCount > 0)
        {
            for (int i = oneCount; i > 0; i--)
            {
                if (sum + 11 + oneCount - 1 > 21)
                {
                    sum += 1;
                }
                else
                {
                    sum += 11;
                }
            }
        }

        return sum;
    }

    public void PrintCards(int startIdx)
    {
        for (int i = startIdx; i < Hands.Length; i++)
        {
            if (Hands[i] != null)
            {
                string shape = string.Empty;
                string numChar = string.Empty;

                Console.Write($" [");

                switch (Hands[i].shape)
                {
                    case 1:
                        shape = "\u2665"; // 하트
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        shape = "\u2660"; // 스페이드
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case 3:
                        shape = "\u25C6"; // 다이아몬드
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 0:
                        shape = "\u2663"; // 클로버
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                }

                Console.Write(shape);

                Console.ResetColor();


                switch (Hands[i].num)
                {
                    case 0:
                        numChar = "A";
                        break;
                    case 10:
                        numChar = "J";
                        break;
                    case 11:
                        numChar = "Q";
                        break;
                    case 12:
                        numChar = "K";
                        break;
                    default:
                        numChar = (Hands[i].num + 1).ToString();
                        break;
                }


                Console.Write(numChar);
                Console.Write("]");
            }
        }
    }

    public void NewGame()
    {
        for (int i = 0; i < Hands.Length; i++)
        {
            Hands[i] = null;
        }
    }
}