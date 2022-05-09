namespace CarRepairApp.Services
{
    public interface IHashGenerator
    {
        void GenerateHash(string inputString,
                          out byte[] outputHash,
                          out byte[] outputSalt,
                          byte[] inputSalt = null);
    }
}
