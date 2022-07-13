using Model;
using Service;

namespace QrCodeMaui;
public partial class Listar : ContentPage
{
    private readonly IQRCodeService qRCodeService;
    public IEnumerable<DadosQRCode> ListaDadosQRCode;

    public Listar(IQRCodeService qRCodeService)
    {
        InitializeComponent();
        this.qRCodeService = qRCodeService;
        this.ListaDadosQRCode = this.RecuperarDados();
        this.CarregarDados();
    }
    private IEnumerable<DadosQRCode> RecuperarDados()
    {
        return this.qRCodeService.RecuperarDados();
    }
    private void CarregarDados()
    {
        string dados = "";
        foreach (var item in this.ListaDadosQRCode)
        {
            dados += item.Dado + "\r\n";
        }
        lblDados.Text = dados;
    }
    public async void OnTapLabel(object sender, EventArgs args)
    {
        Label texto = (Label) sender;
        await Clipboard.Default.SetTextAsync(texto.Text);
    }
}