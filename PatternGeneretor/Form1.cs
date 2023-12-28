namespace PatternGeneretor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = GeneratePattern(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string GeneratePattern(string input)
        {
            // đọc từng dòng
            string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // list để lưu mẫu conver
            List<string> convertedBytes = new List<string>();

            foreach (string line in lines)
            {
                // chia thành từng phần và bỏ qua nội dung địa chỉ
                string[] chunks = line.Split(" - ");

                // nhận byte ở định dạng chuỗi và xóa ký hiệu dòng mới
                string[] bytes = chunks[1].Replace("\r", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // lặp qua byte và thay thế các địa chỉ tiềm năng bằng ký tự đại diện
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i].Length == 8) // giả sử địa chỉ là 8 ký tự
                        convertedBytes.Add("?? ?? ?? ??"); // ký tự đại diện
                    else if (bytes[i] == "00") // loại bỏ các giá trị 00
                        convertedBytes.Add("??");
                    else // chia các byte cuối cùng thành từng cặp và thêm phần còn lại
                    {
                        for (int j = 0; j < bytes[i].Length; j += 2)
                        {
                            convertedBytes.Add(bytes[i].Substring(j, 2));
                        }
                    }
                }
            }
            return String.Join(" ", convertedBytes.ToArray());
        }
    }
}