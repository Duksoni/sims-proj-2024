using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class PaymentService
{
    private readonly IRepository<Payment> _paymentRepository;

    public PaymentService(IRepository<Payment> paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public void Add(Payment payment) => _paymentRepository.Add(payment);

    public void Update(Payment updatedPayment) => _paymentRepository.Update(updatedPayment);

    public IList<Payment> GetAll() => _paymentRepository.GetAll();
}