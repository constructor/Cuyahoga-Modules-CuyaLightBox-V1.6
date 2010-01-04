namespace  Cuyahoga.Modules.CuyaLightBox.Domain
{
    // data object class LightBox for table cm_LightBox
    // By John [2009-05-12] 

    using System;
    using System.Collections;
    using Cuyahoga.Core.Domain;


    public  class LightBox 
    {

        #region Private_Variables
            private int _lightboxid;
            private IList _lightboxImages;
            private string _name;
            private string _description;
            private DateTime _created;
            private Boolean _active;
            private Node _node;
        #endregion

        #region Constructors
            public LightBox(){ 	
		        this._lightboxid = -1;
            }
        #endregion

        #region Properties
            public virtual IList LightboxImages
            {
                get { return _lightboxImages; }
                set { _lightboxImages = value; }
            }
            public virtual int LightBoxId
            {
                get { return _lightboxid; }
	            set { _lightboxid = value; }
            }
            public virtual string Name
            {
                get { return _name; }
	            set { _name = value; }
            }
            public virtual string Description
            {
                get { return _description; }
	            set { _description = value; }
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
            public virtual Node Node
            {
                get { return _node; }
	            set { _node = value; }
            }
        #endregion

	} 
        // LightBox
}
