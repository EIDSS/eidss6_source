Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports Org.BouncyCastle.Crypto.Engines
Imports Org.BouncyCastle.Crypto.Modes
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.Security

Public Class Rijndael
    ''' <summary>
    ''' Encrypts specified plaintext using Rijndael symmetric key algorithm
    ''' and returns a base64-encoded result.
    ''' </summary>
    ''' <param name="plainText">
    ''' Plaintext value to be encrypted.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Encrypted value formatted as a base64-encoded string.
    ''' </returns>
    Public Shared Function Encrypt(ByVal plainText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal passwordIterations As Integer, ByVal initVector As String, _
    ByVal keySize As Integer) As String
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        'Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(CInt(keySize / 8))
        Dim symmetricKey As New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()

        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)

        Return cipherText
    End Function

    ''' <summary>
    ''' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    ''' </summary>
    ''' <param name="cipherText">
    ''' Base64-formatted ciphertext value.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The
    ''' derived password will be used to generate the encryption key.
    ''' Passphrase can be any string. In this example we assume that this
    ''' passphrase is an ASCII string.
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the
    ''' first block of plaintext data. For RijndaelManaged class IV must be
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256.
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Decrypted string value.
    ''' </returns>
    ''' <remarks>
    ''' Most of the logic in this function is similar to the Encrypt
    ''' logic. In order for decryption to work, all parameters of this function
    ''' - except cipherText value - must match the corresponding parameters of
    ''' the Encrypt function which was called to generate the
    ''' ciphertext.
    ''' </remarks>
    Public Shared Function Decrypt(ByVal cipherText As String, ByVal passPhrase As String, ByVal saltValue As String, ByVal passwordIterations As Integer, ByVal initVector As String, _
    ByVal keySize As Integer) As String
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        'Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
        Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        Dim keyBytes As Byte() = password.GetBytes(CInt(keySize / 8))
        Dim symmetricKey As New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC

        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
        Dim memoryStream As New MemoryStream(cipherTextBytes)
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        memoryStream.Close()
        cryptoStream.Close()

        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        Return plainText
    End Function
End Class

