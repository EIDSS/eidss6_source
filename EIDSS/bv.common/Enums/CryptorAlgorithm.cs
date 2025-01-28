using System;

namespace bv.common.Enums
{
    /// <summary>
    /// Type of encryption/decryption algorithm to be applied for credentials
    /// </summary>
    public enum CryptorAlgorithm
    {
        Rijndael = 0,
        AesGCM = 1,
    }
}
