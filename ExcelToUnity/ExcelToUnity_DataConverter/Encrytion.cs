using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Encryption
{
    private byte[] mEncrypKey;

    public Encryption(byte[] pEncryptKey)
    {
        this.mEncrypKey = pEncryptKey;
    }

    public string Encrypt(string pValue)
    {
        if (string.IsNullOrEmpty(pValue))
            return "";

        var plainTextBytes = XOR(System.Text.Encoding.UTF8.GetBytes(pValue), mEncrypKey);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public string Decrypt(string pValue)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(pValue);
        return Encoding.UTF8.GetString(XOR(base64EncodedBytes, mEncrypKey));
    }

    private byte[] XOR(byte[] input, byte[] key)
    {
        if (key == null || key.Length == 0)
        {
            return input;
        }
        byte[] output = new byte[input.Length];
        int kpos = 0;
        int kup = 0;
        int kk = 0;
        for (int pos = 0, n = input.Length; pos < n; ++pos)
        {
            output[pos] = (byte)(input[pos] ^ key[kpos] ^ kk);
            ++kpos;
            if (kpos >= key.Length)
            {
                kpos = 0;
                kup = (kup + 1) % key.Length;
                kk = key[kup];
            }
        }
        return output;
    }
}