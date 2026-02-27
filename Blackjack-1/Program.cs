using System;
using System.Collections.Generic;



BlackJack blackJack = new BlackJack();

Gambler player = new Gambler("플레이어");
Gambler dealer = new Gambler("딜러");



Console.WriteLine("=== 블랙잭 게임 ===");

while (true)
{
    if (!blackJack.Betting(player))
    {
        continue;
    }

    blackJack.StartGame(player, dealer);
    blackJack.EndGame(player, dealer);

    if (!blackJack.NewGame(player, dealer))
    {
        break;
    }
}