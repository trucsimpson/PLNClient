using PLNClient.Enums;
using PLNClient.Models;

namespace PLNClient.Services
{
    public interface IMenuService
    {
        List<MenuItemModel> GetMenuItems(PageType pageType);

        List<MenuItemModel> GetHeaderMenu();

        List<MenuItemModel> SetActiveMenuItems(List<MenuItemModel> menuItems, string currentUrl);
    }

    public class MenuService : IMenuService
    {
        public List<MenuItemModel> GetMenuItems(PageType pageType)
        {
            return pageType switch
            {
                PageType.Assets => GetAssetsMenu(),
                PageType.Divisions => GetDivisionsMenu(),
                PageType.Houses => GetHousesMenu(),
                PageType.Estimating => GetEstimatingMenu(),
                _ => new List<MenuItemModel>()
            };
        }

        public List<MenuItemModel> GetHeaderMenu()
        {
            return new List<MenuItemModel>
            {
                GetAssetsMenu().First(),
                GetEstimatingMenu().First()
            };
        }

        public List<MenuItemModel> SetActiveMenuItems(List<MenuItemModel> menuItems, string currentUrl)
        {
            foreach (var item in menuItems)
            {
                item.IsActive = IsUrlMatch(item.Url, currentUrl);

                if (item.Children != null && item.Children.Any())
                {
                    foreach (var childItem in item.Children)
                    {
                        childItem.IsActive = IsUrlMatch(childItem.Url, currentUrl);
                        if (childItem.IsActive)
                        {
                            item.IsActive = true;
                        }
                    }
                }
            }

            return menuItems;
        }

        private bool IsUrlMatch(string menuUrl, string currentUrl)
        {
            var currentUrlWithoutParams = currentUrl.Split('?')[0];

            menuUrl = menuUrl.Trim('/').ToLower();
            currentUrlWithoutParams = currentUrlWithoutParams.Trim('/').ToLower();

            if (menuUrl == currentUrlWithoutParams) return true;

            return currentUrlWithoutParams.StartsWith(menuUrl + "/");
        }