Public Class AesGCMBouncyCastle
    '' <summary>
    '' Simple Decryption & Authentication (AES-GCM) of a UTF8 Message
    '' </summary>
    '' <param name="encryptedMessage">The encrypted message.</param>
    '' <param name="key">The base 64 encoded 256 bit key.</param>
    '' <param name="nonSecretPayloadLength">Length of the optional non-secret payload.</param>
    '' <returns>Decrypted Message</returns>
    Public Shared Function DecryptWithKey(ByVal encryptedMessage As String, ByVal key As String, ByVal keySize As Integer, ByVal nonceSize As Integer, ByVal macSize As Integer, Optional ByVal nonSecretPayloadLength As Integer = 0) As String
        If String.IsNullOrEmpty(encryptedMessage) Then
            Throw New ArgumentException("Encrypted Message Required!", "encryptedMessage")
        End If

        Dim decodedKey As Byte() = Convert.FromBase64String(key)

        Dim cipherText As Byte() = Convert.FromBase64String(encryptedMessage)

        Dim plaintext As Byte() = DecryptWithKey(cipherText, decodedKey, keySize, nonceSize, macSize, nonSecretPayloadLength)

        Return Encoding.UTF8.GetString(plaintext)
    End Function

    '' <summary>
    '' Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
    '' </summary>
    '' <param name="messageToEncrypt">The string to be encrypted.</param>
    '' <param name="key">The base 64 encoded 256 bit key.</param>
    '' <param name="nonSecretPayload">Optional non-secret payload.</param>
    '' <returns>
    '' Encrypted Message
    '' </returns>
    '' <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
    '' <remarks>
    '' Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
    '' </remarks>
    Public Shared Function EncryptWithKey(ByVal messageToEncrypt As String, ByVal key As String, ByVal keySize As Integer, ByVal nonceSize As Integer, ByVal macSize As Integer, ByVal random As SecureRandom, Optional ByVal nonSecretPayload As Byte() = Nothing) As String
        If String.IsNullOrEmpty(messageToEncrypt) Then
            Throw New ArgumentException("Secret Message Required!", "messageToEncrypt")
        End If

        Dim decodedKey As Byte() = Convert.FromBase64String(key)

        Dim plainText As Byte() = Encoding.UTF8.GetBytes(messageToEncrypt)
        Dim cipherText As Byte() = EncryptWithKey(plainText, decodedKey, keySize, nonceSize, macSize, random, nonSecretPayload)

        Return Convert.ToBase64String(cipherText)
    End Function

    '' <summary>
    '' Helper that generates a random new key on each call.
    '' </summary>
    '' <returns>Base 64 encoded string</returns>
    Public Shared Function NewKey(ByVal keySize As Integer, ByVal random As SecureRandom) As String
        Dim size As Integer = keySize \ 8
        Dim key As Byte() = New Byte(size) {}
        random.NextBytes(key)
        Return Convert.ToBase64String(key)
    End Function

    Private Shared Function DecryptWithKey(ByVal encryptedMessage As Byte(), ByVal key As Byte(), ByVal keySize As Integer, ByVal nonceSize As Integer, ByVal macSize As Integer, Optional ByVal nonSecretPayloadLength As Integer = 0) As Byte()
        'User Error Checks
        CheckKey(key, keySize)

        If encryptedMessage Is Nothing OrElse encryptedMessage.Length = 0 Then
            Throw New ArgumentException("Encrypted Message Required!", "encryptedMessage")
        End If

        Dim cipherStream As New MemoryStream(encryptedMessage)
        Dim cipherReader As New BinaryReader(cipherStream)

        'Grab Payload
        Dim nonSecretPayload As Byte() = cipherReader.ReadBytes(nonSecretPayloadLength)

        'Grab Nonce
        Dim nonce As Byte() = cipherReader.ReadBytes(nonceSize \ 8)

        Dim cipher As New GcmBlockCipher(New AesEngine())
        Dim parameters As New AeadParameters(New KeyParameter(key), macSize, nonce, nonSecretPayload)
        cipher.Init(False, parameters)

        'Decrypt Cipher Text
        Dim cipherText As Byte() = cipherReader.ReadBytes(encryptedMessage.Length - nonSecretPayloadLength - nonce.Length)
        Dim plainText As Byte() = New Byte(cipher.GetOutputSize(cipherText.Length)) {}

        Dim len As Integer = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0)
        cipher.DoFinal(plainText, len)

        cipherReader.Close()
        cipherStream.Close()

        Return plainText
    End Function

    Private Shared Function EncryptWithKey(ByVal messageToEncrypt As Byte(), ByVal key As Byte(), ByVal keySize As Integer, ByVal nonceSize As Integer, ByVal macSize As Integer, ByVal random As SecureRandom, Optional ByVal nonSecretPayload As Byte() = Nothing) As Byte()
        'User Error Checks
        CheckKey(key, keySize)

        'Non-secret Payload Optional
        If nonSecretPayload Is Nothing Then
            nonSecretPayload = New Byte() {}
        End If

        'Using random nonce large enough not to repeat
        Dim nonce As Byte() = New Byte(nonceSize \ 8) {}
        random.NextBytes(nonce, 0, nonce.Length)

        Dim cipher As New GcmBlockCipher(New AesEngine())
        Dim parameters As New AeadParameters(New KeyParameter(key), macSize, nonce, nonSecretPayload)
        cipher.Init(True, parameters)

        'Generate Cipher Text With Auth Tag
        Dim cipherText As Byte() = New Byte(cipher.GetOutputSize(messageToEncrypt.Length)) {}
        Dim len As Integer = cipher.ProcessBytes(messageToEncrypt, 0, messageToEncrypt.Length, cipherText, 0)
        cipher.DoFinal(cipherText, len)

        'Assemble Message
        Dim resMessage As Byte()
        Dim combinedStream As New MemoryStream()
        Dim binaryWriter As New BinaryWriter(combinedStream)

        'Prepend Authenticated Payload
        binaryWriter.Write(nonSecretPayload)
        'Prepend Nonce
        binaryWriter.Write(nonce)
        'Write Cipher Text
        binaryWriter.Write(cipherText)

        resMessage = combinedStream.ToArray()

        binaryWriter.Close()
        combinedStream.Close()

        Return resMessage
    End Function

    Private Shared Sub CheckKey(ByVal key As Byte(), ByVal keySize As Integer)

        If key Is Nothing Then
            Throw New ArgumentException(String.Format("Key needs to be {0} bit! actual:{1}", keySize, 0), "key")
        ElseIf (key.Length <> (keySize \ 8)) Then
            Throw New ArgumentException(String.Format("Key needs to be {0} bit! actual:{1}", keySize, key.Length * 8), "key")
        End If
    End Sub
End Class


Public Class Cryptor


#Region "Constants and Fields Rijndael"
    Shared passPhrase As String = "Error occured"      ' can be any string
    Shared saltValue As String = "exception"           ' can be any string
    Shared passwordIterations As Integer = 2           ' can be any number
    Shared initVector As String = "Dfdfgnds2445ZldE"   ' must be 16 bytes
    Shared keySize As Integer = 256                    ' can be 128 or 256
#End Region

#Region "Constants and Fields AES GCM"
    Shared _aesGCMKey As String = "AL5iDOuEyZM50KrLOzz/9fYUUKlyiPZR2UWi5c+ZLmd="
    Shared _keySize As Integer = 256
    Shared _macSize As Integer = 128
    Shared _nonceSize As Integer = 128

    Private Shared ReadOnly Property _random() As SecureRandom
        Get
            _random = New SecureRandom()
            Exit Property
        End Get
    End Property
#End Region

