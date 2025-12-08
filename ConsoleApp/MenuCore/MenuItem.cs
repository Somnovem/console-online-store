namespace ConsoleApp.MenuCore
{
    public class MenuItem
    {
        private readonly string caption;
        private readonly Action action;

        public MenuItem(string caption, Action action)
        {
            this.caption = caption;
            this.action = action;
        }

        public string Caption
        {
            get { return this.caption; }
        }

        public Action Action
        {
            get { return this.action; }
        }

        public override string ToString()
        {
            return this.caption;
        }
    }
}