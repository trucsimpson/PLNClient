using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using PLNClient.Constants;
using PLNClient.Enums;
using PLNClient.Models;
using PLNClient.Services;

namespace PLNClient.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

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
            Settings = await LocalStorage.GetItemAsync<LayoutSettingsModel>(LayoutConstant.LayoutSettingName);
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
            var urlParts = url.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (urlParts.Length > 0)
            {
                switch (urlParts[0].ToLower())
                {
                    case "houses":
                        return urlParts.Length > 1 ? PageType.Houses : PageType.Assets;

                    case "products":
                        return urlParts.Length > 1 ? PageType.Products : PageType.Estimating;

                    default:
                        return PageType.Assets;
                }
            }

            return PageType.Assets;
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= HandleLocationChanged;
        }
    }
}
