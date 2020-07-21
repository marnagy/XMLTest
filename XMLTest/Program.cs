using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLTest
{
	class Program
	{
		static string filename = "students.xml";
		static void Main(string[] args)
		{
			WriteXML();
			ReadXML();
			AddStudent();
		}

		private static void AddStudent()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			XmlElement student = doc.CreateElement("foo");

			XmlElement FirstName = doc.CreateElement("FirstName");
			FirstName.InnerText = "John";

			XmlElement LastName = doc.CreateElement("LastName");
			LastName.InnerText = "Constantine";

			XmlElement Age = doc.CreateElement("Age");
			Age.InnerText = "24";

			XmlElement Height = doc.CreateElement("Height");
			Height.InnerText = "184.7";

			student.AppendChild(FirstName);
			student.AppendChild(LastName);
			student.AppendChild(Age);
			student.AppendChild(Height);

			doc.DocumentElement.AppendChild(student);
			doc.Save(filename);
		}

		private static void ReadXML()
		{
			List<Student> list = new List<Student>();

			XmlDocument Xdoc = new XmlDocument();
			Xdoc.Load(filename);

			foreach (XmlNode childNode in Xdoc.DocumentElement.ChildNodes)
			{
				if (childNode.Name == Student.XMLElementName)
				{
					foreach (XmlNode item in childNode.ChildNodes)
					{
						Console.WriteLine($"{item.Name}: {item.InnerText}");
					}
					list.Add( Student.FromXml(childNode) );
				}
			}
			Console.WriteLine($"List length: {list.Count}");
		}

		private static void WriteXML()
		{
			List<Student> list = new List<Student>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(new Student {
					FirstName = "Greg" + i, LastName = "Smith" + i,
					Age = (i*20 + 12) % 30, Height = i * 50 + 0.5});
			}

			XmlTextWriter xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);

			xmlWriter.Formatting = Formatting.Indented;
			xmlWriter.WriteStartDocument();

			xmlWriter.WriteStartElement("Students");

			foreach (Student stud in list)
			{
				stud.Write(xmlWriter);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
	}
}
