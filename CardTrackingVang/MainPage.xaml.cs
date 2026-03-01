using System;
using System.Collections.Generic;
using System.Linq;
using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.Models;

namespace CardTrackingVang
{
    public partial class MainPage : ContentPage
    {
        private DataService _dataService;
        private readonly DataContext _dataContext;

        public MainPage(DataContext dataContext)
        {
            this._dataContext = dataContext;
            this._dataService = new DataService(dataContext);

            InitializeComponent();

            // Checks if user does not have data first in this method
            this.AddDummyData();

            // Bind the UI element to the current list of card types
            this.TestOutput.ItemsSource = this._dataContext.CardType.ToList();

            Console.WriteLine("HI!");
        }

        public void AddDummyData()
        {
            var testIfCardTypes = this._dataContext.CardType.ToList();
            if (testIfCardTypes.Count <= 0)
            {
                this._dataContext.CardType.Add(new CardType { Id = 1, Type = "Sport" });
                this._dataContext.CardType.Add(new CardType { Id = 2, Type = "Pokemon" });
                this._dataContext.CardType.Add(new CardType { Id = 3, Type = "Digimon" });
                this._dataContext.CardType.Add(new CardType { Id = 4, Type = "Yu-Gi-Oh!" });
                this._dataContext.CardType.Add(new CardType { Id = 5, Type = "Magic: The Gathering" });

                _dataContext.Card.AddRange(new List<Card>
                {
                    // Sports (ID 1)
                    new Card { Title = "1952 Mickey Mantle #311", Value = 5200000m, CardTypeID = 1 },
                    new Card { Title = "2003 LeBron James Rookie Parallel", Value = 150.5m, CardTypeID = 1 },
                    new Card { Title = "Tom Brady Autograph - New England", Value = 1200m, CardTypeID = 1 },
                    new Card { Title = "Shohei Ohtani Topps Chrome", Value = 45m, CardTypeID = 1 },

                    // Pokemon (ID 2)
                    new Card { Title = "Charizard 1st Edition Shadowless", Value = 35000m, CardTypeID = 2 },
                    new Card { Title = "Umbreon VMAX Alternate Art", Value = 650m, CardTypeID = 2 },
                    new Card { Title = "Pikachu Illustrator Copy", Value = 900m, CardTypeID = 2 },
                    new Card { Title = "Gengar VMAX (Fusion Strike)", Value = 180.25m, CardTypeID = 2 },

                    // Digimon (ID 3)
                    new Card { Title = "Omnimon (Parallel Rare)", Value = 85m, CardTypeID = 3 },
                    new Card { Title = "Beelzemon: Blast Mode", Value = 45.5m, CardTypeID = 3 },
                    new Card { Title = "Alphamon Alternate Art", Value = 110m, CardTypeID = 3 },
                    new Card { Title = "Agumon Ghost Rare", Value = 250m, CardTypeID = 3 },

                    // Yu-Gi-Oh (ID 4)
                    new Card { Title = "Blue-Eyes White Dragon (LOB-001)", Value = 1500m, CardTypeID = 4 },
                    new Card { Title = "Dark Magician Girl (Ghost Rare)", Value = 420m, CardTypeID = 4 },
                    new Card { Title = "Exodia the Forbidden One (Full Set)", Value = 200m, CardTypeID = 4 },
                    new Card { Title = "Pot of Greed (Ultimate Rare)", Value = 95m, CardTypeID = 4 },

                    // Magic (ID 5)
                    new Card { Title = "Black Lotus (Alpha)", Value = 500000m, CardTypeID = 5 },
                    new Card { Title = "The One Ring (Serialized)", Value = 2000000m, CardTypeID = 5 },
                    new Card { Title = "Mox Emerald", Value = 8500m, CardTypeID = 5 },
                    new Card { Title = "Sheoldred, The Apocalypse", Value = 75m, CardTypeID = 5 }
                });

                _dataContext.SaveChanges();
            }
        }
    }
}
