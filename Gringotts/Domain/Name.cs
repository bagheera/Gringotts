using System;

namespace Gringotts.Domain
{
	public class Name
	{
		private readonly string name;

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
	}
}