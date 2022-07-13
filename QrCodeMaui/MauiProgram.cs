using ZXing.Net.Maui;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Service;
using Infrastructure.Context;
using Model;

namespace QrCodeMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		IServiceCollection services = builder.Services;
		RegistrarContexto(services);
		RegistrarRepositorios(services);
		RegistrarViews(services);
		RegistrarServicos(services);
		MauiApp app = builder.Build();
		AtualizarDB(services);
		return app;
	}

	private static void RegistrarContexto(IServiceCollection services)
	{	
		String databasePath =
            Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
				"qrcodemaui.db"
			);
        services.AddEntityFrameworkSqlite()
        .AddDbContext<Infrastructure.Context.QRCodeDbContext>(opcoes =>
        {
            opcoes.UseSqlite($"Filename={databasePath}");
        });
	}
	private static void RegistrarRepositorios(IServiceCollection services)
	{
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IRepository<DadosQRCode>, Repository<DadosQRCode>>();
	}
	private static void RegistrarServicos(IServiceCollection services)
	{
		services.AddScoped<IQRCodeService, QRCodeService>();
	}
	private static void RegistrarViews(IServiceCollection services)
	{
		services.AddScoped<MainPage>();
		services.AddScoped<Camera>();
	}
	private static void AtualizarDB(IServiceCollection services)
	{
		try
		{
			var provider = services.BuildServiceProvider();
			var context = provider.GetRequiredService<QRCodeDbContext>();
			context.Database.Migrate();
		}
		catch (System.Exception e)
		{
			throw e;
		}
	}
}
