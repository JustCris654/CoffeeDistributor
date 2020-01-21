using System;

namespace CoffeeDistributor {
    public class Distributor {
        private int    _maxcoffee;
        private int    _maxThe;
        private int    _maxCups;
        private int    _maxSugar;
        private int    _coffeeCapsules;
        private int    _theCapsules;
        private int    _cups;
        private int    _sugarCubes;
        private string _psw;
        private bool   _outOfOrder;
        private int[]  _supplied;
        public readonly double _cost = 0.30f;        //costo the e caffe
        public readonly double _waterCost = 0.15f;   //costo acqua

        public Distributor(int coffeeCapsules, int theCapsules, int cups, int sugarCubes, string psw) {
            _supplied            = new[] {0, 0, 0};
            _maxcoffee           = 333;
            _maxThe              = 333;
            _maxCups             = 333;
            _maxSugar            = 333;
            this._coffeeCapsules = coffeeCapsules;
            this._theCapsules    = theCapsules;
            this._cups           = cups;
            this._sugarCubes     = sugarCubes;
            this._psw            = psw;
        }

        public Distributor(string psw) : this(0, 0, 0, 0, psw) {
        }

        public bool IsCoffeeAvaible() {
            return _coffeeCapsules > 0;
        }

        public bool IsTheAvaible() {
            return _theCapsules > 0;
        }

        public bool IsCupsAvaible() {
            return _cups > 0;
        }

        public bool IsSugarAvaible() {
            return _sugarCubes > 0;
        }

        public bool SupplyCoffee(Key key) {
            if (key.buyDrink(_cost)) return false;
            if (!IsCoffeeAvaible() || !IsSugarAvaible() || !IsCupsAvaible()) return false;
            _sugarCubes--;
            _cups--;
            _coffeeCapsules--;
            _supplied[0]++;
            _supplied[2]++;
            return true;
        }

        public bool SupplyCoffee(Key key,  int sugarCubes) {
            if (sugarCubes < 0 || sugarCubes > 2) return false;
            if (_sugarCubes < sugarCubes) return false;
            if (!SupplyCoffee(key)) return false;
            _sugarCubes -= sugarCubes - 1;
            return true;
        }

        public bool SupplyThe(Key key) {
            if (key.buyDrink(_cost)) return false;
            if (!IsTheAvaible() || !IsSugarAvaible() || !IsCupsAvaible()) return false;
            _sugarCubes--;
            _cups--;
            _theCapsules--;
            _supplied[1]++;
            _supplied[2]++;
            return true;
        }

        public bool SupplyThe(Key key, int sugarCubes) {
            if (_sugarCubes < sugarCubes) return false;
            if (!SupplyThe(key)) return false;
            _sugarCubes -= sugarCubes - 1;
            return true;
        }

        public bool SupplyWater(Key key) {
            if (key.buyDrink(_waterCost)) return false;
            if (!IsCupsAvaible()) return false;
            _cups--;
            _supplied[2]++;
            return true;
        }

        public bool Refill(string password, int coffeeCapsules, int theCapsules, int cups, int sugarCubes) {
            if (String.Compare(password, _psw) != 0) return false;
            if (_coffeeCapsules + coffeeCapsules >= _maxcoffee) _coffeeCapsules =  _maxcoffee;
            else _coffeeCapsules                                                += coffeeCapsules;

            if (_theCapsules + theCapsules >= _maxThe) _theCapsules =  _maxThe;
            else _theCapsules                                       += theCapsules;

            if (_cups + cups >= _maxCups) _cups =  _maxCups;
            else _cups                          += cups;

            if (_sugarCubes + sugarCubes >= _maxSugar) _sugarCubes =  _maxSugar;
            else _sugarCubes                                       += sugarCubes;

            return true;
        }

        public bool SetOutOfOrder(string password) {
            if (String.Compare(password, _psw) != 0) return false;
            _outOfOrder = true;
            return true;
        }

        public bool IsOutOfOrder => _outOfOrder;

        public bool ChangePassword(string password, string newPassword) {
            if (String.Compare(password, _psw) != 0) return false;
            _psw = newPassword;
            return true;
        }

        public override string ToString() {
            return "Quantità caffè:     " + _coffeeCapsules             +
                   "Quantità the:       " + _theCapsules                +
                   "Quantità zucchero:  " + _sugarCubes                 +
                   "Quantità bicchieri: " + _cups                       +
                   "E' fuori uso:       " + (_outOfOrder ? "si" : "no") +
                   "Prodotti erogati:   " + $"Caffè: {_supplied[0]} The: {_supplied[1]} Zucchero: {_supplied[2]}";
        }
    }
}
