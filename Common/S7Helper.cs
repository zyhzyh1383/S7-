using Model;
using S7.Net;
using System;

namespace Common
{
    public class S7Helper
    {
        public static Plc mPlc;

        public static CpuType GetCpuType(string cpuType)
        {
            return (CpuType)Enum.Parse(typeof(CpuType), cpuType, true);
        }

        public static Plc GetPlc(CpuType cpuType, string ipaddress, int port, short machineTypeNo, short slotNo)
        {
            mPlc = new Plc(cpuType, ipaddress, port, machineTypeNo, slotNo);
            return mPlc;
        }

        public static bool CheckInit()
        {
            return mPlc != null && mPlc.IsConnected;
        }

        public static string ReadValueByVariable(string variable, DataTypeEnum dataTypeEnum)
        {
            if (!CheckInit())
            {
                throw new Exception("请先连接plc");
            }
            string outValue = string.Empty;
            try
            {
                var value = mPlc.Read(variable);
                switch (dataTypeEnum)
                {
                    case DataTypeEnum.Bit:
                        outValue = ((bool)value).ToString();
                        break;
                    case DataTypeEnum.Byte:
                        outValue = ((byte)value).ToString();
                        break;
                    case DataTypeEnum.Word:
                        outValue = ((ushort)value).ConvertToShort().ToString();
                        break;
                    case DataTypeEnum.DWord:
                        outValue = ((uint)value).ConvertToInt().ToString();
                        break;
                    case DataTypeEnum.Int:
                        outValue = ((ushort)value).ConvertToShort().ToString();
                        break;
                    case DataTypeEnum.DInt:
                        outValue = ((uint)value).ConvertToInt().ToString();
                        break;
                    case DataTypeEnum.Real:
                        outValue = ((uint)value).ConvertToFloat().ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outValue;
        }

        public static string ReadValueByDb(DataTypeEnum dataTypeEnum, int db, int startByteAdr, VarType varType, int varCount, byte bitAdr = 0)
        {
            if (!CheckInit())
            {
                throw new Exception("请先连接plc");
            }
            string outValue = string.Empty;
            try
            {
                switch (dataTypeEnum)
                {
                    case DataTypeEnum.Bit:
                        {
                            var valueBit = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.Bit, varCount, bitAdr);
                            outValue = ((bool)valueBit).ToString();
                            break;
                        }
                    case DataTypeEnum.Byte:
                        {
                            var valueByte = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.Byte, varCount, bitAdr);
                            outValue = Convert.ToSByte(valueByte).ToString();
                            break;
                        }
                    case DataTypeEnum.Word:
                        {
                            var valueWord = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.Word, varCount, bitAdr);
                            outValue = ((ushort)valueWord).ConvertToShort().ToString();
                            break;
                        }
                    case DataTypeEnum.DWord:
                        {
                            var valueDWord = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.DWord, varCount, bitAdr);
                            outValue = ((uint)valueDWord).ConvertToInt().ToString();
                            break;
                        }
                    case DataTypeEnum.Int:
                        {
                            var valueInt = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.Int, varCount, bitAdr);
                            outValue = ((ushort)valueInt).ConvertToShort().ToString();
                            break;
                        }
                    case DataTypeEnum.DInt:
                        {
                            var valueDInt = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.DInt, varCount, bitAdr);
                            outValue = ((uint)valueDInt).ConvertToInt().ToString();
                            break;
                        }
                    case DataTypeEnum.Real:
                        {
                            var valueReal = mPlc.Read(DataType.DataBlock, db, startByteAdr, VarType.Real, varCount, bitAdr);
                            outValue = ((uint)valueReal).ConvertToFloat().ToString();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outValue;
        }

        public static bool SetWriteValueByVariable(string variable, DataTypeEnum dataTypeEnum, object value)
        {
            if (!CheckInit())
            {
                throw new Exception("请先连接plc");
            }
            bool result;
            try
            {
                switch (dataTypeEnum)
                {
                    case DataTypeEnum.Bit:
                        mPlc.Write(variable, Convert.ToBoolean(value));
                        break;
                    case DataTypeEnum.Byte:
                        mPlc.Write(variable, Convert.ToByte(value));
                        break;
                    case DataTypeEnum.Word:
                        mPlc.Write(variable, Convert.ToUInt16(value));
                        break;
                    case DataTypeEnum.DWord:
                        mPlc.Write(variable, Convert.ToUInt32(value));
                        break;
                    case DataTypeEnum.Int:
                        mPlc.Write(variable, Convert.ToUInt16(value));
                        break;
                    case DataTypeEnum.DInt:
                        mPlc.Write(variable, Convert.ToUInt32(value));
                        break;
                    case DataTypeEnum.Real:
                        mPlc.Write(variable, Convert.ToSingle(value));
                        break;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                result = false;
            }
            return result;
        }

        public static bool SetWriteValueByDb(DataTypeEnum dataTypeEnum, object value, int db, int startByteAdr, int bitAdr = -1)
        {
            if (!CheckInit())
            {
                throw new Exception("请先连接plc");
            }
            bool result;
            try
            {
                switch (dataTypeEnum)
                {
                    case DataTypeEnum.Bit:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToBoolean(value), bitAdr);
                        break;
                    case DataTypeEnum.Byte:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToByte(value), bitAdr);
                        break;
                    case DataTypeEnum.Word:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToUInt16(value), bitAdr);
                        break;
                    case DataTypeEnum.DWord:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToUInt32(value), bitAdr);
                        break;
                    case DataTypeEnum.Int:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToUInt16(value), bitAdr);
                        break;
                    case DataTypeEnum.DInt:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToUInt32(value), bitAdr);
                        break;
                    case DataTypeEnum.Real:
                        mPlc.Write(DataType.DataBlock, db, startByteAdr, Convert.ToSingle(value), bitAdr);
                        break;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogFileHelper.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
