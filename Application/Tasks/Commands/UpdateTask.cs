using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Abstraction.Messaging;

namespace Application.Task.Commands;

public sealed record UpdateTaskRequest(
    [Required] Guid Id, 
    string Name, 
    string ProjectId, 
    string Assignee, 
    string Status, 
    DateTime StartDate, 
    DateTime EstimatedEndDate, 
    DateTime? ActualEndDate
);

public sealed record UpdateTaskCommand(
    UpdateTaskRequest Request
) : Abstraction.Messaging.ICommand;
public class UpdateProject : ICommandHandler<UpdateTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProject(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var taskToEdit = await _unitOfWork
            .Repository<Domain.Entities.Task>()
            .GetAll()
            .FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

        
        taskToEdit.Update(
            taskToEdit.Name,
            request.ProjectId,
            request.Assignee,
            request.Status,
            request.StartDate,
            request.EstimatedEndDate,
            request.ActualEndDate
        );
        
        _unitOfWork.Repository<Domain.Entities.Task>()
            .Update(taskToEdit);
        await _unitOfWork.Complete(cancellationToken);

        return Unit.Value;
    }
}