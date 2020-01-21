using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeDistributor {
    public class Key {
        private double _balance;


        public Key(double balance) {
            _balance = balance;
        }

        public bool buyDrink(double cost) {
            if (_balance < cost) return false;
            _balance -= cost;
            return true;
        }

        public double Balance => _balance;

    }
}
