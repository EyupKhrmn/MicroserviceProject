using System.Data;
using System.Runtime.CompilerServices;
using CourseServiceCatalog.Shares;
using Dapper;
using Npgsql;

namespace FreeCourse.Services.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }


    public async Task<ResponseDto<List<Entities.Discount>>> GetAll()
    {
        var discount = await _dbConnection.QueryAsync<Entities.Discount>("select * from discount");
        return ResponseDto<List<Entities.Discount>>.Success(discount.ToList(), 200);
    }

    public async Task<ResponseDto<Entities.Discount>> GetById(int id)
    {
        var discount =
            (await _dbConnection.QueryAsync<Entities.Discount>("select * from discount where id=@Id", new { Id = id }))
            .SingleOrDefault();

        if (discount == null)
        {
            return ResponseDto<Entities.Discount>.Fail("Discount not Found", 404);
        }

        return ResponseDto<Entities.Discount>.Success(discount, 200);
    }

    public async Task<ResponseDto<NoContent>> Save(Entities.Discount discount)
    {
        var saveStatus =
            await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) values (@UserId,@Rate,@Code)",discount);

        if (saveStatus > 0)
        {
            return ResponseDto<NoContent>.Success(204);
        }

        return ResponseDto<NoContent>.Fail("an error occurred while adding", 500);
    }

    public async Task<ResponseDto<NoContent>> Update(Entities.Discount discount)
    {
        var status =
            await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", new
            {
                Id = discount.Id,
                UserId = discount.UserId,
                Code = discount.Code,
                Rate = discount.Rate
            });

        if (status > 0)
        {
            return ResponseDto<NoContent>.Success(204);
        }

        return ResponseDto<NoContent>.Fail("discount not found", 404);
    }

    public async Task<ResponseDto<NoContent>> Delete(int id)
    {
        var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

        return status > 0
            ? ResponseDto<NoContent>.Success(204)
            : ResponseDto<NoContent>.Fail("discount not found", 404);
    }

    public async Task<ResponseDto<Entities.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discount = await _dbConnection.QueryAsync<Entities.Discount>(
            "select * from discount where userid=@UserId and code=@Code",
            new { UserId = userId, Code = code });

        var hasDiscount = discount.FirstOrDefault();

        if (hasDiscount == null)
        {
            return ResponseDto<Entities.Discount>.Fail("discount not found", 404);
        }

        return ResponseDto<Entities.Discount>.Success(hasDiscount,200);
    }
}