using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XMLTest
{
	[Serializable]
	public class Child : Person
	{
		public Child()
		{

		}
		public Child(string firstName, string lastName, int age)
		{
			Type = 0;
			FirstName = firstName;
			LastName = lastName;
			Age = age;
		}
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Type", Type);
			info.AddValue("FirstName", FirstName);
			info.AddValue("LastName", LastName);
			info.AddValue("Age", Age);
		}
		public Child(SerializationInfo info, StreamingContext context)
		{
			Type = (int)info.GetValue("Type", typeof(int));
			FirstName = (string)info.GetValue("FirstName", typeof(string));
			LastName = (string)info.GetValue("LastName", typeof(string));
			Age = (int)info.GetValue("Age", typeof(int));
		}
	}
}
