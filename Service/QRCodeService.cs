using Model;
using Infrastructure.Repositories;

namespace Service;
public class QRCodeService : IQRCodeService
{
    private readonly IRepository<DadosQRCode> repository;

    public QRCodeService(IRepository<DadosQRCode> repository)
    {
        this.repository = repository;
    }

    public IEnumerable<DadosQRCode> RecuperarDados()
    {
        return this.repository.QueryLazyLoad();
    }

    public void Salvar(DadosQRCode dadosQRCode)
    {
        this.Validar(dadosQRCode);
        this.repository.Add(dadosQRCode);
    }
    private void Validar(DadosQRCode dadosQRCode)
    {
        DadosQRCode? dado = this.repository.QueryLazyLoad()
            .FirstOrDefault(d => d.Dado == dadosQRCode.Dado);
        if (dado != null) 
            throw new Exception("Dado já cadastrado.");
    }
}
