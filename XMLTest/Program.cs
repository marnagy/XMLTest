using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLTest
{
	class Program
	{
		static string Xmlfilename = "students.xml";
		static string Binfilename = "students.dat";
		static int port = 2000;
		static void Main(string[] args)
		{
			//WriteXML();
			//ReadXML();
			//AddStudent();

			WriteBinary();
			ReadBinary();

			Thread server = new Thread(new ThreadStart(() => RunServer()));
			server.IsBackground = true;
			server.Start();

			Thread client = new Thread(new ThreadStart(() => RunClient()));
			client.IsBackground = false;
			client.Start();
		}
		/// <summary>
		/// Test for sending multiple objects instead of whole collection
		/// </summary>
		private static void RunClient()
		{
			List<Person> list;

			using (Stream stream = new StreamReader(Binfilename).BaseStream)
			{
				var serializer = new BinaryFormatter();
				list = (List<Person>)serializer.Deserialize(stream);
			}

			using (TcpClient client = new TcpClient("localhost", port))
			using (Stream stream = new StreamReader(client.GetStream()).BaseStream)
			{
				var bf = new BinaryFormatter();
				foreach (var stud in list)
				{
					bf.Serialize(stream, stud);
				}
			}

		}

		private static void RunServer()
		{
			TcpListener listener = new TcpListener(port);

			listener.Start();
			while (true)
			{
				using (TcpClient client = listener.AcceptTcpClient())
				using (Stream stream = new StreamReader(client.GetStream()).BaseStream)
				{
					var bf = new BinaryFormatter();
					Console.WriteLine("Server");
					try
					{
						while (true)
						{
							var person = (Person)bf.Deserialize(stream);
							var type = person.GetType();
							if (type == typeof(Student))
							{
								Console.WriteLine($"Type Student -> {person.FirstName} {person.LastName}");
							}
							else if (type == typeof(Child))
							{
								Console.WriteLine($"Type Child -> {person.FirstName} {person.LastName}");
							}
						}
					}
					catch (SerializationException)
					{

					}
				}
			}
		}

		private static void ReadBinary()
		{
			List<Person> list;
			using( Stream stream = File.Open(Binfilename, FileMode.Open))
			{
				var bf = new BinaryFormatter();
				list = (List<Person>)bf.Deserialize(stream);
			}

			Console.WriteLine("Binary");
			foreach (var person in list)
			{
				Console.WriteLine($"Type {person.GetType()}-{person.Type} -> {person.FirstName} {person.LastName}");
			}
		}

		private static void WriteBinary()
		{
			List<Person> list = new List<Person>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(new Student(
					firstName: "Greg" + i, lastName: "Smith" + i,
					age: (i*20 + 12) % 30, university: "UK"));
			}
			for (int i = 0; i < 5; i++)
			{
				list.Add(new Child(
					firstName: "Peter" + i, lastName: "Smith" + i,
					age: (i*20 + 12) % 30));
			}

			using (Stream stream = File.Open(Binfilename, FileMode.Create))
			{
				new BinaryFormatter().Serialize(stream, list);
			}
		}

		private static void AddStudent()
		{
			List<Person> list;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

			using (TextReader tr = new StreamReader(Xmlfilename))
			{
				list = (List<Person>)xmlSerializer.Deserialize(tr);
			}

			list.Add(new Student("John", "Constantine", 24, "UK"));

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
			List<Person> list = new List<Person>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(new Student(
					firstName: "Greg" + i, lastName: "Smith" + i,
					age: (i*20 + 12) % 30, university: "UK"));
			}

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

			using (TextWriter tw = new StreamWriter(Xmlfilename))
			{
				xmlSerializer.Serialize(tw, list);
			}
		}
	}
}
