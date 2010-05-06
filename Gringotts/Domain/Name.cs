namespace Gringotts.Domain
{
	public class Name
	{
		private string name;

		public Name(string name)
		{
			this.name = name;
		}

		public Name()
		{
		}

		public string GetValue()
		{
			return name;
		}

		public bool Equals(Name other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.name, name);
		}

        public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Name)) return false;
			return Equals((Name) obj);
		}

		public override int GetHashCode()
		{
			return (name != null ? name.GetHashCode() : 0);
		}
	}
}