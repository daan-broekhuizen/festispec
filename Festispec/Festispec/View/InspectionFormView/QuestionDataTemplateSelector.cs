using Festispec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Festispec.View.InspectionFormView;

namespace Festispec.View.InspectionFormView
{
    public class QuestionDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object vraag, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if(element != null && vraag != null && vraag is Vraag)
            {
                Vraag v = vraag as Vraag;

                switch (v.Vraagtype)
                {
                    case "ov":
                        return element.FindResource("OpenQuestionEdit") as DataTemplate;

                    default:
                        return null;
                }
               

            }

            return null;
        }
    }
}
