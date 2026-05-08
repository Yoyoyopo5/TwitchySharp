using System;

namespace TwitchySharp.Api;

public interface ITwitchClientBuilder
{
    ITwitchClientBuilder Use(Func<TwitchRequestHandler, TwitchRequestHandler> func);
    ITwitchClient Build();
}
