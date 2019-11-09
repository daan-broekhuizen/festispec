using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec
{
    public class ViewFactory
    {
        public IEnumerable<string> ViewNames => _views.Keys;

        private Dictionary<string, UserControl> _views;

        public ViewFactory()
        {
            _views = new Dictionary<string, UserControl>();
            //test menu views
            _views["Customers"] = new Page1();
            _views["Dashboard"] = new HomeWindow();
        }

        public UserControl GetView(string name)
        {
            if (!_views.ContainsKey(name)) return null;
            return _views[name];
        }

        
    }
}
