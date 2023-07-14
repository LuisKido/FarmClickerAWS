using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using System.Security.Policy;
using FarmClicker.Models;
using Amazon;
using Microsoft.VisualBasic.ApplicationServices;
using User = FarmClicker.Models.User;

namespace FarmClicker
{
    public partial class Main : Form
    {

        private User _user;
        private Farm _farm;

        public Main()
        {
            InitializeComponent();

        }

        public async void cargaDatosAsync()
        {
            // Crear el contexto de DynamoDB
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1 // Reemplaza con la región de tu tabla
            };
            var client = new AmazonDynamoDBClient(config);
            var context = new DynamoDBContext(client);

            // Definir la clave del elemento que quieres cargar
            string userId = "1"; // Reemplaza con el UserId real

            // Cargar el usuario
            _user = await context.LoadAsync<User>(userId);

            // Ahora puedes acceder a las propiedades de 'user'
            lblUser.Text = _user.UserName;
            lblCreationDate.Text = _user.CreationDate.ToString();
            lblEmail.Text = _user.Email;
            lblScore.Text = _user.TotalScore.ToString();

            var farms = await context.QueryAsync<Farm>(userId).GetRemainingAsync();

            // Ahora 'farms' es una lista de las granjas del usuario
            foreach (var farm in farms)
            {
                _farm = farm;
                lblFarmType.Text = farm.FarmType;
                lblFarmLevel.Text = farm.Level.ToString();
                lblFarmScore.Text = farm.CurrentScore.ToString();
                // etc...
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cargaDatosAsync();
        }

        private void btnFarmClick_Click(object sender, EventArgs e)
        {
            IncrementFarmClick("fb8ba256-d076-4ad4-986e-9c8a4d324ef6", "1");
        }

        private async void IncrementFarmClick(string farmId, string userId)
        {

            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1 // Reemplaza con la región de tu tabla
            };
            var client = new AmazonDynamoDBClient(config);
            var context = new DynamoDBContext(client);

            // Cargar la granja y el usuario desde la base de datos
            //var farm = await context.LoadAsync<Farm>(userId, farmId);
            //var user = await context.LoadAsync<User>(userId);

            // Incrementar la puntuación de la granja y el usuario
            int produce = _farm.ProducePerClick;
            _farm.CurrentScore += produce;
            _user.TotalScore += produce;
            _farm.Level = _farm.Level+1;

            // Guardar los cambios en la base de datos
            await context.SaveAsync(_farm);
            await context.SaveAsync(_user);

            // Actualizar la interfaz de usuario para reflejar los cambios
            // (deberías reemplazar esto con el código para actualizar tu interfaz de usuario)
            lblFarmScore.Text = _farm.CurrentScore.ToString();
            lblFarmLevel.Text = _farm.Level.ToString();
            lblScore.Text = _user.TotalScore.ToString();

 
        }
    }
}