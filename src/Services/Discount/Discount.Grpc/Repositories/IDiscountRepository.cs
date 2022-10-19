using Dapper;
using Discount.Api.Entities;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Api.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        Task<bool> CreateCoupon(Coupon coupon);

        Task<bool> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(string couponId);
    }

    public class DiscountRepository : IDiscountRepository
    {
        private readonly DatabaseSettings _settings;

        public DiscountRepository(IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
            
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var npgsqlConnection = new NpgsqlConnection(_settings.ConnectionString);
            var coupon = await npgsqlConnection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return coupon ?? new Coupon { ProductName = "Default", Amount = 0, Description = "No Discount" };
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using var npgsqlConnection = new NpgsqlConnection(_settings.ConnectionString);
            var affected = await npgsqlConnection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount});

            return affected != 0;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using var npgsqlConnection = new NpgsqlConnection(_settings.ConnectionString);
            var affected = await npgsqlConnection.ExecuteAsync(
                "UPDATE Coupon SET ProductName = @ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id=coupon.Id });

            return affected != 0;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var npgsqlConnection = new NpgsqlConnection(_settings.ConnectionString);
            var affected = await npgsqlConnection.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName=@ProductName",
                new { ProductName = productName });

            return affected != 0;
        }
    }
}
