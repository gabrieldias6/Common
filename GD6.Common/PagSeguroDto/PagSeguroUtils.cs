using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml;

namespace GD6.Common
{
    public static class PagSeguroUtils
    {
        public static PagSeguroTransactionDto GetTransaction(string content)
        {
            var xmlTransaction = GetXmlObject<XmlTransaction>(content);
            return xmlTransaction?.Transaction;
        }

        public static IEnumerable<PagSeguroTransactionDto> GetTransactionSearch(string content)
        {
            var xmlTransaction = GetXmlObject<PagSeguroXmlTransactionSearchResult>(content);
            return xmlTransaction?.TransactionSearchResult.Transactions.Transaction;
        }

        public static PagSeguroTransactionDto GetTransactionSearchOne(string content)
        {
            var xmlTransaction = GetXmlObject<PagSeguroXmlTransactionSearchResultOne>(content);
            return xmlTransaction?.TransactionSearchResult.Transactions.Transaction;
        }

        private static T GetXmlObject<T>(string content)
        {
            // Cria um XML com a Resposta
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            // Transforma a resposta em Json
            var json = JsonConvert.SerializeXmlNode(doc);
            // Joga pro Objeto
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
