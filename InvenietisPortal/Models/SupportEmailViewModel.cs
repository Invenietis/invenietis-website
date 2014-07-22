using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CK.Mailer;
using InvPortal.Properties;

namespace InvPortal.Models
{
    public class SupportEmailViewModel : IMailConfigurator<SupportEmailViewModel>
    {
        public SupportEmailViewModel()
        {

        }

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "Email_AddressInvalid", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessage = null, ErrorMessageResourceName = "Email_Required", ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Subject_Required", ErrorMessageResourceType = typeof(Resources))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "Body_Required", ErrorMessageResourceType = typeof(Resources))]
        public string Body { get; set; }

        #region IMailConfigurator<SupportEmailViewModel> Members

        public void ConfigureMail( SupportEmailViewModel model, MailParams mailParams )
        {
        }

        public string GetSubject( SupportEmailViewModel model )
        {
            return "Support CK-MultiPlan";
        }

        #endregion
    }
}