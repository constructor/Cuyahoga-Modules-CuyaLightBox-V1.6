using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Web;

using Cuyahoga.Core; //Contains: INHibernateModule
using Cuyahoga.Core.Domain; //Contains: ModuleBase
using Cuyahoga.Core.Service;
using Cuyahoga.Core.Service.Files;
using Cuyahoga.Core.Service.SiteStructure;

using Cuyahoga.Core.Search;
using Cuyahoga.Core.Util;
using Cuyahoga.Core.Communication;

using Cuyahoga.Web.Util;
using Cuyahoga.Core.DataAccess; //Contains: ICommonDao
using Castle.Services.Transaction;
using Castle.Core;

using Cuyahoga.Modules.CuyaLightBox.DataAccess; //Needed or: 'as it has dependencies to be satisfied'
using Cuyahoga.Modules.CuyaLightBox.Domain;

namespace Cuyahoga.Modules.CuyaLightBox
{
    [Transactional]
    public class CuyaLightBoxModule : ModuleBase, INHibernateModule//, IActionConsumer, IActionProvider
    {
        private ICommonDao _commonDao;
        public ICuyaLightBoxDao _cuyaLightBoxDao;
        public IFileService _fileService;
        public ISectionService _sectionService;
        //private ActionCollection _inboundActions;
        //private ActionCollection _outboundActions;

        public CuyaLightBoxModule(ICommonDao commonDao, ICuyaLightBoxDao cuyaLightBoxDao, IFileService fileService, ISectionService sectionService)
        {
            this._commonDao = commonDao;
            this._cuyaLightBoxDao = cuyaLightBoxDao;
            this._fileService = fileService;
            this._sectionService = sectionService;
            //InitInboundActions();
            //InitOutboundActions();

        }

        #region Actions
            //private void InitInboundActions()
            //{
            //    this._inboundActions = new ActionCollection();
            //    this._inboundActions.Add(new Cuyahoga.Core.Communication.Action("EditRecruiterProfile", null)); //Replace null with passed values
            //}
            //public ActionCollection GetInboundActions()
            //{
            //    return this._inboundActions;
            //}

            //private void InitOutboundActions()
            //{
            //    this._outboundActions = new ActionCollection();
            //    this._outboundActions.Add(new Cuyahoga.Core.Communication.Action("EditRecruiterProfile", null));//Replace null with passed values
            //}
            //public ActionCollection GetOutboundActions()
            //{
            //    return this._outboundActions;
            //}
        #endregion Actions

        #region Data Access

        #endregion Data Access

    }
}
