using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class PagSeguroBoletoReturn
    {
        public PagSeguroBoletoReturn()
        {
            Sucesso = true;
        }

        public bool Sucesso { get; set; }
        public string ErroMensagem { get; set; }
        public IEnumerable<PagSeguroBoletoReturnDto> Boletos { get; set; }
    }

    public class PagSeguroBoletoReturnDto
    {
        public string Code { get; set; }
        public string PaymentLink { get; set; }
        public string BarCode { get; set; }
        public DateTime DueDate { get; set; }
    }
}
