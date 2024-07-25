using System.ComponentModel.DataAnnotations;
using Application.Abstraction.Messaging;
using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Task.Commands;

public sealed record DeleteTasksRequest(
    [Required] List<int> Ids
);

public sealed record DeleteTasksCommand(
    DeleteTasksRequest Request
) : ICommand;

public class DeleteTasksHandler : ICommandHandler<DeleteTasksCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TTDContext _context;

    public DeleteTasksHandler(IUnitOfWork unitOfWork, TTDContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTasksCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var entitiesToDelete = await _context
            .Tasks
            .Where(p => request.Ids.Contains(p.Id))
            .ToListAsync(cancellationToken);

        _context
            .Tasks
            .RemoveRange(entitiesToDelete);

        await _unitOfWork.Complete(cancellationToken);
        return Unit.Value;
    }
}