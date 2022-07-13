using Service;

namespace QrCodeMaui;

public partial class MainPage : ContentPage
{
    private readonly Camera camera;
    private readonly IQRCodeService qRCodeService;

    public MainPage(Camera camera, IQRCodeService qRCodeService)
	{
		InitializeComponent();
        this.camera = camera;
        this.qRCodeService = qRCodeService;
    }

	private void OnCameraClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(camera);
	}
	private void OnListarClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new Listar(qRCodeService));
	}
}

