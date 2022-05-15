using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zohobooks.model;

namespace zohobooks.parser;

class ContactParser
{

    internal static async Task<ContactList> GetContactListAsync(HttpResponseMessage response)
    {
        var contactList = new ContactList();
        var jsonContent = await response.Content.ReadAsStringAsync();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(jsonContent);
        if (jsonObj.ContainsKey("contacts"))
        {
            var contactsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["contacts"].ToString());
            foreach (var contactObj in contactsArray)
            {
                var contact = new Contact();
                contact = await ZohoSerializer.DeserializeAsync<Contact>(contactObj.ToString());
                contactList.Add(contact);
            }
        }
        if (jsonObj.ContainsKey("page_context"))
        {
            var pageContext = new PageContext();
            pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
            contactList.page_context = pageContext;
        }
        return contactList;
    }

    internal static async Task<Contact> GetContactAsync(HttpResponseMessage responce)
    {
        var contact = new Contact();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("contact"))
        {
            contact = await ZohoSerializer.DeserializeAsync<Contact>(jsonObj["contact"].ToString());
        }
        return contact;
    }

    internal static async Task<string> GetMessageAsync(HttpResponseMessage responce)
    {
        string message = "";
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("message"))
            message = jsonObj["message"].ToString();
        return message;
    }

    internal static async Task<Email> GetEmailContentAsync(HttpResponseMessage responce)
    {
        var mailContent = new Email();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("data"))
        {
            mailContent = await ZohoSerializer.DeserializeAsync<Email>(jsonObj["data"].ToString());
        }
        return mailContent;
    }

    internal static async Task<CommentList> getCommentListAsync(HttpResponseMessage responce)
    {
        var commentList = new CommentList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("contact_comments"))
        {
            var commentsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["contact_comments"].ToString());
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

    internal static async Task<CreditNoteRefundList> getCreditNoteRefundListAsync(HttpResponseMessage responce)
    {
        var creditnoteRefundList = new CreditNoteRefundList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("creditnote_refunds"))
        {
            var creditNotesArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["creditnote_refunds"].ToString());
            foreach (var creditNoteObj in creditNotesArray)
            {
                var creditNote = new CreditNote();
                creditNote = await ZohoSerializer.DeserializeAsync<CreditNote>(creditNoteObj.ToString());
                creditnoteRefundList.Add(creditNote);
            }
        }
        if (jsonObj.ContainsKey("page_context"))
        {
            var pageContext = new PageContext();
            pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
            creditnoteRefundList.page_context = pageContext;
        }
        return creditnoteRefundList;
    }

    internal static async Task<ContactPersonList> getContactPersonListAsync(HttpResponseMessage responce)
    {
        var contactPersonList = new ContactPersonList();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("contact_persons"))
        {
            var contactPersonsArray = await ZohoSerializer.DeserializeAsync<List<object>>(jsonObj["contact_persons"].ToString());
            foreach (var contactPersonObj in contactPersonsArray)
            {
                var contactPerson = new ContactPerson();
                contactPerson = await ZohoSerializer.DeserializeAsync<ContactPerson>(contactPersonObj.ToString());
                contactPersonList.Add(contactPerson);
            }
        }
        if (jsonObj.ContainsKey("page_context"))
        {
            var pageContext = new PageContext();
            pageContext = await ZohoSerializer.DeserializeAsync<PageContext>(jsonObj["page_context"].ToString());
            contactPersonList.page_context = pageContext;
        }
        return contactPersonList;
    }

    internal static async Task<ContactPerson> getContactPersonAsync(HttpResponseMessage responce)
    {
        var contactPerson = new ContactPerson();
        var jsonObj = await ZohoSerializer.DeserializeAsync<Dictionary<string, object>>(responce.Content.ReadAsStringAsync().Result);
        if (jsonObj.ContainsKey("contact_person"))
        {
            contactPerson = await ZohoSerializer.DeserializeAsync<ContactPerson>(jsonObj["contact_person"].ToString());
        }
        return contactPerson;
    }
}
