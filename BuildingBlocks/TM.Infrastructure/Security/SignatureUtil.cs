using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TM.Infrastructure.Utils;

namespace TM.Infrastructure.Security
{
    public class SignatureUtil
    {
        private static RSAPKCS1SignatureFormatter rsaPKCS1SignatureFormatter;
        private static RSAPKCS1SignatureDeformatter rsaPKCS1SignatureDeformatter;

        public static void InitSign(string keystoreFile, string keystorePass)
        {
            try
            {
                RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();

                rsaPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsaCryptoServiceProvider);

                X509Certificate2 x509Certificate2 = new X509Certificate2(keystoreFile, keystorePass, X509KeyStorageFlags.MachineKeySet);

                rsaPKCS1SignatureFormatter.SetKey(x509Certificate2.PrivateKey);

                rsaPKCS1SignatureFormatter.SetHashAlgorithm("SHA1");
            }
            catch (CryptographicException e)
            {
                throw e;
            }
            catch (SystemException e)
            {
                throw e;
            }
        }

        public static void InitVerify(string cert)
        {
            try
            {
                X509Certificate2 x509Certificate2 = new X509Certificate2(cert);

                string publicKeyString = x509Certificate2.PublicKey.Key.ToXmlString(false);

                RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();

                rsaCryptoServiceProvider.FromXmlString(publicKeyString);

                rsaPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsaCryptoServiceProvider);

                rsaPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
            }
            catch (CryptographicException e)
            {
                throw e;
            }
            catch (SystemException e)
            {
                throw e;
            }
        }

        public static string Sign(string data)
        {
            try
            {
                byte[] hash = CPCNUtils.Hash(data);

                byte[] signature = rsaPKCS1SignatureFormatter.CreateSignature(hash);

                string sig = CPCNUtils.ConvertBytesToHexString(signature);

                return sig;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
            catch (SystemException e)
            {
                throw e;
            }

        }


        public static bool Verify(string text, byte[] signature)
        {
            try
            {
                byte[] hash = CPCNUtils.Hash(text);

                bool isValid = rsaPKCS1SignatureDeformatter.VerifySignature(hash, signature);

                if (isValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CryptographicException e)
            {
                throw e;
            }
            catch (SystemException e)
            {
                throw e;
            }
        }

        public static bool Verify(string text, string signature)
        {
            return Verify(text, CPCNUtils.hex2bytes(signature));
        }

    }
}
