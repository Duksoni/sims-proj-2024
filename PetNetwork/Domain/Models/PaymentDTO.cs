using Newtonsoft.Json;

namespace PetNetwork.Domain.Models;

/// <summary>
/// Class modeled after the schema found on <br/>
/// <see href="https://epp.trezor.gov.rs/docs/user/bank-accounts/sheme/#json"/>
/// </summary>
public class PaymentDTO
{
    public string SvrhaDoznake { get; } // Payment purpose

    public decimal Iznos { get; } // Amount

    public string NazivZaduzenja { get; } // Payer

    public string RacunZaduzenja { get; } // Payer bank account

    public DateTime VremeIzvrsenja { get; } // Payment approval date

    [JsonConstructor]
    public PaymentDTO(string svrhaDoznake, decimal iznos, string nazivZaduzenja, string racunZaduzenja, DateTime vremeIzvrsenja)
    {
        SvrhaDoznake = svrhaDoznake;
        Iznos = iznos;
        NazivZaduzenja = nazivZaduzenja;
        RacunZaduzenja = racunZaduzenja;
        VremeIzvrsenja = vremeIzvrsenja;
    }
}