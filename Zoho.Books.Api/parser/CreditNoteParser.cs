using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zohobooks.model;

namespace zohobooks.parser
{
    /// <summary>
    /// Used to define the parser object of CreditNoteApi.
    /// </summary>
    class CreditNoteParser
    {
        internal static async Task<CreditNoteList> GetCreditnoteListAsync(HttpResponseMessage responce)
        {
            var creditNoteList = new CreditNoteList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("creditnotes"))
            {
                var creditnotesArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["creditnotes"].ToString());
                foreach (var creditnoteObj in creditnotesArray)
                {
                    var creditnote = new CreditNote();
                    creditnote = await ZohoSerializer.DeserializeAsync<CreditNote>(creditnoteObj.ToString());
                    creditNoteList.Add(creditnote);
                }
            }
            if (jsonObj.ContainsKey("page_context"))
            {
                var pageContext = new PageContext();
                pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
                creditNoteList.page_context = pageContext;
            }
            return creditNoteList;
        }

        internal static async Task<CreditNote> GetCreditnoteAsync(HttpResponseMessage responce)
        {
            var creditnote = new CreditNote();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("creditnote"))
            {
                creditnote = await ZohoSerializer.DeserializeAsync<CreditNote>(jsonObj["creditnote"].ToString());
            }
            return creditnote;
        }

        internal static async Task<string> GetMessageAsync(HttpResponseMessage responce)
        {
            string message = "";
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("message"))
                message = jsonObj["message"].ToString();
            return message;
        }

        internal static async Task<EmailHistoryList> GetEmailHistoryListAsync(HttpResponseMessage responce)
        {
            var emailHistoryList = new EmailHistoryList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("email_history"))
            {
                var emailHistoryArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["email_history"].ToString());
                foreach (var emailHistoryObj in emailHistoryArray)
                {
                    var emailHistory = new EmailHistory();
                    emailHistory = await ZohoSerializer.DeserializeAsync<EmailHistory>(emailHistoryObj.ToString());
                    emailHistoryList.Add(emailHistory);
                }
            }
            return emailHistoryList;
        }

        internal static async Task<TemplateList> GetTemplateListAsync(HttpResponseMessage responce)
        {
            var templateList = new TemplateList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("templates"))
            {
                var templatesArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["templates"].ToString());
                foreach (var templateObj in templatesArray)
                {
                    var template = new Template();
                    template = await ZohoSerializer.DeserializeAsync<Template>(templateObj.ToString());
                    templateList.Add(template);
                }
            }
            return templateList;
        }

        internal static async Task<CreditedInvoiceList> GetCreditedInvoiceListAsync(HttpResponseMessage responce)
        {
            var creditedInvoceList = new CreditedInvoiceList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("invoices_credited"))
            {
                var creditedInvoicesArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["invoices_credited"].ToString());
                foreach (var invoiceObj in creditedInvoicesArray)
                {
                    var creditedInvoice = new CreditedInvoice();
                    creditedInvoice = await ZohoSerializer.DeserializeAsync<CreditedInvoice>(invoiceObj.ToString());
                    creditedInvoceList.Add(creditedInvoice);
                }
            }
            return creditedInvoceList;
        }

        internal static async Task<CreditedInvoiceList> GetCreditsAppliedInvoicesAsync(HttpResponseMessage responce)
        {
            var creditedInvoiceList = new CreditedInvoiceList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("apply_to_invoices"))
            {
                var applyToInvoiceObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(jsonObj["apply_to_invoices"].ToString());
                if (applyToInvoiceObj.ContainsKey("invoices"))
                {
                    var invoicesArray = await ZohoSerializer.DeserializeAsync<List<object>>(applyToInvoiceObj["invoices"].ToString());
                    foreach (var invoiceObj in invoicesArray)
                    {
                        var creditedinvoice = new CreditedInvoice();
                        creditedinvoice = await ZohoSerializer.DeserializeAsync<CreditedInvoice>(invoiceObj.ToString());
                        creditedInvoiceList.Add(creditedinvoice);
                    }
                }
            }
            return creditedInvoiceList;
        }

        internal static async Task<CreditNoteRefundList> GetCreditnoteRefundListAsync(HttpResponseMessage responce)
        {
            var creditnoterefundList = new CreditNoteRefundList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("creditnote_refunds"))
            {
                var refundsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["creditnote_refunds"].ToString());
                foreach (var refundObj in refundsArray)
                {
                    var creditnote = new CreditNote();
                    creditnote = await ZohoSerializer.DeserializeAsync<CreditNote>(refundObj.ToString());
                    creditnoterefundList.Add(creditnote);
                }
            }
            if (jsonObj.ContainsKey("page_context"))
            {
                var pageContext = new PageContext();
                pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
                creditnoterefundList.page_context = pageContext;
            }
            return creditnoterefundList;
        }

        internal static async Task<CreditNote> GetCreditnoteRefundAsync(HttpResponseMessage responce)
        {
            var creditnote = new CreditNote();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("creditnote_refund"))
            {
                creditnote = await ZohoSerializer.DeserializeAsync<CreditNote>(jsonObj["creditnote_refund"].ToString());
            }
            return creditnote;
        }

        internal static async Task<CommentList> GetCommentListAsync(HttpResponseMessage responce)
        {
            var commentList = new CommentList();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("comments"))
            {
                var commentsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["comments"].ToString());
                foreach (var commentObj in commentsArray)
                {
                    var comment = new Comment();
                    comment = await ZohoSerializer.DeserializeAsync<Comment>(commentObj.ToString());
                    commentList.Add(comment);
                }
            }
            if (jsonObj.ContainsKey("page_context"))
            {
                var pageContext = new PageContext();
                pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
                commentList.page_context = pageContext;
            }
            return commentList;
        }

        internal static async Task<Comment> GetCommentAsync(HttpResponseMessage responce)
        {
            var comment = new Comment();
            var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
            if (jsonObj.ContainsKey("comment"))
            {
                comment = await ZohoSerializer.DeserializeAsync<Comment>(jsonObj["comment"].ToString());
            }
            return comment;
        }
    }
}
