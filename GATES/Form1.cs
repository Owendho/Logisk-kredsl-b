using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GATES
{
    
    public partial class Form1 : Form
    {

        Gate OR1;
        //Gate OR2;
        Gate NOT;
        Gate AND;
        //Gate AND2;
        


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OR1 = new Gate("OR", inputBit1, inputBit2, null);

            NOT = new Gate("NOT", inputBit3, null, null);

            AND = new Gate("AND", NOT, OR1, outputBit1);
        }
        private void UpdateGates_Tick(object sender, EventArgs e)
        {
            OR1.UpdateGate();
            NOT.UpdateGate();
            //OR2.UpdateGate();
            AND.UpdateGate();

        }
    }
    public class Gate
    {
        private string gateType;
        private object referenceToFirstInput;
        private object referenceToSecondInput;
        public bool GateOutput;
        private RadioButton outputBit;

        //Vores sgate
        public Gate(string temp_gateType, object temp_first, object temp_sekund, RadioButton temp_outputBit)
        {
            gateType = temp_gateType; 
            referenceToFirstInput = temp_first; 
            referenceToSecondInput = temp_sekund; 
            outputBit = temp_outputBit;
        }
        
        public void UpdateGate() //Opdatere vores gate
        {
            //Er vores input som bliver sent til gate. False og True er det samme som 0 og 1
            bool input1 = false;    
            bool input2 = false;
            

            //Hvis 
            if (referenceToFirstInput.GetType() == typeof(CheckBox))
            {
                CheckBox first_converted = (CheckBox)referenceToFirstInput;

                //Hvis first converted = true så bliver input1 true
                if (first_converted.Checked == true) input1 = true;

                //Hvis first converted = false så bliver input1 false
                if (first_converted.Checked == false) input1 = false;
            }

            if (referenceToFirstInput.GetType() == typeof(Gate))
            {
                Gate first_converted = (Gate)referenceToFirstInput;

                if (first_converted.GateOutput == true) input1 = true;
                if (first_converted.GateOutput == false) input1 = false;
            }

            if (referenceToSecondInput != null)
            {
                if (referenceToSecondInput.GetType() == typeof(CheckBox))
                {
                    CheckBox sekund_converted = (CheckBox)referenceToSecondInput;
                    if (sekund_converted.Checked == true) input2 = true;
                    if (sekund_converted.Checked == false) input2 = false;
                }       

                
                if (referenceToSecondInput.GetType() == typeof(Gate))
                {
                    Gate sekund_converted = (Gate)referenceToSecondInput;

                    //Hvis first converted = false så bliver input2 true
                    if (sekund_converted.GateOutput == true) input2 = true;

                    //Hvis first converted = false så bliver input2 false
                    if (sekund_converted.GateOutput == false) input2 = false;
                }
            }
            //Vores AND gate 
            if (gateType == "AND") GateOutput = input1 && input2;
            if (gateType == "OR") GateOutput = input1 || input2;
            if (gateType == "NOT") GateOutput = !input1;
            if (outputBit != null) outputBit.Checked = GateOutput;

        }
    }
}
