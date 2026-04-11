using CardTrackingVang.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CardTrackingVang.ViewModel
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private readonly Card _card;

        private int _Id;

        private string _title;

        private decimal _value;

        private int _cardTypeId;

        public CardType _cardType;

        private CardImage _cardImage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CardViewModel(Card c) 
        {
            _card = c;
            _Id = c.Id;
            _title = c.Title;
            _value = c.Value;
            _cardTypeId = c.CardTypeID;
            _cardType = c.CardType;
            _cardImage = c.CardImage;
        }
        public int Id
        {
            get => this._Id; 
            set
            {
                this._Id = value;
                NotifyPropertyChanged();
            }
        }

        public string Title
        {
            get => this._title;

            set
            {
                this._title = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Value
        {
            get => this._value;
            set
            {
                this._value = value;
                NotifyPropertyChanged();
            }
        }

        public int CardTypeID
        {
            get => this._cardTypeId;
            set
            {
                this._cardTypeId = value;
                NotifyPropertyChanged();
            }
        }

        public CardType CardType
        {
            get => this._cardType;
            set
            {
                this._cardType = value;
                NotifyPropertyChanged();
            }
        }

        // Gets an image based on the cardtype.
        public ImageSource GetCardImage
        {
            get
            {
                if (this._card.CardImage != null && !string.IsNullOrEmpty(this._card.CardImage.ImagePath))
                {
                    // Return the custom photo (Azure AI Vision result or manual upload)
                    return ImageSource.FromFile(this._card.CardImage.ImagePath);
                }
                else
                {

                    switch (this.CardType.Type)
                    {
                        case ("Sport"):
                            return ImageSource.FromFile("runningicon.png");
                        case ("Pokemon"):
                            return ImageSource.FromFile("pikapi.png");
                        case ("Digimon"):
                            return ImageSource.FromFile("digimonlogo.png");
                        case ("Yu-Gi-Oh!"):
                            return ImageSource.FromFile("yugioh.png");
                        case ("Magic: The Gathering"):
                            return ImageSource.FromFile("magicgatheringlogo.png");
                        default:
                            return ImageSource.FromFile("dotnet_bot.png");
                    }
                }
            }
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-10.0
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
