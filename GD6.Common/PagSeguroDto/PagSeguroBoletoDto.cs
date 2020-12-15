using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class PagSeguroBoletoDto
    {
        public PagSeguroBoletoDto()
        {
            NumberOfPayments = 1;
            Periodicity = "monthly";
            Instructions = "Mensalidade Online Clínica";
        }

        public string Reference { get; set; }
        public DateTime FirstDueDate { get; set; }
        public int NumberOfPayments { get; set; }
        public string Periodicity { get; set; }
        public decimal Amount { get; set; }
        public string Instructions { get; set; }
        public string Description { get; set; }
        public PagSeguroCustomerDto Customer { get; set; }
        public string NotificationURL { get; set; }

    }

    public class PagSeguroCustomerDto
    {
        public PagSeguroCustomerDocument Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public PagSeguroCustomerPhone Phone { get; set; }
    }

    public class PagSeguroCustomerDocument
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public PagSeguroCustomerDocumentType Type { get; set; }
        public string Value { get; set; }
    }

    public class PagSeguroCustomerPhone
    {
        public int AreaCode { get; set; }
        public int Number { get; set; }
    }

    public enum PagSeguroCustomerDocumentType
    {
        CPF,
        CNPJ
    }
}