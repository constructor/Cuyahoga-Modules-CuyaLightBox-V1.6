    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Cuyahoga.Core.Util;
    using Cuyahoga.Core.Domain;
    using Cuyahoga.Web.UI;
    using Cuyahoga.Web.Util;

    using Cuyahoga.Modules.CuyaLightBox;
    using Cuyahoga.Modules.CuyaLightBox.Domain;

namespace Cuyahoga.Modules.CuyaLightBox.Web
{
    public partial class CuyaLightBox : BaseModuleControl
    {
        //Create an instance of this module...
        private CuyaLightBoxModule _Module;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get THIS module class and put it in _Module
            //ALWAYS GET THE MODULE AND PUT IT IN A PRIVATE VARIABLE
            this._Module = this.Module as CuyaLightBoxModule;

            //Register jQuery (Note: I think that when registering jQuery the 'key' should be jQuery to prevent multiple registrations?
            RegisterJavaScript("jQuery", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/JavaScript/jquery-1.3.2.min.js");
            //Register Javascripts
            RegisterJavaScript("jQueryLightBox", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/JavaScript/jquery.lightbox-0.5.min.js");
            
            //Register Css
            RegisterStyleSheet("jQueryLightBoxCss", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/css/jquery.lightbox-0.5.css");
            RegisterStyleSheet("jQueryLightBoxCss2", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/css/CuyaLightBox.ascx.css");

            int lightboxid = Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"].ToString());
            if(this._Module._cuyaLightBoxDao.LightBoxExists(lightboxid)){
                LightBox l = this._Module._cuyaLightBoxDao.GetLightBox(lightboxid);

                //rptImageItems.DataSource = l.LightboxImages;
                rptImageItems.DataSource = this._Module._cuyaLightBoxDao.GetActiveLightboxSpecificImages(l);
                rptImageItems.DataBind();
            }
            else
            {
                lblMessages.Text = "There is no lightbox assigned to this section.";
            }

            string lightboxjs = "<script type=\"text/javascript\">" + System.Environment.NewLine
                + "$(document).ready(function() {" + System.Environment.NewLine
                + "$(\"#" + pnlGallery.ClientID + " a\").lightBox();" + System.Environment.NewLine
                + "$(\"#" + pnlGallery.ClientID + "\").addClass(\"lightbox\");" + System.Environment.NewLine 
                + "});" + System.Environment.NewLine
                + "</script>" + System.Environment.NewLine;
 
            Page.RegisterClientScriptBlock(this._Module.Section.Id.ToString(), lightboxjs);
        }

        public string GetImage(string filename)
        { 
            string fileRoot = UrlHelper.GetApplicationPath() + "UserFiles/CuyaLightBox/" + this._Module.Section.Node.Id.ToString() + "/";
            return VirtualPathUtility.Combine(fileRoot, filename.Replace("thumb_", ""));
        }

        public string GetThumbImage(string filename)
        {
            string fileRoot = UrlHelper.GetApplicationPath() + "UserFiles/CuyaLightBox/" + this._Module.Section.Node.Id.ToString() + "/";
            return VirtualPathUtility.Combine(fileRoot, filename);
        }

    }
}