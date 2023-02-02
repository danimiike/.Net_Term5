/* Program:         QuiddlerLibrary
 * Module:          IDeck.cs
 * Author:          Danielle Menezes de Mello Miike
 *                  Priscilla Peron
 * Date:            February 10, 2022
 * Description:     
 */

namespace QuiddlerLibrary
{
    public interface IDeck
    {
        string About { get; }
        int CardCount { get; }
        int CardsPerPlayer { get; set; }
        string TopDiscard { get; }
        IPlayer NewPlayer();
        string ToString();
    }
}
