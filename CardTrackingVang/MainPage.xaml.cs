using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.Models;
using CardTrackingVang.ViewModel;

namespace CardTrackingVang
{
    public partial class MainPage : ContentPage
    {
        private CardsListViewModel _cardListVM;

        public MainPage(CardsListViewModel cm)
        {
            this._cardListVM = cm;

            InitializeComponent();

            // Bind the UI element to the current list of card types
            this.TestOutput.ItemsSource = this._cardListVM.Cards;
        }
    }
}
