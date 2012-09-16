// A tiny RFC 6238 TOTP generator — the Google Authenticator algorithm.
// Compile: csc Totp.cs   Run: Totp.exe JBSWY3DPEHPK3PXP
//
// Shared secret is Base32. Code = truncated HMAC-SHA1 over the 30s time step.
using System;
using System.Security.Cryptography;

class Totp
{
    static long TimeStep(DateTime utc, int stepSeconds = 30)
    {
        var unix = (long)(utc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        return unix / stepSeconds;
    }

    static string Generate(byte[] key, long counter, int digits = 6)
    {
        byte[] msg = BitConverter.GetBytes(counter);
        if (BitConverter.IsLittleEndian) Array.Reverse(msg);
        using (var hmac = new HMACSHA1(key))
        {
            byte[] hash = hmac.ComputeHash(msg);
            int offset = hash[hash.Length - 1] & 0x0F;
            int binary = ((hash[offset] & 0x7F) << 24)
                       | ((hash[offset + 1] & 0xFF) << 16)
                       | ((hash[offset + 2] & 0xFF) << 8)
                       | (hash[offset + 3] & 0xFF);
            int otp = binary % (int)Math.Pow(10, digits);
            return otp.ToString().PadLeft(digits, '0');
        }
    }

    static byte[] FromBase32(string s)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        s = s.TrimEnd('=').ToUpperInvariant();
        int bits = 0, value = 0;
        var output = new System.Collections.Generic.List<byte>();
        foreach (char c in s)
        {
            value = (value << 5) | alphabet.IndexOf(c);
            bits += 5;
            if (bits >= 8) { output.Add((byte)((value >> (bits - 8)) & 0xFF)); bits -= 8; }
        }
        return output.ToArray();
    }

    static void Main(string[] args)
    {
        string secret = args.Length > 0 ? args[0] : "JBSWY3DPEHPK3PXP";
        byte[] key = FromBase32(secret);
        long step = TimeStep(DateTime.UtcNow);
        // Print the current code and the adjacent windows we'd also accept (clock drift).
        Console.WriteLine("prev: " + Generate(key, step - 1));
        Console.WriteLine("now : " + Generate(key, step));
        Console.WriteLine("next: " + Generate(key, step + 1));
    }
}
