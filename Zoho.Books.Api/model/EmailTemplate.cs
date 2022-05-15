using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zohobooks.model;

public class EmailTemplate
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="EmailTemplate" /> is selected.
    /// </summary>
    /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
    public bool selected { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string name { get; set; }
    /// <summary>
    /// Gets or sets the email_template_id.
    /// </summary>
    /// <value>The email_template_id.</value>
    public string email_template_id { get; set; }
}
