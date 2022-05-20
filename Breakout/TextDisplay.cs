namespace Breakout {
    public class TextDisplay {
        protected List<TextField> textFields = new();

        public void AddTextField(TextField textField) {
            textFields.Add(textField);
        }

        public void RenderText() {
            foreach (var tf in textFields) {
                tf.Render();
            }
        }
    }
}