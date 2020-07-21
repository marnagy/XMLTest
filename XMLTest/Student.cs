using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLTest
{
	public struct Student : IXMLWriteable
	{
		public const string XMLElementName = "Student";

		public string FirstName;
		public string LastName;
		public int Age;
		public double Height;

		public void Write(XmlTextWriter xmlWriter)
		{
			xmlWriter.WriteStartElement(XMLElementName);

			xmlWriter.WriteElementString("FirstName", this.FirstName);
			xmlWriter.WriteElementString("LastName", this.LastName);
			xmlWriter.WriteElementString("Age", this.Age.ToString());
			xmlWriter.WriteElementString("Height", this.Height.ToString());

			xmlWriter.WriteEndElement();
		}
		public static Student FromXml(XmlNode node)
		{
			if (node.Name == XMLElementName)
			{
				var children = node.ChildNodes;
				var temp = new Student{ FirstName = children.Item(0).InnerText, LastName = children.Item(1).InnerText,
					Age = int.Parse(children.Item(2).InnerText), Height = double.Parse(children.Item(3).InnerText)};
				return temp;
			}
			else
			{
				throw new ArgumentException("Given node is not a student node.");
			}
		}
	}
}
