using System;

namespace GD6.Common
{
    [Serializable]
    public class ErroException : Exception
    {
        public string Detail { get; set; }
        public int? Code { get; set; }
        public bool SaveErro { get; set; } = true;

        public ErroException(string message) : this(message, string.Empty) { }

        public ErroException(string message, bool saveErro) : this(message)
        {
            this.SaveErro = saveErro;
        }

        public ErroException(string message, string detail) : this(message, detail, null) { }

        public ErroException(string message, string detail, int? code) : base(message)
        {
            Detail = detail;
            Code = code;
        }
    }
}
