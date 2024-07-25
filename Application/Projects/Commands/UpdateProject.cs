using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Abstraction.Messaging;

namespace Application.Project.Commands;

public sealed record UpdateProjectRequest(
    [Required] Guid Id,
   string Name,
    string ProjectManagerId,
    string ClientEmail,
    string SlackLink,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate,
    Decimal? Completion
);

public sealed record UpdateProjectCommand(
    UpdateProjectRequest Request
) : Abstraction.Messaging.ICommand;
public class UpdateProject : ICommandHandler<UpdateProjectCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var projectToEdit = await _unitOfWork
            .Repository<Domain.Entities.Project>()
            .GetAll()
            .FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

        
        projectToEdit.Update(
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
            .Update(projectToEdit);
        await _unitOfWork.Complete(cancellationToken);

        return Unit.Value;
    }
}