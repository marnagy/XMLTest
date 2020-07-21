using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace XMLTest
{	[Serializable]
	public struct Student : ISerializable
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

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("FirstName", FirstName);
			info.AddValue("LastName", LastName);
			info.AddValue("Age", Age);
			info.AddValue("Height", Height);
		}
		public Student(SerializationInfo info, StreamingContext context)
		{
			FirstName = (string)info.GetValue("FirstName", typeof(string));
			LastName = (string)info.GetValue("LastName", typeof(string));
			Age = (int)info.GetValue("Age", typeof(int));
			Height = (double)info.GetValue("Height", typeof(double));
		}
	}
}
