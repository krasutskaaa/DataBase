using DataBase.Abstractions.Entities;
using DataBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.VisualBasic;

namespace DataBase
{
    public class ApplicationContextOperationsManager
    {
        public void AddClient(string realName, string username, Gender gender, Guid? coachId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Clients.Any(client => client.Username.Equals(username)))
                    throw new ArgumentException("The username already exists", nameof(username));
                db.Clients.Add(new Client
                {
                    RealName = realName,
                    Username = username,
                    Gender = gender,
                    CoachId = coachId
                });
                db.SaveChanges();
            }
        }
        public void AddCard(Guid clientId, Type cardType, bool isActive)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Cards.Any(card => card.ClientId.Equals(clientId)))
                    throw new ArgumentException("The card already exists", nameof(clientId));
                db.Cards.Add(new Card
                {
                    ClientId = clientId,
                    CardType = cardType,
                    IsActive = isActive
                });
                db.SaveChanges();
            }
        }
        public void AddCoach(string realName, string username, Gender gender)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Coaches.Any(coach => coach.Username.Equals(username)))
                    throw new ArgumentException("The username already exists", nameof(username));
                db.Coaches.Add(new Coach
                {
                    RealName = realName,
                    Username = username,
                    Gender = gender
                });
                db.SaveChanges();
            }
        }
        public List<Client> GetClients()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var clients = db.Clients.ToList();
                return clients;
            }
        }
        public List<Card> GetCards()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var cards = db.Cards.ToList();
                return cards;
            }
        }
        public List<Coach> GetCoaches()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var coaches = db.Coaches.ToList();
                return coaches;
            }
        }
        public void RemoveClient(string username)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Clients.Any(client => client.Username.Equals(username)))
                    throw new ArgumentException("The client isn't found", nameof(username));
                Client? client = db.Clients.FirstOrDefault(client => client.Username.Equals(username));
                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
            }
        }
        public void RemoveCard(Guid clientId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Cards.Any(card => card.ClientId.Equals(clientId)))
                    throw new ArgumentException("The card isn't found", nameof(clientId));
                Card? card = db.Cards.FirstOrDefault(card => card.ClientId.Equals(clientId));
                if (card != null)
                {
                    db.Cards.Remove(card);
                    db.SaveChanges();
                }
            }
        }
        public void RemoveCoach(string username)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Coaches.Any(coach => coach.Username.Equals(username)))
                    throw new ArgumentException("The coach isn't found", nameof(username));
                Coach? coach = db.Coaches.FirstOrDefault(coach => coach.Username.Equals(username));
                if (coach != null)
                {
                    db.Coaches.Remove(coach);
                    db.SaveChanges();
                }
            }
        }
        public void EditClient(string realName, string username, Gender gender)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Clients.Any(client => client.Username.Equals(username)))
                    throw new ArgumentException("The client isn't found", nameof(username));
                Client? client = db.Clients.FirstOrDefault(client => client.Username.Equals(username));
                if (client != null)
                {
                    client.RealName = realName;
                    client.Gender = gender;
                    db.SaveChanges();
                }

            }
        }
        public void EditCard(Guid clientId, Type cardType, bool isActive)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Cards.Any(card => card.ClientId.Equals(clientId)))
                    throw new ArgumentException("The card isn't found", nameof(clientId));
                Card? card = db.Cards.FirstOrDefault(card => card.ClientId.Equals(clientId));
                if (card != null)
                {
                    card.CardType = cardType;
                    card.IsActive = isActive;
                    db.SaveChanges();
                }
            }
        }
        public void EditCoach(string realName, string username, Gender gender)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Coaches.Any(coach => coach.Username.Equals(username)))
                    throw new ArgumentException("The coach isn't found", nameof(username));
                Coach? coach = db.Coaches.FirstOrDefault(coach => coach.Username.Equals(username));
                if (coach != null)
                {
                    coach.RealName = realName;
                    coach.Gender = gender;
                    db.SaveChanges();
                }
            }
        }

        public List<Client> GetClientsByCardType(Type cardType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Cards.Any(card => card.CardType.Equals(cardType)))
                    throw new ArgumentException("There aren't  any cards with such type", nameof(cardType));
                var clientIds = db.Cards.Where(card => card.CardType.Equals(cardType)).Select(card => card.ClientId);
                var clients = db.Clients.Where(client => clientIds.Contains(client.Id)).ToList();
                return clients;
            } 
        }
        public void DeleteClientAndCardByCardIsInactive()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var card in db.Cards)
                {
                    // the statement if can be changed due to customer needs
                    if (card.IsActive.Equals(false) && DateTime.Now.Subtract(card.CreateTime).TotalMinutes > 10)
                    {
                        db.Clients.Remove(db.Clients.FirstOrDefault(client => client.Id.Equals(card.ClientId)));
                        db.Cards.Remove(card);
                        db.SaveChanges();
                    }
                }
            }   
        }
        public Client GetClientByCardClientId(Guid clientId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Clients.Any(client => client.Id.Equals(clientId)))
                    throw new ArgumentException("The client isn't found", nameof(clientId));
                var client = db?.Clients.FirstOrDefault(client => client.Id.Equals(clientId));
                return client;
            }
        }
        public Card GetCardByClientUsername(string username)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Clients.Any(client => client.Username.Equals(username)))
                    throw new ArgumentException("The client doesn't exist", nameof(username));
                var client = db?.Clients.FirstOrDefault(client => client.Username.Equals(username));
                var card = db?.Cards.FirstOrDefault(card => card.ClientId.Equals(client.Id));
                return card;
            }
        }

        public List<Coach> GetCoachesByGender(Gender gender)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Coaches.Any(coach => coach.Gender.Equals(gender)))
                    throw new ArgumentException("There aren't any coaches with such gender", nameof(gender));
                var coaches = db.Coaches.Where(coach => coach.Gender.Equals(gender)).ToList();
                return coaches;
            }
        }

        public List<Client> GetClientsByCoachUsername(string username)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.Coaches.Any(coach => coach.Username.Equals(username)))
                    throw new ArgumentException("There aren't any coaches with the username", nameof(username));
                var coach = db?.Coaches.FirstOrDefault(coach => coach.Username.Equals(username));
                var clients = db?.Clients.Where(client => client.CoachId.Equals(coach.Id)).ToList();
                return clients;
            }   
        }
    }
}

