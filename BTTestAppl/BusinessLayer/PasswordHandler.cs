
using BTTestAppl.Models;
using System;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;

namespace BTTestAppl.BusinessLayer
{
    public class PasswordHandler
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private ObjectCache _cache = MemoryCache.Default;
        private static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rngCsp.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(valid[(int)(num % (uint)valid.Length)]);
            }

            return res.ToString();
        }
        public Password GetOneTimePassword(string memberId)
        {
            Password val = (Password)_cache.Get("memberId" + memberId);
            if (val != null)
                return val;
            else return null;
        }

        public Password GenerateOneTimePassword(Member member, DateTime expirationTime) {
            var oneTimePassword = RandomString(15);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);

            Password password = new Password { ExpirationDate = expirationTime, WasUsed = false, Value = oneTimePassword };

            if(GetOneTimePassword(member.MemberId) != null)
                _cache.Set("memberId" + member.MemberId, password, policy);
            else
                _cache.Add("memberId" + member.MemberId, password, policy);
            return password;
        }

        public void SetPasswordUsed(string memberId) {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);

            Password password = GetOneTimePassword(memberId);
            password.WasUsed = true;
            _cache.Set("memberId" + memberId, password, policy);
        }
    }
}
