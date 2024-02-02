using DataBase.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Client> clients;
        public DbSet<Card> cards;
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
        public void AddClient(string realName, string username)
        {
            if (clients.Any(client => client.Username.Equals(username)))
                throw new ArgumentException("The username already exists", nameof(username));
            using (ApplicationContext db = new ApplicationContext())
            {
                db.clients.Add(new Client
                {
                    RealName = realName,
                    Username = username
                });
                db.SaveChanges();
            }
        }
        public void AddCard(Guid clientId, Type cardType, bool isActive)
        {
            if (!clients.Any(client => client.Id.Equals(clientId)))
                throw new ArgumentException("The card isn't found", nameof(clientId));
            using (ApplicationContext db = new ApplicationContext())
            {
                db.cards.Add(new Card
                {
                    ClientId = clientId,
                    CardType = cardType,
                    IsActive = isActive
                });
                db.SaveChanges();
            }
        }
        public List<Client> GetClients()
        {
            using(ApplicationContext db =  new ApplicationContext())
            {
                var clients = db.clients.ToList();
                return clients;
            }
        }
        public List<Card> GetCards()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var cards = db.cards.ToList();
                return cards;
            }
        }
        public void RemoveClient(string username)
        {
            if (!clients.Any(client => client.Username.Equals(username)))
                throw new ArgumentException("The client isn't found", nameof(username));
            using (ApplicationContext db = new ApplicationContext())
            {
                Client? client = db.clients.FirstOrDefault(client => client.Username.Equals(username));
                if(client!=null)
                {
                    db.clients.Remove(client);
                    db.SaveChanges();
                }
            }
        }
        public void RemoveCard(Guid clientId)
        {
            if (!clients.Any(client => client.Id.Equals(clientId)))
                throw new ArgumentException("The card isn't found", nameof(clientId));
            using (ApplicationContext db = new ApplicationContext())
            {
                Card? card = db.cards.FirstOrDefault(card => card.ClientId.Equals(clientId));
                if (card != null)
                {
                    db.cards.Remove(card);
                    db.SaveChanges();
                }
            }
        }
        public void EditClient(string realName, string username)
        {
            if (!clients.Any(client => client.Username.Equals(username)))
                throw new ArgumentException("The client isn't found", nameof(username));
            using (ApplicationContext db = new ApplicationContext())
            {
                Client? client = db.clients.FirstOrDefault(client => client.Username.Equals(username));
                if(client != null)
                {
                    client.RealName = realName;
                    db.SaveChanges();
                }
               
            }
        }
        public void EditCard(Guid clientId, Type cardType, bool isActive)
        {
            if (!clients.Any(client => client.Id.Equals(clientId)))
                throw new ArgumentException("The card isn't found", nameof(clientId));
            using (ApplicationContext db = new ApplicationContext())
            {
                Card? card = db.cards.FirstOrDefault(card => card.ClientId.Equals(clientId));
                if (card != null)
                {
                    card.CardType = cardType;
                    card.IsActive = isActive;
                    db.SaveChanges();
                }
            }
        }
      
        public IEnumerable<Client> GetClientsByCardType (Type cardType)
        {
            if (!cards.Any(card => card.CardType.Equals(cardType)))
                throw new ArgumentException("There aren't exist any cards with such type",nameof(cardType));
            foreach (var card in cards)
            {
                if (card.CardType.Equals(cardType))
                {
                    yield return clients.FirstOrDefault(client
                        => client.Id.Equals(card.ClientId));
                }
            }
        }
        public void DeleteClientAndCardByCardIsActive()
        {
            foreach(var card in cards)
            {
                if(card.IsActive.Equals(false)&& DateTime.Now.Subtract(card.CreateTime)>TimeSpan.FromDays(365))
                {
                    clients.Remove(clients.FirstOrDefault(client => client.Id.Equals(card.ClientId)));
                    cards.Remove(card);
                }
            }
        }

        public Client GetClientByCardClientId(Guid clientId)
        {
            if (!clients.Any(client => client.Id.Equals(clientId)))
                throw new ArgumentException("The client isn't found", nameof(clientId));
            return clients.FirstOrDefault(client => client.Id.Equals(clientId));
        }
        public IEnumerable<Card> GetCardByClientUserName(string username)
        {
            if (!clients.Any(client => client.Username.Equals(username)))
                throw new ArgumentException("The client doesn't exist", nameof(username));
            foreach (var client in clients)
            {
                if(client.Username.Equals(username))
                {
                    yield return cards.FirstOrDefault(card => card.ClientId.Equals(client.Id));
                }
            }
        }
    }
}
