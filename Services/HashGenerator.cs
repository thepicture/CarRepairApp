using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarRepairApp.Services
{
    public class HashGenerator : IHashGenerator
    {
        public void GenerateHash(string inputString,
                                 out byte[] outputHash,
                                 out byte[] outputSalt,
                                 byte[] inputSalt = null)
        {
            byte[] passwordBytes = Encoding.UTF8
               .GetBytes(inputString);
            outputSalt = inputSalt;
            if (outputSalt == null)
            {
                outputSalt = Encoding.UTF8
                    .GetBytes(
                        Guid.NewGuid()
                            .ToString()
                            .Substring(0, 16));
            }
            byte[] passwordAndSalt = passwordBytes
                .Concat(outputSalt)
                .ToArray();
            outputHash = SHA256
                .Create()
                .ComputeHash(passwordAndSalt);
        }
    }
}
