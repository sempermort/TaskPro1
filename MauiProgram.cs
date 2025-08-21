using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using TaskPro1.Helpers;
using TaskPro1.Helpers.Interfaces;
using TaskPro1.Helpers.Interfaces.Implementations;
using TaskPro1.ViewModels;
using TaskPro1.Views;

namespace TaskPro1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseMauiCommunityToolkit()
                   .UseSkiaSharp()
                   .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                        fonts.AddFont("FluentSystemIcons-Filled.ttf", "FluentIconFilled");
                        fonts.AddFont("FluentSystemIconsRegular.ttf", "FluentIconRegular");
                        fonts.AddFont("FluentSystemIcons-Resizable.ttf", "FluentIconResizable");
                    });
            builder.Services.AddSingleton<FontAwesomeHelper>();
            
            // Register our image ontap operation service
            builder.Services.AddSingleton<IAnimateOnTap,OnTapService>();
            // Register our CenterMap operation service
            builder.Services.AddSingleton<ICenterOnMap, MapCenterService>();

            // Register IMessenger (singleton)
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // Register view models with dependencies
            builder.Services.AddTransient<MapPageViewModel>(sp =>
            {
                var fontHelper = sp.GetRequiredService<FontAwesomeHelper>();
           
                return new MapPageViewModel(fontHelper);
            });
            builder.Services.AddTransient<SearchViewModel>(sp =>
            {
                var fontHelper = sp.GetRequiredService<FontAwesomeHelper>();

                return new SearchViewModel(fontHelper);
            });
            // Register MapPage with a factory to inject dependencies
            builder.Services.AddTransient<MapPageCompositeViewModel>(sp =>
            {
                var mapPageVM = sp.GetRequiredService<MapPageViewModel>();
                var searchVM = sp.GetRequiredService<SearchViewModel>();
                return new MapPageCompositeViewModel(mapPageVM, searchVM);
            });
            // Register MapPage with composite view model
            builder.Services.AddTransient<MapPage>(sp =>
            {
                var compositeVM = sp.GetRequiredService<MapPageCompositeViewModel>();
                return new MapPage(compositeVM);
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
