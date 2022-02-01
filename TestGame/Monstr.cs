using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class Monstr
    {
        private int _HP { get; set; }
        private int _Damage { get; set; }
        private int Xmonstr { get; set; }
        private int Ymonstr { get; set; }
        public Monstr(int Hp, int damage, int x, int y) {
            _HP = Hp;
            _Damage = damage;
            Xmonstr = x;
            Ymonstr = y;
        }

        public bool haveMonstr(int x, int y) {
            if (x == Xmonstr && y == Ymonstr)
            {
                return true;
            }
            else return false;
        }
        public int addDamage(int attakPerson) {
            _HP += attakPerson;
            return _HP;
        }
        public int attack() {
            return _Damage;
        }
    }
}
