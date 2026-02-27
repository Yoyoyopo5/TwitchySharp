namespace TwitchySharp.Api.AuthorizationResolution;

internal record MiddlewarePipelineBuilder<TResolver>
{
    private readonly List<Func<TResolver, TResolver>> _components = [];
    public MiddlewarePipelineBuilder<TResolver> Use(Func<TResolver, TResolver> func)
    {
        _components.Add(func);
        return this;
    }
    public MiddlewarePipelineBuilder<TResolver> Use(TResolver func)
    {
        _components.Add((_) => func);
        return this;
    }
    public TResolver Finally(TResolver terminal)
        => _components.AsEnumerable().Reverse().Aggregate(terminal, (current, next) => next(current));
}