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
            templateContainer.style.flexGrow = 1.0f;
            hierarchy.Add(templateContainer);
        }

        public ChoiceButton(string content) : this()
        {
            templateContainer.Q<Label>().text = content;
        }

        //  可以为templateContainer的instance增加方法或者改变属性
    }
}
