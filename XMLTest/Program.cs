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
			//WriteXML();
			ReadXML();
		}

		private static void ReadXML()
		{
			List<Student> list = new List<Student>();

			XmlDocument Xdoc = new XmlDocument();
			Xdoc.Load(filename);

			foreach (XmlNode childNode in Xdoc.DocumentElement.ChildNodes)
			{
				//Console.WriteLine(childNode.Name);
				if (childNode.Name == "Student")
				{

					foreach (XmlNode item in childNode.ChildNodes)
					{
						Console.WriteLine($"{item.Name}: {item.InnerText}");
					}
				}
			} 
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
				xmlWriter.WriteStartElement("Student");

				xmlWriter.WriteElementString("FirstName", stud.FirstName);
				xmlWriter.WriteElementString("LastName", stud.LastName);
				xmlWriter.WriteElementString("Age", stud.Age.ToString());
				xmlWriter.WriteElementString("Height", stud.Height.ToString());

				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
	}
}
