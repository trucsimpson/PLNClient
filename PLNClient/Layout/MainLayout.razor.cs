using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using PLNClient.Constants;
using PLNClient.Enums;
using PLNClient.Models;
using PLNClient.Services;

namespace PLNClient.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private ILocalStorageService localStorage { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IMenuService MenuService { get; set; }

        private LayoutSettingsModel Settings { get; set; }

        public List<MenuItemModel> MenuItems { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentSettingsAsync();
            NavigationManager.LocationChanged += HandleLocationChanged;
            LoadMenu();
        }

        private async Task GetCurrentSettingsAsync()
        {
            Settings = await localStorage.GetItemAsync<LayoutSettingsModel>(LayoutConstant.LayoutSettingName);
            if (Settings == null)
            {
                Settings = new LayoutSettingsModel();
            }
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            LoadMenu();
            StateHasChanged();
        }

        private void LoadMenu()
        {
            var currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            var pageType = DeterminePageType(currentUrl);
            MenuItems = MenuService.GetMenuItems(pageType);
            MenuItems = MenuService.SetActiveMenuItems(MenuItems, currentUrl);
        }

        private PageType DeterminePageType(string url)
        {
            if (url == "houses")
            {
                return PageType.Assets;
            }
            else if (url.Contains("houses/"))
            {
                return PageType.Houses;
            }
            else if (url == "products")
            {
                return PageType.Estimating;
            }
            else
            {
                return PageType.Assets;
            }
        }

        private async Task ToggleSidebar()
        {
            await JSRuntime.InvokeVoidAsync("appInterop.toggleSidebar");
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}
