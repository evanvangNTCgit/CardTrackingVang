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
            this._dataService.EnsureSeedData();

            // Bind the UI element to the current list of card types
            this.TestOutput.ItemsSource = this._dataService.GetCardTypes();

            Console.WriteLine("HI!");
        }
    }
}
