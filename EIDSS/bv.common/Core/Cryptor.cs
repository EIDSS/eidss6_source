using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using bv.common.Diagnostics;
using bv.common.Enums;

namespace bv.common.Core
{
    public class Rijndael
    {

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText, string passPhrase, string saltValue, int passwordIterations, string initVector, int keySize)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                //Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
                var password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7};

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();
                string cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;
            }
            catch (Exception e)
            {
                Dbg.Debug("Encryption error: {0}", e);
            }
            return "";
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        public static string Decrypt(string cipherText, string passPhrase, string saltValue, int passwordIterations, string initVector, int keySize)
        {
            if (String.IsNullOrEmpty(cipherText))
            {
                return null;
            }

            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                //Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
                var password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 };

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                var plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return plainText;
            }
            catch (Exception e)
            {
                Dbg.Debug("decryption error: {0}:", e);
            }
            return null;
        }
    }

    public class AesGCMBouncyCastle
    {
        #region Public Methods and Operators

        /// <summary>
        /// Simple Decryption & Authentication (AES-GCM) of a UTF8 Message
        /// </summary>
        /// <param name="encryptedMessage">The encrypted message.</param>
        /// <param name="key">The base 64 encoded 256 bit key.</param>
        /// <param name="nonSecretPayloadLength">Length of the optional non-secret payload.</param>
        /// <returns>Decrypted Message</returns>
        public static string DecryptWithKey(string encryptedMessage, string key, int keySize, int nonceSize, int macSize, int nonSecretPayloadLength = 0)
        {
            if (string.IsNullOrEmpty(encryptedMessage))
            {
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
            }

            var decodedKey = Convert.FromBase64String(key);

            var cipherText = Convert.FromBase64String(encryptedMessage);

            var plaintext = DecryptWithKey(cipherText, decodedKey, keySize, nonceSize, macSize, nonSecretPayloadLength);

            return Encoding.UTF8.GetString(plaintext);
        }

        /// <summary>
        /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
        /// </summary>
        /// <param name="messageToEncrypt">The string to be encrypted.</param>
        /// <param name="key">The base 64 encoded 256 bit key.</param>
        /// <param name="nonSecretPayload">Optional non-secret payload.</param>
        /// <returns>
        /// Encrypted Message
        /// </returns>
        /// <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
        /// <remarks>
        /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
        /// </remarks>
        public static string EncryptWithKey(string messageToEncrypt, string key, int keySize, int nonceSize, int macSize, SecureRandom random, byte[] nonSecretPayload = null)
        {
            if (string.IsNullOrEmpty(messageToEncrypt))
            {
                throw new ArgumentException("Secret Message Required!", "messageToEncrypt");
            }

            var decodedKey = Convert.FromBase64String(key);

            var plainText = Encoding.UTF8.GetBytes(messageToEncrypt);
            var cipherText = EncryptWithKey(plainText, decodedKey, keySize, nonceSize, macSize, random, nonSecretPayload);
            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Helper that generates a random new key on each call.
        /// </summary>
        /// <returns>Base 64 encoded string</returns>
        public static string NewKey(int keySize, SecureRandom random)
        {
            var key = new byte[keySize / 8];
            random.NextBytes(key);
            return Convert.ToBase64String(key);
        }

        #endregion

        #region Methods

        private static byte[] DecryptWithKey(byte[] encryptedMessage, byte[] key, int keySize, int nonceSize, int macSize, int nonSecretPayloadLength = 0)
        {
            //User Error Checks
            CheckKey(key, keySize);

            if (encryptedMessage == null || encryptedMessage.Length == 0)
            {
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
            }

            using (var cipherStream = new MemoryStream(encryptedMessage))
            using (var cipherReader = new BinaryReader(cipherStream))
            {
                //Grab Payload
                var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);

                //Grab Nonce
                var nonce = cipherReader.ReadBytes(nonceSize / 8);

                var cipher = new GcmBlockCipher(new AesEngine());
                var parameters = new AeadParameters(new KeyParameter(key), macSize, nonce, nonSecretPayload);
                cipher.Init(false, parameters);

                //Decrypt Cipher Text
                var cipherText = cipherReader.ReadBytes(encryptedMessage.Length - nonSecretPayloadLength - nonce.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

                var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                cipher.DoFinal(plainText, len);

                return plainText;
            }
        }

        private static byte[] EncryptWithKey(byte[] messageToEncrypt, byte[] key, int keySize, int nonceSize, int macSize, SecureRandom random, byte[] nonSecretPayload = null)
        {
            //User Error Checks
            CheckKey(key, keySize);

            //Non-secret Payload Optional
            nonSecretPayload = nonSecretPayload ?? new byte[] { };

            //Using random nonce large enough not to repeat
            var nonce = new byte[nonceSize / 8];
            random.NextBytes(nonce, 0, nonce.Length);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), macSize, nonce, nonSecretPayload);
            cipher.Init(true, parameters);

            //Generate Cipher Text With Auth Tag
            var cipherText = new byte[cipher.GetOutputSize(messageToEncrypt.Length)];
            var len = cipher.ProcessBytes(messageToEncrypt, 0, messageToEncrypt.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            //Assemble Message
            using (var combinedStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(combinedStream))
                {
                    //Prepend Authenticated Payload
                    binaryWriter.Write(nonSecretPayload);
                    //Prepend Nonce
                    binaryWriter.Write(nonce);
                    //Write Cipher Text
                    binaryWriter.Write(cipherText);
                }
                return combinedStream.ToArray();
            }
        }

        private static void CheckKey(byte[] key, int keySize)
        {
            if (key == null || key.Length != keySize / 8)
            {
                throw new ArgumentException(String.Format("Key needs to be {0} bit! actual:{1}", keySize, key != null ? key.Length * 8 : 0), "key");
            }
        }

        #endregion

    }

    public class Cryptor
    {
        #region Constants and Fields Rijndael

        private const string PassPhrase = "Error occured"; // can be any string
        private const string SaltValue = "exception"; // can be any string
        private const int PasswordIterations = 2; // can be any number
        private const string InitVector = "Dfdfgnds2445ZldE"; // must be 16 bytes
        private const int KeySize = 256; // can be 128 or 256

        #endregion

        #region Constants and Fields AES GCM

        private const string _aesGCMKey = "AL5iDOuEyZM50KrLOzz/9fYUUKlyiPZR2UWi5c+ZLmd=";
        private const int _keySize = 256;
        private const int _macSize = 128;
        private const int _nonceSize = 128;

        private static SecureRandom _random
        {
            get
            {
                return new SecureRandom();
            }
        }

        #endregion

        #region Encrypt/Decrypt Rijndael

        public static string Encrypt(string enryptedString)
        {
            string decryptedString = Rijndael.Encrypt(enryptedString, PassPhrase, SaltValue, PasswordIterations, InitVector, KeySize);
            return (decryptedString);
        }

        public static string Encrypt(string enryptedString, string passPhrase)
        {
            string decryptedString = Rijndael.Encrypt(enryptedString, passPhrase, SaltValue, PasswordIterations, InitVector, KeySize);
            return (decryptedString);
        }

        public static string Decrypt(string encryptedString)
        {
            string decryptedString = Rijndael.Decrypt(encryptedString, PassPhrase, SaltValue, PasswordIterations, InitVector, KeySize);
            return (decryptedString);
        }

        public static string Decrypt(string encryptedString, string passPhrase)
        {
            string decryptedString = Rijndael.Decrypt(encryptedString, passPhrase, SaltValue, PasswordIterations, InitVector, KeySize);
            return (decryptedString);
        }

        #endregion

        #region Encrypt/Decrypt AES GCM

        internal static string EncryptAesGCM(string messageToEncrypt)
        {
            var messageEncrypted = string.Empty;
            try
            {
                messageEncrypted = AesGCMBouncyCastle.EncryptWithKey(messageToEncrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, _random, null);
            }
            catch (Exception ex)
            {
                Exception exFail = new Exception("Encryption failed", ex);
                throw exFail;
            }
            return (messageEncrypted);
        }

        internal static string EncryptAesGCM(string messageToEncrypt, string nonSecretPayload)
        {
            var messageEncrypted = string.Empty;
            try
            {
                byte[] arrNonSecretPayload = Encoding.UTF8.GetBytes(nonSecretPayload);
                messageEncrypted = AesGCMBouncyCastle.EncryptWithKey(messageToEncrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, _random, arrNonSecretPayload);
            }
            catch (Exception ex)
            {
                Exception exFail = new Exception("Encryption failed", ex);
                throw exFail;
            }
            return (messageEncrypted);
        }

        internal static string DecryptAesGCM(string messageToDecrypt)
        {
            if (String.IsNullOrEmpty(messageToDecrypt))
            {
                return null;
            }

            var messageDecrypted = string.Empty;
            try
            {
                messageDecrypted = AesGCMBouncyCastle.DecryptWithKey(messageToDecrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, 0);
            }
            catch (Exception ex)
            {
                Exception exFail = new Exception("Decryption failed", ex);
                throw exFail;
            }
            return (messageDecrypted);
        }

        internal static string DecryptAesGCM(string messageToDecrypt, string nonSecretPayload)
        {
            var messageDecrypted = string.Empty;
            try
            {
                var nonSecretPayloadLen = string.IsNullOrEmpty(nonSecretPayload) ? 0 : nonSecretPayload.Length;
                messageDecrypted = AesGCMBouncyCastle.DecryptWithKey(messageToDecrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, nonSecretPayloadLen);
            }
            catch (Exception ex)
            {
                Exception exFail = new Exception("Decryption failed", ex);
                throw exFail;
            }
            return (messageDecrypted);
        }

        #endregion

        #region Encrypt/Decrypt Based on Algorithm

        public static string Encrypt(string messageToEncrypt, CryptorAlgorithm alg)
        {
            return (alg == CryptorAlgorithm.AesGCM ? EncryptAesGCM(messageToEncrypt) : Encrypt(messageToEncrypt));
        }

        public static string Encrypt(string messageToEncrypt, string passPhrase, CryptorAlgorithm alg)
        {
            return (alg == CryptorAlgorithm.AesGCM ? EncryptAesGCM(messageToEncrypt, passPhrase) : Encrypt(messageToEncrypt, passPhrase));
        }

        public static string Decrypt(string messageToDecrypt, CryptorAlgorithm alg)
        {
            return (alg == CryptorAlgorithm.AesGCM ? DecryptAesGCM(messageToDecrypt) : Decrypt(messageToDecrypt));
        }

        public static string Decrypt(string messageToDecrypt, string passPhrase, CryptorAlgorithm alg)
        {
            return (alg == CryptorAlgorithm.AesGCM ? DecryptAesGCM(messageToDecrypt, passPhrase) : Decrypt(messageToDecrypt, passPhrase));
        }

        #endregion

    }


}
