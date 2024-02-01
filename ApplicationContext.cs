using DataBase.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
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
        public void AddClient(string realName, string username, Guid cardId)
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
                throw new ArgumentException("User not found", nameof(clientId));
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
            if (!clients.Any(client => client.Id.Equals(username)))
                throw new ArgumentException("User not found", nameof(clientId));
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
        public void RemoveClient(string username)
        {
            if (!clients.Any(client => client.Id.Equals(username)))
                throw new ArgumentException("User not found", nameof(clientId));
            using (ApplicationContext db = new ApplicationContext())
            {
                Client? client = db.clients.FirstOrDefault(client => client.Username.Equals(username));
                if (client != null)
                {
                    db.clients.Remove(client);
                    db.SaveChanges();
                }
            }
        }


    }
}
