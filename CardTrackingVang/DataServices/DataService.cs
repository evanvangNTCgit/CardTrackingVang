using CardTrackingVang.DataAccess;
using CardTrackingVang.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CardTrackingVang.DataServices
{
    public class DataService
    {
        private readonly DataContext _dataContext;
        public DataService(DataContext dc)
        {
            this._dataContext = dc;
        }

        public void EnsureSeedData()
        {
            // Ensure some sample data exists in the database
            if (!this._dataContext.CardType.Any())
            {
                var types = new List<CardType>
                {
                    new CardType { Id = 1, Type = "Sport" },
                    new CardType { Id = 2, Type = "Pokemon" },
                    new CardType { Id = 3, Type = "Digimon" },
                    new CardType { Id = 4, Type = "Yu-Gi-Oh!" },
                    new CardType { Id = 5, Type = "Magic: The Gathering" }
                };

                this._dataContext.CardType.AddRange(types);

                this._dataContext.Card.AddRange(new List<Card>
                {
                    // Sports (ID 1)
                    new Card { Title = "1952 Mickey Mantle #311", Value = 5200000m, CardTypeID = 1 },
                    new Card { Title = "2003 LeBron James Rookie Parallel", Value = 150.5m, CardTypeID = 1 },

                    // Pokemon (ID 2)
                    new Card { Title = "Charizard 1st Edition Shadowless", Value = 35000m, CardTypeID = 2 },
                    new Card { Title = "Umbreon VMAX Alternate Art", Value = 650m, CardTypeID = 2 },
                });

                this._dataContext.SaveChanges();
            }
        }

        public List<CardType> GetCardTypes()
        {
            return this._dataContext.CardType.ToList();
        }

        public int GetCardTypeId(string s) 
        {
            var type = this._dataContext.CardType
                .Where(c => c.Type == s)
                .FirstOrDefault();

            return type!.Id;
        }

        public CardType GetCardType(int id) 
        {
            return this._dataContext.CardType
                .Where(c => c.Id == id)
                .FirstOrDefault()!;
        }

        public List<Card> GetCards()
        {
            return this._dataContext.Card
                .Include(c => c.CardType)
                .Include(ci => ci.CardImage)
                .ToList();
        }

        public void AddCard(Card card)
        {
            this._dataContext.Add(card);
            this._dataContext.SaveChanges();
        }

        public void AddCardType(CardType ct)
        {
            this._dataContext.Add(ct);
            this._dataContext.SaveChanges();
        }

        public void RemoveCard(int id)
        {
            try
            {
                var cardGettingRemoved = this._dataContext.Card.FirstOrDefault(c => c.Id == id);
                this._dataContext.Remove(cardGettingRemoved);
                this._dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                // Just dont delete the card..
            }
        }

        public void UpdateCard(Card c) 
        {
            this._dataContext.Card.Update(c);
            this._dataContext.SaveChanges();
        }
    }
}
