using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.AVG
{
    public class ChoiceButton : VisualElement
    {
        private readonly TemplateContainer templateContainer;
        public new class UxmlFactory : UxmlFactory<ChoiceButton> { }

        public ChoiceButton()
        {
            templateContainer = Resources.Load<VisualTreeAsset>("ViewDocument/ChoiceButton").Instantiate();
            
            hierarchy.Add(templateContainer);
        }

        //  可以为templateContainer的instance增加方法或者改变属性
    }
}
