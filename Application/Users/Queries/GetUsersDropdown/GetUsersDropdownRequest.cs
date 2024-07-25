using Domain.Enums;

namespace Application.Users.Queries.GetUsersDropdown;

public record GetUsersDropdownRequest(JobTypeEnum? JobType, string? DesignatedCountry, bool? ByDepartment, int? department);