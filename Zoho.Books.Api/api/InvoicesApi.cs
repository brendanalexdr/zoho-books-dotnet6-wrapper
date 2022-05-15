using zohobooks.model;
using zohobooks.parser;
using zohobooks.util;

namespace zohobooks.api;

public class InvoicesApi : Api
{

    static string baseAddress = baseurl + "/invoices";

    public InvoicesApi(string auth_token, string organization_Id)
        : base(auth_token, organization_Id)
    {

    }

    public async Task<InvoicesList> GetInvoicesAsync(Dictionary<object, object> parameters)
    {
        string url = baseAddress;
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters(parameters));
        return await InvoiceParser.GetInvoiceListAsync(responce);
    }

    public async Task<Invoice> GetAsync(string invoice_id, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id;
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters(parameters));
        return await InvoiceParser.GetInvoiceAsync(responce);
    }

    public async Task<Invoice> CreateAsync(Invoice new_invoice_info, Dictionary<object, object> parameters)
    {
        string url = baseAddress;
        var json = await ZohoSerializer.SerializeAsync<Invoice>(new_invoice_info);
        parameters.Add("JSONString", json);
        var response = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters));
        var responceContent = await response.Content.ReadAsStringAsync();
        return await InvoiceParser.GetInvoiceAsync(response);
    }

    public async Task<Invoice> UpdateAsync(string invoice_id, Invoice update_info, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id;
        var json = await ZohoSerializer.SerializeAsync<Invoice>(update_info);
        parameters.Add("JSONString", json);
        var response = await ZohoHttpClient.PutAsync(url, getQueryParameters(parameters));
        var responceContent = await response.Content.ReadAsStringAsync();
        return await InvoiceParser.GetInvoiceAsync(response);
    }

    public async Task<string> DeleteAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id;
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }


    public async Task<string> MarkAsSentAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/status/sent";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> MarkAsVoidAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/status/void";
        var response = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(response);
    }

    public async Task<string> MarkAsDraftAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/status/draft";
        var response = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(response);
    }

    public async Task<string> SendEmailAsync(string invoice_id, EmailNotification email_details, string[] attachment_paths, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id + "/email";
        var json = await ZohoSerializer.SerializeAsync<EmailNotification>(email_details);
        var jsonString = new Dictionary<object, object>();
        jsonString.Add("JSONString", json);
        var files = new KeyValuePair<string, string[]>("attachments", attachment_paths);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters), jsonString, files);
        string responceContent = await responce.Content.ReadAsStringAsync();
        return await InvoiceParser.GetMessageAsync(responce); 
    }

    public async Task<string> EmailInvoicesAsync(Contacts contacts, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/email";
        var json = await ZohoSerializer.SerializeAsync<Contacts>(contacts);
        parameters.Add("JSONString", json);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters));
        string responceContent = await responce.Content.ReadAsStringAsync();
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<Email> GetEmailContentAsync(string invoice_id, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id + "/email";
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters(parameters));
        return await ContactParser.GetEmailContentAsync(responce);
    }

    public async Task<string> RemindCustomerAsync(string invoice_id, EmailNotification notify_details, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id + "/paymentreminder";
        var json = await ZohoSerializer.SerializeAsync<EmailNotification>(notify_details);
        parameters.Add("JSONString", json);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters));
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> BulkInvoiceReminder(Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/paymentreminder";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters));
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<Email> GetPaymentReminderAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/paymentreminder";
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters());
        return await ContactParser.GetEmailContentAsync(responce);
    }


    public async Task<string> BulkExportAsync(Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/pdf";
        await ZohoHttpClient.GetFileAsync(url, getQueryParameters(parameters));
        return "the selected invoices are exported ";

    }

    public async Task<string> BulkPrintAsync(Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/print";
        await ZohoHttpClient.GetFileAsync(url, getQueryParameters(parameters));
        return "the selected invoices are printed ";
    }

    public async Task<string> DisablePaymentReminderAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/paymentreminder/disable";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }


    public async Task<string> EnablePaymentReminderAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/paymentreminder/enable";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> WriteoffInvoiceAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/writeoff";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> CancelWriteoffAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/writeoff/cancel";
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> UpdateBillingAddressAsync(string invoice_id, Address update_info)
    {
        string url = baseAddress + "/" + invoice_id + "/address/billing";
        var json =  await ZohoSerializer.SerializeAsync<Address>(update_info);
        var jsonstring = new Dictionary<object, object>();
        jsonstring.Add("JSONString", json);
        var responce = await ZohoHttpClient.PutAsync(url, getQueryParameters(jsonstring));
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> UpdateShippingAddressAsync(string invoice_id, Address update_info)
    {
        string url = baseAddress + "/" + invoice_id + "/address/shipping";
        var json = await ZohoSerializer.SerializeAsync<Address>(update_info);
        var jsonstring = new Dictionary<object, object>();
        jsonstring.Add("JSONString", json);
        var responce = await ZohoHttpClient.PutAsync(url, getQueryParameters(jsonstring));
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<TemplateList> GetTemplatesAsync()
    {
        string url = baseAddress + "/templates";
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters());
        return await CreditNoteParser.GetTemplateListAsync(responce);
    }

    public async Task<string> UpdateTemplateAsync(string invoice_id, string template_id)
    {
        string url = baseAddress + "/" + invoice_id + "/templates/" + template_id;
        var responce = await ZohoHttpClient.PutAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<PaymentList> GetPaymentsAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/payments";
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters());
        return await InvoiceParser.GetPaymentsListAsync(responce);
    }

    public async Task<CreditNoteList> GetCreditsAppliedAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/creditsapplied";
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters());
        return await InvoiceParser.GetCreditsAsync(responce);
    }

    public async Task<UseCredits> AddCreditsAsync(string invoice_id, UseCredits credits_to_apply)
    {
        string url = baseAddress + "/" + invoice_id + "/credits";
        var json = await ZohoSerializer.SerializeAsync<UseCredits>(credits_to_apply);
        var jsonstring = new Dictionary<object, object>();
        jsonstring.Add("JSONString", json);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(jsonstring));
        return await InvoiceParser.GetUseCreditsAsync(responce);
    }

    public async Task<string> DeletePaymentAsync(string invoice_id, string payment_id)
    {
        string url = baseAddress + "/" + invoice_id + "/payments/" + payment_id;
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

     public async Task<string> DelteAppliedCreditAsync(string invoice_id, string creditnote_id)
    {
        string url = baseAddress + "/" + invoice_id + "/creditsapplied/" + creditnote_id; ;
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }
    public async Task<CommentList> GetCommentsAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/comments"; ;
        var responce = await ZohoHttpClient.GetAsync(url, getQueryParameters());
        return await CreditNoteParser.GetCommentListAsync(responce);
    }

     public async Task<Comment> AddCommentAsync(string invoice_id, Comment new_comment_info)
    {
        string url = baseAddress + "/" + invoice_id + "/comments";
        var json = await ZohoSerializer.SerializeAsync<Comment>(new_comment_info);
        var jsonstring = new Dictionary<object, object>();
        jsonstring.Add("JSONString", json);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(jsonstring));
        return await CreditNoteParser.GetCommentAsync(responce);
    }

    public async Task<Comment> UpdateCommentAsync(string invoice_id, string comment_id, Comment update_info)
    {
        string url = baseAddress + "/" + invoice_id + "/comments/" + comment_id;
        var json = await ZohoSerializer.SerializeAsync<Comment>(update_info);
        var jsonstring = new Dictionary<object, object>();
        jsonstring.Add("JSONString", json);
        var responce = await ZohoHttpClient.PutAsync(url, getQueryParameters(jsonstring));
        return await CreditNoteParser.GetCommentAsync(responce);
    }

    public async Task<string> DeleteCommentAsync(string invoice_id, string comment_id)
    {
        string url = baseAddress + "/" + invoice_id + "/comments/" + comment_id;
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }
    public async Task<string> GetAttachmentAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/attachment";
        await ZohoHttpClient.GetFileAsync(url, getQueryParameters());
        return "the selected expense receipt is saved at home directory";
    }


    public async Task<string> AddAttachmentAsync(string invoice_id, string attachment_Path, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id + "/attachment";
        var attachment = new string[] { attachment_Path };
        var file = new KeyValuePair<string, string[]>("attachment", attachment);
        var responce = await ZohoHttpClient.PostAsync(url, getQueryParameters(parameters), null, file);
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> UpdateAttachmentAsync(string invoice_id, Dictionary<object, object> parameters)
    {
        string url = baseAddress + "/" + invoice_id + "/attachment";
        var responce = await ZohoHttpClient.PutAsync(url, getQueryParameters(parameters));
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> DeleteAttachmentAsync(string invoice_id)
    {
        string url = baseAddress + "/" + invoice_id + "/attachment";
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }

    public async Task<string> DeleteExpenseReceiptAsync(string expense_id)
    {
        string url = baseAddress + "/expenses/" + expense_id + "/receipt";
        var responce = await ZohoHttpClient.DeleteAsync(url, getQueryParameters());
        return await InvoiceParser.GetMessageAsync(responce);
    }
}
