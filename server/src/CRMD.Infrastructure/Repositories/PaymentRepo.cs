using System;
using System.Data;

namespace CRMD.Infrastructure.Repositories;

public class PaymentRepo : IPaymentRepo
{
    public async Task<string> RegisterPaymentAsync(clsPayment payment)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_RegisterPayment", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", payment.SupplierId);
                cmd.Parameters.AddWithValue("@InvoiceId", payment.InvoiceId);
                cmd.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@PaymentMethod", (object?)payment.PaymentMethod ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object?)payment.Notes ?? DBNull.Value);

                var outputIdParam = new SqlParameter("@PaymentId", SqlDbType.UniqueIdentifier)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return outputIdParam.Value.ToString()!;
            }
        }
    }
}
