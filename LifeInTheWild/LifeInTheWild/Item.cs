using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInTheWild
{
    public class Item
    {

        public string Name;
        public int Quantity;

        public Item(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public string getName()
        {
            return this.Name;
        }

        public int getQuantity()
        {
            return this.Quantity;
        }

        public void addQuantity(int a)
        {
            this.Quantity += a;
        }

    }
}
