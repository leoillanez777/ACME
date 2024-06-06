using ACME.SchoolManagement.Domain;

namespace ACME.SchoolManagement.Interfaces;

public interface IPaymentGateway {
  bool ProcessPayment(decimal amount, Course course);
}