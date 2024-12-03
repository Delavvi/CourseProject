using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace CourseProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string FileName = "tree.txt";
        public Node root = null;
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
            DisplayCards(results);
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

        private Panel CreateCard(Node node)
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
                MessageBox.Show($"Failed to load image from URL: {node.Url}\nError: {ex.Message}");
                pictureBox.Image = SystemIcons.Warning.ToBitmap();
            }

            Label titleLabel = new Label
            {
                Text = node.Name,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Top = 170,
                Left = 10,
                Width = 180,
                AutoSize = false
            };

            Label descriptionLabel = new Label
            {
                Text = $"{node.Value}",
                Top = 200,
                Left = 10,
                Width = 180,
                Height = 80,
                AutoSize = false
            };

            cardPanel.Controls.Add(pictureBox);
            cardPanel.Controls.Add(titleLabel);
            cardPanel.Controls.Add(descriptionLabel);

            return cardPanel;
        }

        private void DisplayCards(List<Node> cards)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            foreach (var card in cards)
            {
                var cardPanel = CreateCard(card);
                flowLayoutPanel.Controls.Add(cardPanel);
            }

            this.Controls.Add(flowLayoutPanel);
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
}

