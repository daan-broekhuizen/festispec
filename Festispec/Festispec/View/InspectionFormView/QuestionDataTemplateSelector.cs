using Festispec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Festispec.View.InspectionFormView;
using Festispec.ViewModel.InspectionFormViewModels;

namespace Festispec.View.InspectionFormView
{
    public class QuestionDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object question, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if(element != null && question != null && question is QuestionViewModel)
            {
                QuestionViewModel q = question as QuestionViewModel;

                switch (q.QuestionType)
                {
                    case "ov":
                        return element.FindResource("OpenQuestionEdit") as DataTemplate;
                    case "av":
                        return element.FindResource("PictureQuestionEdit") as DataTemplate;
                    case "tx":
                        return element.FindResource("TextEdit") as DataTemplate;
                    case "mv":
                        return element.FindResource("MultipleChoiceQuestionEdit") as DataTemplate;
                    case "sv":
                        return element.FindResource("ScaleQuestionEdit") as DataTemplate;
                    case "t2":
                        return element.FindResource("Table2QuestionEdit") as DataTemplate;
                    case "t3":
                        return element.FindResource("Table3QuestionEdit") as DataTemplate;
                    default:
                        return null;
                }
               

            }

            return null;
        }
    }
}
