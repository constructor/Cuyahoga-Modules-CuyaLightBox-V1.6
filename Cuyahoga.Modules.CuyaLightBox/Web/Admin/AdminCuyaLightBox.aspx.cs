using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using Cuyahoga.Web.UI;
using Cuyahoga.Web.Util;

using Cuyahoga.Core.Domain;

using Cuyahoga.Modules.CuyaLightBox;
using Cuyahoga.Modules.CuyaLightBox.Domain;

namespace Cuyahoga.Modules.CuyaLightBox.Web.Admin
{
    public partial class AdminCuyaLightBox : ModuleAdminBasePage
    {

        //Create an instance of this module...
        private CuyaLightBoxModule _Module;
        private User _currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get THIS module class and put it in _Module
            //ALWAYS GET THE MODULE AND PUT IT IN A PRIVATE VARIABLE
            this._Module = this.Module as CuyaLightBoxModule;

            ////Add Admin Page Specific CSS and Javascript to page
            //RegisterAdminJavascript("AdminScript", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/javascript/AdminCuyaLightBox.aspx.cs.js");
            RegisterAdminStylesheet("AdminCss", UrlHelper.GetApplicationPath() + "Modules/CuyaLightBox/Css/Editor.css");

            Cuyahoga.Core.Domain.User _currentUser = Context.User.Identity as Cuyahoga.Core.Domain.User;

            ShowSelectedLightBox();

            CheckSelected();

            if (!Page.IsPostBack)
            {
                DatabindLightBoxes();
                DatabindLightBoxImages(Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"]));
            }
        }

        private void CheckSelected()
        {
            if ((Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"]) < 1))
            {
                btnLightBoxImage.Enabled = false;
            }
            else
            {
                btnLightBoxImage.Enabled = true;
            }
        }

        private void DatabindLightBoxes()
        {
            gvLightbox.DataSource = this._Module._cuyaLightBoxDao.GetNodesLightBoxs(this._Module.Section.Node);
            gvLightbox.DataBind();
        }

        private void DatabindLightBoxImages(int lightboxid)
        {
            if (this._Module._cuyaLightBoxDao.LightBoxExists(lightboxid))
            {
                LightBox l = this._Module._cuyaLightBoxDao.GetLightBox(lightboxid);
                gvLightBoxImage.DataSource = l.LightboxImages;
                gvLightBoxImage.DataBind();
            }
            else
            {
                lblMessages.Text = "There is no lightbox assigned to this section.";
            }
        }

        protected void btnLightBox_Click(object sender, EventArgs e)
        {
            LightBox l = new LightBox();
            l.Name = TextBoxName.Text;
            l.Description = TextBoxDescription.Text;
            l.Created = DateTime.Now;
            l.Active = CheckBoxActive.Checked;
            l.Node = this._Module.Section.Node;

            this._Module._cuyaLightBoxDao.SaveLightBox(l);

            DatabindLightBoxes();

            //Clear TextBoxes
            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
        }

        protected void btnLightBoxImage_Click(object sender, EventArgs e)
        {
            lblMessages.Text = "";
            if (ImageFileUpload.HasFile)
            {

                string extension = System.IO.Path.GetExtension(ImageFileUpload.FileName);
                if (extension == ".jpg" || extension == ".gif" || extension == ".png")
                {
                    LightboxImage l = new LightboxImage();

                    string fileName = ImageFileUpload.FileName;
                    string fileRoot = HttpContext.Current.Server.MapPath("~/UserFiles/CuyaLightBox/" + this.Section.Node.Id.ToString() + "/");

                    if (!Directory.Exists(fileRoot))
                    {
                        Directory.CreateDirectory(fileRoot);
                    }

                    string fileImagePath = Path.Combine(fileRoot, fileName);
                    ImageFileUpload.SaveAs(fileImagePath);

                    l.Filename = "thumb_" + fileName;
                    l.Title = TextBoxTitle.Text;
                    l.AltText = TextBoxAltText.Text;

                    int lbid = Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"]);
                    l.LightBoxId = this._Module._cuyaLightBoxDao.GetLightBox(lbid);

                    Bitmap bm = new Bitmap(ImageFileUpload.PostedFile.InputStream);

                    //To do: Handle the sizes of thumbnails from a new module setting 

                    //int newWidth = 80;
                    //int newHeight = bm.Height * newWidth / bm.Width;

                    //Height uniformity is tidy. Widths can vary.
                    int newHeight = 80;
                    int newWidth = bm.Width * newHeight / bm.Height;

                    Bitmap tn = bm.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero) as Bitmap;

                    string fileThumbImagePath = Path.Combine(fileRoot, "thumb_" + fileName);

                    l.Width = tn.Width;
                    l.Height = tn.Height;
                    l.Created = DateTime.Now;
                    l.Active = cbxImageActive.Checked;

                    this._Module._cuyaLightBoxDao.SaveLightboxImage(l);

                    tn.Save(fileThumbImagePath, ImageFormat.Jpeg);
                    bm.Dispose();
                    tn.Dispose();

                    DatabindLightBoxImages(Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"].ToString()));
                    //Clear Textboxes
                    TextBoxAltText.Text = "";
                    TextBoxTitle.Text = "";
                }
                else
                {
                    lblMessages.Text = "Only .jpg, .gif and .png image file formats allowed";
                }
            }
        }

        private void ShowSelectedLightBox()
        {
            lblModuleSetting.Text = "No LightBox Seleted";
            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);
            if (this._Module._cuyaLightBoxDao.LightBoxExists(lightboxid))
            {
                LightBox l = this._Module._cuyaLightBoxDao.GetLightBox(lightboxid);
                lblModuleSetting.Text = "Current LightBox: " + l.Name + " (Created " + l.Created.ToShortDateString() + ")";
            }
        }


        #region gvLightBox
        protected void gvLightbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._Module.Section.Settings.Contains("LIGHTBOX_NAME"))
            {
                this._Module.Section.Settings.Remove("LIGHTBOX_NAME");
                this._Module.Section.Settings.Add("LIGHTBOX_NAME", gvLightbox.SelectedValue.ToString());
                this._Module._sectionService.SaveSection(this._Module.Section);

                ShowSelectedLightBox();

                DatabindLightBoxes();
                DatabindLightBoxImages(Convert.ToInt32(this._Module.Section.Settings["LIGHTBOX_NAME"].ToString()));
            }
            CheckSelected();
        }

