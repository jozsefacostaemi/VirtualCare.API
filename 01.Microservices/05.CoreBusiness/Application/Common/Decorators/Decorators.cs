using MediatR;
using Shared.Common.RequestResult;

namespace Web.Core.API.Application.Common.Decorators;

public class ModuleCommandHandlerDecorator<TCommand> : IRequestHandler<TCommand, RequestResult>, IDecorators where TCommand : IRequest<RequestResult>
{
    private readonly IRequestHandler<TCommand, RequestResult> _inner;
    public ModuleCommandHandlerDecorator(IRequestHandler<TCommand, RequestResult> inner) => _inner = inner;
    public async Task<RequestResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var result = await _inner.Handle(command, cancellationToken);
        result.Module = typeof(TCommand).Name;
        return result;
    }
}

public class ModuleQueryHandlerDecorator<TQuery> : IRequestHandler<TQuery, RequestResult>, IDecorators where TQuery : IRequest<RequestResult>
{
    private readonly IRequestHandler<TQuery, RequestResult> _inner;
    public ModuleQueryHandlerDecorator(IRequestHandler<TQuery, RequestResult> inner) => _inner = inner;
    public async Task<RequestResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var result = await _inner.Handle(query, cancellationToken);
        result.Module = typeof(TQuery).Name;
        return result;
    }
}
public interface IDecorators { }