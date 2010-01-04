using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;

using NHibernate;

using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.CuyaLightBox.Domain;

namespace Cuyahoga.Modules.CuyaLightBox.DataAccess
{
    /// <summary>
    /// Specific Data Access functionality for CuyaLightBox module.
    /// NOTE: Set the 'Delete Rule' and 'Update Rule' on the relationships between tables (in the Database Diagrams): Database Designer: Update and Delete Specification
    /// </summary>
    [Transactional]
    public class CuyaLightBoxDao : Cuyahoga.Modules.CuyaLightBox.DataAccess.ICuyaLightBoxDao
    {

        private ISessionManager _sessionManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sessionManager"></param>
        public CuyaLightBoxDao(ISessionManager sessionManager)
	    {
	        this._sessionManager = sessionManager;
        }

        #region LightBox Data Access
            public IList GetLightBoxs()
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightBox c";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    //q.SetInt32("siteId", site.Id);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightBox' object list: " + x.Message);
                }
            }

            public IList GetNodesLightBoxs(Node node)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightBox c where c.Node = :Node";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetEntity("Node", node);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightBox' object list: " + x.Message);
                }
            }

            public Boolean LightBoxExists(int lightBoxID)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightBox c where c.LightBoxId = :LightBoxID";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("LightBoxID", lightBoxID);
                    return q.List().Count > 0;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightBox object: " + x.Message);
                }
            }

            public LightBox GetLightBox(int lightBoxID)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightBox c where c.LightBoxId = :LightBoxID";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("LightBoxID", lightBoxID);
                    return q.List()[0] as LightBox;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightBox object: " + x.Message);
                }
            }

            public void SaveLightBox(LightBox lightBoxToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.SaveOrUpdate(lightBoxToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'LightBox object: " + x.Message);
                }
            }

            public void DeleteLightBox(LightBox lightBoxToDelete)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Delete(lightBoxToDelete);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not delete the 'LightBox object: " + x.Message);
                }
            }
        #endregion LightBox Data Access


        #region LightboxImage Data Access
            public IList GetLightboxImages()
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightboxImage c";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    //q.SetInt32("siteId", site.Id);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightboxImage' object list: " + x.Message);
                }
            }

            public IList GetActiveLightboxImages()
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightboxImage c where c.Active = :active";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetBoolean("active", true);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightboxImage' object list: " + x.Message);
                }
            }

            public IList GetActiveLightboxSpecificImages(LightBox lightBox)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightboxImage c where c.Active = :active and c.LightBoxId = :lightBox";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetBoolean("active", true);
                    q.SetEntity("lightBox", lightBox);
                    return q.List();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightboxImage' object list: " + x.Message);
                }
            }

            public LightboxImage GetLightboxImage(int lightboxImageID)
            {
                try
                {
                    string hql = "select c from Cuyahoga.Modules.CuyaLightBox.Domain.LightboxImage c where LightboxImageId = :LightboxImageId";
                    IQuery q = this._sessionManager.OpenSession().CreateQuery(hql);
                    q.SetInt32("LightboxImageId", lightboxImageID);
                    return q.List()[0] as LightboxImage;
                }
                catch (Exception x)
                {
                    throw new Exception("Could not get the 'LightboxImage object: " + x.Message);
                }
            }

            public void SaveLightboxImage(LightboxImage lightboxImageToSave)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.SaveOrUpdate(lightboxImageToSave);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not save the 'LightboxImage object: " + x.Message);
                }
            }

            public void DeleteLightboxImage(LightboxImage lightboxImageToDelete)
            {
                try
                {
                    ISession currentSession = this._sessionManager.OpenSession();
                    currentSession.Delete(lightboxImageToDelete);
                    currentSession.Flush();
                }
                catch (Exception x)
                {
                    throw new Exception("Could not delete the 'LightboxImage object: " + x.Message);
                }
            }
        #endregion LightboxImage Data Access

    }
}
