using System.Collections.Generic;
using System.Threading.Tasks;
using Veek.Microservices.ProductService.Localization;
using Veek.Microservices.ProductService.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Veek.Microservices.ProductService.Web.Menus;

public class ProductServiceMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemProducts(context, moduleMenu);
        return Task.CompletedTask;
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            ProductServiceMenus.Prefix,
            context.GetLocalizer<ProductServiceResource>()["Menu:ProductService"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
        
    private static void AddMenuItemProducts(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                ProductServiceMenus.Products,
                context.GetLocalizer<ProductServiceResource>()["Menu:Products"],
                url: "/Products"
            ).RequirePermissions(ProductServicePermissions.Products.Default));

        context.Menu.Items.AddIfNotContains(parentMenu);
    }
}