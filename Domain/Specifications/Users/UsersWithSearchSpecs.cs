using Domain.Entities;
using Domain.Specifications.Base;

namespace Domain.Specifications.Users;

public class UsersWithSearchSpecs : BaseSpecification<User>
{
    public UsersWithSearchSpecs(BasePagingRequest paging) 
        : base(u => (string.IsNullOrWhiteSpace(paging.Search) || u.Name.ToLower().Contains(paging.Search)))
    {
        AddOrderByDescending(x => x.Id);
        ApplyPaging(paging.PageSize * (paging.PageIndex - 1), paging.PageSize);
    }
    
}