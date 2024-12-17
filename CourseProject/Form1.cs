using DocumentFormat.OpenXml;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string FileName = "tree.txt";
       //public string order = "order.txt";

        public Node root = null;
        private List<Node> selectedNodes = new List<Node>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, FileName);

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            TreeCreation();

            List<Node> results = new List<Node>();
            Stack<Node> nodes = new Stack<Node>();
            Node cur = root;

            while (cur != null || nodes.Count > 0)
            {

                while (cur != null)
                {
                    nodes.Push(cur);
                    cur = cur.Left;
                }

                cur = nodes.Pop();
                results.Add(cur);
                cur = cur.Right;
            }
            DisplayCards(results, Tabs, selectedNodes);
        }

        public void TreeCreation()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, FileName);
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string data;
                    string pattern = @"(.+?)\s+(\d+)\s+(.+)";
                    Regex regex = new Regex(pattern);

                    if ((data = reader.ReadLine()) != null)
                    {
                        Match matches = regex.Match(data);
                        if (matches.Success)
                        {
                            root = new Node(matches.Groups[1].Value, int.Parse(matches.Groups[2].Value), matches.Groups[3].Value);
                        }
                        else
                        {
                            MessageBox.Show("Invalid data format in file.");
                            return;
                        }
                    }

                    while ((data = reader.ReadLine()) != null)
                    {
                        Match matches = regex.Match(data);
                        if (matches.Success)
                        {
                            string name = matches.Groups[1].Value;
                            int price = int.Parse(matches.Groups[2].Value);
                            string url = matches.Groups[3].Value;
                            Node node = new Node(name, price, url);
                            Node cur = root;
                            while (cur != null)
                            {
                                if (cur < node)
                                {
                                    if (cur.Right != null)
                                    {
                                        cur = cur.Right;
                                    }
                                    else
                                    {
                                        cur.Right = node;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (cur.Left != null)
                                    {
                                        cur = cur.Left;
                                    }
                                    else
                                    {
                                        cur.Left = node;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid data format in file.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}");
            }
        }
        public class Node
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(string name, int value, string url)
            {
                Name = name;
                Url = url;
                Value = value;
                Left = null;
                Right = null;
            }

            public static bool operator <(Node a, Node b)
            {
                if (a == null || b == null)
                {
                    throw new ArgumentNullException("Cannot compare null Node objects.");
                }
                return a.Value < b.Value;
            }

            public static bool operator >(Node a, Node b)
            {
                if (a == null || b == null)
                {
                    throw new ArgumentNullException("Cannot compare null Node objects.");
                }
                return a.Value > b.Value;
            }
        }



        private Panel CreateCard(Node node, List<Node> selectedNodes)
        {
            Panel cardPanel = new Panel
            {
                Width = 200,
                Height = 300,
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pictureBox = new PictureBox
            {
                Width = 180,
                Height = 150,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var client = new System.Net.WebClient())
                {
                    var imageBytes = client.DownloadData(node.Url);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Failed to load image from URL: {node.Url}\nError: {ex.Message}");
                pictureBox.Image = SystemIcons.Warning.ToBitmap();
            }

            Label titleLabel = new Label
            {
                Text = node.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Top = 170,
                Left = 10,
                Width = 150,
                AutoSize = false
            };

            Label descriptionLabel = new Label
            {
                Text = $"{node.Value}",
                Top = 200,
                Left = 10,
                Width = 180,
                Height = 40,
                AutoSize = false
            };

            CheckBox selectCheckBox = new CheckBox
            {
                Text = "Замовити",
                Top = 240,
                Left = 10,
                Width = 180,
                AutoSize = false
            };

            NumericUpDown Number = new NumericUpDown
            {
                Top = 265,
                Left = 10,
                Width = 180,
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Visible = false,
                Name = $"quantity_{node.Name}"
            };

            selectCheckBox.CheckedChanged += (sender, e) =>
            {
                if (selectCheckBox.Checked)
                {
                    selectedNodes.Add(node);
                    Number.Visible = true;

                }
                else
                {
                    selectedNodes.Remove(node);
                    Number.Visible = false;
                }
            };

            cardPanel.Controls.Add(pictureBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(descriptionLabel);
            cardPanel.Controls.Add(selectCheckBox);
            cardPanel.Controls.Add(Number);
            return cardPanel;
        }



        private void DisplayCards(List<Node> cards, TabControl Tabs, List<Node> selectedNodes)
        {

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            foreach (var card in cards)
            {
                var cardPanel = CreateCard(card, selectedNodes);
                flowLayoutPanel.Controls.Add(cardPanel);
            };

            if (Tabs.TabPages.Count > 0)
            {
                Tabs.TabPages[0].Controls.Add(flowLayoutPanel);
            }
            else
            {
                MessageBox.Show("No tabs available to display cards.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> orderDetails = new List<string>();
            decimal totalSum = 0;

            var flowPanel = Tabs.TabPages[0].Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (flowPanel != null)
            {
                foreach (Panel card in flowPanel.Controls.OfType<Panel>())
                {
                    var checkBox = card.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Text == "Замовити");
                    var quantityBox = card.Controls.OfType<NumericUpDown>().FirstOrDefault();
                    var titleLabel = card.Controls.OfType<Label>().FirstOrDefault(l => l.Font.Bold);
                    var descriptionLabel = card.Controls.OfType<Label>().FirstOrDefault(l => !l.Font.Bold);

                    if (checkBox != null && checkBox.Checked && titleLabel != null && descriptionLabel != null && quantityBox != null)
                    {
                        string itemName = titleLabel.Text;
                        decimal itemPrice;
                        if (decimal.TryParse(descriptionLabel.Text, out itemPrice))
                        {
                            int quantity = (int)quantityBox.Value;
                            decimal itemTotal = itemPrice * quantity;
                            totalSum += itemTotal;

                            orderDetails.Add($"{quantity} {itemName} {itemPrice} ");
                        }
                        else
                        {
                            MessageBox.Show($"Помилка в ціні: {descriptionLabel.Text}");
                        }
                    }
                }

                if (orderDetails.Any())
                {
                    orderDetails.Add($"Сума: {totalSum}");
                    MessageBox.Show(string.Join(Environment.NewLine, orderDetails), "Замовлення");
                    DisplayOrder(orderDetails);
                    orderTOofile();
                }
                else
                {
                    MessageBox.Show("Не вибрано жодної страви.");
                }

                ClearCardSelections(flowPanel);
            }

        }
        private void ClearCardSelections(FlowLayoutPanel flowPanel)
        {
            foreach (Panel card in flowPanel.Controls.OfType<Panel>())
            {
                var checkBox = card.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Text == "Замовити");
                var quantityBox = card.Controls.OfType<NumericUpDown>().FirstOrDefault();

                if (checkBox != null)
                {
                    checkBox.Checked = false;
                }

                if (quantityBox != null)
                {
                    quantityBox.Value = 1;
                    quantityBox.Visible = false;
                }
            }
        }
        private void DisplayOrder(List<string> orderDetails)
        {

            FlowLayoutPanel flowLayoutPanel;

            if (Tabs.TabPages[1].Controls.OfType<FlowLayoutPanel>().Any())
            {
                flowLayoutPanel = Tabs.TabPages[1].Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            }
            else
            {
                flowLayoutPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true
                };

                Tabs.TabPages[1].Controls.Add(flowLayoutPanel);
            }

            var cardPanel = CreateOrderSummaryCard(orderDetails);
            flowLayoutPanel.Controls.Add(cardPanel);
        }



        private Panel CreateOrderSummaryCard(List<string> orderDetails)
        {
            int labelHeight = 25;
            int baseHeight = 50;
            int calculatedHeight = baseHeight + orderDetails.Count * labelHeight;

            Panel CardOrder = new Panel
            {
                Width = 200,
                Height = calculatedHeight,
                BorderStyle = BorderStyle.FixedSingle
            };

            int topOffset = 10;

            foreach (var detail in orderDetails)
            {
                Label detailLabel = new Label
                {
                    Text = detail,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    Top = topOffset,
                    Left = 10,
                    Width = 180,
                    AutoSize = false
                };

                CardOrder.Controls.Add(detailLabel);
                topOffset += labelHeight;
            }

            Button deleteButton = new Button
            {
                Text = "Видалити",
                Top = topOffset + 5,
                Left = 10,
                Width = 180,
                Height = 30
            };

            deleteButton.Click += (s, e) =>
            {
                var parentPanel = deleteButton.Parent;
                parentPanel.Parent.Controls.Remove(parentPanel);
                orderTOofile();
            };

            CardOrder.Controls.Add(deleteButton);

            return CardOrder;
        }

        private void orderTOofile()
        {
            var flowPanel = Tabs.TabPages[1].Controls.OfType<FlowLayoutPanel>().FirstOrDefault();

            if (flowPanel != null)
            {
                List<string> updatedOrderDetails = new List<string>();

                foreach (Panel orderPanel in flowPanel.Controls.OfType<Panel>())
                {
                    var detailLabels = orderPanel.Controls.OfType<Label>().Where(l => l.Font.Style == FontStyle.Regular).ToList();

                    foreach (var label in detailLabels)
                    {
                        updatedOrderDetails.Add(label.Text);
                    }
                    updatedOrderDetails.Add(string.Empty);
                }
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "OrderDetails.txt");

                try
                {
                    File.WriteAllLines(filePath, updatedOrderDetails);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося записати дані у файл: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Немає даних для збереження.");
            }
        }
    }

}

