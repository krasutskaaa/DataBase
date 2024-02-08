using DataBase;
using DataBase.Abstractions.Entities;
using DataBase.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


ApplicationContextOperationsManager manager = new ApplicationContextOperationsManager();
//manager.AddCoach("Tom Barter", "tom_barter78", DataBase.Abstractions.Gender.Male);
//manager.AddCoach("Alice Piterson", "alice_in_wonderland", DataBase.Abstractions.Gender.Female);
//manager.AddCoach("Kate Brown", "kateeeBrown", DataBase.Abstractions.Gender.Transgender);
//manager.AddCoach("Damon Salavtor", "damonSalvatorrr", DataBase.Abstractions.Gender.Male);
//manager.AddCoach("Stefan Salvator", "stefaaanSalvvvaator", DataBase.Abstractions.Gender.Gey);

//manager.AddClient("Venessa Paradis", "venessaLoveU", DataBase.Abstractions.Gender.Female,
//Guid.Parse("97B56B90-46F5-4EB5-841D-0F85B4E274B6"));
//manager.AddClient("Chloe Yang", "ccchhloe", DataBase.Abstractions.Gender.Bigender, Guid.Empty);
//manager.AddClient("Pamela Reif", "reif.pamela", DataBase.Abstractions.Gender.Demigirl,
//    Guid.Parse("365CF6A8-9BDE-4219-B23A-03246DB99E9F"));
//manager.AddClient("Brad Pitt", "pitty", DataBase.Abstractions.Gender.Male,
//    Guid.Parse("365CF6A8-9BDE-4219-B23A-03246DB99E9F"));
//manager.AddClient("Rege-Jean Page", "regeee78page", DataBase.Abstractions.Gender.Male,
//    Guid.Empty);
//manager.AddClient("Jacob Elordi", "elordiJacobbbbb", DataBase.Abstractions.Gender.Demiboy,
//    Guid.Parse("92445C7E-CFD5-4D08-A8AE-56BF2C146AC1"));


//manager.AddCard(Guid.Parse("AB484E5E-55C0-4105-ADD7-1FD460B71C8A"), Type.Premium, true);
//manager.AddCard(Guid.Parse("6891FF3D-93CA-4BE2-81AD-B5ABE67506DD"), Type.Sport, true);
//manager.AddCard(Guid.Parse("EEFD789C-23FC-41AC-B1B5-551866F2CEAC"), Type.LuxSpa, false);
//manager.AddCard(Guid.Parse("E7EE5859-9321-42DF-990B-95DC76535C69"), Type.LuxSpa, true);
//manager.AddCard(Guid.Parse("71CD210A-DD79-46BF-8F86-A9EB50B47028"), Type.Sport, false);
//manager.AddCard(Guid.Parse("35E84C91-1736-4940-8CF3-AB5138A61AC5"), Type.SportPool, true);

//Console.WriteLine();
//var coaches = manager.GetCoachesByGender(Gender.Male);
//foreach (var coach in coaches)
//{
//    Console.WriteLine($"{coach.RealName} - {coach.Username} - {coach.Gender}");
//}


//Console.WriteLine();
//var cardd = manager.GetCardByClientUsername("ccchhloe");
//Console.WriteLine($"{cardd.ClientId} - {cardd.CardType} - {cardd.IsActive}");

//Console.WriteLine();
//manager.DeleteClientAndCardByCardIsInactive();

var cards = manager.GetCards();
foreach (var card in cards)
{
    Console.WriteLine($"{card.ClientId} - {card.CardType} - {card.IsActive}");
}

