using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XMLTest
{
	[Serializable]
	public abstract class Person : ISerializable
	{
		public int Type;
		public string FirstName;
		public string LastName;
		public int Age;

		public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