#Region "Encrypt/Decrypt Rijndael"

    Public Shared Function Encrypt(ByVal enryptedString As String) As String
        Dim decryptedString As String
        decryptedString = Rijndael.Encrypt _
                            ( _
                                enryptedString, _
                                passPhrase, _
                                saltValue, _
                                passwordIterations, _
                                initVector, _
                                keySize _
                            )
        Return (decryptedString)
    End Function
    Public Shared Function Encrypt(ByVal enryptedString As String, ByVal passPhrase As String) As String
        Dim decryptedString As String
        decryptedString = Rijndael.Encrypt _
                            ( _
                                enryptedString, _
                                passPhrase, _
                                saltValue, _
                                passwordIterations, _
                                initVector, _
                                keySize _
                            )
        Return (decryptedString)
    End Function
    Public Shared Function Decrypt(ByVal encryptedString As String) As String
        Dim decryptedString As String
        decryptedString = Rijndael.Decrypt _
                            ( _
                                encryptedString, _
                                passPhrase, _
                                saltValue, _
                                passwordIterations, _
                                initVector, _
                                keySize _
                            )
        Return (decryptedString)
    End Function
    Public Shared Function Decrypt(ByVal encryptedString As String, ByVal passPhrase As String) As String
        Dim decryptedString As String
        decryptedString = Rijndael.Decrypt _
                            ( _
                                encryptedString, _
                                passPhrase, _
                                saltValue, _
                                passwordIterations, _
                                initVector, _
                                keySize _
                            )
        Return (decryptedString)
    End Function

#End Region

#Region "Encrypt/Decrypt AES GCM"

    Private Shared Function EncryptAesGCM(ByVal messageToEncrypt As String) As String
        Dim messageEncrypted As String = String.Empty
        Try
            messageEncrypted = AesGCMBouncyCastle.EncryptWithKey(messageToEncrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, _random, Nothing)
        Catch ex As Exception
            Dim exFail As Exception = New Exception("Encryption failed", ex)
            Throw exFail
        End Try

        Return messageEncrypted
    End Function

    Private Shared Function EncryptAesGCM(ByVal messageToEncrypt As String, ByVal nonSecretPayload As String) As String
        Dim messageEncrypted As String = String.Empty
        Try
            Dim arrNonSecretPayload As Byte() = Encoding.UTF8.GetBytes(nonSecretPayload)
            messageEncrypted = AesGCMBouncyCastle.EncryptWithKey(messageToEncrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, _random, arrNonSecretPayload)
        Catch ex As Exception
            Dim exFail As Exception = New Exception("Encryption failed", ex)
            Throw exFail
        End Try

        Return messageEncrypted
    End Function

    Private Shared Function DecryptAesGCM(ByVal messageToDecrypt As String) As String
        Dim messageDecrypted As String = String.Empty
        Try
            messageDecrypted = AesGCMBouncyCastle.DecryptWithKey(messageToDecrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, 0)
        Catch ex As Exception
            Dim exFail As Exception = New Exception("Decryption failed", ex)
            Throw exFail
        End Try
        Return messageDecrypted
    End Function

    Private Shared Function DecryptAesGCM(ByVal messageToDecrypt As String, ByVal nonSecretPayload As String) As String
        Dim messageDecrypted As String = String.Empty
        Try
            Dim nonSecretPayloadLen As Integer
            If String.IsNullOrEmpty(nonSecretPayload) Then
                nonSecretPayloadLen = 0
            Else
                nonSecretPayloadLen = nonSecretPayload.Length
            End If
            messageDecrypted = AesGCMBouncyCastle.DecryptWithKey(messageToDecrypt, _aesGCMKey, _keySize, _nonceSize, _macSize, nonSecretPayloadLen)
        Catch ex As Exception
            Dim exFail As Exception = New Exception("Decryption failed", ex)
            Throw exFail
        End Try
        Return messageDecrypted
    End Function

#End Region


#Region "Encrypt/Decrypt Based on Algorithm"

    Public Shared Function Encrypt(ByVal messageToEncrypt As String, ByVal alg As CryptorAlgorithm) As String
        If alg = CryptorAlgorithm.AesGCM Then
            Return EncryptAesGCM(messageToEncrypt)
        Else
            Return Encrypt(messageToEncrypt)
        End If
    End Function

    Public Shared Function Encrypt(ByVal messageToEncrypt As String, ByVal passPhrase As String, ByVal alg As CryptorAlgorithm) As String
        If alg = CryptorAlgorithm.AesGCM Then
            Return EncryptAesGCM(messageToEncrypt, passPhrase)
        Else
            Return Encrypt(messageToEncrypt, passPhrase)
        End If
    End Function

    Public Shared Function Decrypt(ByVal messageToDecrypt As String, ByVal alg As CryptorAlgorithm) As String
        If alg = CryptorAlgorithm.AesGCM Then
            Return DecryptAesGCM(messageToDecrypt)
        Else
            Return Decrypt(messageToDecrypt)
        End If
    End Function

    Public Shared Function Decrypt(ByVal messageToDecrypt As String, ByVal passPhrase As String, ByVal alg As CryptorAlgorithm) As String
        If alg = CryptorAlgorithm.AesGCM Then
            Return DecryptAesGCM(messageToDecrypt, passPhrase)
        Else
            Return Decrypt(messageToDecrypt, passPhrase)
        End If
    End Function

#End Region

End Class

