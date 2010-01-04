using System;
using System.Collections;
using Cuyahoga.Core.Domain;
using Cuyahoga.Modules.CuyaLightBox.Domain;
using System.Data.SqlClient;

namespace Cuyahoga.Modules.CuyaLightBox.DataAccess
{
    public interface ICuyaLightBoxDao
    {

        #region LightBox Data Access
            IList GetLightBoxs();

            IList GetNodesLightBoxs(Node node);

            LightBox GetLightBox(int lightBoxID);

            void SaveLightBox(LightBox lightBoxToSave);

            void DeleteLightBox(LightBox lightBoxToDelete);
        #endregion LightBox Data Access

        #region LightboxImage Data Access
            IList GetLightboxImages();

            IList GetActiveLightboxImages();

            IList GetActiveLightboxSpecificImages(LightBox lightBox);

            Boolean LightBoxExists(int lightBoxID);

	        LightboxImage GetLightboxImage(int lightboxImageID);

            void SaveLightboxImage(LightboxImage lightboxImageToSave);

            void DeleteLightboxImage (LightboxImage lightboxImageToDelete);
        #endregion LightboxImage Data Access

    }
}
