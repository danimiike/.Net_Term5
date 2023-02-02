/* Program:         QuiddlerLibrary
 * Module:          IPlayer.cs
 * Author:          Danielle Menezes de Mello Miike
 *                  Priscilla Peron
 * Date:            February 10, 2022
 * Description:     
 */

namespace QuiddlerLibrary
{
    public interface IPlayer
    {
        int CardCount { get; }
        int TotalPoints { get; }
        string DrawCard();
        bool Discard(string card);
        string PickupTopDiscard();
        int PlayWord(string candidate);
        int TestWord(string candidate);
        string ToString();
    }
}
