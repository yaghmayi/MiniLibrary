using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniLibrary.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Category Category { get; set; }
	}
}
