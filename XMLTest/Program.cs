using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XMLTest
{
	class Program
	{
		static string Xmlfilename = "students.xml";
		static string Binfilename = "students.dat";
		static void Main(string[] args)
		{
			WriteXML();
			ReadXML();
			AddStudent();

			WriteBinary();
			ReadBinary();
		}

		private static void ReadBinary()
		{
			List<Student> list;
			using( Stream stream = File.Open(Binfilename, FileMode.Open))
			{
				var bf = new BinaryFormatter();
				list = (List<Student>)bf.Deserialize(stream);
			}

			Console.WriteLine("Binary");
			foreach (var stud in list)
			{
				Console.WriteLine(stud.FirstName);
			}
		}

		private static void WriteBinary()
		{
			List<Student> list = new List<Student>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(new Student {
					FirstName = "Greg" + i, LastName = "Smith" + i,
					Age = (i*20 + 12) % 30, Height = i * 50 + 0.5});
			}

			using (Stream stream = File.Open(Binfilename, FileMode.Create))
			{
				new BinaryFormatter().Serialize(stream, list);
			}
		}

		private static void AddStudent()
		{
			List<Student> list;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

			using (TextReader tr = new StreamReader(Xmlfilename))
			{
				list = (List<Student>)xmlSerializer.Deserialize(tr);
			}

			list.Add(new Student { FirstName = "John", LastName = "Constantine",
				Age = 24, Height = 184.7});

			using (TextWriter tw = new StreamWriter(Xmlfilename))
			{
				xmlSerializer.Serialize(tw, list);
			}
		}

		private static void ReadXML()
		{
			List<Student> list;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

			using (TextReader tr = new StreamReader(Xmlfilename))
			{
				list = (List<Student>)xmlSerializer.Deserialize(tr);
			}

			Console.WriteLine("XML");
			foreach (var stud in list)
			{
				Console.WriteLine(stud.FirstName);
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

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

			using (TextWriter tw = new StreamWriter(Xmlfilename))
			{
				xmlSerializer.Serialize(tw, list);
			}
		}
	}
}
