using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zohobooks.model;

namespace zohobooks.parser;

/// <summary>
/// Used to define the parser object of InvoiceApi.
/// </summary>
public class InvoiceParser
{
    internal static async Task<string> GetMessageAsync(HttpResponseMessage responce)
    {
        string message = "";
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(await responce.Content.ReadAsStringAsync());
        if (jsonObj.ContainsKey("message"))
            message = jsonObj["message"].ToString();
        return message;
    }

    internal static async Task<InvoicesList> GetInvoiceListAsync(HttpResponseMessage responce)
    {
        var invoiceList = new InvoicesList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(await responce.Content.ReadAsStringAsync());
        if (jsonObj.ContainsKey("invoices"))
        {
            var invoicesArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["invoices"].ToString());
            foreach (var invoiceObj in invoicesArray)
            {
                var invoice = new Invoice();
                invoice = await ZohoSerializer.DeserializeAsync<Invoice>(invoiceObj.ToString());
                invoiceList.Add(invoice);
            }
        }
        if (jsonObj.ContainsKey("page_context"))
        {
            var pageContext = new PageContext();
            pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
            invoiceList.page_context = pageContext;
        }
        return invoiceList;
    }

    internal static async Task<Invoice> GetInvoiceAsync(HttpResponseMessage responce)
    {
        var invoice = new Invoice();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(await responce.Content.ReadAsStringAsync());
        if (jsonObj.ContainsKey("invoice"))
        {
            invoice = await ZohoSerializer.DeserializeAsync<Invoice>(jsonObj["invoice"].ToString());
        }
        return invoice;
    }

    internal static async Task<PaymentList> GetPaymentsListAsync(HttpResponseMessage responce)
    {
        var paymentList = new PaymentList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("payments"))
        {
            var paymentsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["payments"].ToString());
            foreach (var paymentObj in paymentsArray)
            {
                var payment = new Payment();
                payment = await ZohoSerializer.DeserializeAsync<Payment>(paymentObj.ToString());
                paymentList.Add(payment);
            }
        }
        return paymentList;
    }

    internal static async Task<CreditNoteList> GetCreditsAsync(HttpResponseMessage responce)
    {
        var creditList = new CreditNoteList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("credits"))
        {
            var paymentsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["credits"].ToString());
            foreach (var paymentObj in paymentsArray)
            {
                var credit = new CreditNote();
                credit = await ZohoSerializer.DeserializeAsync<CreditNote>(paymentObj.ToString());
                creditList.Add(credit);
            }
        }
        return creditList;
    }

    internal static async Task<UseCredits> GetUseCreditsAsync(HttpResponseMessage response)
    {
        var useCredits = new UseCredits();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(response.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("use_credits"))
        {
            useCredits = await ZohoSerializer.DeserializeAsync<UseCredits>(jsonObj["use_credits"].ToString());
        }
        return useCredits;
    }
}