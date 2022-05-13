namespace zohobooks.service;

public class ZohoBooks
{
    /// <summary>
    /// Class ZohoBooks is used to provide all api instances for the Zoho Books services.
    /// </summary>
    public class ZohoBooks
    {
        /// <summary>
        /// The authentication token
        /// </summary>
        string authToken;
        /// <summary>
        /// The organisation identifier
        /// </summary>
        string organisationId;
        /// <summary>
        /// Initialize ZohoBooks using user's authtoken and organization id.
        /// </summary>
        /// <param name="auth_token">The auth_token is the user's authtoken.</param>
        /// <param name="organization_id">The organization_id is the identifier of the organization.</param>
        public void initialize(string auth_token, string organization_id)
        {
            this.authToken = auth_token;
            this.organisationId = organization_id;
        }
        /// <summary>
        /// Gets an instance of invoices API.
        /// </summary>
        /// <returns>InvoicesApi object.</returns>
        public InvoicesApi GetInvoicesApi()
        {
            var invoicesApi = new InvoicesApi(authToken, organisationId);
            return invoicesApi;
        }
        /// <summary>
        /// Gets an instance of bank accounts API.
        /// </summary>
        /// <returns>BankAccountsApi object.</returns>

    }
}
