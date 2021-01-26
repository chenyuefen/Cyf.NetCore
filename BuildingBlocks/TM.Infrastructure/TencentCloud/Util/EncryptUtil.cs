/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：BufferedCipherTransform
// 文件功能描述:利用CryptoStream进行加密
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日18:16:38
0// 
//----------------------------------------------------------------*/

using Org.BouncyCastle.Crypto;
using System.IO;
using System.Security.Cryptography;

public class EncryptUtil
{

    public static byte[] Encrypt(byte[] message, BufferedAsymmetricBlockCipher cipher)
    {
        using (var buffer = new MemoryStream())
        {
            using (var transform = new BufferedCipherTransform(cipher))
            using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
            using (var messageStream = new MemoryStream(message))
                messageStream.CopyTo(stream);
            return buffer.ToArray();
        }
    }
}

