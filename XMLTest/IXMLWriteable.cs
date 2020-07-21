using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLTest
{
	public interface IXMLWriteable
	{
		void Write(XmlTextWriter Xwriter);
	}
}
