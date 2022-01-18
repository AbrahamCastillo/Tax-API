using Models;
using Helper;
using Newtonsoft.Json.Linq;

namespace TaxClient
{
    public partial class ClientForm : Form
    {
        static HttpClient client = new HttpClient();
        private List<TaxCalculator> taxCalculatorList;

        public ClientForm()
        {
            InitializeComponent();
            taxCalculatorList = Helper.Helper.LoadTaxCalculators();
            LoadCalculators();
        }

        private void LoadCalculators()
        {
            foreach (var taxCalculator in taxCalculatorList)
            {
                comboBox1.Items.Add(new ListViewItem(taxCalculator.Id.ToString(),taxCalculator.Name));
            }
            comboBox1.DataSource = taxCalculatorList;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            tabControl1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = true;
            tabControl1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetTaxRate();
        }

        public async void GetTaxRate()
        {
            var callPath = String.Format("https://localhost:7155/TaxCaller/taxRate?zipCode={0}&country={1}&calculatorId={2}", textBox1.Text, textBox2.Text, comboBox1.SelectedValue);
            var response = await client.GetAsync(callPath);
            Root taxRates = Helper.Helper.LoadTaxRate(await response.Content.ReadAsStringAsync());
            textBox3.Text = Helper.Helper.ObjectToText(taxRates.rate);
        }

        public async void GetTaxes()
        {
            var callPath = String.Format("https://localhost:7155/TaxCaller/calculateTax?country={0}&zipCode={1}&state={2}&destinationCountry={3}&destinationZip={4}&destinationState={5}&amount={6}&shipping={7}&calculatorId={8}", 
                textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, comboBox1.SelectedValue);
            var response = await client.GetAsync(callPath);
            TaxCalculation taxCalculation = Helper.Helper.LoadTaxCalculation(await response.Content.ReadAsStringAsync());
            textBox13.Text = Helper.Helper.ObjectToText(taxCalculation.tax);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetTaxes();
        }
    }
}