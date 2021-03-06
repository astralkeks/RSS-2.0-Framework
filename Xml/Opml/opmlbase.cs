using System;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;
// OpmlOutline Processor Markup Language 
namespace Raccoom.Xml.Opml
{	
	/// <summary>A body contains one or more outline elements</summary>
	public interface IOpmlBody
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>OpmlOutline elements.</summary>
		System.Collections.IList Items
		{
			get; 
		}
        IOpmlOutline CreateOutline();
	}
	
	/// <summary>Opml is an XML element, with a single required attribute, version; a head element and a body element, both of which are required. The version attribute is a version string, of the form, x.y, where x and y are both numeric strings.</summary>
	public interface IOpmlDocument 
	{
		/// <summary>A head contains zero or more optional elements</summary>
		IOpmlHead Head
		{
			get; 
			set; 
		}
		
		// end OpmlHead
		
		/// <summary>A body contains one or more outline elements</summary>
		IOpmlBody Body
		{
			get; 
			set; 
		}
		
		// end
	}
	
	/// <summary>A head contains zero or more optional elements</summary>
	public interface IOpmlHead
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>The title of the document.</summary>
		string Title
		{
			get; 
			set; 
		}
		
		// end Title
		
		/// <summary>date-time, indicating when the document was created.</summary>
		DateTime DateCreated
		{
			get; 
			set; 
		}
		
		// end DateCreated
		
		/// <summary>Date-time, indicating when the document was last modified.</summary>
		DateTime DateModified
		{
			get; 
			set; 
		}
		
		// end DateModified
		
		/// <summary>the owner of the document.</summary>
		string OwnerName
		{
			get; 
			set; 
		}
		
		// end OwnerName
		
		/// <summary>the email address of the owner of the document.</summary>
		string OwnerEmail
		{
			get; 
			set; 
		}
		
		// end OwnerEmail
		
		/// <summary>comma-separated list of line numbers that are expanded. The line numbers in the list tell you which headlines to expand. The order is important. For each element in the list, X, starting at the first summit, navigate flatdown X times and expand. Repeat for each element in the list</summary>
		string ExpansionState
		{
			get; 
			set; 
		}
		
		// end ExpansionState
		
		/// <summary>is a number, saying which line of the outline is displayed on the top line of the window. This number is calculated with the expansion state already applied.</summary>
		int VertScrollState
		{
			get; 
			set; 
		}
		
		// end VertScrollState
		
		/// <summary>is a number, the pixel location of the top edge of the window.</summary>
		int WindowTop
		{
			get; 
			set; 
		}
		
		// end WindowTop
		
		/// <summary>is a number, the pixel location of the left edge of the window.</summary>
		int WindowLeft
		{
			get; 
			set; 
		}
		
		// end WindowLeft
		
		/// <summary>is a number, the pixel location of the bottom edge of the window</summary>
		int WindowBottom
		{
			get; 
			set; 
		}
		
		// end WindowBottom
		
		/// <summary>is a number, the pixel location of the right edge of the window</summary>
		int WindowRight
		{
			get; 
			set; 
		}
		
		// end WindowRight

        /// <summary> is the http address of a web page that contains a form that allows a human reader to communicate with the author of the document via email or other means.</summary>
        string OwnerId
        {
            get;
            set;
        }

        // end ownerId
	}
	
	/// <summary>An outline is an XML element, possibly containing one or more attributes, and containing any number of outline sub-elements.</summary>
	public interface IOpmlOutline
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>Gets the outline that this outline is assigned to.</summary>
		/// <remarks>
		/// If the outline is at the root level, the Parent property returns null. 
		/// </remarks>
		IOpmlOutline Parent
		{
			get; 
		}
		
		/// <summary>Text is the string of characters that's displayed when the outline is being browsed or edited. There is no specific limit on the length of the text attribute.</summary>
		string Text
		{
			get; 
			set; 
		}
		
		// end Text
		
		/// <summary>Type is a string, it says how the other attributes of the outline are interpreted</summary>
		string Type
		{
			get; 
			set; 
		}
		
		// end Type
		
		/// <summary></summary>
		string Description
		{
			get; 
			set; 
		}
		
		// end Description

        /// <summary>When outline type is link url must have a value that is an http address.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        string Url
        {
            get;
            set;
        }

        // end Url

		/// <summary>Gets or sets the favorite url.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        string XmlUrl
		{
			get; 
			set; 
		}
		
		// end XmlUrl
		
		/// <summary></summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        string HtmlUrl
		{
			get; 
			set; 
		}
		
		// end HtmlUrl
		
		/// <summary>IsComment is a string, either true or false, indicating whether the outline is commented or not. By convention if an outline is commented, all subordinate outlines are considered to be commented as well. If it's not present, the value is false.</summary>
		bool IsComment
		{
			get; 
			set; 
		}
		
		// end IsComment
		
		/// <summary>IsBreakpoint is a string, either true or false, indicating whether a breakpoint is set on this outline. This attribute is mainly necessary for outlines used to edit scripts that execute. If it's not present, the value is false.</summary>
		bool IsBreakpoint
		{
			get; 
			set; 
		}
		
		// end IsBreakpoint
		
		/// <summary>OpmlOutline elements.</summary>
		System.Collections.IList Items
		{
			get; 
		}
        IOpmlOutline CreateOutline();
		// end Items

        /// <summary>is the date-time that the outline node was created.</summary>
        DateTime Created
        {
            get;
            set;
        }

        // end Created

        /// <summary>A string of comma-separated slash-delimited category strings, in the format defined by the RSS 2.0 category element. To represent a "tag," the category string should contain no slashes.</summary>
        string Category
        {
            get;
            set;
        }

        // end RssCategory

        /// <summary> the value of the top-level language element. title is probably the same as text, it should not be omitted. title contains the top-level title element from the feed.</summary>
        System.Globalization.CultureInfo Language
        {
            get;
            set;
        }

        // end Language

        /// <summary>version varies depending on the version of RSS that's being supplied. It was invented at a time when we thought there might be some processors that only handled certain versions, but that hasn't turned out to be a major issue. The values it can have are: RSS1 for RSS 1.0; RSS for 0.91, 0.92 or 2.0; scriptingNews for scriptingNews format. There are no known values for Atom feeds, but they certainly could be provided.</summary>
        string Version
        {
            get;
            set;
        }

        // end Version
	}
	
    public interface IOpmlReader
    {
        IOpmlDocument Read(string opmlSource);
    }

    public interface IOpmlWriter
    {
        void Write(string name, IOpmlDocument document);
    }
}