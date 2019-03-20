using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using HospitalManagementSystem.UI.Admin.UI;
using HospitalManagementSystem.UI.Doctor.UI;
using HospitalManagementSystem.UI.Accountant.UI;
using MetroFramework;
using HospitalManagementSystem.UI.Receptionist.UI;

namespace HospitalManagementSystem.UI.Login.UI
{
    public partial class FormMain : MetroFramework.Forms.MetroForm
    {
        Timer t = new Timer();
        private hmsDataContext DBcon = new hmsDataContext();
        public FormMain()
        {
            InitializeComponent();
            metroTextBox2.PasswordChar = '●';
            metroPanelLoginBody.Hide();
        }
        public static string user = "";
        private void metroTextButton1_Click(object sender, EventArgs e)
        {
            metroPanelLoginBody.Show();
        }

        private void metroTextButtonLogin_Click(object sender, EventArgs e)
        {
            Login_ log = new Login_();
            try
            {
                log = DBcon.Login_s.SingleOrDefault(x => x.LoginUserName == metroTextBox1.Text && x.LoginPassword == metroTextBox2.Text);
                if (log != null)
                {
                    user = log.LoginUserName;
                    this.Hide();
                    metroTextBox1.Clear();
                    metroTextBox2.Clear();
                    var profile = log.Profile.ToString();
                    if(profile == "ADMIN")
                    {
                        var adminForm = new FormAdmin();
                        adminForm.Show();
                    } else if(profile =="DOCTOR")
                    {
                        var doctorForm = new FormDoctor();
                        doctorForm.Show();
                    } else if(profile== "ACCOUNTANT")
                    {
                        var accform = new FormAccountant();
                        accform.Show();
                    } else if(profile == "RECEPTIONIST")
                    {
                        var recform = new FormReceptionist();
                        recform.Show();
                    }
                } else
                {
                    MetroMessageBox.Show(this, "Incorrect Login Details", "Login Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Critical Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
               
            }
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            t.Interval = 1000; // in ms
            t.Tick += new EventHandler(this.timer1_Tick);
            t.Start();
        }
        // Time
        private void timer1_Tick(object sender, EventArgs e)
        {
            string clock = DateTime.Now.ToString("HH:mm:ss");
            string date = DateTime.Now.ToString("dd:MM:yyyy");
            metroTile1.Text = clock;
            metroTile2.Text = date;
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            metroPanelLoginBody.Show();
            metroPanelLoginBody.BringToFront();
        }
    }
}
