using Model;
using Service;
using ZXing.Net.Maui;

namespace QrCodeMaui;
public partial class Camera : ContentPage
{
    private readonly IQRCodeService qRCodeService;
    private bool emUso = false;

    public Camera(IQRCodeService qRCodeService) 
    {
        InitializeComponent();
        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
        this.qRCodeService = qRCodeService;
    }

    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
        string dadoRecuperado = "";
        try
        {
            if (!this.emUso) 
            {
                this.emUso = true;
                await Device.InvokeOnMainThreadAsync(async() =>
                {
                    var r = e.Results.FirstOrDefault();
                    dadoRecuperado = r.Value;
                    DadosQRCode dadosQRCode = new ();
                    dadosQRCode.Dado = dadoRecuperado;
                    this.qRCodeService.Salvar(dadosQRCode);
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        await DisplayAlert("QRCodeMaui",
                            "QRCode gravado com sucesso!",
                            "Ok", " ");
                            this.emUso = false;
                    });
                });
            }
        }
        catch (System.Exception ex)
        {
            Device.BeginInvokeOnMainThread(async() =>
           {
               await DisplayAlert("QRCodeMaui",
                $"{ex.Message}",
                "Ok", " ");
                this.emUso = false;
           });
        }
    }
}