        protected void gvLightbox_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLightbox.EditIndex = e.NewEditIndex;
            DatabindLightBoxes();
        }

        protected void gvLightbox_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLightbox.EditIndex = -1;
            DatabindLightBoxes();
        }

        protected void gvLightbox_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int lightboxid = Convert.ToInt32(gvLightbox.DataKeys[e.RowIndex].Value);
            LightBox l = this._Module._cuyaLightBoxDao.GetLightBox(lightboxid);

            TextBox tbName = gvLightbox.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox;
            l.Name = tbName.Text;

            CheckBox cbActive = gvLightbox.Rows[e.RowIndex].Cells[2].Controls[0] as CheckBox;
            l.Active = cbActive.Checked;

            this._Module._cuyaLightBoxDao.SaveLightBox(l);

            gvLightbox.EditIndex = -1;
            DatabindLightBoxes();
        }
        #endregion

        #region gvLightBoxImage
        protected void gvLightBoxImage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);
            gvLightBoxImage.EditIndex = e.NewEditIndex;
            DatabindLightBoxImages(lightboxid);
        }

        protected void gvLightBoxImage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tbAltTextImage = gvLightBoxImage.Rows[e.RowIndex].FindControl("tbAltTextImage") as TextBox;
            CheckBox cbActiveImage = gvLightBoxImage.Rows[e.RowIndex].FindControl("cbActiveImage") as CheckBox;

            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);
            int lightboximageid = (int)gvLightBoxImage.DataKeys[e.RowIndex].Value;
            LightboxImage i = this._Module._cuyaLightBoxDao.GetLightboxImage(lightboximageid);

            i.AltText = tbAltTextImage.Text;
            i.Active = cbActiveImage.Checked;

            this._Module._cuyaLightBoxDao.SaveLightboxImage(i);

            gvLightBoxImage.EditIndex = -1;
            DatabindLightBoxImages(lightboxid);
        }

        protected void gvLightBoxImage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);
            gvLightBoxImage.EditIndex = -1;
            DatabindLightBoxImages(lightboxid);
        }
        #endregion

        protected void gvLightBoxImage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);

            int lightboximageid = (int)gvLightBoxImage.DataKeys[e.RowIndex].Value;
            LightboxImage i = this._Module._cuyaLightBoxDao.GetLightboxImage(lightboximageid);
            this._Module._cuyaLightBoxDao.DeleteLightboxImage(i);

            DatabindLightBoxImages(lightboxid);
        }

        protected void gvLightbox_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int dellightboxid = (int)gvLightbox.DataKeys[e.RowIndex].Value;
            int lightboxid = Convert.ToInt32(_Module.Section.Settings["LIGHTBOX_NAME"]);
            LightBox l = this._Module._cuyaLightBoxDao.GetLightBox(dellightboxid);
            this._Module._cuyaLightBoxDao.DeleteLightBox(l);
            if (dellightboxid == lightboxid)
            {
                this._Module.Section.Settings.Remove("LIGHTBOX_NAME");
                this._Module.Section.Settings.Add("LIGHTBOX_NAME", "0");
                this._Module._sectionService.SaveSection(this._Module.Section);
                gvLightBoxImage.DataBind();
            }
            ShowSelectedLightBox();
            CheckSelected();
            DatabindLightBoxes();
        }

    }
}