using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

public record TwitchAuthorizationRequestOptions(ClientId ClientId, AccessToken? BearerToken);
