using Ardalis.SharedKernel;
using LoanHub.Backend.Core.UserAggregate;
using LoanHub.Backend.Infrastructure.Data;
using LoanHub.Backend.UseCases.Features.Counter;
using Microsoft.EntityFrameworkCore;

namespace LoanHub.Backend.Web.Statistics;

public sealed class GetUsersCountHandler : IQueryHandler<GetUsersCountQuery, GetUsersCountResult>
{
    private readonly AppDbContext _db;

    public GetUsersCountHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<GetUsersCountResult> Handle(GetUsersCountQuery query, CancellationToken ct)
    {
        var count = await _db.Set<User>().Where(u => !u.IsDeleted).CountAsync(ct);
        return new GetUsersCountResult(count);
    }
}
