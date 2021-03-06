using System;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;
using Raccoom.Xml.ComponentModel;

namespace Raccoom.Xml.Opml
{
    /// <summary>
    /// GenericOpmlFactory implements the operations to create <see cref="IOpmlDocument"/> objects
    /// </summary>
    public abstract class GenericOpmlFactory<TReader, TWriter>
        where TReader : IOpmlReader , new()
        where TWriter : IOpmlWriter , new()
    {
        #region fields
        /// <summary>web proxy to use when get feeds from da web</summary>
        System.Net.WebProxy _webProxy;
        TReader _reader;
        TWriter _writer;

        #endregion

        #region constructors

        /// <summary>Initializes a new instance of GenericOpmlFactory</summary>
        protected GenericOpmlFactory()
        {
            _reader = new TReader();
            _writer = new TWriter();
        }

        #endregion

        #region public interface
        public IOpmlDocument Read(string opmlDocument)
        {
            System.Diagnostics.Debug.Assert(_reader != null);
            return _reader.Read(opmlDocument);
        }
        public void Write(string document, IOpmlDocument opmlDocument)
        {
            System.Diagnostics.Debug.Assert(_writer != null);
            _writer.Write(document, opmlDocument);
        }

        /// <summary>
        /// Gets or sets the WebProxy that is used to connect to the network, can be null
        /// </summary>
        public System.Net.WebProxy Proxy
        {
            get
            {
                return _webProxy;
            }

            set
            {
                _webProxy = value;
            }
        }

        public TReader Reader
        {
            get
            {
                return _reader;
            }
            set
            {
                _reader = value;
            }
        }

        public TWriter Writer
        {
            get
            {
                return _writer;
            }
            set
            {
                _writer = value;
            }
        }

        /// <summary>
        /// Gets (create) the requested <see cref="IOpmlDocument"/> instance
        /// </summary>
        /// <param name="opmlDocument">The URI of the resource to receive the data.</param>
        /// <returns>The requested see cref="IOpmlDocument" instance</returns>
        public virtual IOpmlDocument GetDocument(string opmlDocument)
        {
            System.Diagnostics.Debug.Assert(_reader != null);
            return _reader.Read(opmlDocument);
        }
        public virtual IOpmlDocument GetDocument()
        {
            return new OpmlDocument();
        }

        #endregion
    }
    public class OpmlFactory : GenericOpmlFactory<OpmlXmlReader, OpmlXmlWriter> { } 
    public class OpmlXmlWriter : IOpmlWriter
    {
        public void Write(System.IO.Stream stream, IOpmlDocument document)
        {
            XmlTextWriter writer = null;
            System.Reflection.AssemblyName assemblyName = this.GetType().Assembly.GetName();
            string copyright = string.Format("Generated by {0} {1}, Copyright � 2007 by Christoph Richner. All rights reserved. http://www.raccoom.net", assemblyName.Name, assemblyName.Version);
            using (writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8))
            {
                //Use indenting for readability.
                writer.Formatting = Formatting.Indented;
                //Write the XML delcaration. 
                writer.WriteStartDocument();
                writer.WriteComment(copyright);
                // serialize the content
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(document.GetType());
                ser.Serialize(writer, document);
                //                
                writer.WriteEndDocument();
            }            
        }
        public void Write(string name, IOpmlDocument document)
        {
            using (System.IO.FileStream stream = new System.IO.FileStream(name, System.IO.FileMode.Create))
            {
                Write(stream, document);
            }
        }
    }
    public class OpmlXmlReader : SyndicationObjectParser, IOpmlReader
    {
        #region fields

        /// <summary>web proxy to use when get feeds from da web</summary>
        private System.Net.WebProxy _webProxy;

        #endregion

        #region public interface
        /// <summary>
        /// Gets or sets the WebProxy that is used to connect to the network, can be null
        /// </summary>
        public System.Net.WebProxy Proxy
        {
            get
            {
                return _webProxy;
            }

            set
            {
                _webProxy = value;
            }
        }
        public IOpmlDocument Read(string opmlSource)
        {
            OpmlDocument opmlDocument = new OpmlDocument();
            using (XmlTextReader reader = new XmlTextReader(opmlSource))
            {
                Parse(opmlDocument, reader);
            }
            return opmlDocument;
        }
        #endregion

        #region internal interface
        //protected override void Parse(object target, System.Xml.XmlReader xmlTextReader)
        //    {
        //        base.Parse(target, xmlTextReader);
        //        return;
        //        int depth = xmlTextReader.Depth;
        //        // reflect complex tags
        //        System.Collections.Generic.Dictionary<string, System.Reflection.PropertyInfo> complexProperties = complexProperties = GetComplexProperties(target);
        //        // initalize
        //        System.Reflection.PropertyInfo pi = null;
        //        string name;
        //        object childObject;
        //        // process channel
        //        while (!xmlTextReader.EOF)
        //        {
        //            xmlTextReader.Read();
        //            xmlTextReader.MoveToContent();
        //            //
        //            if (xmlTextReader.NodeType == XmlNodeType.EndElement || xmlTextReader.NodeType == XmlNodeType.None) continue;
        //            //                
        //            name = xmlTextReader.Name;
        //            // value of element strictly mapped to Value property _> rss guid
        //            if (string.IsNullOrEmpty(name)) name = "Value";
        //            // is it a complex type like item or source
        //            if (complexProperties.ContainsKey(name) & depth < xmlTextReader.Depth)
        //            {
        //                // is it a collection based item and needs to be created and added?
        //                if (complexProperties[name].PropertyType.GetInterface(typeof(System.Collections.IList).Name) != null)
        //                {
        //                    // get create item method
        //                    System.Reflection.MethodInfo mi = target.GetType().GetMethod("Create" + name, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.NonPublic | BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //                    // invoke create method
        //                    childObject = mi.Invoke(target, null);
        //                    // add item
        //                    complexProperties[name].PropertyType.InvokeMember("Add", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, complexProperties[name].GetValue(target, null), new object[] { childObject });
        //                }
        //                else
        //                {
        //                    // get existing instance from target
        //                    childObject = complexProperties[name].GetValue(target, null);
        //                }
        //                // recursive call to parse child object
        //                Parse(childObject, xmlTextReader.ReadSubtree());
        //            }
        //            else
        //            {
        //                // get attributes if any exists
        //                if (xmlTextReader.HasAttributes)
        //                {
        //                    while (xmlTextReader.MoveToNextAttribute())
        //                    {
        //                        // try to get attribute property
        //                        pi = GetPropertyByName(xmlTextReader.Name, target);
        //                        if (pi == null) continue;
        //                        // get attribute value
        //                        SetPropertyValue(xmlTextReader, target, pi);
        //                    }
        //                }
        //                // try to get element property
        //                pi = GetPropertyByName(name, target);
        //                if (pi == null) continue;
        //                // read if not already done
        //                if (xmlTextReader.NodeType == XmlNodeType.Element) xmlTextReader.Read();
        //                // get element value
        //                SetPropertyValue(xmlTextReader, target, pi);
        //            }
        //        }
        //    }
        #endregion
    }
}