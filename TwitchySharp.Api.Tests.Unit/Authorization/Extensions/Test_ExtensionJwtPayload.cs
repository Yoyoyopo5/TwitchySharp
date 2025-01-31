using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;

namespace TwitchySharp.Api.Tests.Unit.Authorization.Extensions;
public class Test_ExtensionJwtPayload
{
    [Fact]
    public void Sign_ExtensionJwtPayload_ReturnSignedJwt()
    {
        const string FAKE_USER_ID = "123";
        const string FAKE_EXTENSION_SECRET = "iWLtPMsRJPfdZNdC/4Ug9gtZNdWlfFDvGweKuW4EVjk=";
        DateTimeOffset fakeExpiry = DateTimeOffset.FromUnixTimeSeconds(9001);
        ExtensionJwtPayload stubPayload = new(FAKE_USER_ID) { ExpiresAt = fakeExpiry };
        const string MOCK_SIGNED_JWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHQiOjkwMDEsInVzZXJfaWQiOiIxMjMiLCJyb2xlIjoiZXh0ZXJuYWwifQ.82xiab2B8PIYDs65fUVgYDxApDooCtjUfkzDtDqj94E";

        string actual = stubPayload.Sign(FAKE_EXTENSION_SECRET);

        Assert.Equal(MOCK_SIGNED_JWT, actual);
    }
}
