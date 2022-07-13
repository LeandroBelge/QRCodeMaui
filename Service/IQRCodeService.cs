using Model;
namespace Service;
public interface IQRCodeService
{
    void Salvar(DadosQRCode dadosQRCode);
    IEnumerable<DadosQRCode> RecuperarDados();
}
