using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UIElement
{
    [UxmlElement]
    public partial class Image : VisualElement
    {
        [UxmlAttribute]
        public Sprite ImageSprite
        {
            get => _image.sprite;
            set
            {
                _image.sprite = value;
            }
        }

        private readonly UnityEngine.UIElements.Image _image;

        public Image()
        {
            _image = new UnityEngine.UIElements.Image();

            Add(_image);
        }
    }
}
