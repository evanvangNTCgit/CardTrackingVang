using CardTrackingVang.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CardTrackingVang.ViewModel
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private int _Id;

        private string _title;

        private decimal _value;

        private int _cardTypeId;

        public CardType _cardType;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CardViewModel(Card c) 
        {
            _Id = c.Id;
            _title = c.Title;
            _value = c.Value;
            _cardTypeId = c.CardTypeID;
            _cardType = c.CardType;
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
