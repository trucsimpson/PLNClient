using Microsoft.AspNetCore.Components;
using PLNClient.Models;

namespace PLNClient.Layout
{
    public partial class HeaderMenu
    {
        [Parameter]
        public LayoutSettingsModel CurrentSettings { get; set; }

        [Parameter]
        public List<MenuItemModel> MenuItems { get; set; } = new();

        [Inject]
        private NavigationManager NavigationManager { get; set; }
    }
}