        private List<MenuItemModel> GetAssetsMenu()
        {
            return new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Assets</span>
                        </li>",
                    Url = string.Empty,
                    Title = "Assets",
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Divisions",
                            Icon = "ti ti-layout-sidebar-right",
                            Url = "/divisions",
                        },
                        new MenuItemModel
                        {
                            Title = "Communities",
                            Icon = "ti ti-building-community",
                            Url = "/communities",
                        },
                        new MenuItemModel
                        {
                            Title = "Series",
                            Icon = "ti ti-layout-dashboard",
                            Url = "/series",
                        },
                        new MenuItemModel
                        {
                            Title = "Houses",
                            Icon = "ti ti-home",
                            Url = "/houses",
                        },
                        new MenuItemModel
                        {
                            Title = "Options",
                            Icon = "ti ti-adjustments",
                            Url = "/options",
                        },
                        new MenuItemModel
                        {
                            Title = "Option Groups",
                            Icon = "ti ti-category",
                            Url = "/option-groups",
                        },
                        new MenuItemModel
                        {
                            Title = "Option Types",
                            Icon = "ti ti-list-details",
                            Url = "/option-types",
                        },
                        new MenuItemModel
                        {
                            Title = "Custom Options",
                            Icon = "ti ti-pencil-plus",
                            Url = "/custom-options",
                        }
                    }
                }
            };
        }

        private List<MenuItemModel> GetDivisionsMenu()
        {
            return new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Divisions</span>
                        </li>",
                    Title = "Divisions",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Division Details",
                            Url = "/divisions/division-details"
                        },
                        new MenuItemModel
                        {
                            Title = "Location",
                            Url = "/divisions/location"
                        },
                        new MenuItemModel
                        {
                            Title = "Communities",
                            Url = "/divisions/communities"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Costing</span>
                        </li>",
                    Title = "Costing",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Vendors",
                            Url = "/divisions/vendors"
                        },
                        new MenuItemModel
                        {
                            Title = "Community Taxes",
                            Url = "/divisions/community-taxes"
                        }
                    }
                }
            };
        }

        private List<MenuItemModel> GetHousesMenu()
        {
            return new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Houses</span>
                        </li>",
                    Title = "Houses",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "House Details",
                            Url = "/houses/house-details"
                        },
                        new MenuItemModel
                        {
                            Title = "Options",
                            Url = "/houses/options"
                        },
                        new MenuItemModel
                        {
                            Title = "Floor Plans",
                            Url = "/houses/floor-plans"
                        },
                        new MenuItemModel
                        {
                            Title = "Resources",
                            Url = "/houses/resources"
                        },
                        new MenuItemModel
                        {
                            Title = "Communities",
                            Url = "/houses/communities"
                        },
                        new MenuItemModel
                        {
                            Title = "Documents",
                            Url = "/houses/documents"
                        },
                        new MenuItemModel
                        {
                            Title = "Custom Fields",
                            Url = "/houses/custom-fields"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">BOM Quantities</span>
                        </li>",
                    Title = "BOM Quantities",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Quantities",
                            Url = "/houses/quantities"
                        },
                        new MenuItemModel
                        {
                            Title = "Plus/Minus View",
                            Url = "/houses/plus-minus-view"
                        },
                        new MenuItemModel
                        {
                            Title = "Import",
                            Url = "/houses/import"
                        },
                        new MenuItemModel
                        {
                            Title = "Comparison Groups",
                            Url = "/houses/comparison-groups"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">BOM</span>
                        </li>",
                    Title = "BOM",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "House BOM",
                            Url = "/houses/house-BOM"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Products</span>
                        </li>",
                    Title = "Products",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Spec Sets",
                            Url = "/houses/spec-sets"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Costing</span>
                        </li>",
                    Title = "Costing",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Bid Costs",
                            Url = "/houses/bid-costs"
                        },
                        new MenuItemModel
                        {
                            Title = "Estimate",
                            Url = "/houses/estimate"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Sales</span>
                        </li>",
                    Title = "Sales",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Options",
                            Url = "/houses/options"
                        },
                        new MenuItemModel
                        {
                            Title = "Profiler",
                            Url = "/houses/profiler"
                        }
                    }
                },
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Marketing</span>
                        </li>",
                    Title = "Marketing",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Floorplan Conditions",
                            Url = "/houses/floorplan-conditions"
                        }
                    }
                }
            };
        }

        private List<MenuItemModel> GetEstimatingMenu()
        {
            return new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    SpecialMenu = @"
                        <li class=""nav-small-cap"">
                            <i class=""ti ti-dots nav-small-cap-icon fs-4""></i>
                            <span class=""hide-menu"">Estimating</span>
                        </li>",
                    Title = "Estimating",
                    Url = string.Empty,
                    Children = new List<MenuItemModel>
                    {
                        new MenuItemModel
                        {
                            Title = "Products",
                            Icon = "ti ti-package",
                            Url = "/products",
                        },
                        new MenuItemModel
                        {
                            Title = "Building Groups",
                            Icon = "ti ti-building-skyscraper",
                            Url = "/building-groups",
                        },
                        new MenuItemModel
                        {
                            Title = "Building Phases",
                            Icon = "ti ti-timeline-event",
                            Url = "/building-phases",
                        },
                        new MenuItemModel
                        {
                            Title = "Styles",
                            Icon = "ti ti-brush",
                            Url = "/styles",
                        },
                        new MenuItemModel
                        {
                            Title = "Manufacturers",
                            Icon = "ti ti-building-factory",
                            Url = "/manufacturers",
                        },
                        new MenuItemModel
                        {
                            Title = "Uses",
                            Icon = "ti ti-tool",
                            Url = "/uses",
                        },
                        new MenuItemModel
                        {
                            Title = "Units",
                            Icon = "ti ti-ruler",
                            Url = "/units",
                        },
                        new MenuItemModel
                        {
                            Title = "Categories",
                            Icon = "ti ti-folders",
                            Url = "/categories",
                        },
                        new MenuItemModel
                        {
                            Title = "Spec Sets",
                            Icon = "ti ti-notebook",
                            Url = "/spec-sets",
                        },
                        new MenuItemModel
                        {
                            Title = "BOM Logic Rules",
                            Icon = "ti ti-hierarchy",
                            Url = "/BOM-logic-rules",
                        },
                        new MenuItemModel
                        {
                            Title = "Worksheets",
                            Icon = "ti ti-clipboard-list",
                            Url = "/worksheets",
                        }
                    }
                },
            };
        }
    }
}
