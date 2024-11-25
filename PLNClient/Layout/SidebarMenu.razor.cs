using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PLNClient.Models;

namespace PLNClient.Layout
{
    public partial class SidebarMenu
    {
        [Parameter]
        public LayoutSettingsModel CurrentSettings { get; set; }

        [Parameter]
        public List<MenuItemModel> MenuItems { get; set; } = new();

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private void ToggleMenu(MenuItemModel item)
        {
            // item.IsActive = !item.IsActive;

            if (item.IsActive)
            {
                CloseAllMenus(new List<MenuItemModel> { item });
            }
            else
            {
                CloseAllMenus(MenuItems);
                item.IsActive = true;
            }
        }

        private void CloseAllMenus(List<MenuItemModel> items)
        {
            foreach (var item in items)
            {
                item.IsActive = false;
                if (item.HasChildren)
                {
                    CloseAllMenus(item.Children);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var layoutType = "vertical";
            if (CurrentSettings != null && !string.IsNullOrEmpty(CurrentSettings.Layout))
            {
                layoutType = CurrentSettings.Layout;
            }

            if (layoutType == "vertical")
            {
                await HandleVerticalLayout();
            }
            else if (layoutType == "horizontal")
            {
                await HandleHorizontalLayout();
            }
        }

        private async Task HandleVerticalLayout()
        {
            var currentNewURL = NavigationManager.Uri;
            var baseUrl = NavigationManager.BaseUri;
            var currentLink = await JSRuntime.InvokeAsync<string>("document.getElementById", "get-url");
        }

        private async Task HandleHorizontalLayout()
        {
            var currentNewUrl = NavigationManager.Uri;
        }
    }
}
