using System;
using System.Security.Cryptography;
using System.Text;

namespace PokemonPc.Utils.Functions;

public static class UuidGenerator
{
    public static Guid GenerateUuid(Guid namespaceId, string userId)
    {
        using (var sha1 = SHA1.Create())
        {
            byte[] namespaceBytes = namespaceId.ToByteArray();
            Array.Reverse(namespaceBytes);

            byte[] userIdBytes = Encoding.UTF8.GetBytes(userId);

            byte[] data = new byte[namespaceBytes.Length + userIdBytes.Length];
            Buffer.BlockCopy(namespaceBytes, 0, data, 0, namespaceBytes.Length);
            Buffer.BlockCopy(userIdBytes, 0, data, namespaceBytes.Length, userIdBytes.Length);

            byte[] hash = sha1.ComputeHash(data);

            hash[6] = (byte)((hash[6] & 0x0F) | 0x50);
            hash[8] = (byte)((hash[8] & 0x3F) | 0x80);

            return new Guid(hash.Take(16).ToArray());
        }
    }
}
