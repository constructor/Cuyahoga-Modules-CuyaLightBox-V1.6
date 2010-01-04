namespace  Cuyahoga.Modules.CuyaLightBox.Domain
{
    // data object class LightboxImage for table cm_LightboxImage
    // By John [2009-05-12] 

    using System;
    using System.Collections;
    using Cuyahoga.Core.Domain;


    public  class LightboxImage 
    {

        #region Private_Variables
            private int _lightboximageid;
            private LightBox _lightBoxId;
            private string _filename;
            private string _title;
            private string _altText;
            private int _width;
            private int _height;
            private DateTime _created;
            private Boolean _active;
        #endregion

        #region Constructors
            public LightboxImage(){ 	
		        this._lightboximageid = -1;
            }
        #endregion

        #region Properties
            public virtual int LightBoxImageId
            {
                get { return _lightboximageid; }
	            set { _lightboximageid = value; }
            }
            public virtual LightBox LightBoxId
            {
                get { return _lightBoxId; }
	            set { _lightBoxId = value; }
            }
            public virtual string Filename
            {
                get { return _filename; }
	            set { _filename = value; }
            }
            public virtual string Title
            {
                get { return _title; }
	            set { _title = value; }
            }
            public virtual string AltText
            {
                get { return _altText; }
	            set { _altText = value; }
            }
            public virtual int Width
            {
                get { return _width; }
	            set { _width = value; }
            }
            public virtual int Height
            {
                get { return _height; }
	            set { _height = value; }
            }
            public virtual DateTime Created
            {
                get { return _created; }
	            set { _created = value; }
            }
            public virtual Boolean Active
            {
                get { return _active; }
	            set { _active = value; }
            }
        #endregion

	} 
        // LightboxImage
}
