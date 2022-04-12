using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{

    public class PagSeguroXmlTransactionSearchResultOne
    {
        public TransactionSearchResultOne TransactionSearchResult { get; set; }
    }

    public class TransactionSearchResultOne
    {
        public TransactionSearchResultTransactionsOne Transactions { get; set; }
    }

    public class TransactionSearchResultTransactionsOne
    {
        public PagSeguroTransactionDto Transaction { get; set; }
    }




    public class PagSeguroXmlTransactionSearchResult
    {
        public TransactionSearchResult TransactionSearchResult { get; set; }
    }

    public class TransactionSearchResult
    {
        public TransactionSearchResultTransactions Transactions { get; set; }
    }

    public class TransactionSearchResultTransactions
    {
        public IEnumerable<PagSeguroTransactionDto> Transaction { get; set; }
    }

    public class XmlTransaction
    {
        public PagSeguroTransactionDto Transaction { get; set; }
    }

    public enum PagSeguroTransactionStatus
    {
        AguardandoPagamento = 1,
        EmAnalise = 2,
        Paga = 3,
        Disponivel = 4,
        EmDisputa = 5,
        Devolvida = 6,
        Cancelada = 7,
        Debitado = 8,
        RetencaoTemporaria = 9,
        Erro = 10
    }


    public class PagSeguroTransactionDto
    {

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Transaction code
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// You can use the reference code to store an identifier so you can 
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference
        {
            get;
            set;
        }

        /// <summary>
        /// Transaction type
        /// </summary>
        public int TransactionType
        {
            get;
            set;
        }

        /// <summary>
        /// Transaction status
        /// </summary>
        public int TransactionStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Payment method
        /// </summary>
        public PaymentMethod PaymentMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Payment link
        /// </summary>
        public string PaymentLink
        {
            get;
            set;
        }

        /// <summary>
        /// Gross amount of the transaction
        /// </summary>
        public decimal GrossAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Discount amount
        /// </summary>
        public decimal DiscountAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Fee amount
        /// </summary>
        public decimal FeeAmount
        {
            get
            {
                return (CreditorFees?.IntermediationFeeAmount + CreditorFees?.IntermediationRateAmount) ?? 0;
            }
        }

        public PagSeguroTransactionDtoCreditorFees CreditorFees { get; set; }

        /// <summary>
        /// Net amount
        /// </summary>
        public decimal NetAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Extra amount
        /// </summary>
        public decimal ExtraAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Last event date
        /// </summary>
        public DateTime LastEventDate
        {
            get;
            set;
        }

        /// <summary>
        /// Name
        /// </summary>
        /// <remarks>
        /// Name of PreApproval
        /// </remarks>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Tracker
        /// </summary>
        /// <remarks>
        /// Status of PreApproval
        /// </remarks>
        public string Tracker
        {
            get;
            set;
        }

        /// <summary>
        /// Status
        /// </summary>
        /// <remarks>
        /// PreApproval Status
        /// </remarks>
        public PagSeguroTransactionStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// charge
        /// </summary>
        /// <remarks>
        /// Manual or Auto
        /// </remarks>
        public string Charge
        {
            get;
            set;
        }


    }

    public class PagSeguroTransactionDtoCreditorFees
    {
        public decimal IntermediationRateAmount { get; set; }
        public decimal IntermediationFeeAmount { get; set; }
    }

    /// <summary>
    /// Payment method
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Payment method code
        /// </summary>
        public int PaymentMethodCode
        {
            get;
            set;
        }

        /// <summary>
        /// Payment method type
        /// </summary>
        public int PaymentMethodType
        {
            get;
            set;
        }
    }
}
