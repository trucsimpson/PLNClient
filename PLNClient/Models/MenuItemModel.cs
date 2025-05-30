﻿namespace PLNClient.Models
{
    public class MenuItemModel
    {
        public string? Title { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public bool IsActive { get; set; }

        public string? SpecialMenu { get; set; }

        public bool HasChildren => Children != null && Children.Any();

        public List<MenuItemModel> Children { get; set; } = new();
    }
}
