using System;

namespace Events
{
    public class Cat
    {
        private int health;

        public int Id { get; set; }

        public string Name { get; set; }

        public int Health 
        {
            get => this.health;
            set
            {
                this.health = value;
                this.OnHealthChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<int> OnHealthChanged;
    }
}
