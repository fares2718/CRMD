using System;

namespace CRMD.Domain.Repos.Interfaces;

public interface IPaymentRepo
{
    public Task<string> RegisterPaymentAsync(clsPayment payment);
}
