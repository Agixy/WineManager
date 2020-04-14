using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WineDto
    {
        public int Id { get; set; }
        public string Nr { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string LocationId { get; set; }

        public WineDto(int id, string name, string skrot = null)
        {
            Id = id;
            Name = name;
            Nr = skrot;
        }
    }
}
