using System;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.ValueObjects;

namespace UnibenWeb.Domain.Interfaces.Services
{
    public interface IPagarContaService: IDisposable
    {
        ValidationResult Adicionar(PagarConta pagarConta);
        void Excluir(PagarConta pagarConta);
    }
}
