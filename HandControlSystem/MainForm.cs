using Common;
using Model;
using S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandControlSystem
{
    public partial class MainForm : Form
    {
        private GlobalSystemConfig mConfig;

        private static List<FunMapper> listFunMapper;

        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            InitConfig();
            WatchValue();
        }

        public void InitConfig()
        {
            try
            {
                cmbModelType.Source = CommonFun.GetSource(Enum.GetNames(typeof(CpuType)));
                mConfig = JsonHelper.DeserializeObject<GlobalSystemConfig>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "app.json"));
                txtIpaddress.InputText = mConfig.Ipaddress;
                txtMachineTypeNo.InputText = mConfig.MachineTypeNo;
                txtPort.InputText = mConfig.Port;
                txtSlotNo.InputText = mConfig.SlotNo;
                cmbModelType.SelectedValue = mConfig.ModelType;
                txtRepeatTimes.InputText = mConfig.RepeatTimes.ToString();
                txtPostionValue.InputText = mConfig.PostionValue.ToString();

            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveConfig()
        {
            try
            {
                var temp = txtPostionValue.InputText ?? null;
                mConfig = new GlobalSystemConfig
                {
                    Ipaddress = txtIpaddress.InputText.Trim(),
                    MachineTypeNo = txtMachineTypeNo.InputText.Trim(),
                    Port = txtPort.InputText.Trim(),
                    SlotNo = txtSlotNo.InputText.Trim(),
                    ModelType = cmbModelType.SelectedValue.Trim()
                };
                if (!string.IsNullOrEmpty(txtPostionValue.InputText))
                    mConfig.PostionValue = Convert.ToDouble(txtPostionValue.InputText);
                if (!string.IsNullOrEmpty(txtRepeatTimes.InputText))
                    mConfig.RepeatTimes = Convert.ToInt32(txtRepeatTimes.InputText);
                var content = JsonHelper.SerializeObject(mConfig);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "app.json", content);
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void SetContorlEnable(bool enable)
        {
            txtIpaddress.Enabled = enable;
            txtPort.Enabled = enable;
            cmbModelType.Enabled = enable;
            txtMachineTypeNo.Enabled = enable;
            txtSlotNo.Enabled = enable;
            btnConnect.Enabled = enable;
            ledPlcStatus.Value = !enable;
            btnDisConnect.Enabled = !enable;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();
                CpuType cpuType = S7Helper.GetCpuType(mConfig.ModelType);
                Task.Run(() =>
                {
                    S7Helper.GetPlc(cpuType, mConfig.Ipaddress, int.Parse(mConfig.Port), short.Parse(mConfig.MachineTypeNo), short.Parse(mConfig.SlotNo));
                    S7Helper.mPlc.Open();
                    if (S7Helper.mPlc.IsConnected)
                    {
                        SetContorlEnable(false);
                        MessageBox.Show("连接成功");
                    }
                    else
                    {
                        MessageBox.Show("连接失败");
                    }
                });
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (S7Helper.mPlc.IsConnected)
            {
                S7Helper.mPlc.Close();
                SetContorlEnable(true);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lbl_CurrentTime.Text = "当前时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void WatchValue()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (S7Helper.mPlc != null && S7Helper.mPlc.IsConnected)
                        {
                            txtCurrent1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtCurrent2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.2", DataTypeEnum.Bit);
                            txtCurrent3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.3", DataTypeEnum.Bit);
                            txtCurrent4.Text = S7Helper.ReadValueByVariable("DB1.DBX0.4", DataTypeEnum.Bit);
                            txtCurrent5.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtCurrent6.Text = S7Helper.ReadValueByVariable("DB1.DBW14", DataTypeEnum.Int);

                            txtFirstJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFirstJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFirstJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtSecondJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtSecondJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtSecondJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtThirdJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtThirdJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtThirdJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtFourthJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFourthJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFourthJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtFifthJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFifthJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtFifthJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtSTDJG1.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtSTDJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtSTDJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtWegabschnittJG1.Text= S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtWegabschnittJG2.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);
                            txtWegabschnittJG3.Text = S7Helper.ReadValueByVariable("DB1.DBX0.1", DataTypeEnum.Bit);

                            txtFirstLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFirstLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFirstLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtSecondLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtSecondLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtSecondLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtThirdLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtThirdLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtThirdLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtFourthLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFourthLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFourthLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtFifthLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFifthLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtFifthLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtSTDLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtSTDLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtSTDLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);

                            txtWegabschnittLS1.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtWegabschnittLS2.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                            txtWegabschnittLS3.Text = S7Helper.ReadValueByVariable("DB1.DBD2", DataTypeEnum.Real);
                        }
                    }
                    catch
                    {
                        await Task.Delay(2000);
                    }
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //var listFunMapper2 = new List<FunMapper>();
            //listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Up, DataTypeEnum = DataTypeEnum.Bit, OperationTypeEnum = OperationTypeEnum.Hold, Variable = "DB1.DBX0.1", FirstClickOrDownValue = 1, SecondClickOrUpValue = 0 });//上升
            //listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.StartTest, DataTypeEnum = DataTypeEnum.Real, OperationTypeEnum = OperationTypeEnum.Switch, Variable = "DB1.DBD2", FirstClickOrDownValue = 1, SecondClickOrUpValue = 2 });//开始测试
            //listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.TestReset, DataTypeEnum = DataTypeEnum.Int, OperationTypeEnum = OperationTypeEnum.Switch, Variable = "DB1.DBW14", FirstClickOrDownValue = 7, SecondClickOrUpValue = 8 });//测试复位

            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Up, DataTypeEnum = DataTypeEnum.Bit, OperationTypeEnum = OperationTypeEnum.Hold, Variable = "DB1.DBX0.1", FirstClickOrDownValue = 1, SecondClickOrUpValue = 0 });//上升
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Down, DataTypeEnum = DataTypeEnum.Real, Variable = "DB1.DBD2", FirstClickOrDownValue = 1.1, SecondClickOrUpValue = 6.0 });//下降
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Position, DataTypeEnum = DataTypeEnum.Int, Variable = "DB1.DBD14", FirstClickOrDownValue = 7, SecondClickOrUpValue = 8 });//定位
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Origin, DataTypeEnum = DataTypeEnum.Bit, Variable = "DB1.DBX0.3", MouseDownValue = 1, MouseUpValue = 0 });//原点
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.Reset, DataTypeEnum = DataTypeEnum.Bit, Variable = "DB1.DBX0.4", MouseDownValue = 1, MouseUpValue = 0 });//复位
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.StartTest, DataTypeEnum = DataTypeEnum.Real, Variable = "DB1.DBD2", MouseDownValue = 1.1, MouseUpValue = 2.2 });//开始测试
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.TestReset, DataTypeEnum = DataTypeEnum.Int, Variable = "DB1.DBW14", MouseDownValue = 9, MouseUpValue = 10 });//测试复位
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.TestGrap, DataTypeEnum = DataTypeEnum.Int, Variable = "DB10.DBX.0.0", MouseDownValue = "1", MouseUpValue = null });//测试气抓
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.GrapPrepareOk, DataTypeEnum = DataTypeEnum.Int, Variable = "DB10.DBX.0.0", MouseDownValue = "1", MouseUpValue = null });//气抓准备OK
            //////listFunMapper2.Add(new FunMapper { FunName = FunNameEnum.ClearData, DataTypeEnum = DataTypeEnum.Int, Variable = "DB10.DBX.0.0", MouseDownValue = "1", MouseUpValue = null });//清除数据
            //var str = JsonHelper.SerializeObject(listFunMapper2);
            listFunMapper = JsonHelper.DeserializeObject<List<FunMapper>>(
                File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "FunMapper.json"));

        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Up, MouseEnum.MouseDownValue);
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Up, MouseEnum.MouseUpValue);
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Down, MouseEnum.MouseDownValue);
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Down, MouseEnum.MouseUpValue);
        }

        private void btnPosition_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Position, MouseEnum.MouseDownValue);
        }

        private void btnOrigin_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Origin, MouseEnum.MouseDownValue);
        }

        private void btnOrigin_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Origin, MouseEnum.MouseUpValue);
        }

        private void btnReset_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Reset, MouseEnum.MouseDownValue);
        }

        private void btnReset_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownOrUpEventHandler(FunNameEnum.Reset, MouseEnum.MouseUpValue);
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            ClickSwitchEventHandler(FunNameEnum.StartTest);
        }

        private void btnTestReset_Click(object sender, EventArgs e)
        {
            ClickSwitchEventHandler(FunNameEnum.TestReset);
        }

        public void MouseDownOrUpEventHandler(FunNameEnum funNameEnum, MouseEnum mouseEnum)
        {
            try
            {
                var temp = listFunMapper.FindAll(f => f.FunName == funNameEnum).FirstOrDefault();
                if (temp != null)
                {
                    var value = mouseEnum == MouseEnum.MouseDownValue ? temp.FirstClickOrDownValue : temp.SecondClickOrUpValue;
                    S7Helper.SetWriteValueByVariable(temp.Variable, temp.DataTypeEnum, value);
                }
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                MessageBox.Show(ex.Message.ToString() + "【请确认PLC是否已连接！若已连接可尝试断开后重新连接】");
            }
        }


        public void ClickSwitchEventHandler(FunNameEnum funNameEnum)
        {
            try
            {
                var temp = listFunMapper.FindAll(f => f.FunName == funNameEnum).FirstOrDefault();
                if (temp != null)
                {
                    var firstClickOrDownValue = temp.FirstClickOrDownValue;
                    var secondClickOrUpValue = temp.SecondClickOrUpValue;
                    var lastSendToPlcValue = temp.LastSendToPlcValue;
                    var currentSendToValue = firstClickOrDownValue != lastSendToPlcValue ? firstClickOrDownValue : secondClickOrUpValue;
                    S7Helper.SetWriteValueByVariable(temp.Variable, temp.DataTypeEnum, currentSendToValue);
                    temp.LastSendToPlcValue = currentSendToValue;
                }
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                MessageBox.Show(ex.Message.ToString() + "【请确认PLC是否已连接！若已连接可尝试断开后重新连接】");
            }
        }
    }
}
