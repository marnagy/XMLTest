using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace XMLTest
{
	[Serializable]
	public class Student : Person
	{
		public const string XMLElementName = "Student";
		public string University;

		public Student()
		{
			Type = 1;
		}

		public Student(string firstName, string lastName, int age, string university)
		{
			Type = 1;
			FirstName = firstName;
			LastName = lastName;
			Age = age;

		}

		//public override void GetObjectData(SerializationInfo info, StreamingContext context)
		//{
		//	info.AddValue("FirstName", FirstName);
		//	info.AddValue("LastName", LastName);
		//	info.AddValue("Age", Age);
		//	info.AddValue("University", University);
		//}
		public Student(SerializationInfo info, StreamingContext context)
		{
			Type = (int)info.GetValue("Type", typeof(int));
			FirstName = (string)info.GetValue("FirstName", typeof(string));
			LastName = (string)info.GetValue("LastName", typeof(string));
			Age = (int)info.GetValue("Age", typeof(int));
			University = (string)info.GetValue("University", typeof(string));
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Type", Type);
			info.AddValue("FirstName", FirstName);
			info.AddValue("LastName", LastName);
			info.AddValue("Age", Age);
			info.AddValue("University", University);
		}
	}
}
