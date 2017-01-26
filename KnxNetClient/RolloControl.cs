using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeData;
using EIBDef;
using System.Threading;

namespace Knx
{

    public partial class RolloControl : UserControl
    {
        public delegate void SendKnxDelegate(cEMI emi);

        private SendKnxDelegate Send;
        private EIB_Adress eibAdress_AufAb;
        private EIB_Adress eibAdress_Lamelle;



        public RolloControl()
        {
            InitializeComponent();
        }

        public string EibAdress_Lamelle
        {
            get { return eibAdress_Lamelle.ToString(); }
            set { eibAdress_Lamelle = new EIB_Adress(value); }
        }

        public string EibAdress_AufAb
        {
            get { return eibAdress_AufAb.ToString(); }
            set { eibAdress_AufAb = new EIB_Adress(value);  }
        }

        public string Titel
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }


        public void SetKnxSendFunction(SendKnxDelegate send)
        {
            this.Send = send;
        }

        private void btn_auf_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_AufAb, false);
            Send(emi);
        }

        private void btn_ab_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_AufAb, true);
            Send(emi);
        }


        private void btn_Lamelle_Korr_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_AufAb, false);
            Send(emi);
            Thread.Sleep(30000);
            emi.Eis1 = true;
            emi = new cEMI(eibAdress_Lamelle, true);
            for (int i = 0; i < 7; i++)
            {
                Send(emi);
                Thread.Sleep(400);
            }
        }

        private void btn_Lamelle_auf_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_Lamelle, false);
            Send(emi);
        }

        private void btn_Lamelle_ab_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_Lamelle, true);
            Send(emi);
        }
    }
}
