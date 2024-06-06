using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Interfaces;
using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Services;

public class FakePaymentGateway : IPaymentGateway {
  private readonly Random random = new();

  public bool ProcessPayment(decimal amount, Course course) => random.Next(2) == 0;
}