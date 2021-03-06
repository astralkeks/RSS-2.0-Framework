using System;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;

namespace Raccoom.Xml.Rss
{	
	/// <summary>Optional sub-element of item. Its value is the name of the RSS channel that the item came from, derived from its title. It has one required attribute, url, which links to the XMLization of the source.The purpose of this element is to propagate credit for links, to publicize the sources of news items. It can be used in the Post command of an aggregator. It should be generated automatically when forwarding an item from an aggregator to a weblog authoring tool.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("048FF54F-94DF-4879-A355-880832C49A2C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssSource")]
	[System.Xml.Serialization.XmlTypeAttribute("source")]
	[Serializable]	
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class RssSource : ComponentModel.SyndicationObjectBase, IRssSource
	{
		#region fields
		/// <summary>IsPermaLink</summary>
		private string _uri;
		/// <summary>RssGuid</summary>
		private string _value;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of RssGuid with default values</summary>
		public RssSource ()
		{
		
		}
		#endregion
		
		#region public interface
        public override bool Specified
        {
            get
            {                
                return !string.IsNullOrEmpty(Url) || !string.IsNullOrEmpty(Value);
            }
        }
		
		/// <summary>The RSS channel url that the item came from.</summary>
		[System.ComponentModel.Category("RssSource"), System.ComponentModel.Description("The RSS channel url that the item came from.")]
		[System.Xml.Serialization.XmlAttribute("url")]
		public string Url
		{
			get
			{
				return _uri;
			}
			
			set
			{
				bool changed = !object.Equals(_uri, value);
				_uri = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Url));
			}
		}
		
		// end IsPermaLink
		
		/// <summary>The RSS channel name that the item came from.</summary>
		[System.ComponentModel.Category("RssSource"), System.ComponentModel.Description("The RSS channel name that the item came from.")]
		[System.Xml.Serialization.XmlTextAttribute]
		public string Value
		{
			get
			{
				return _value;
			}
			
			set
			{
				bool changed = !object.Equals(_value, value);
				_value = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Value));
			}
		}
		
		// end RssGuid
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return Value;
		}
		
		#endregion
		
		#region protected interface
		
		#endregion
		
		#region nested classes
		
		/// <summary>
		/// public writeable class properties
		/// </summary>		
		internal struct Fields
		{
			public const string Url = "Url";
			public const string Value = "_value";
		}
		
		#endregion
	}
}