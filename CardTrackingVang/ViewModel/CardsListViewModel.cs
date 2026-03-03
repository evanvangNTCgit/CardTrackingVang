using CardTrackingVang.DataServices;
using CardTrackingVang.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CardTrackingVang.ViewModel
{
    public class CardsListViewModel
    {
        // The selected card of user.
        private CardViewModel? _selectedCard;

        private readonly DataService _dataService;

        // Commands for making updates to the DB:
        public ICommand AddCardCommand { get; private set; }
        public ICommand RemoveCardCommand { get; private set; }

        // The list of User cards for them to see not just the DB directly...
        public ObservableCollection<CardViewModel> Cards { get; set; }

        // Needs the DataService to update the DB.
        public CardsListViewModel(DataService ds)
        {
            Cards = [];
            _dataService = ds;

            this._dataService.EnsureSeedData();

            this.RefreshCards();
            this.AddCardCommand = new Command<CardViewModel>(AddCard);
            this.RemoveCardCommand = new Command<int>(DeleteCard);
        }

        /// <summary>
        /// Adds a card to Users .db file.
        /// </summary>
        /// <param name="c">CardViewModel to make users card object to later add to DB</param>
        public void AddCard(CardViewModel c)
        {
            // quickly make a new Card Object to add to the DB...
            var cardGettingAdded = new Card
            {
                Id = c.Id,
                Title = c.Title,
                Value = c.Value,
                CardTypeID = c.CardTypeID,
                CardType = c.CardType,
            };
            this._dataService.AddCard(cardGettingAdded);

            this.RefreshCards();
        }

        /// <summary>
        /// Adds a card to Users .db file.
        /// </summary>
        /// <param name="c">CardViewModel to make users card object to later add to DB</param>
        public void AddCardWithModel(Card c)
        {
            this._dataService.AddCard(c);

            this.RefreshCards();
        }

        /// <summary>
        /// Deletes a card from the Users .db file
        /// </summary>
        /// <param name="id">ID of the card getting deleted.</param>
        public void DeleteCard(int id)
        {
            this._dataService.RemoveCard(id);

            this.RefreshCards();
        }

        // A refresh just to make sure that all cards get displayed after DB Operations...
        public void RefreshCards()
        {
            IEnumerable<Card> cardsData = this._dataService.GetCards();

            this.Cards.Clear();
            foreach (Card c in cardsData)
            {
                // Make a viewModel for each of the cards now...
                this.Cards.Add(new CardViewModel(c));
            }
        }

        // This is for my add card page so the user can see types of cards.
        public List<CardType> GetCardTypes()
        {
            return this._dataService.GetCardTypes();
        }
    }
}
