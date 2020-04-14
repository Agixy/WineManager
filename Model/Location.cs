using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentState { get; set; }
        public int MaxState { get; set; }
    }
}
