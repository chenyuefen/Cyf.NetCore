
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：BufferedCipherTransform
// 文件功能描述： 为利用CryptoStream，对BufferedAsymmetricBlockCipher进行封装，实现ICryptoTransform接口
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日18:16:38
0// 
//----------------------------------------------------------------*/
using Org.BouncyCastle.Crypto;
using System;
using System.Security.Cryptography;

public class BufferedCipherTransform : ICryptoTransform
{
    private readonly BufferedAsymmetricBlockCipher _cipher;

    public int InputBlockSize { get; private set; }
    public int OutputBlockSize { get; private set; }
    public bool CanTransformMultipleBlocks { get { return false; } }
    public bool CanReuseTransform { get { return true; } }

    public BufferedCipherTransform(BufferedAsymmetricBlockCipher cipher)
    {
        _cipher = cipher;
        InputBlockSize = _cipher.GetBlockSize();
        OutputBlockSize = _cipher.GetOutputSize(0);
    }

    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
        var len = _cipher.ProcessBytes(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
        return _cipher.DoFinal(inputBuffer, inputOffset, len, outputBuffer, outputOffset);
    }

    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        return _cipher.DoFinal(inputBuffer, inputOffset, inputCount);
    }

    public void Dispose()
    {
        _cipher.Reset();
        GC.SuppressFinalize(this);
    }
}
