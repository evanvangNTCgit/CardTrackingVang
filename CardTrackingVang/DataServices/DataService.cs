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

        public List<CardType> GetCardTypes() 
        {
            return this._dataContext.CardType.ToList();
        }

        public List<Card> GetCards() 
        {
            return this._dataContext.Card
                .Include(c => c.CardType)
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
    }
}
