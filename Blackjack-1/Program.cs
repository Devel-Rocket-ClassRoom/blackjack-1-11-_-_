using System;
using System.Collections.Generic;



Random random = new Random();

Card[] cards = new Card[52];
int nextCard = -1;

int d = 0;

Card[] playerHands = new Card[21];
Card[] dealerHands = new Card[21];

int playerChip = 1000;

char winner = 'N';

Console.WriteLine("=== 블랙잭 게임 ===");

while (true)
{
    Console.Write($"보유 칩: {playerChip}개\n배팅 금액을 입력하세요: ");

    int betChip = 0;
    if (!int.TryParse(Console.ReadLine(), out betChip))
    {
        Console.WriteLine("다시 입력해주세요");
        continue;
    }
    else if (betChip <= 0)
    {
        Console.WriteLine("다시 입력해주세요");
        continue;
    }
    else if (betChip > playerChip)
    {
        Console.WriteLine("칩이 부족합니다");
        continue;
    }

    Console.WriteLine();

    if (nextCard == -1)
    {
        SuffleDeck();
    }

    GetCard(ref dealerHands);
    GetCard(ref dealerHands);
    GetCard(ref playerHands);
    GetCard(ref playerHands);

    Console.Write("딜러의 패: [??]");
    PrintCards(dealerHands, 1);

    Console.WriteLine();
    Console.WriteLine($"딜러의 점수: ??\n");

    Console.Write("플레이어의 패:");
    PrintCards(playerHands, 0);

    Console.WriteLine();
    Console.WriteLine($"플레이어의 점수: {SumScore(playerHands)}\n");



    PlayerBlackJack();

    if (winner == 'N') DealerBlackJack();


    Console.WriteLine("\n=== 게임 결과 ===");
    Console.WriteLine($"플레이어: {SumScore(playerHands)}점");
    Console.WriteLine($"딜러: {SumScore(dealerHands)}점");


    if (SumScore(playerHands) > SumScore(dealerHands) && winner == 'N')
    {
        winner = 'P';
    }
    else if (SumScore(dealerHands) > SumScore(playerHands) && winner == 'N')
    {
        winner = 'D';
    }


    if (winner == 'P')
    {
        Console.WriteLine($"\n플레이어 승리! (+{betChip})");
        playerChip += betChip;
    }
    else if (winner == 'D')
    {
        Console.WriteLine($"\n딜러 승리! (-{betChip}개)");
        playerChip -= betChip;
    }
    else
    {
        Console.WriteLine($"무승부");
    }


    if (playerChip == 0)
    {
        Console.WriteLine("칩 부족으로 인한 게임 오버!");
        return;
    }


    Console.Write("새 게임을 하시겠습니다? (Y/N): ");

    string newGame;

    try
    {
        newGame = Console.ReadLine().Substring(0, 1).ToUpper();
    }
    catch
    {
        return;
    }

    if (newGame.Equals("Y"))
    {
        Console.Clear();
        Console.WriteLine("=== 새 게임 시작 ===\n");
        winner = 'N';
        playerHands = new Card[21];
        dealerHands = new Card[21];
        betChip = 0;
    }
    else
    {
        break;
    }
}





void PlayerBlackJack()
{
    while (true)
    {
        Console.Write("H(Hit)또는 S(Stand)를 선택하세요: ");

        string playerInput;

        try
        {
            playerInput = Console.ReadLine().Substring(0, 1).ToUpper();
        }
        catch
        {
            continue;
        }

        if (playerInput.Equals("H"))
        {
            Console.WriteLine("플레이어가 카드를 받았습니다\n");
            GetCard(ref playerHands);
            PrintCards(playerHands, 0);
            Console.WriteLine($"플레이어의 점수: {SumScore(playerHands)}");

            if (SumScore(playerHands) > 21)
            {
                Console.WriteLine("버스트! 21을 초과했습니다\n");
                winner = 'D';
                break;
            }
        }
        else if (playerInput.Equals("S"))
        {
            Console.WriteLine("플레이어가 S를 선택했습니다\n");
            break;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            continue;
        }
    }
}


void DealerBlackJack()
{
    Console.Write("딜러의 숨겨진 카드:");
    PrintCards(dealerHands, 0);
    Console.WriteLine();

    while (SumScore(dealerHands) < 17)
    {
        Console.WriteLine("딜러가 카드를 받습니다");
        GetCard(ref dealerHands);
        PrintCards(dealerHands, 0);

        Console.WriteLine($"딜러 점수: {SumScore(dealerHands)}");

        Console.WriteLine();

        if (SumScore(dealerHands) > 21)
        {
            winner = 'P';
            Console.WriteLine("버스트! 21을 초과했습니다\n");
            break;
        }
    }
}



void PrintCards(Card[] deck, int start)
{
    for(int i = start; i < deck.Length && deck[i] != null; i++)
    {
        string shape = string.Empty;
        string numChar = string.Empty;

        switch (deck[i].shape)
        {
            case 1:
                shape = "♠";
                break;
            case 2:
                shape = "♥";
                break;
            case 3:
                shape = "♦";
                break;
            case 0:
                shape = "♣";
                break;
        }

        switch (deck[i].num)
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
                numChar = (deck[i].num + 1).ToString();
                break;
        }


        Console.Write($" [{shape}{numChar}]");
    }
}


int SumScore(Card[] deck)
{
    int sum = 0;
    int oneCount = 0;

    for (int i = 0; i < deck.Length && deck[i] != null; i++)
    {
        if (deck[i].num == 0)
        {
            oneCount++;
            continue;
        }

        if (deck[i].num >= 10)
        {
            sum += 10;
            continue;
        }

        sum += deck[i].num + 1;
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


void GetCard(ref Card[] deck)
{
    for (int i = 0; i < deck.Length; i++)
    {
        if (deck[i] == null)
        {
            deck[i] = cards[nextCard++];
            if (nextCard >= 52)
            {
                SuffleDeck();
            }
            break;
        }
    }
}



void SuffleDeck()
{
    int count = 0;

    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 13; j++)
        {
            cards[count] = new Card(i, j);
            count++;
        }
    }

    Console.WriteLine("카드를 섞는 중...\n");

    for (int i = 0; i < 200; i++)
    {
        int rand = random.Next(52), rand2 = random.Next(52);
        Card temp = cards[rand];
        cards[rand] = cards[rand2];
        cards[rand2] = temp;
    }

    nextCard = 0;

    /*for (int i = 0; i < cards.Length; i++)
    {
        string shape = string.Empty;
        
        switch (cards[i].shape)
        {
            case 1:
                shape = "♠";
                break;
            case 2:
                shape = "♥";
                break;
            case 3:
                shape = "♦";
                break;
            case 0:
                shape = "♣";
                break;
        }

        Console.Write($" [{shape}{cards[i].num + 1}]");
    }*/
}