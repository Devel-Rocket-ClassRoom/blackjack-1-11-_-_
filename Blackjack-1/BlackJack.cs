using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public class BlackJack
{
    int bettingChips = 0;



    public string Loser { get; private set; } = string.Empty;

    public void StartGame(Gambler player, Gambler dealer)
    {
        Deck.GiveCard(player);
        Deck.GiveCard(player);
        Deck.GiveCard(dealer);
        Deck.GiveCard(dealer);

        Console.Write("딜러의 패: [??]");
        dealer.PrintCards(1);

        Console.WriteLine();
        Console.WriteLine($"딜러의 점수: ??\n");

        Console.Write("플레이어의 패:");
        player.PrintCards(0);

        Console.WriteLine();
        Console.WriteLine($"플레이어의 점수: {player.SumScore()}\n");


        PlayerBlackJack(player);
        if (Loser == string.Empty) DealerBlackJack(dealer);
    }


    public void EndGame(Gambler player, Gambler dealer)
    {
        Console.WriteLine("\n=== 게임 결과 ===");
        Console.WriteLine($"{player.Name}: {player.SumScore()}점");
        Console.WriteLine($"{dealer.Name}: {dealer.SumScore()}점");

        if (player.SumScore() > dealer.SumScore() && Loser == string.Empty)
        {
            Loser = dealer.Name;
        }
        else if (dealer.SumScore() >  player.SumScore())
        {
            Loser = player.Name;
        }


        if (Loser == dealer.Name)
        {
            Console.WriteLine($"\n플레이어 승리! (+{bettingChips})");
            player.Chip += bettingChips;
        }
        else if (Loser == player.Name)
        {
            Console.WriteLine($"\n딜러 승리! (-{bettingChips}개)");
            player.Chip -= bettingChips;
        }
        else
        {
            Console.WriteLine($"무승부");
        }
    }


    public bool NewGame(Gambler player, Gambler dealer)
    {
        if (player.Chip == 0)
        {
            return false;
        }

        Console.Write("새 게임을 시작하시겠습니까. Y/N : ");

        string newGame;

        try
        {
            newGame = Console.ReadLine().Substring(0, 1).ToUpper();
        }
        catch
        {
            return false;
        }

        if (newGame.Equals("Y"))
        {
            Console.Clear();
            Console.WriteLine("=== 새 게임 시작 ===\n");
            Loser = string.Empty;
            player.NewGame();
            dealer.NewGame();
            bettingChips = 0;
            return true;
        }

        return false;
    }


    public bool Betting(Gambler gambler)
    {
        Console.Write($"보유 칩: {gambler.Chip}개\n배팅 금액을 입력하세요: ");

        if (!int.TryParse(Console.ReadLine(), out bettingChips))
        {
            Console.WriteLine("다시 입력해주세요");
            return false;
        }
        else if (bettingChips <= 0)
        {
            Console.WriteLine("다시 입력해주세요");
            return false;
        }
        else if (bettingChips > gambler.Chip)
        {
            Console.WriteLine("칩이 부족합니다");
            return false;
        }

        return true;
    }


    void PlayerBlackJack(Gambler player)
    {
        while (Loser.Equals(string.Empty))
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
                HitCard(player);
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


    void DealerBlackJack(Gambler dealer)
    {
        Console.Write("\n딜러의 숨겨진 카드:");
        dealer.PrintCards(0);
        Console.WriteLine('\n');

        while (dealer.SumScore() < 17 && Loser.Equals(string.Empty))
        {
            HitCard(dealer);
        }
    }


    void HitCard(Gambler gambler)
    {
        Deck.GiveCard(gambler);

        int lastIdx = 0;

        for (int i = 0; gambler.Hands[i] != null; i++)
        {
            lastIdx++;
        }

        Console.Write($"\n{gambler.Name}가 카드를 받았습니다");
        gambler.PrintCards(--lastIdx);
        Console.WriteLine();

        Console.WriteLine($"{gambler.Name}의 패");
        gambler.PrintCards( 0);
        Console.WriteLine();
        Console.WriteLine($"{gambler.Name}의 점수: {gambler.SumScore()}");

        if (gambler.SumScore() > 21)
        {
            Console.WriteLine("\n버스트! 21을 초과했습니다\n");

            Loser = gambler.Name;
        }
    }
}
