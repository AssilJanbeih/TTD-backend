using Application.Abstraction.Messaging;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Application.Task.Commands;

public sealed record AddTaskCommand(
    AddTaskRequest Request
) : ICommand<AddTaskResponse>;

public sealed record AddTaskResponse(
    int Id
);

public sealed record AddTaskRequest(
    string Name,
    string ProjectId,
    string Assignee,
    string Status,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate
);

public class AddTaskHandler : ICommandHandler<AddTaskCommand, AddTaskResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddTaskHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddTaskResponse> Handle(AddTaskCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        Domain.Entities.Task task = Domain.Entities.Task
            .Create(
                request.Name,
                request.ProjectId,
                request.Assignee,
                request.Status,
                request.StartDate,
                request.EstimatedEndDate,
                request.ActualEndDate
            );
        

        _unitOfWork.Repository<Domain.Entities.Task>()
            .Add(task);
        await _unitOfWork.Complete(cancellationToken);

        var project = _unitOfWork
                    .Repository<Domain.Entities.Project>()
                    .GetAll()
                    .FirstOrDefault(p => p.EntityId.Equals(request.ProjectId));
        project!.UpdateCompletion();
        _unitOfWork.Repository<Domain.Entities.Project>().Update(project);
        await _unitOfWork.Complete(cancellationToken);

        return new AddTaskResponse(task.Id);
    }
}