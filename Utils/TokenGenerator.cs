using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

namespace ComIT_eLearning.Utils
{
  public static class TokenGenerator
  {
    public static string GenerateShortToken(int length = 24)
    {
      var bytes = RandomNumberGenerator.GetBytes(length);
      return WebEncoders.Base64UrlEncode(bytes);
    }
  }
}
