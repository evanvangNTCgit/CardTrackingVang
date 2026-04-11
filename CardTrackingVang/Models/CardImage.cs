using System;
using System.Collections.Generic;
using System.Text;

namespace CardTrackingVang.Models
{
    public class CardImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public int CardId { get; set; }
        public Card? Card { get; set; }
    }
}
