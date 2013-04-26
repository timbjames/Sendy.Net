using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sendy.Net;

namespace Sendy.Net.Samples
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Subscription subscriptionService = null;
            try
            {
                subscriptionService = new Subscription();
                subscriptionService.Subscribe("", "", true);
                Response.Write(subscriptionService.StatusDescrption);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}