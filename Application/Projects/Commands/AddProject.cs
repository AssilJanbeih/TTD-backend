using Application.Abstraction.Messaging;
using Domain.Abstractions;

namespace Application.Projects.Commands;

public sealed record AddProjectCommand(
    AddProjectRequest Request
) : ICommand<AddProjectResponse>;

public sealed record AddProjectResponse(
    int Id
);

public sealed record AddProjectRequest(
    string Name,
    string ProjectManagerId,
    string ClientEmail,
    string SlackLink,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate,
    Decimal? Completion
);

public class AddProjectHandler : ICommandHandler<AddProjectCommand ,AddProjectResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProjectHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddProjectResponse> Handle(AddProjectCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        Domain.Entities.Project project = Domain.Entities.Project
            .Create(
                request.Name,
                request.ProjectManagerId,
                request.ClientEmail,
                request.SlackLink,
                request.StartDate,
                request.EstimatedEndDate,
                request.ActualEndDate,
                request.Completion
            );
        

        _unitOfWork.Repository<Domain.Entities.Project>()
            .Add(project);
        await _unitOfWork.Complete(cancellationToken);

        await _unitOfWork.Complete(cancellationToken);

        return new AddProjectResponse(project.Id);
    }
}