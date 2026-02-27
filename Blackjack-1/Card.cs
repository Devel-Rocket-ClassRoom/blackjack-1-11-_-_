using System;
using System.Collections.Generic;
using System.Text;

public class Card
{
    public int shape;
    public int num;


    public Card(int shape, int num)
    {
        this.shape = shape;
        this.num = num;
    }
}

public static class Deck
{
    public static int nextCard = 0;

    public static Card[] deck = new Card[52];


    static Deck()
    {
        MakeDeck();
        SuffleDeck();
    }


    public static void MakeDeck()
    {
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck[count] = new Card(i, j);
                count++;
            }
        }
    }

    public static void GiveCard(Gambler gambler)
    {
        for (int i = 0; i < gambler.Hands.Length; i++)
        {
            if (gambler.Hands[i] == null)
            {
                gambler.Hands[i] = deck[nextCard++];

                if (nextCard >= 52)
                {
                    SuffleDeck();
                }
                return;
            }
        }
    }


  public static void SuffleDeck()
    {
        Console.WriteLine("카드를 섞는 중...\n");

        Random random = new Random();

        for (int i = 0; i < 200; i++)
        {
            int rand = random.Next(52), rand2 = random.Next(52);
            Card temp = deck[rand];
            deck[rand] = deck[rand2];
            deck[rand2] = temp;
        }

        nextCard = 0;
    }
}