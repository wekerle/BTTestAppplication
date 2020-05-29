using System;

namespace BTTestAppl.Models
{
    public class Password
    {
        public DateTime ExpirationDate { get; set; }
        public bool WasUsed { get; set; }
        public string Value { get; set; }
    }
}
