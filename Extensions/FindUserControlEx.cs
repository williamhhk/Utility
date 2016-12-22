using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Extensions
{
    // http://stackoverflow.com/questions/14386572/how-can-i-call-method-from-usercontrol-from-another-usercontrol-using-asp-net
    public static class FindUserControlEx
    {
        public static T FindUserControl<T>(this Control parent) where T : Control
        {
            T control = parent as T;
            if (control != null) return control;
            foreach(Control Control in parent.Controls)
            {
                control = Control.FindUserControl<T>();
                if (control != null) return control;
            }
            return null;
        }
    }
}
